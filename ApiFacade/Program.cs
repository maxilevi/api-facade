using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ApiFacade.Parser;
using ApiFacade.Writer;

namespace ApiFacade
{
    public static class Program
    {
        private static readonly Dictionary<FacadeType, Type> ClassWriters = new Dictionary<FacadeType, Type>
        {
            {FacadeType.Normal, typeof(FacadeNormalClassWriter)},
            {FacadeType.Abstract, typeof(FacadeAbstractClassWriter)},
            {FacadeType.Static, typeof(FacadeStaticClassWriter)},
            {FacadeType.Sealed, typeof(FacadeStaticClassWriter)}
        };

        public static void Main(string[] Args)
        {
            var apiPath = Path.GetFullPath("api.json");
            var apiFolder = Path.GetDirectoryName(Path.GetFullPath("api.json"));
            var configuration = Configuration.Load(apiPath);
            if(configuration == null) throw new ArgumentException("Configuration file is invalid.");

            FacadeClass.Namespace = configuration.TargetNamespace;
            for (var i = 0; i < configuration.Folders.Length; i++)
            {
                var folderPath = configuration.Folders[i].Path;
                var excludeList = configuration.Folders[i].ExcludeClasses;
                var includeList = configuration.Folders[i].IncludeClasses;
                var files = Directory.GetFiles(apiFolder + folderPath);
                for (var k = 0; k < files.Length; k++)
                {
                    if (!Path.GetExtension(files[k])?.EndsWith(".cs") ?? true) continue;
                    if (excludeList != null && excludeList.Any(C => C.Name == Path.GetFileName(files[k]))) continue;

                    var any = includeList?.FirstOrDefault(C => C.Name == Path.GetFileName(files[k]));
                    if (includeList != null && any == null) continue;

                    var facadeClass = any == null 
                        ? FacadeClass.Build(File.ReadAllText(files[k])) 
                        : FacadeClass.Build(File.ReadAllText(files[k]), any);

                    var writer = (FacadeClassWriter) Activator.CreateInstance(ClassWriters[facadeClass.Type], facadeClass);
                    File.WriteAllText($"{apiFolder}/{FacadeClass.Namespace}.{Path.GetFileName(files[k])}", writer.Build());
                }
            }
        }
    }
}
