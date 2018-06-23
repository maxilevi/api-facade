using ApiFacade.Parser;

namespace ApiFacade.Writer
{
    public class FacadeStaticClassWriter : FacadeClassWriter
    {
        public FacadeStaticClassWriter(FacadeClass Class) : base(Class)
        {
        }

        protected override string ClassDeclaration => $"public static class {Class.Name}";
    }
}
