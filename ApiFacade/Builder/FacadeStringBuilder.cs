using System;
using System.Text;

namespace ApiFacade.Builder
{
    public class FacadeStringBuilder
    {
        private readonly StringBuilder _builder;
        private readonly StringBuilder _indentationBuilder;

        public FacadeStringBuilder()
        {
            _builder = new StringBuilder();
            _indentationBuilder = new StringBuilder();
        }

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

        public override string ToString()
        {
            return _builder.ToString();
        }

        public int IndentationLevel { get; set; }
    }
}