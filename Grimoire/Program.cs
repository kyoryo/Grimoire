using Grimoire.Core;
using Grimoire.Core.Childs;
using Grimoire.Enums;
using Grimoire.Helper;
using Grimoire.Logic.Generator;
using Grimoire.Processor;
using Grimoire.Processors;
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
        private static bool _renderRequired = true;

        public static Player Player { get; set; }
        public static DungeonMap DungeonMap { get; private set; }
        public static IRandom Random { get; private set; }
        public static Commands Commands { get; private set; }

        static void Main()
        {
            string fontFile = AppHelper.GetFontFile();
            int seed = (int)DateTime.UtcNow.Ticks;
            Random = new DotNetRandom(seed);

            string title = "Grimoire (developing)";

            _rootConsole = new RLRootConsole(fontFile, ScreenFrame.Width, ScreenFrame.Height, _charWidth, _charHeight, 1f, title);
            _mapConsole = new RLConsole(MapFrame.Width, MapFrame.Height);
            _messageConsole = new RLConsole(MessageFrame.Width, MessageFrame.Height);
            _statusConsole = new RLConsole(StatusFrame.Width, MessageFrame.Height);
            _inventoryConsole = new RLConsole(StatusFrame.Width, MessageFrame.Height);

            Player = new Player();
            MapGenerator mapGenerator = new MapGenerator(MapFrame.Width, MapFrame.Height/*, 20, 13, 7*/);
            DungeonMap = mapGenerator.CreateMap();

            Commands = new Commands();

            DungeonMap.UpdatePlayerFieldOfView();


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

            bool isPlayerAct = false;
            var pressedKey = _rootConsole.Keyboard.GetKeyPress();
            if (pressedKey != null)
            {
                if(pressedKey.Key == RLKey.Up)
                {
                    isPlayerAct = Commands.MovePlayer(Directions.Up);
                }
                else if (pressedKey.Key == RLKey.Down)
                {
                    isPlayerAct = Commands.MovePlayer(Directions.Down);
                }
                else if (pressedKey.Key == RLKey.Left)
                {
                    isPlayerAct = Commands.MovePlayer(Directions.Left);
                }
                else if (pressedKey.Key == RLKey.Right)
                {
                    isPlayerAct = Commands.MovePlayer(Directions.Right);
                }
                else if (pressedKey.Key == RLKey.Escape)
                {
                    _rootConsole.Close();
                }
            }
            if (isPlayerAct)
            {
                _renderRequired = true;
            }
        }
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            if (_renderRequired)
            {
                DungeonMap.Draw(_mapConsole);
                Player.Draw(_mapConsole, DungeonMap);

                RLConsole.Blit(_mapConsole, 0, 0, MapFrame.Width, MapFrame.Height, _rootConsole, 0, InventoryFrame.Height);
                RLConsole.Blit(_statusConsole, 0, 0, StatusFrame.Width, StatusFrame.Height, _rootConsole, MapFrame.Width, 0);
                RLConsole.Blit(_messageConsole, 0, 0, MessageFrame.Width, MessageFrame.Height, _rootConsole, 0, ScreenFrame.Height - MessageFrame.Height);
                RLConsole.Blit(_inventoryConsole, 0, 0, InventoryFrame.Width, InventoryFrame.Height, _rootConsole, 0, 0);

                _rootConsole.Draw();
                _renderRequired = false;
            }
            
        }
    }
}
