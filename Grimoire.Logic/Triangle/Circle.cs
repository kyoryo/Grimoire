using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grimoire.Logic.Models;

namespace Grimoire.Logic.Triangle
{
    public class Circle : ICloneable
    {
        public float radius = 0;
        public Point center = new Point();

        public Circle()
        {
        }

        public Circle(float radius, Point center)
        {
            this.radius = radius;
            this.center = center;
        }

        public object Clone()
        {
            return  new Circle(radius, (Point)center.Clone());
        }
    }
}
