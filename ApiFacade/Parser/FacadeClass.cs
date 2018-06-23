using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ApiFacade.Parser
{
    public class FacadeClass
    {
        public static string Namespace { get; set; }
        public static string ParentNamespaceName => "Namespace";
        public string Name { get; private set; }
        public string[] Usings { get; private set; }
        public string ClassModifier { get; private set; }
        public FacadeMethod[] Methods { get; private set; }
        public FacadeMethod[] Constructors { get; private set; }
        public FacadeProperty[] Properties { get; private set; }
        public FacadeType Type { get; private set; }
        public string ParentNamespace { get; private set; }

        public static FacadeClass Build(string SourceCode)
        {
            return FacadeClass.Build(SourceCode, ConfigurationClass.Default);
        }

        public static FacadeClass Build(string SourceCode, ConfigurationClass Class)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(SourceCode);
            var root = (CompilationUnitSyntax)syntaxTree.GetRoot();
            var walker = new FacadeClassWalker();
            walker.Visit(root);
            return new FacadeClass
            {
                Type = ParseFacadeType(walker.ClassModifers[0]),
                Name = walker.ClassNames[0],
                Usings = walker.Usings.OrderBy(S => S.Length).ThenBy(S => S).ToArray(),
                ParentNamespace = walker.DeclaredNamespaces[0],
                ClassModifier = walker.ClassModifers[0].Aggregate((S0,S1) => $"{S0} {S1} ").TrimEnd(' '),
                Methods = walker.Methods
                    .Where(M => Class.ExcludedMethods == null || Class.ExcludedMethods != null && !Class.ExcludedMethods.Contains(M.Name))
                    .Where(M => Class.IncludedMethods == null || Class.IncludedMethods != null && Class.IncludedMethods.Contains(M.Name))
                    .OrderBy(S => S.Name).ToArray(),
                Constructors = walker.Constructors.ToArray()
            };
        }

        private static FacadeType ParseFacadeType(string[] ClassModifiers)
        {
            return ClassModifiers.Any(S => S.ToString() == "static") ? FacadeType.Static 
                : ClassModifiers.Any(S => S.ToString() == "sealed") 
                ? FacadeType.Sealed : FacadeType.Normal;
        }

        private FacadeClass() { }
    }

    public enum FacadeType
    {
        Normal,
        Static,
        Sealed,
        Abstract,
    }
}