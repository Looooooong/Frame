
using System.Text;

namespace EditorTool
{
    public class ScriptBuilder
    {
        private const string NEW_LINE = "\r\n";
        public ScriptBuilder()
        {
            builder = new StringBuilder();
        }

        private StringBuilder builder;
        public int Indent { get; set; }

        private int currentCharIndex;

        public void Write(string val, bool noAutoIndent = false)
        {
            if (!noAutoIndent)
                val = GetIndents() + val;
            if (currentCharIndex == builder.Length)
                builder.Append(val);
            else
                builder.Insert(currentCharIndex, val);
            currentCharIndex += val.Length;
        }

        public void WriteLine(string val, bool noAutoIndent = false)
        {
            Write(val + NEW_LINE);
        }

        public void WriteCurlyBrackets(bool inside = true)
        {
            var openBracket = GetIndents() + "{" + NEW_LINE;
            var closeBracket = GetIndents() + "}" + NEW_LINE;
            Write(openBracket + closeBracket, true);
            if(inside)
                currentCharIndex -= closeBracket.Length;
        }

        public string GetIndents()
        {
            var str = "";
            for (var i = 0; i < Indent; i++)
                str += "    ";
            return str;
        }

        public override string ToString()
        {
            return builder.ToString();
        }
    }
}
