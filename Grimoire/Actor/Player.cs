using Grimoire.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Actor
{
    public class Player : Actor
    {
        public Player()
        {
            FOV = 15;
            Name = "Player";
            Color = Colors.Player;
            Symbol = '@';
            X = 10;
            Y = 10;
        }
    }
}
