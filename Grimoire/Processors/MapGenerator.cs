using Grimoire.Core;
using RogueSharp;
using RogueSharp.Random;
using System;
using System.Linq;

namespace Grimoire.Processor
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

        #region const
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
            _map.Initialize(_width,_height);

            for (int r = _maxRooms; r > 0; r--)
            {
                int roomWidth = Program.Random.Next(_roomMinSize, _roomMaxSize);
                int roomHeight = Program.Random.Next(_roomMinSize, _roomMaxSize);
                int roomXPosition = Program.Random.Next(0, _width - roomWidth - 1);
                int roomYPosition = Program.Random.Next(0, _height - roomHeight - 1);

                var newRoom = new Rectangle(roomXPosition, roomYPosition,
                  roomWidth, roomHeight);

                bool newRoomIntersects = _map.Rooms.Any(room => newRoom.Intersects(room));

                if (!newRoomIntersects)
                {
                    _map.Rooms.Add(newRoom);
                }
            }
            foreach (Rectangle room in _map.Rooms)
            {
                CreateRoom(room);
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

            return _map;
        }

        private void CreateRoom(Rectangle room)
        {
            for (int x = room.Left + 1; x < room.Right; x++)
            {
                for (int y = room.Top + 1; y < room.Bottom; y++)
                {
                    _map.SetCellProperties(x, y, true, true, true);
                }
            }
        }
    }
}
