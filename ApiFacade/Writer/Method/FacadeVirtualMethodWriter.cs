using ApiFacade.Parser;

namespace ApiFacade.Writer.Method
{
    public class FacadeVirtualMethodWriter : FacadeMethodWriter
    {
        protected override string Declaration => $"new virtual {base.Method.ReturnType} {base.Method.Name}({base.ParametersDeclaration})";
        protected override string Body => $"{base.Parent}.{Method.Name}({base.Parameters});";

        public FacadeVirtualMethodWriter(FacadeClass Class, FacadeMethod Method) : base(Class, Method)
        {
        }
    }
}
