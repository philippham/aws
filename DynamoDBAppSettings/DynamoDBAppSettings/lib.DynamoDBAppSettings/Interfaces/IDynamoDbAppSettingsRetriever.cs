using System.Collections.Generic;

namespace lib.DynamoDBAppSettings.Interfaces
{
    internal interface IDynamoDbAppSettingsRetriever
    {
        List<AppSettingConfigItem> Retrieve(string domain);
    }
}
