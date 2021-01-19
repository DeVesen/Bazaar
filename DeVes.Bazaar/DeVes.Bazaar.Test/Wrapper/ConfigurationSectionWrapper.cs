using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace DeVes.Bazaar.Test.Wrapper
{
    public class ConfigurationSectionWrapper : IConfigurationSection
    {
        public string this[string key]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public string Key { get; }
        public string Path { get; }
        public string Value { get; set; }


        public ConfigurationSectionWrapper(string key, string path, string value)
        {
            Key = key;
            Path = path;
            Value = value;
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            return Enumerable.Empty<IConfigurationSection>();
        }

        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            throw new NotImplementedException();
        }
    }
}