using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuOC.UnitTest
{
    internal class FausseConsole : IConsole
    {
        public StringBuilder StringBuilder = new StringBuilder();

        public void Write(string text)
        {
            StringBuilder.Append(text);
        }
        public void WriteLine(string text)
        {
            StringBuilder.AppendLine(text);
        }
    }
}
