using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grimoire.Logic.Models;

namespace Grimoire.Logic.Triangle
{
    public class TriangleCalculator : ICloneable
    {
        public Point a, b, c;

        bool FindAssign(Point a1, Point a2, Point a3, float low)
        {
            if (a1.X == low)
            {
                if (a2.X == low)
                {
                    if (a1.Y > a2.Y)
                    {
                        this.a = a1;
                        this.b = a2;
                        this.c = a3;
                        return true;
                    }
                    else
                    {
                        this.a = a2;
                        this.b = a1;
                        this.c = a3;
                        return true;
                    }
                }
                else if (a3.X == low)
                {
                    if (a1.Y > a3.Y)
                    {
                        this.a = a1;
                        this.b = a3;
                        this.c = a2;
                        return true;
                    }
                    else
                    {
                        this.a = a3;
                        this.b = a1;
                        this.c = a2;
                        return true;
                    }
                }
                else if (a2.X>a3.X)
                {
                    this.a = a3;
                    this.b = a1;
                    this.c = a2;
                    return true;
                }
                else
                {
                    this.a = a2;
                    this.b = a1;
                    this.c = a3;
                }
            }
            return false;
        }

        public TriangleCalculator(Point a, Point b, Point c)
        {
            if (a.Equals(b)||a.Equals(c)||b.Equals(c))
                throw new Exception("More than 2 points of triangle are equals");
            float low = Math.Min(a.X, b.X);
            low = Math.Min(low, c.X);
            if (!FindAssign(a, b, c, low) && !FindAssign(b, a, c, low))
                FindAssign(c, a, b, low);
        }
        public TriangleCalculator() { }

        public override bool Equals(object obj)
        {
            TriangleCalculator otherTriangle = obj as TriangleCalculator;
            return otherTriangle.a.Equals(a) && otherTriangle.b.Equals(b) && otherTriangle.c.Equals(c);
        }

        #region ICloneable Implementation
        public object Clone()
        {
            return new TriangleCalculator((Point)a.Clone(), (Point)b.Clone(), (Point)c.Clone());
        }
        #endregion
    }
}
