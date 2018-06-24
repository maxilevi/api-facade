using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace ApiFacade
{
    public class Configuration
    {
        public string ProjectFile { get; set; }
        public string TargetNamespace { get; set; }
        public ConfigurationNamespace[] Folders { get; set; }

        public static Configuration Load(string Path)
        {
            try
            {
                return JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(Path));
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }

    public class ConfigurationClass
    {
        public string Name { get; set; }
        public string[] IncludedMethods { get; set; }
        public string[] ExcludedMethods { get; set; }

        public static ConfigurationClass Default { get; set; } = new ConfigurationClass();
    }

    public class ConfigurationNamespace
    {
        public string Path { get; set; }
        public ConfigurationClass[] IncludeClasses { get; set; }
        public ConfigurationClass[] ExcludeClasses { get; set; }
    }
}
