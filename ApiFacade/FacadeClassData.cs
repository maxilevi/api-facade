namespace ApiFacade
{
    public class FacadeClassData
    {
        public string Namespace { get; set; }
        
        
        public static FacadeClassData Parse()
        {
            SyntaxTree tree = SyntaxTree.ParseCompilationUnit(
                @" C# code here ");
            var root = (CompilationUnitSyntax)tree.Root;
        }
        
        private FacadeClassData() {}
    }
}