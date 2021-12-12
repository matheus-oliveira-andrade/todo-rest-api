using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;

namespace Todo.API.Data.Services
{
    public class DynamoDBClient
    {
        public readonly IAmazonDynamoDB DataBase;
        private readonly RegionEndpoint _defaultRegionEndpoint = RegionEndpoint.USEast1;


        public DynamoDBClient(string profileName = "default", RegionEndpoint regionEndpoint = null)
        {
            if (regionEndpoint == null)
            {
                regionEndpoint = _defaultRegionEndpoint;
            }

            DataBase = new AmazonDynamoDBClient(GetLocalStoredCredentials(profileName), regionEndpoint);
        }

        protected AWSCredentials GetLocalStoredCredentials(string profileName)
        {
            var chain = new CredentialProfileStoreChain();
            AWSCredentials awsCredentials = default;

            bool success = chain.TryGetAWSCredentials(profileName, out awsCredentials);

            return awsCredentials;
        }
    }
}
