using System;
using System.IO;
using ApiFacade.Builder;

namespace ApiFacade
{
    public static class Program
    {
        public static void Main(string[] Args)
        {
            FacadeClass.Namespace = "Hedra.Engine.API";
            var facadeClass = FacadeClass.Build(File.ReadAllText("World.cs"));
            FacadeBuilder builder = null;
            switch (facadeClass.Type)
            {
                case FacadeType.Normal:
                    builder = new FacadeNormalClassBuilder(facadeClass);
                    break;
                case FacadeType.Static:
                    builder = new FacadeStaticClassBuilder(facadeClass);
                    break;
                case FacadeType.Sealed:
                    throw new ArgumentOutOfRangeException($"api-façade doesnt support sealed classes.");
                default:
                    throw new ArgumentOutOfRangeException();
            }
            File.WriteAllText("API.World.cs", builder.Build());
        }
    }
}
