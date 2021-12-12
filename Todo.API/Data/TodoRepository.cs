using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.API.Data.Services;

namespace Todo.API.Data
{
    public class TodoRepository : ITodoRepository
    {
        private readonly IAmazonDynamoDB _dataBase;
        private readonly string _tableName = "Todos";
        private readonly string _profileName = "default";

        public TodoRepository()
        {
            var dynamoClient = new DynamoDBClient(_profileName);

            _dataBase = dynamoClient.DataBase;
        }

        public async Task Add(Domain.Todo todo)
        {
            var document = Document.FromJson(JsonConvert.SerializeObject(todo));

            var req = new PutItemRequest()
            {
                TableName = _tableName,
                Item = document.ToAttributeMap(),
            };

            await _dataBase.PutItemAsync(req);
        }

        public async Task Delete(Guid id)
        {
            var req = new DeleteItemRequest
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "Id", new AttributeValue { S = id.ToString() } }
                }
            };

            await _dataBase.DeleteItemAsync(req);
        }

        public async Task<List<Domain.Todo>> GetAll()
        {
            var req = new ScanRequest
            {
                TableName = _tableName,
            };

            var resp = await _dataBase.ScanAsync(req);

            if(!resp.Items.Any())
            {
                return null;
            }

            return resp.Items.Select(item =>
            {
                string jsonTodo = Document.FromAttributeMap(item).ToJson();

                return JsonConvert.DeserializeObject<Domain.Todo>(jsonTodo);
            }).ToList();                               
        }

        public async Task<Domain.Todo> GetById(Guid id)
        {
            var resp = await _dataBase.GetItemAsync(new GetItemRequest()
            {
                TableName = _tableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "Id", new AttributeValue { S = id.ToString() } }
                },
            });

            if (!resp.IsItemSet)
            {
                return null;
            }

            string jsonTodo = Document.FromAttributeMap(resp.Item).ToJson();

            return JsonConvert.DeserializeObject<Domain.Todo>(jsonTodo);
        }
    }
}
