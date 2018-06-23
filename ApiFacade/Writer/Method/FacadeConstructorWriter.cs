using ApiFacade.Parser;

namespace ApiFacade.Writer.Method
{
    public class FacadeConstructorWriter : FacadeMethodWriter
    {
        public FacadeConstructorWriter(FacadeClass Class, FacadeMethod Method) : base(Class, Method)
        {
        }

        // FIXME: Sealed classes should do something different
        protected override string Declaration => $"{base.Method.Name}({base.ParametersDeclaration}) : base({base.Parameters})";
        protected override string Body => string.Empty;
    }
}
