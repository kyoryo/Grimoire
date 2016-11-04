using Grimoire.Core;
using Grimoire.UI;
using RogueSharp.DiceNotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Enemys
{
    public class Goblin:Enemy
    {
        public static Goblin Create(int level)
        {
            int health = Dice.Roll("2D5");
            return new Goblin
            {
                Attack = Dice.Roll("1D3") + level / 3,
                AttackChance = Dice.Roll("25D3"),
                Color = Colors.Goblin,
                Defence = Dice.Roll("1D3") + level / 3,
                DefenceChance = Dice.Roll("10D4"),
                Money = Dice.Roll("5D5"),
                Health = health,
                HealthMax = health,
                Name = "Goblin",
                Speed = 14,
                Symbol = 'g'
            };
        }
    }
}
