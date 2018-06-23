using ApiFacade.Parser;

namespace ApiFacade.Writer.Method
{
    public class FacadeStaticMethodWriter : FacadeMethodWriter
    {
        protected override string Declaration => $"static {base.Method.ReturnType} {base.Method.Name}({base.ParametersDeclaration})";
        protected override string Body => $"{FacadeClass.ParentNamespaceName}.{Class.Name}.{Method.Name}({base.Parameters});";

        public FacadeStaticMethodWriter(FacadeClass Class, FacadeMethod Method) : base(Class, Method)
        {
        }
    }
}
