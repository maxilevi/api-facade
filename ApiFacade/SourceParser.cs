using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;

namespace ApiFacade
{
    public class SourceParser
    {
        public SourceParser()
        {
            
        }

        public void Parse()
        {
            var provider = CodeDomProvider.CreateProvider();
            var result = provider.CompileAssemblyFromSource(CompilerParameters);
            var assembly = result.CompiledAssembly;
            var names = (from type in assembly.GetTypes()
                from method in type.GetMethods(
                    BindingFlags.Public | BindingFlags.NonPublic |
                    BindingFlags.Instance | BindingFlags.Static)
                select type.FullName + ":" + method.Name).Distinct().ToList();
        }
    }
}