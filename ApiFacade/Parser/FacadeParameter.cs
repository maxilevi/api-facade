using System.Reflection;

namespace ApiFacade.Parser
{
    public class FacadeParameter
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public ParameterModifier Modifier { get; set; }

        public string ModifierString => ParameterModifier.None != Modifier ? Modifier.ToString().ToLowerInvariant() + " " : string.Empty;
    }

    public enum ParameterModifier
    {
        Out,
        Ref,
        None
    }
}
