namespace lib.DynamoDBAppSettings.Interfaces
{
    internal interface IAppSettingDomainConverter
    {
        DomainName Convert(string input);
    }
}
