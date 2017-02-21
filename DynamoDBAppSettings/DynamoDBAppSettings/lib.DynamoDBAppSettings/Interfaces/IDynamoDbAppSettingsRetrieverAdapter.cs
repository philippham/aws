using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;

namespace lib.DynamoDBAppSettings.Interfaces
{
    internal interface IDynamoDbAppSettingsRetrieverAdapter
    {
        List<Dictionary<string, AttributeValue>> Retrieve(string domain);
    }
}
