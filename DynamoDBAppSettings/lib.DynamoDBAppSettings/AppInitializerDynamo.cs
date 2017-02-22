using lib.DynamoDBAppSettings.Interfaces;

namespace lib.DynamoDBAppSettings
{
    public class AppInitializerDynamo : IAppInitializer
    {
        private readonly DynamoConnectionSettings _settings;

        public AppInitializerDynamo(DynamoConnectionSettings settings)
        {
            _settings = settings;
        }

        public void Initialize()
        {
            var adaptor = new DynamoDbAppSettingsRetrieverAdapter(_settings.Credentials, _settings.Region, _settings.ConfigurationTableName);

            var settingsConverter = new AppSettingDomainConverter(_settings.Domain);
            var dynamoConverter = new DynamoDbDictionaryToAppSettingConverter(settingsConverter);
            var retriever = new DynamoDbAppSettingsRetriever(adaptor, dynamoConverter);
            var setter = new AppSettingsSetter();
            var configurator = new AppSettingsConfigurator(setter, retriever);

            configurator.ConfigureSettings(_settings.Domain);
        }
    }
}
