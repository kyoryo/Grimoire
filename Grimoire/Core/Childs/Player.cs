using Grimoire.UI;
using RLNET;
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
            Attack = 2;
            AttackChance = 50;
            Defence = 15;
            DefenceChance = 20;
            Health = 100;
            HealthMax = 100;
            Money = 0;
            Speed = 10;
            FieldOfView = 15;
            Name = "Player";
            Color = Colors.Player;
            Symbol = '@';
        }
        public void DrawStats(RLConsole statConsole)
        {
            statConsole.Print(1, 1, $"Name:    {Name}", Colors.TextHeading);
            statConsole.Print(1, 3, $"Health:  {Health}/{HealthMax}", Colors.TextHeading);
            statConsole.Print(1, 5, $"Attack:  {Attack} ({AttackChance}%)", Colors.TextHeading);
            statConsole.Print(1, 7, $"Defense: {Defence} ({DefenceChance}%)", Colors.TextHeading);
            statConsole.Print(1, 9, $"Gold:    {Money}", Colors.TextHeading);
        }
    }
}
