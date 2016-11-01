using RLNET;
using RogueSharp;
using Grimoire.UI;
using Grimoire.Interfaces;

namespace Grimoire.Core
{
    public class Actor : IAmAnActor, IAmDrawable
    {
        public string Name { get; set; }
        public int FieldOfView { get; set; }

        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public void Draw(RLConsole console, IMap map)
        {
            if (!map.GetCell(X, Y).IsExplored)
            {
                return;
            }
            if (map.IsInFov(X, Y))
            {
                console.Set(X, Y, Color, Colors.FloorBackgroundFov, Symbol);
            }
            else
            {
                console.Set(X, Y, Colors.Floor, Colors.FloorBackground, '.');
            }
        }
    }
}
