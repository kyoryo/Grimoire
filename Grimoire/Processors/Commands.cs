using Grimoire.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Processors
{
    public class Commands
    {
        //accessed every button push
        public bool MovePlayer(Directions dir)
        {
            int x = Program.Player.X;
            int y = Program.Player.Y;

            switch (dir)
            {
                case Directions.Up:
                    y = Program.Player.Y - 1;
                    break;
                case Directions.Down:
                    y = Program.Player.Y + 1;
                    break;
                case Directions.Left:
                    x = Program.Player.X - 1;
                    break;
                case Directions.Right:
                    x = Program.Player.X + 1;
                    break;
                default: return false;
            }

            if (Program.DungeonMap.SetActorPosition(Program.Player, x, y))
            {
                return true;
            }
            return false;
        }
    }
}
