using Grimoire.Domain.Actors.Player;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Logic.Generator
{
    public class DungeonMap : Map
    {
        private static Player player { get; set; }
        public List<Rectangle> Rooms;
        public DungeonMap()
        {
            Rooms = new List<Rectangle>();
        }
        public void UpdatePlayerFieldOfView()
        {
            
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
