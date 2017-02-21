using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using lib.DynamoDBAppSettings.Interfaces;

namespace lib.DynamoDBAppSettings
{
    internal class DynamoDbAppSettingsRetrieverAdapter : IDynamoDbAppSettingsRetrieverAdapter
    {
        private const string GlobalSettingsDomainName = "Global";
        private const string DomainColumn = "Domain";
        private const string ComparisonOp = "EQ";
        private readonly AWSCredentials _awsCredentials;
        private readonly Amazon.RegionEndpoint _regionEndpoint;
        private readonly string _tableName;

        public DynamoDbAppSettingsRetrieverAdapter(AWSCredentials awsCredentials, Amazon.RegionEndpoint regionEndpoint, string tableName)
        {
            _awsCredentials = awsCredentials;
            _regionEndpoint = regionEndpoint;
            _tableName = tableName;
        }


        public List<Dictionary<string, AttributeValue>> Retrieve(string domain)
        {
            if (string.IsNullOrWhiteSpace(domain))
            {
                throw new ArgumentException("Domain cannot be null or empty");
            }

            using (var client = new AmazonDynamoDBClient(_awsCredentials, new AmazonDynamoDBConfig { RegionEndpoint = _regionEndpoint }))
            {
                var appSettings = new List<Dictionary<string, AttributeValue>>();

                var queryRequest = CreateQueryRequest(DomainColumn, GlobalSettingsDomainName);
                var globalSettings = client.Query(queryRequest);
                appSettings.AddRange(globalSettings.Items);

                queryRequest = CreateQueryRequest(DomainColumn, domain);
                var appSpecificSettings = client.Query(queryRequest);
                appSettings.AddRange(appSpecificSettings.Items);

                return appSettings;
            }
        }

        private QueryRequest CreateQueryRequest(string columnName, string columnValue)
        {
            var keyCondition = new Condition()
            {
                ComparisonOperator = ComparisonOp,
                AttributeValueList = new List<AttributeValue>() { new AttributeValue() { S = columnValue } }
            };

            var queryRequest = new QueryRequest(_tableName);
            queryRequest.KeyConditions.Add(columnName, keyCondition);

            return queryRequest;
        }
    }
}
