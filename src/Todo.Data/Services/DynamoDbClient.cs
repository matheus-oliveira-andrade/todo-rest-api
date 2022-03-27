using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;

namespace Todo.Data.Services
{
    public class DynamoDbClient
    {
        public readonly IAmazonDynamoDB DataBase;
        private readonly RegionEndpoint _defaultRegionEndpoint = RegionEndpoint.USEast1;
        
        public DynamoDbClient(string profileName = "default", RegionEndpoint regionEndpoint = null)
        {
            regionEndpoint ??= _defaultRegionEndpoint;

            DataBase = new AmazonDynamoDBClient(GetLocalStoredCredentials(profileName), regionEndpoint);
        }

        private static AWSCredentials GetLocalStoredCredentials(string profileName)
        {
            var chain = new CredentialProfileStoreChain();
            var success = chain.TryGetAWSCredentials(profileName, out var awsCredentials);

            return awsCredentials;
        }
    }
}
