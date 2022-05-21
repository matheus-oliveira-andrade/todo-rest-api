using Amazon.DynamoDBv2;

namespace Todo.Data.Services
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
