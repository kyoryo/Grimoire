using Grimoire.Core;
using Grimoire.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grimoire.Core.Childs;
using RogueSharp;
using RogueSharp.DiceNotation;

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
            Enemy enemy = Program.DungeonMap.GetEnemyAt(x, y);
            if (enemy != null)
            {
                Attack(Program.Player, enemy);
                return true;
            } 
            return false;
        }

        public void Attack(Actor attacker, Actor defender)
        {
            StringBuilder attackMessage = new StringBuilder();
            StringBuilder defenceMessage = new StringBuilder();

            int hits = ResolveAttack(attacker, defender, attackMessage);
            int blocks = ResolveDefence(defender, hits, attackMessage, defenceMessage);

            Program.MessageLog.Add(attackMessage.ToString());
            if (!string.IsNullOrWhiteSpace(defenceMessage.ToString()))
            {
                Program.MessageLog.Add(defenceMessage.ToString());
            }
            int damage = hits - blocks;
            ResolveDamage(defender, damage);
        }

        

        private static int ResolveAttack(Actor attacker, Actor defender, StringBuilder attackMessage)
        {
            int hits = 0;
            attackMessage.AppendFormat("{0} attacks {1} and did: ", attacker.Name, defender.Name);

            DiceExpression attackDice = new DiceExpression().Dice(attacker.Attack, 100);
            DiceResult attackResult = attackDice.Roll();

            foreach(TermResult termResult in attackResult.Results)
            {
                attackMessage.Append(termResult.Value + ", ");
                if (termResult.Value >= 100 - attacker.AttackChance)
                {
                    hits++;
                }
            }
            return hits;
        }
        private static int ResolveDefence(Actor defender, int hits, StringBuilder attackMessage, StringBuilder defenceMessage)
        {
            int blocks = 0;
            if (hits > 0)
            {
                attackMessage.AppendFormat("did {0} hits", hits);
                defenceMessage.AppendFormat(" {0} defends and did: ", defender.Name);

                DiceExpression defenceDice = new DiceExpression().Dice(defender.Defence, 100);
                DiceResult defenceRoll = defenceDice.Roll();

                foreach (TermResult termResult in defenceRoll.Results)
                {
                    defenceMessage.Append(termResult.Value + ", ");
                    if (termResult.Value >= 100 - defender.DefenceChance)
                    {
                        blocks++;
                    }
                }
                defenceMessage.AppendFormat(" blocked {0}.", blocks);
            }
            else
            {
                attackMessage.Append("missed the attack.");
            }
            return blocks;
        }
        private static void ResolveDamage(Actor defender, int damage)
        {
            if (damage > 0)
            {
                defender.Health = defender.Health - damage;
                Program.MessageLog.Add($" {defender.Name} suffered {damage} damage");
                if (defender.Health <= 0)
                {
                    ResolveDeath(defender);
                }
            }
            else
            {
                Program.MessageLog.Add($" {defender.Name} blocked all damage");
            }
        }

        private static void ResolveDeath(Actor defender)
        {
            if (defender is Player)
            {
                Program.MessageLog.Add($" {defender.Name} was killed. GAME OVER.");
            }
            else if(defender is Enemy)
            {
                Program.DungeonMap.RemoveEnemy((Enemy)defender);
                Program.MessageLog.Add($" {defender.Name} died and dropped {defender.Money} silver");
            }

        }

        public void MoveEnemy(Enemy enemy, Cell cell)
        {
            if (!Program.DungeonMap.SetActorPosition(enemy, cell.X, cell.Y))
            {
                if (Program.Player.X == cell.X && Program.Player.Y == cell.Y)
                {
                    Attack(enemy, Program.Player);
                }
            }
        }
    }
}
