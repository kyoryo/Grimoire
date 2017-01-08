using System;
using System.Collections.Generic;
using System.Linq;
using Grimoire.Core;
using Grimoire.Core.Childs;
using Grimoire.Enemys;
using Grimoire.Enums;
using Grimoire.Logic.Triangle;
using RogueSharp;
using RogueSharp.DiceNotation;

namespace Grimoire.Processors
{
    public class MapGenerator
    {
        private readonly int _width;
        private readonly int _height;
        private readonly int _maxRooms;
        private readonly int _roomMaxSize;
        private readonly int _roomMinSize;
        private readonly DungeonMap _map;
        //private static IRandom rng {get;set;} 
        //private readonly RandomNumber _rng;

        #region ctor

        public MapGenerator(int width, int height, int maxRooms, int roomMaxSize, int roomMinSize)
        {
            _width = width;
            _height = height;
            _maxRooms = maxRooms;
            _roomMaxSize = roomMaxSize;
            _roomMinSize = roomMinSize;
            _map = new DungeonMap();
            //_rng = new RandomNumber();
        }

        #endregion

        public DungeonMap CreateMap()
        {
            #region DEBUG console log
#if DEBUG
            Console.WriteLine($"max rooms:{_maxRooms}");
            Console.WriteLine($"room max size: {_roomMaxSize}");
            Console.WriteLine($"room min size: {_roomMinSize}");
#endif
            #endregion
            _map.Initialize(_width, _height);
            var pointsCalculator = new PointsCalculator();
            var points = pointsCalculator.GetPointInsideRectangle(_width, _height, _maxRooms);
            DelaunayTriangulation delaunay = new DelaunayTriangulation(points);

            #region DEBUG console log

#if DEBUG
            foreach (var del in delaunay.Triangulate())
            {
                Console.WriteLine($"triangle abc {del.a}{del.b}{del.c}");
            }
#endif

            #endregion
            List<Rectangle> newRoomsList = new List<Rectangle>();
            foreach (var point in points)
            {
                int roomWidth = Program.Random.Next(_roomMinSize, _roomMaxSize);
                int roomHeight = Program.Random.Next(_roomMinSize, _roomMaxSize);
                //  int roomXPosition = Program.Random.Next(0, _width - roomWidth - 1);
                //  int roomYPosition = Program.Random.Next(0, _height - roomHeight - 1);
                int roomXPosition = point.X;
                int roomYPosition = point.Y;

                var newRoom = new Rectangle(roomXPosition, roomYPosition, roomWidth, roomHeight);
                bool newRoomIntersects = _map.Rooms.Any(room => newRoom.Intersects(room));

                #region DEBUG console log

#if DEBUG
                int counter = 0;
                Console.WriteLine($"romm width {counter} : {roomWidth}");
                Console.WriteLine($"room height {counter} : {roomHeight}");
                Console.WriteLine($"room pos X {counter} : {roomXPosition}");
                Console.WriteLine($"room pos y {counter} : {roomYPosition}");
                counter++;
#endif

                #endregion
                #region temporary
                //bool newRoomIntersects = false;
                //foreach (var room in _map.Rooms)
                //{
                //    if (newRoom.Intersects(room))//newroom intersecting with previous room
                //    {
                //        newRoomIntersects = true;
                //        do
                //        {
                //            roomXPosition += 1;
                //            newRoom = new Rectangle(roomXPosition,roomXPosition,roomWidth,roomHeight);
                //            if (newRoom.Intersects(room))
                //            {
                //                roomYPosition += 1;
                //                newRoom = new Rectangle(roomXPosition,roomYPosition,roomWidth,roomHeight);
                //            }
                //            if (!newRoom.Intersects(room))
                //            {
                //                newRoomIntersects = false;
                //            }
                //        } while (newRoomIntersects);
                //        //break;
                //    }
                //}
                #endregion

                #region DEBUG Console log
#if DEBUG
                Console.WriteLine($"IsOverlapping: {newRoomIntersects}");
#endif
                #endregion
                if (!newRoomIntersects)
                {
                    _map.Rooms.Add(newRoom);
                }
            }

            foreach (Rectangle room in _map.Rooms)
            {
                CreateRoom(room);
            }
            for (int r = 1; r < _map.Rooms.Count; r++)
            {
                int previousRoomCenterX = _map.Rooms[r - 1].Center.X;
                int previousRoomCenterY = _map.Rooms[r - 1].Center.Y;
                int currentRoomCenterX = _map.Rooms[r].Center.X;
                int currentRoomCenterY = _map.Rooms[r].Center.Y;

                if (Program.Random.Next(1, 2) == 1)
                {
                    CreateHorizontalHallways(previousRoomCenterX, currentRoomCenterX, previousRoomCenterY);
                    CreateVerticalHallways(previousRoomCenterY, currentRoomCenterY, currentRoomCenterX);
                }
                else
                {
                    CreateVerticalHallways(previousRoomCenterY, currentRoomCenterY, previousRoomCenterX);
                    CreateHorizontalHallways(previousRoomCenterX, currentRoomCenterX, currentRoomCenterY);
                }
            }


            //foreach (var cell in _map.GetAllCells())
            //{
            //    _map.SetCellProperties(cell.X, cell.Y, true, true); //all walkable temp
            //}
            //foreach(var cell in _map.GetCellsInRows(0, _height - 1))
            //{
            //    _map.SetCellProperties(cell.X, cell.Y, false, false, true);
            //}
            //foreach (var cell in _map.GetCellsInColumns(0, _width - 1))
            //{
            //    _map.SetCellProperties(cell.X, cell.Y, false,false,true);
            //}
            PlaceEnemys();
            PlacePlayer();

            return _map;
        }

        private void CreateRoom(Rectangle room)
        {
            for (int x = room.Left + 1; x < room.Right; x++)
            {
                for (int y = room.Top + 1; y < room.Bottom; y++)
                {
                    try
                    {
                        _map.SetCellProperties(x, y, true, true, true);

                    }
                    catch (Exception e)
                    {
                        //unhandled exception(need camera movements)
                        Console.WriteLine(e.InnerException);
                        continue;
                    }
                }
            }
        }
        private void PlacePlayer()
        {
            Player player = Program.Player;
            if (player == null)
            {
                player = new Player();
            }
            player.X = _map.Rooms[0].Center.X;
            player.Y = _map.Rooms[0].Center.Y;

            _map.AddPlayer(player);
        }
        private void CreateHorizontalHallways(int xStart, int xEnd, int yPos)
        {
            for(int x=Math.Min(xStart,xEnd); x<= Math.Max(xStart, xEnd); x++)
            {
                _map.SetCellProperties(x, yPos, true, true);

                #region DEBUG console log

#if DEBUG
                Console.WriteLine($"horizontal corridor (X,Y) ({x},{yPos})");
#endif
                #endregion
            }
        }
        private void CreateVerticalHallways(int yStart, int yEnd, int xPos)
        {
            for (int y = Math.Min(yStart, yEnd); y <= Math.Max(yStart, yEnd); y++)
            {
                _map.SetCellProperties(xPos, y, true, true);
                #region DEBUG console log

#if DEBUG
                Console.WriteLine($"vertical corridor (X,Y) ({xPos},{y})");
#endif
                #endregion
            }
        }
        private void PlaceEnemys()
        {
            foreach(var room in _map.Rooms)
            {
                //monster chance to spawning
                if (Dice.Roll("1D10") < 7)
                {
                    //room spawn rate
                    var enemysNumber = Dice.Roll("1D4");
                    for (var i = 0; i < enemysNumber; i++)
                    {
                        Point randomRoomLocation = _map.GetRandomWalkableLocationInTheRoom(room);
                        if (randomRoomLocation != null)
                        {
                            // Temporarily hard code this monster to be created at level 1
                            var monster = Goblin.Create(1);
                            monster.X = randomRoomLocation.X;
                            monster.Y = randomRoomLocation.Y;
                            _map.AddEnemy(monster);
                        }
                    }
                }
            }
        }
        
    }
}
