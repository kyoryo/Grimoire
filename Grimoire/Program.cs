using Grimoire.Actor;
using Grimoire.Domain.Actors.Player;
using Grimoire.Helper;
using Grimoire.Logic.Generator;
using Grimoire.UI;
using Grimoire.UI.Frames;
using RLNET;
using RogueSharp.Random;
using System;

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

        public static DungeonMap DungeonMap { get; private set; }
        public static IRandom Random { get; private set; }

        static void Main()
        {
            string fontFile = AppHelper.GetFontFile();
            int seed = (int)DateTime.UtcNow.Ticks;
            Random = new DotNetRandom(seed);


            //string fontFile = "terminal8x8.png";
            string title = "Grimoire (developing)";
            //var map = new RLConsole(Frame._inventoryWidth, Frame._inventoryHeight);

            _rootConsole = new RLRootConsole(fontFile, ScreenFrame.Width, ScreenFrame.Height, _charWidth, _charHeight, 1f, title);
            _mapConsole = new RLConsole(MapFrame.Width, MapFrame.Height);
            _messageConsole = new RLConsole(MessageFrame.Width, MessageFrame.Height);
            _statusConsole = new RLConsole(StatusFrame.Width, MessageFrame.Height);
            _inventoryConsole = new RLConsole(StatusFrame.Width, MessageFrame.Height);

            MapGenerator mapGenerator = new MapGenerator(MapFrame.Width, MapFrame.Height, 20, 12, 6);
            DungeonMap = mapGenerator.CreateMap();

            _rootConsole.Update += OnRootConsoleUpdate;
            _rootConsole.Render += OnRootConsoleRender;
            _rootConsole.Run();

        }
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            _mapConsole.SetBackColor(0, 0, MapFrame.Width, MapFrame.Height, Colors.FloorBackground);
            _mapConsole.Print(1, 1, "for map", Colors.TextHeading);

            _messageConsole.SetBackColor(0, 0, MessageFrame.Width, MessageFrame.Height, Colors.Message);
            _messageConsole.Print(1, 1, "for message", Colors.TextHeading);

            _statusConsole.SetBackColor(0, 0, StatusFrame.Width, StatusFrame.Height, Colors.Status);
            _statusConsole.Print(1, 1, "for status", Colors.TextHeading);

            _inventoryConsole.SetBackColor(0, 0, InventoryFrame.Width, InventoryFrame.Height, Colors.Inventory);
            _inventoryConsole.Print(1, 1,"for invent", Colors.TextHeading);
        }
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            RLConsole.Blit( _mapConsole, 0, 0, MapFrame.Width, MapFrame.Height, _rootConsole, 0, InventoryFrame.Height);
            RLConsole.Blit(_statusConsole, 0, 0, StatusFrame.Width, StatusFrame.Height, _rootConsole, MapFrame.Width, 0);
            RLConsole.Blit(_messageConsole, 0, 0, MessageFrame.Width, MessageFrame.Height, _rootConsole, 0, ScreenFrame.Height - MessageFrame.Height);
            RLConsole.Blit(_inventoryConsole, 0, 0, InventoryFrame.Width, InventoryFrame.Height, _rootConsole, 0, 0);

            _rootConsole.Draw();
        }
        private void UpdatePlayerFOV(object sender, UpdateEventArgs e)
        {
            Player player = new Player();
        }
    }
}
