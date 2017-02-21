using System;
using lib.DynamoDBAppSettings.Interfaces;

namespace lib.DynamoDBAppSettings
{
    internal class AppSettingDomainConverter : IAppSettingDomainConverter
    {
        private const string GlobalDomain = "Global";

        private readonly string _appDomain;

        public AppSettingDomainConverter(string appDomain)
        {
            _appDomain = appDomain;
        }


        public DomainName Convert(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("The domain name is null or empty");
            }

            if (input == GlobalDomain)
            {
                return DomainName.Global;
            }

            if (input == _appDomain)
            {
                return DomainName.ApplicationSpecific;
            }

            throw new ArgumentException($"The domain name {input} is invalid. Expected domain names are: {GlobalDomain} or {_appDomain}");
        }
    }
}
