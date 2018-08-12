using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
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
            var targetFile = Args[0];
            var methodsPath = Args[1];
            var appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if(!File.Exists(targetFile)) 
                throw new ArgumentException($"Provided file {Path.GetFileName(targetFile)} does not exists.");
            if (!Path.GetExtension(targetFile)?.EndsWith(".cs") ?? true) 
                throw new ArgumentOutOfRangeException("");
            
            var facadeClass = FacadeClass.Build(File.ReadAllText(targetFile), Configuration.Load(methodsPath));
            if(facadeClass == null) throw new ArgumentOutOfRangeException("");

            var writer = (FacadeClassWriter) Activator.CreateInstance(ClassWriters[facadeClass.Type], facadeClass);
            var outPath = $"{appPath}/{Path.GetFileName(targetFile)}.fcd.cs";
            File.WriteAllText(outPath, writer.Build());
            Console.WriteLine($"Writing {facadeClass.Type} class '{outPath}'...");
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Correct usage: api-facade.exe <path-to-file> <path-to-config>");
        }
    }
}
