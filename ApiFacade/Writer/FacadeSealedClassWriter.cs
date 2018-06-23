using ApiFacade.Parser;

namespace ApiFacade.Writer
{
    public class FacadeSealedClassWriter : FacadeClassWriter
    {
        public FacadeSealedClassWriter(FacadeClass Class) : base(Class)
        {
        }

        protected override string ClassDeclaration => $"public sealed class {Class.Name}";
    }
}
