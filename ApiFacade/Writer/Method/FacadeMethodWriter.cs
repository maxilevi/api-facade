﻿using System.Linq;
using ApiFacade.Parser;

namespace ApiFacade.Writer.Method
{
    public abstract class FacadeMethodWriter
    {
        protected FacadeClass Class { get; }
        protected FacadeMethod Method { get; }

        protected FacadeMethodWriter(FacadeClass Class, FacadeMethod Method)
        {
            this.Class = Class;
            this.Method = Method;
        }

        public void Write(FacadeStringWriter Writer)
        {
            Writer.Append($"{Method.Modifier.ToString().ToLowerInvariant()} {Declaration}");
            if (IsDeclared)
            {
                Writer.AppendLine(string.Empty);
                Writer.AppendLine("{");
                Writer.IndentationLevel++;
                if(!string.IsNullOrEmpty(this.Body)) Writer.AppendLine($"{(Method.ReturnType.ToLowerInvariant() != "void" ? "return " : string.Empty)}{Body}");
                Writer.IndentationLevel--;
                Writer.Append("}");
            }
            Writer.AppendLine(string.Empty);
        }

        protected string ParametersDeclaration =>
            Method.Parameters.Length > 0 ? Method.Parameters.Select(P => $"{P.Type} {P.ModifierString}{P.Name}").Aggregate((P0, P1) => $"{P0}, {P1}") : string.Empty;

        protected string Parameters =>
           Method.Parameters.Length > 0 ? Method.Parameters.Select(P => $"{P.ModifierString}{P.Name}").Aggregate((P0, P1) => $"{P0}, {P1}") : string.Empty;

        protected abstract string Declaration { get; }

        protected abstract string Body { get; }

        protected virtual bool IsDeclared => true;

        protected string Parent => Class.Type == FacadeType.Sealed ? "Parent" : "base";
    }
}
