using Grimoire.UI.Palettes;
using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.UI
{
    public class Colors : MainPalettes
    {
        public static RLColor FloorBackground = RLColor.Black;
        public static RLColor Floor = AlternateDarkest;
        public static RLColor FloorBackgroundFov = DbDark;
        public static RLColor FloorFov = Alternate;

        public static RLColor WallBackground = SecondaryDarkest;
        public static RLColor Wall = Secondary;
        public static RLColor WallBackgroundFov = SecondaryDarker;
        public static RLColor WallFov = SecondaryLighter;

        public static RLColor TextHeading = DbLight;

        public static RLColor Message = PrimaryDarker;
        public static RLColor Inventory = Secondary;
        public static RLColor Status = RLColor.Gray;
        public static RLColor Map = FloorBackground;

        public static RLColor Player = DbLight;
        public static RLColor Text = RLColor.White;
        public static RLColor Gold = DbSun;
    }
}
