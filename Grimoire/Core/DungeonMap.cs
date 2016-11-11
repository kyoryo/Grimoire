using Grimoire.Core.Childs;
using Grimoire.UI;
using Grimoire.UI.Cells;
using RLNET;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Core
{
    public class DungeonMap : Map
    {
        private List<Enemy> Enemys;
        public List<Rectangle> Rooms;
        public DungeonMap()
        {
            Enemys = new List<Enemy>();
            Rooms = new List<Rectangle>();
        }
        // drawing map every level
        public void Draw(RLConsole mapConsole, RLConsole statConsole)
        {
            foreach (var cell in GetAllCells())
            {
                SetConsoleSymbolForCell(mapConsole, cell);
            }

            int counter = 0;
            foreach (var enemy in Enemys)
            {
                enemy.Draw(mapConsole, this);
                if (IsInFov(enemy.X, enemy.Y))
                {

                    enemy.DrawStats(statConsole, counter);
                    counter++;
                }
            }

        }
        private void SetConsoleSymbolForCell(RLConsole console, Cell cell)
        {
            //dont draw if not explored
            if (!cell.IsExplored)
            {
                return;
            }
            //draw if explored
            if (IsInFov(cell.X, cell.Y))
            {
                if (cell.IsWalkable)
                    console.Set(cell.X, cell.Y, Colors.FloorFov, Colors.FloorBackgroundFov, CellSymbol.floor);
                else
                    console.Set(cell.X, cell.Y, Colors.WallFov, Colors.WallBackgroundFov, CellSymbol.wall);
            }
            else
            {
                if (cell.IsWalkable)
                    console.Set(cell.X, cell.Y, Colors.Floor, Colors.FloorBackground, CellSymbol.floor);
                else
                    console.Set(cell.X, cell.Y, Colors.Wall, Colors.WallBackground, CellSymbol.wall);
            }
        }

        public void UpdatePlayerFieldOfView()
        {
            Player player = Program.Player;
            // Compute the field-of-view based on the player's location and awareness
            ComputeFov(player.X, player.Y, player.FieldOfView, true);
            // Mark all cells in field-of-view as having been explored
            foreach (Cell cell in GetAllCells())
            {
                if (IsInFov(cell.X, cell.Y))
                {
                    SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
                }
            }
        }

        public bool SetActorPosition (Actor actor, int x, int y)
        {
            if (GetCell(x, y).IsWalkable)
            {
                SetIsWalkable(actor.X, actor.Y, true);

                //update pos
                actor.X = x;
                actor.Y = y;

                SetIsWalkable(actor.X, actor.Y, false);

                if (actor is Player)
                {
                    UpdatePlayerFieldOfView();
                }
                return true;
            }
            return false;
        }

        private void SetIsWalkable(int x, int y, bool isWalkable)
        {
            var getCell = GetCell(x, y);
            SetCellProperties(getCell.X, getCell.Y, getCell.IsTransparent, isWalkable, getCell.IsExplored);
        }
        public void AddPlayer(Player player)
        {
            Program.Player = player;
            SetIsWalkable(player.X, player.Y, false);
            UpdatePlayerFieldOfView();
        }
        public void AddEnemy(Enemy enemy)
        {
            Enemys.Add(enemy);
            SetIsWalkable(enemy.X, enemy.Y, false);
        }
        /// <summary>
        /// Get Random room that is walkable
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public Point GetRandomWalkableLocationInTheRoom(Rectangle room)
        {
            if (DoesRoomHaveWalkableSpace(room))
            {
                for (int i = 0; i < 100; i++)
                {
                    int x = Program.Random.Next(1, room.Width - 2) + room.X;
                    int y = Program.Random.Next(1, room.Height - 2) + room.Y;
                    if (IsWalkable(x, y))
                    {
                        return new Point(x, y);
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// Iterate through each Cell in room
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public bool DoesRoomHaveWalkableSpace(Rectangle room)
        {
            for (int x= 1; x <= room.Width - 2; x++)
            {
                for (int y =1; y <= room.Width -2; y++)
                {
                    if (IsWalkable(x + room.X, y + room.Y))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// remove enemy if die
        /// </summary>
        /// <param name="enemy"></param>
        public void RemoveEnemy(Enemy enemy)
        {
            Enemys.Remove(enemy);
            SetIsWalkable(enemy.X, enemy.Y, true);
        }
        public Enemy GetEnemyAt(int x, int y)
        {
            return Enemys.FirstOrDefault(ePos => ePos.X == x && ePos.Y == y);
        }
        
    }
}
