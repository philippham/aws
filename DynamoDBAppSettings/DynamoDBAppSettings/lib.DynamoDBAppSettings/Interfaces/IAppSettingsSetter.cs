using System.Collections.Generic;

namespace lib.DynamoDBAppSettings.Interfaces
{
    internal interface IAppSettingsSetter
    {
        void SetConfiguration(List<AppSettingConfigItem> items);
    }
}
