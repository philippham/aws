using Amazon;
using Amazon.Runtime;

namespace lib.DynamoDBAppSettings
{
    public class DynamoConnectionSettings
    {
        public string Domain { get; set; }
        public string ConfigurationTableName { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string RegionName { get; set; }
        public RegionEndpoint Region => RegionEndpoint.GetBySystemName(RegionName);

        public AWSCredentials Credentials => new BasicAWSCredentials(AccessKey, SecretKey);
    }
}
