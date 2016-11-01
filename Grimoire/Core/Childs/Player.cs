using Grimoire.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Core.Childs
{
    public class Player : Actor
    {
        public Player()
        {
            FieldOfView = 15;
            Name = "Player";
            Color = Colors.Player;
            Symbol = '@';
            X = 10;
            Y = 10;
        }
    }
}
