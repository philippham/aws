using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;

namespace lib.DynamoDBAppSettings.Interfaces
{
    internal interface IDynamoDbDictionaryToAppSettingConverter
    {
        AppSettingConfigItem Convert(Dictionary<string, AttributeValue> input);
    }
}
