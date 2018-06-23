using ApiFacade.Parser;

namespace ApiFacade.Writer.Method
{
    public class FacadeNormalMethodWriter : FacadeMethodWriter
    {
        protected override string Declaration => $"new {base.Method.ReturnType} {base.Method.Name}({base.ParametersDeclaration})";
        protected override string Body => $"{base.Parent}.{Method.Name}({base.Parameters});";

        public FacadeNormalMethodWriter(FacadeClass Class, FacadeMethod Method) : base(Class, Method)
        {
        }
    }
}
