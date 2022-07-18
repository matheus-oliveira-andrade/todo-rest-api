using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Newtonsoft.Json;
using Todo.Infrastructure.DynamoDb.Client;
using Todo.Infrastructure.Interfaces;

namespace Todo.Infrastructure.DynamoDb
{
    public class TodoRepository : ITodoRepository
    {
        private readonly IAmazonDynamoDB _dataBase;
        private const string TableName = "Todos";

        public TodoRepository()
        {
            var dynamoClient = new DynamoDbClient();

            _dataBase = dynamoClient.DataBase;
        }

        public async Task AddAsync(Domain.Entities.Todo todo)
        {
            var document = Document.FromJson(JsonConvert.SerializeObject(todo));

            var req = new PutItemRequest
            {
                TableName = TableName,
                Item = document.ToAttributeMap(),
            };

            await _dataBase.PutItemAsync(req);
        }

        public async Task UpdateAsync(Domain.Entities.Todo todo)
        {
            var attributesUpdates = new Dictionary<string, AttributeValueUpdate>
            {
                ["Title"] = new()
                {
                    Action = AttributeAction.PUT,
                    Value = new AttributeValue { S = todo.Title }
                },
                ["Description"] = new()
                {
                    Action = AttributeAction.PUT,
                    Value = new AttributeValue { S = todo.Description }
                },
                ["Status"] = new()
                {
                    Action = AttributeAction.PUT,
                    Value = new AttributeValue { N = ((int)todo.Status).ToString() }
                },
                ["Tags"] = new()
                {
                    Action = AttributeAction.PUT,
                    Value = new AttributeValue { SS = todo.Tags }
                }
            };

            var req = new UpdateItemRequest
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "Id", new AttributeValue { S = todo.Id.ToString() } }
                },
                AttributeUpdates = attributesUpdates,
            };

            await _dataBase.UpdateItemAsync(req);
        }

        public async Task<List<Domain.Entities.Todo>> GetAllAsync()
        {
            var req = new ScanRequest
            {
                TableName = TableName,
            };

            var resp = await _dataBase.ScanAsync(req);

            if (!resp.Items.Any())
                return null;

            return resp.Items
                .Select(Document.FromAttributeMap)
                .Select(x => x.ToJson())
                .Select(JsonConvert.DeserializeObject<Domain.Entities.Todo>)
                .ToList();
        }

        public async Task<Domain.Entities.Todo> GetByIdAsync(Guid id)
        {
            var resp = await _dataBase.GetItemAsync(new GetItemRequest
            {
                TableName = TableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "Id", new AttributeValue { S = id.ToString() } }
                },
            });

            if (!resp.IsItemSet)
                return null;

            var jsonTodo = Document.FromAttributeMap(resp.Item).ToJson();

            return JsonConvert.DeserializeObject<Domain.Entities.Todo>(jsonTodo);
        }
    }
}