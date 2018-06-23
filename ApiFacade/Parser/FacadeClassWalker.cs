using System;
using System.Collections.Generic;
using System.Linq;
using ApiFacade.Writer;
using ApiFacade.Writer.Method;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ApiFacade.Parser
{
    public class FacadeClassWalker : CSharpSyntaxWalker
    {
        public int ClassCount => ClassNames.Count;
        public List<string> Usings { get; }
        public List<string> ClassNames { get; }
        public List<string> DeclaredNamespaces { get; }
        public List<string[]> ClassModifers { get; }
        public List<FacadeMethod> Methods { get; }
        public List<FacadeMethod> Constructors { get; }
        public bool IsInterface { get; private set; }

        public FacadeClassWalker()
        {
            ClassNames = new List<string>();
            ClassModifers = new List<string[]>();
            DeclaredNamespaces = new List<string>();
            Usings = new List<string>();
            Methods = new List<FacadeMethod>();
            Constructors = new List<FacadeMethod>();
        }

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            base.VisitInterfaceDeclaration(node);
            IsInterface = true;
        }

        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax Node)
        {
            base.VisitConstructorDeclaration(Node);
            Constructors.Add(
                this.ParseMethod(Node.Identifier, null, Node.Modifiers, Node.ParameterList)
            );
        }

        public override void VisitUsingDirective(UsingDirectiveSyntax Node)
        {
            base.VisitUsingDirective(Node);
            Usings.Add(Node.Name.ToString());
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax Node)
        {
            base.VisitClassDeclaration(Node);
            ClassNames.Add(Node.Identifier.ToString());
            ClassModifers.Add(Node.Modifiers.Select(M => M.ToString()).ToArray());
        }

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax Node)
        {
            base.VisitNamespaceDeclaration(Node);
            DeclaredNamespaces.Add(Node.Name.ToString());
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax Node)
        {
            base.VisitMethodDeclaration(Node);
            if (Node.Modifiers.Any(M => M.ToString() == "private")) return;
            Methods.Add(
                this.ParseMethod(Node.Identifier, Node.ReturnType, Node.Modifiers, Node.ParameterList)
            );
        }

        public FacadeMethod ParseMethod(SyntaxToken Identifier, TypeSyntax ReturnType, SyntaxTokenList Modifiers, ParameterListSyntax Parameters)
        {
            var parameters = new List<FacadeParameter>();
            for (var i = 0; i < Parameters.Parameters.Count; i++)
            {
                parameters.Add(new FacadeParameter
                {
                    Type = Parameters.Parameters[i].Type.ToString(),
                    Name = Parameters.Parameters[i].Identifier.ToString(),
                    Modifier = ParseParameterModifier(Parameters.Parameters[i].Modifiers)
                });
            }
            return new FacadeMethod
            {
                Parameters = parameters.ToArray(),
                Name = Identifier.ToString(),
                ReturnType = ReturnType?.ToString() ?? string.Empty,
                Type = ParseType(Modifiers),
                Modifier = ParseModifier(Modifiers)
            };
        }

        private static ParameterModifier ParseParameterModifier(SyntaxTokenList Modifiers)
        {
            return Modifiers.Any(M => M.ToString().ToLowerInvariant() == "out") ? ParameterModifier.Out
                : Modifiers.Any(M => M.ToString().ToLowerInvariant() == "ref") ? ParameterModifier.Ref : ParameterModifier.None;
        }

        private static MethodModifierType ParseModifier(SyntaxTokenList Modifiers)
        {
            return Modifiers.Any(M => M.ToString().ToLowerInvariant() == "public") ? MethodModifierType.Public
                : Modifiers.Any(M => M.ToString().ToLowerInvariant() == "protected") ? MethodModifierType.Protected : MethodModifierType.Public;
        }

        private static MethodWriterType ParseType(SyntaxTokenList Modifiers)
        {
            return Modifiers.Any(M => M.ToString().ToLowerInvariant() == "abstract") ? MethodWriterType.Abstract
            : Modifiers.Any(M => M.ToString().ToLowerInvariant() == "virtual") ? MethodWriterType.Virtual
            : Modifiers.Any(M => M.ToString().ToLowerInvariant() == "static") ? MethodWriterType.Static : MethodWriterType.Normal;
        }
    }
}
