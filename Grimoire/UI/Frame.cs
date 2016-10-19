using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.UI
{
    /// <summary>
    /// The Whole screen frame
    /// </summary>
    public struct ScreenFrame
    {
        public static int Width { get { return 100; } }
        public static int Height { get {return 70; } }
    }
    /// <summary>
    /// Map Frame
    /// </summary>
    public struct MapFrame
    {
        public static int Width { get { return 80; } }
        public static int Height { get { return 48; } }
    }
    /// <summary>
    /// Message Frame
    /// </summary>
    public struct MessageFrame
    {
        public static int Width { get { return 80; } }
        public static int Height { get { return 11; } }
    }
    /// <summary>
    /// Status Frame
    /// </summary>
    public struct StatusFrame
    {
        public static int Width { get { return 20; } }
        public static int Height { get { return 70; } }
    }
    /// <summary>
    /// Inventory Frame
    /// </summary>
    public struct InventoryFrame
    {
        public static int Width { get { return 80; } }
        public static int Height { get { return 11; } }
    }
}
