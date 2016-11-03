using Grimoire.UI;
using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Processors
{
    public class MessageLog
    {
        private static readonly int maxLines = 8;
        private readonly Queue<string> _lines;
        public MessageLog()
        {
            _lines = new Queue<string>();
        }
        public void Add(string message)
        {
            _lines.Enqueue(message);
            if(_lines.Count > maxLines)
            {
                _lines.Dequeue(); //remove if more than max
            }
        }
        public void Draw(RLConsole console)
        {
            console.Clear();
            string[] lines = _lines.ToArray();
            for (var i = 0; i < lines.Length; i++)
            {
                console.Print(1, i + 1, lines[i], Colors.TextHeading);
            }
        }
    }
}
