using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ApiFacade.Builder
{
    public class FacadeClassWalker : CSharpSyntaxWalker
    {
        public int ClassCount { get; private set; }
        public List<string> ClassNames { get; private set; }
        public List<string> DeclaredNamespaces { get; private set; }
        public List<string[]> ClassModifers { get; private set; }

        public FacadeClassWalker()
        {
            ClassNames = new List<string>();
            ClassModifers = new List<string[]>();
            DeclaredNamespaces = new List<string>();
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax Node)
        {
            base.VisitClassDeclaration(Node);
            if(Node.Modifiers.Any(M => M.ToString().Contains("sealed")))
                throw new ArgumentException($"Building Facades around sealed classes is not supported.");
            ClassNames.Add(Node.Identifier.ToString());
            ClassModifers.Add(Node.Modifiers.Select(M => M.ToString()).ToArray());
        }

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax Node)
        {
            base.VisitNamespaceDeclaration(Node);
            DeclaredNamespaces.Add(Node.Name.ToString());
        }
    }
}
