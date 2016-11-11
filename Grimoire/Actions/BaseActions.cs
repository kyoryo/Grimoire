using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grimoire.Core;
using Grimoire.Core.Childs;
using Grimoire.Interfaces;
using Grimoire.Processors;
using RogueSharp;

namespace Grimoire.Actions
{
    public class BaseActions : IAmBehavable
    {
        public bool Act(Enemy enemy, Commands commands)
        {
            DungeonMap dungeonMap = Program.DungeonMap;
            Player player = Program.Player;
            FieldOfView enemyFieldOfView = new FieldOfView(dungeonMap);
            if (!enemy.TurnsAlerted.HasValue)
            {
                enemyFieldOfView.ComputeFov(enemy.X, enemy.Y, enemy.FieldOfView, true);
                if (enemyFieldOfView.IsInFov(player.X, player.Y))
                {
                    Program.MessageLog.Add($"{enemy.Name} is ready to fight {player.Name}");
                    enemy.TurnsAlerted = 1;
                }
            }
            if (enemy.TurnsAlerted.HasValue)
            {
                dungeonMap.SetIsWalkable(enemy.X, enemy.Y, true);
                dungeonMap.SetIsWalkable(player.X, player.Y, true);

                PathFinder pathFinder = new PathFinder(dungeonMap);
                Path path = null;
                try
                {
                    path = pathFinder.ShortestPath(dungeonMap.GetCell(enemy.X, enemy.Y),
                        dungeonMap.GetCell(player.X, player.Y));
                }
                catch (PathNotFoundException)
                {
                    Program.MessageLog.Add($"{enemy.Name} waiting for a turn");
                }
                dungeonMap.SetIsWalkable(enemy.X, enemy.Y, false);
                dungeonMap.SetIsWalkable(player.X, player.Y, false);

                if (path != null)
                {
                    try
                    {
                        commands.MoveEnemy(enemy, path.StepForward());
                    }
                    catch (NoMoreStepsException)
                    {
                        Program.MessageLog.Add($"{enemy.Name} waiting for a turn");
                    }
                }
                enemy.TurnsAlerted++;
                if (enemy.TurnsAlerted>15)
                {
                    enemy.TurnsAlerted = null;
                }
            }
            return true;
        }
    }
}
