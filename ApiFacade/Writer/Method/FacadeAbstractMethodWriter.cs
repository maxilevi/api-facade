using ApiFacade.Parser;

namespace ApiFacade.Writer.Method
{
    public class FacadeAbstractMethodWriter : FacadeMethodWriter
    {
        protected override string Declaration => $"abstract {base.Method.ReturnType} {base.Method.Name}({base.ParametersDeclaration});";
        protected override string Body => string.Empty;
        protected override bool IsDeclared => false;

        public FacadeAbstractMethodWriter(FacadeClass Class, FacadeMethod Method) : base(Class, Method)
        {
        }
    }
}
