﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;
using Grimoire.Domain.Actor;

namespace Grimoire.Domain.Actors
{
    public class Actor : IAmAnActor/*, ICanDrawMap*/
    {
        public string Name { get; set; }
        public int FOV { get; set; }
        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public void Draw(RLConsole console, IMap map)
        {
            //if (!map.GetCell(X, Y).IsExplored)
            //{
            //    return;
            //}
            //if (map.IsInFov(X, Y))
            //{
            //    console.Set(X, Y, Color, Colors.FloorBackgroundFov, Symbol);
            //}
            //else
            //{
            //    console.Set(X, Y, Colors.Floor, Colors.FloorBackground, '.');
            //}
        }
    }
}
