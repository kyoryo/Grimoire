using Grimoire.Helper;
using Grimoire.UI;
using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire
{
    public class Program
    {
        private static readonly int _charWidth = 8;
        private static readonly int _charHeight = 8;
        private static RLRootConsole _rootConsole;
        private static RLConsole _mapConsole;
        private static RLConsole _messageConsole;
        private static RLConsole _statusConsole;
        private static RLConsole _inventoryConsole;

        static void Main()
        {
            string fontFile = AppHelper.GetFontFile();
            //string fontFile = "terminal8x8.png";
            string title = "Tes Level 1";
            //var map = new RLConsole(Frame._inventoryWidth, Frame._inventoryHeight);

            _rootConsole = new RLRootConsole(fontFile, ScreenFrame.Width, ScreenFrame.Height, _charWidth, _charHeight, 1f, title);
            _mapConsole = new RLConsole(MapFrame.Width, MapFrame.Height);
            _messageConsole = new RLConsole(MessageFrame.Width, MessageFrame.Height);
            _statusConsole = new RLConsole(StatusFrame.Width, MessageFrame.Height);
            _inventoryConsole = new RLConsole(StatusFrame.Width, MessageFrame.Height);
            _rootConsole.Update += OnRootConsoleUpdate;
            _rootConsole.Render += OnRootConsoleRender;
            _rootConsole.Run();

        }
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            _mapConsole.SetBackColor(0, 0, RLColor.Black);
            _mapConsole.Print(1, 1, "for map", RLColor.White);

            _messageConsole.SetBackColor(0, 0, RLColor.Gray);
            _messageConsole.Print(1, 1, "for message", RLColor.White);

            _statusConsole.SetBackColor(0, 0, RLColor.Cyan);
            _statusConsole.Print(1, 1, "for status", RLColor.White);

            _inventoryConsole.SetBackColor(0, 0, RLColor.Black);
            _inventoryConsole.Print(1, 1,"for invent", RLColor.White);
        }
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            RLConsole.Blit( _mapConsole, 0, 0, MapFrame.Width, MapFrame.Height, _rootConsole, 0, InventoryFrame.Height);
            RLConsole.Blit(_statusConsole, 0, 0, StatusFrame.Width, StatusFrame.Height, _rootConsole, MapFrame.Width, 0);
            RLConsole.Blit(_messageConsole, 0, 0, MessageFrame.Width, MessageFrame.Height, _rootConsole, 0, ScreenFrame.Height - MessageFrame.Height);
            RLConsole.Blit(_inventoryConsole, 0, 0, InventoryFrame.Width, InventoryFrame.Height, _rootConsole, 0, 0);

            _rootConsole.Draw();
        }
    }
}
