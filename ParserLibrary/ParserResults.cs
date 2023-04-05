using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrammarLibrary;

namespace ParserLibrary
{
    public class ParserResults
    {
        public ActionType? ActionType { get; set; }
        public Action<List<Token>?>? Action { get; set; }
        public List<Token> Tokens { get; set; } = new List<Token>();
        public List<string> Errors { get; set; } = new List<string>();
        public bool Success { get; set; }
    }
}
