using System.IO;

namespace ApiFacade
{
    public class Configuration
    {
        public string[] IncludedMethods { get; set; }
        public string[] ExcludedMethods { get; set; }

        private Configuration()
        {
            
        }
        
        public static Configuration Load(string Path)
        {
            return new Configuration
            {
                IncludedMethods = File.ReadAllLines(Path)
            };
        }
    }
}
