using System;

namespace Grimoire.Logic.Models
{
    public class Point : IEquatable<Point>, ICloneable
    {
        private static readonly Point _zeroPoint = new Point();
        public int X { get; set; }
        public int Y { get; set; }

        /// Return point (0,0)
        public static Point Zero
        {
            get
            {
                return Point._zeroPoint;
            }
        }

        /// ctor
        public Point()
        {
        }

        /// <summary>Initializes a new instance of Point</summary>
        /// <param name="x">The x-coordinate of the Point</param>
        /// <param name="y">The y-coordinate of the Point</param>
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>Determines whether two Point instances are equal</summary>
        /// <param name="a">Point on the left side of the equal sign</param>
        /// <param name="b">Point on the right side of the equal sign</param>
        /// <returns>True if a and b are equal; False otherwise</returns>
        public static bool operator ==(Point a, Point b)
        {
            if ((object)a == null && (object)b == null)
                return true;
            if ((object)a == null)
                return false;
            return a.Equals(b);
        }

        /// <summary>Determines whether two Point instances are not equal</summary>
        /// <param name="a">Point on the left side of the equal sign</param>
        /// <param name="b">Point on the right side of the equal sign</param>
        /// <returns>True if a and b are not equal; False otherwise</returns>
        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }

        /// <summary>Determines whether two Point instances are equal</summary>
        /// <param name="other">The Point to compare this instance to</param>
        /// <returns>True if the instances are equal; False otherwise</returns>
        /// <exception cref="T:System.NullReferenceException">Thrown if .Equals is invoked on null Point</exception>
        public bool Equals(Point other)
        {
            if (other == (Point)null || this.X != other.X)
                return false;
            return this.Y == other.Y;
        }

        /// <summary>Determines whether two Point instances are equal</summary>
        /// <param name="obj">The Object to compare this instance to</param>
        /// <returns>True if the instances are equal; False otherwise</returns>
        /// <exception cref="T:System.NullReferenceException">Thrown if .Equals is invoked on null Point</exception>
        public override bool Equals(object obj)
        {
            Point other = obj as Point;
            if (other == (Point)null)
                return false;
            return this.Equals(other);
        }

        /// <summary>
        /// Gets the hash code for this object which can help for quick checks of equality
        /// or when inserting this Point into a hash-based collection such as a Dictionary or Hashtable
        /// </summary>
        /// <returns>An integer hash used to identify this Point</returns>
        public override int GetHashCode()
        {
            return this.X ^ this.Y;
        }

        /// <summary>Returns a string that represents the current Point</summary>
        /// <returns>A string that represents the current Point</returns>
        public override string ToString()
        {
            return string.Format("{{X:{0} Y:{1}}}", new object[2]
            {
        (object) this.X,
        (object) this.Y
            });
        }

        /// <summary>
        /// Duplicate points
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new Point(X,Y);
        }
    }
}
