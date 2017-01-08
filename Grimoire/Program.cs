using Grimoire.Core;
using Grimoire.Core.Childs;
using Grimoire.Enums;
using Grimoire.Logic.Generator;
using Grimoire.Processors;
using Grimoire.UI;
using Grimoire.UI.Frames;
using RLNET;
using RogueSharp.Random;
using System;
using Grimoire.Logic.Helpers;
using Grimoire.Logic.Interfaces;
using Grimoire.Logic.Random;

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
        public static IRandomNumber Random { get; private set; }
        public static Commands Commands { get; private set; }
        public static MessageLog MessageLog { get; private set; }
        public static Schedule Schedule { get; private set; }

        static void Main()
        {
            Console.WriteLine("Now Initializing"); 
            string fontFile = AppHelper.GetFontFile();
            int seed = 2;
            //int seed = (int)DateTime.UtcNow.Ticks;
            //int seed = 443041680;
            Random = new NormalDistributionRandom(seed);
#if DEBUG
            Console.WriteLine($"Initial Seed: {seed}");
#endif


            #region Test
            //int radius = MapFrame.Width;
            //int ellipseWidth = MapFrame.Width;
            //int ellipseHeight = MapFrame.Height;
            //var circle = new PointsCalculator();
            ////var randomPoints = circle.GetPointInsideCircle(radius, 20, PointsType.CanBeNegative);
            //var randomPoints = circle.GetPointInsideRectangle(ellipseWidth, ellipseHeight, 20);
            //foreach (var point in randomPoints)
            //{
            //    Console.WriteLine("X = " + point.X);
            //    Console.WriteLine("Y = " + point.Y);

            //}
            #endregion

            string title = "Grimoire (developing)";

            MessageLog = new MessageLog();
            MessageLog.Add($"Level 1 with seed:'{seed}'");

            _rootConsole = new RLRootConsole(fontFile, ScreenFrame.Width, ScreenFrame.Height, _charWidth, _charHeight, 1f, title);

            #region DEBUG console log
#if DEBUG
            Console.WriteLine($"Screen width : {ScreenFrame.Width}");
            Console.WriteLine($"Screen Height : {ScreenFrame.Height}");
#endif
            #endregion

            _mapConsole = new RLConsole(MapFrame.Width, MapFrame.Height);
            #region DEBUG console log
#if DEBUG
            Console.WriteLine($"Map width : {MapFrame.Width}");
            Console.WriteLine($"Map Height : {MapFrame.Height}");
#endif
            #endregion

            _messageConsole = new RLConsole(MessageFrame.Width, MessageFrame.Height);
            _statusConsole = new RLConsole(StatusFrame.Width, StatusFrame.Height);
            _inventoryConsole = new RLConsole(InventoryFrame.Width, InventoryFrame.Height);


            #region DEBUG console log
#if DEBUG
            Console.WriteLine($"dungeon width : {DungeonFrame.Width}");
            Console.WriteLine($"dungeon Height : {DungeonFrame.Height}");
#endif
            #endregion
            MapGenerator mapGenerator = new MapGenerator(DungeonFrame.Width, DungeonFrame.Height, 20, 13, 7);
            
            //MapGenerator mapGenerator = new MapGenerator(100, 100, 60, 13, 7);
            DungeonMap = mapGenerator.CreateMap();
            DungeonMap.UpdatePlayerFieldOfView();

            Commands = new Commands();

            _rootConsole.Update += OnRootConsoleUpdate;
            _rootConsole.Render += OnRootConsoleRender;


            _inventoryConsole.SetBackColor(0, 0, InventoryFrame.Width, InventoryFrame.Height, Colors.DbSkin);
            _inventoryConsole.Print(1, 1, "for invent", Colors.TextHeading);

            _rootConsole.Run();

        }
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            //_mapConsole.SetBackColor(0, 0, MapFrame.Width, MapFrame.Height, Colors.FloorBackground);
            //_mapConsole.Print(1, 1, "for map", Colors.TextHeading);

            //_statusConsole.SetBackColor(0, 0, StatusFrame.Width, StatusFrame.Height, Colors.FloorBackground);
            //_statusConsole.Print(1, 1, "for status", Colors.TextHeading);


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
                _mapConsole.Clear();
                _statusConsole.Clear();
                _messageConsole.Clear();

                DungeonMap.Draw(_mapConsole, _statusConsole);
                Player.Draw(_mapConsole, DungeonMap);
                Player.DrawStats(_statusConsole);
                MessageLog.Draw(_messageConsole);

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
