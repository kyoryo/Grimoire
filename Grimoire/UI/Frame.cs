using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.UI
{
    /// <summary>
    /// Frame using BitBlit
    /// </summary>
    public class Frame
    {
        // Screen
        private static readonly int _screenWidth = 100;
        private static readonly int _screenHeight = 70;

        // MAP
        private static readonly int _mapWidth = 80;
        private static readonly int _mapHeight = 48;

        // Message
        private static readonly int _messageWidth = 80;
        private static readonly int _messageHeight = 11;

        // Status
        private static readonly int _statusWidth = 20;
        private static readonly int _statusHeight = 70;

        // Invent
        private static readonly int _inventoryWidth = 80;
        private static readonly int _inventoryHeight = 11;

        public static int GetScreenWidth()
        {
            return _screenWidth;
        }
        public static int GetScreenHeight()
        {
            return _screenHeight;
        }
        public static int GetMapWidth()
        {
            return _mapWidth;
        }
        public static int statusWidth()
        {
            return _statusWidth;
        }
        public static int statusHeight()
        {
            return _statusHeight;
        }
        public static int inventoryWidth()
        {
            return _inventoryWidth;
        }
        public static int inventoryHeight()
        {
            return _inventoryHeight;
        }

    }
}
