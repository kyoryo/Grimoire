using Grimoire.UI;
using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Core
{
    public class Enemy : Actor
    {
        public void DrawStats(RLConsole statConsole, int pos)
        {
            int yPos = 13 + (pos * 2);
            statConsole.Print(1, yPos, Symbol.ToString(), Colors.TextHeading);

            int width = Convert.ToInt32(((double)Health / (double)HealthMax) * 16.0);
            int remWidth = 16 - width; //remaining width

            statConsole.SetBackColor(3, yPos, width, 1, Colors.Primary);
            statConsole.SetBackColor(3+width, yPos, remWidth, 1, Colors.Primary);

            statConsole.Print(2, yPos, $": {Name}", Colors.DbLight);
        }
    }
}
