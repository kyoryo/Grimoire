using System;
using System.Collections.Generic;
using Grimoire.Logic.Utilities;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RogueSharp;

namespace Grimoire.Processors
{
    public class PointsCalculator
    {
        //uniformly
        //static readonly Random rand = new Random();
        //https://stackoverflow.com/questions/29060069/random-points-inside-a-circle
        /// <summary>
        /// Generate n random points inside circle
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="numberOfPoints">points to be generated (aka max room)</param>
        /// <returns></returns>
        public List<Point> GetPointInsideCircle(int radius, int numberOfPoints)
        {
            List<Point> points = new List<Point>();
            for (int pointIndex = 0; pointIndex < numberOfPoints; pointIndex++)
            {
                int distance = Program.Random.Next(radius);
                double angle = Program.Random.Next(360)/(2*Math.PI); //angle in radians

                int x = (int) (distance*Math.Cos(angle));
                int y = (int) (distance*Math.Sin(angle));
                if (x<0)
                {
                    x = ~x;
                }
                if (y<0)
                {
                    y = ~y;
                }

                Point randomPoint = new Point(x,y);
                points.Add(randomPoint);
            }
            return points;
        }
        /// <summary>
        /// Generate n random points inside circle
        /// </summary>
        /// <param name="ellipseWidth"></param>
        /// <param name="ellipseHeight"></param>
        /// <param name="numberOfPoints">points to be generated (aka max room)</param>
        /// <returns></returns>
        public List<Point> GetPointInsideCircle(int ellipseWidth, int ellipseHeight, int numberOfPoints)
        {
            if (ellipseWidth > ellipseHeight)
            {
                var utils = new Utils();
                utils.Swap(ref  ellipseWidth,ref ellipseHeight);
            }
            List<Point> points = new List<Point>();
            for (int pointIndex = 0; pointIndex < numberOfPoints; pointIndex++)
            {
                int distance = Program.Random.Next(ellipseWidth,ellipseHeight);
                double angle = Program.Random.Next(360)/(2*Math.PI);

                int x = (int) (distance*Math.Cos(angle));
                int y = (int) (distance*Math.Sin(angle));

                Point randomPoint = new Point(x, y);
                points.Add(randomPoint);
            }
            return points;
        }
    }
}
