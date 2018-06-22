using System;
using System.Text;

namespace ApiFacade 
{
    public class FacadeStringBuilder
    {
        private readonly StringBuilder _builder;
        private readonly StringBuilder _indentationBuilder;

        public void Append(string Text)
        {
            _builder.Append($"{this.CreateIndentationString()}{Text}");
        }

        public void AppendLine(string Text)
        {
            this.Append($"{Text}{Environment.NewLine}");
        }

        private string CreateIndentationString()
        {
            _indentationBuilder.Clear();
            for (var i = 0; i < IndentationLevel * 4; i++)
            {
                _indentationBuilder.Append(" ");
            }
            
            return _indentationBuilder.ToString();
        }
        
        public int IndentationLevel { get; set; }
    }
}