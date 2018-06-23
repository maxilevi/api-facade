using ApiFacade.Parser;

namespace ApiFacade.Writer
{
    public class FacadeNormalClassWriter : FacadeClassWriter
    {
        public FacadeNormalClassWriter(FacadeClass Class) : base(Class)
        {
        }

        protected override string ClassDeclaration => $"public class {Class.Name} : {FacadeClass.ParentNamespaceName}.{Class.Name}";
    }
}
