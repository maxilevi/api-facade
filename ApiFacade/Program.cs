using System;
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
            FacadeWriter writer = null;
            switch (facadeClass.Type)
            {
                case FacadeType.Normal:
                    writer = new FacadeNormalClassWriter(facadeClass);
                    break;
                case FacadeType.Static:
                    writer = new FacadeStaticClassWriter(facadeClass);
                    break;
                case FacadeType.Sealed:
                    throw new ArgumentOutOfRangeException($"api-façade doesn't support sealed classes.");
                default:
                    throw new ArgumentOutOfRangeException();
            }
            File.WriteAllText("API.World.cs", writer.Build());
        }
    }
}
