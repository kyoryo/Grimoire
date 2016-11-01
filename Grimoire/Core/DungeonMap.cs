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
        //private static Player player { get; set; }
        //public List<Rectangle> Rooms;
        //public DungeonMap()
        //{
        //    Rooms = new List<Rectangle>();
        //}
        // drawing map every level
        public void Draw(RLConsole mapConsole)
        {
            mapConsole.Clear();
            foreach (var cell in GetAllCells())
            {
                SetConsoleSymbolForCell(mapConsole, cell);
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
    }
}
