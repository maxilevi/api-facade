using System;
using System.Collections.Generic;
using System.IO;
using ApiFacade.Parser;
using ApiFacade.Writer;

namespace ApiFacade
{
    public static class Program
    {
        public static void Main(string[] Args)
        {
            FacadeClass.Namespace = "Hedra.Engine.API";
            var facadeClass = FacadeClass.Build(File.ReadAllText("World.cs"));
            var ClassWriters = new Dictionary<FacadeType, Type>
            {
                {FacadeType.Normal, typeof(FacadeNormalClassWriter)},
                {FacadeType.Abstract, typeof(FacadeAbstractClassWriter)},
                {FacadeType.Static, typeof(FacadeStaticClassWriter)},
                {FacadeType.Sealed, typeof(FacadeStaticClassWriter)}
            };
            var writer = (FacadeClassWriter) Activator.CreateInstance(ClassWriters[facadeClass.Type], facadeClass);
            File.WriteAllText("API.World.cs", writer.Build());
        }
    }
}
