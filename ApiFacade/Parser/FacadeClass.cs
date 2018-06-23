﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ApiFacade.Parser
{
    public class FacadeClass
    {
        public static string Namespace { get; set; }
        public string Name { get; private set; }
        public string[] Usings { get; private set; }
        public string ClassModifier { get; private set; }
        public FacadeClass Parent { get; private set; }
        public FacadeMethod[] Methods { get; private set; }
        public FacadeProperty[] Properties { get; private set; }
        public FacadeType Type { get; private set; }

        public static FacadeClass Build(string SourceCode)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(SourceCode);
            var root = (CompilationUnitSyntax)syntaxTree.GetRoot();
            var walker = new FacadeClassWalker();
            walker.Visit(root);
            return new FacadeClass
            {
                Type = ParseFacadeType(walker.ClassModifers[0]),
                Name = walker.ClassNames[0],
                Usings = FacadeClass.Combine<string>(walker.Usings.ToArray(), new []{walker.DeclaredNamespaces[0]}),
                ClassModifier = walker.ClassModifers[0].Aggregate((S0,S1) => $"{S0} {S1} ").TrimEnd(' '),
                Methods = walker.Methods.ToArray()
            };
        }

        private static FacadeType ParseFacadeType(string[] ClassModifiers)
        {
            return ClassModifiers.Any(S => S.ToString() == "static") ? FacadeType.Static 
                : ClassModifiers.Any(S => S.ToString() == "sealed") 
                ? FacadeType.Sealed : FacadeType.Normal;
        }

        private FacadeClass() { }

        private static T[] Combine<T>(params IEnumerable<T>[] Items) => Items.SelectMany(I => I).Distinct().ToArray();
    }

    public enum FacadeType
    {
        Normal,
        Static,
        Sealed,
    }
}