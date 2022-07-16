using Amazon.DynamoDBv2;

namespace Todo.Infrastructure.DynamoDb.Client
{
    public class DynamoDbClient
    {
        public readonly IAmazonDynamoDB DataBase;

        public DynamoDbClient()
        {
            DataBase = new AmazonDynamoDBClient();
        }
    }
}