using ApiFacade.Writer;

namespace ApiFacade.Parser
{
    public class FacadeMethod
    {
        public MethodWriterType Type { get; set; }
        public string Name { get; set; }
        public string ReturnType { get; set; }
        public MethodModifierType Modifier { get; set; }
        public FacadeParameter[] Parameters { get; set; } 
    }
}
