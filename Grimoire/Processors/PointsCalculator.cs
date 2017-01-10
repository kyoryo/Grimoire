using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Grimoire.Enums;
using Grimoire.Logic.Helpers;
using Grimoire.Logic.Models;

namespace Grimoire.Processors
{
    public class PointsCalculator
    {
        //uniformly
        static readonly Random rand = new Random();
        //https://stackoverflow.com/questions/29060069/random-points-inside-a-circle
        /// <summary>
        /// Generate n random points inside circle by given radius
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="numberOfPoints">points to be generated (aka max room)</param>
        /// <returns></returns>
        public List<Point> GetPointInsideCircle(int radius, int numberOfPoints, PointsType? type = PointsType.CanBeNegative)
        {
            List<Point> points = new List<Point>();
            
            radius = radius - 1;
            switch (type)
            {
                case PointsType.AlwaysPositive: //Quadrant 1
                    for (int pointIndex = 0; pointIndex < numberOfPoints; pointIndex++)
                    {
                        int distance = Program.Random.Next(radius);
                        double angle = Program.Random.Next(360) / (2 * Math.PI); //angle in radians

                        int x = (int)(distance * Math.Cos(angle));
                        int y = (int)(distance * Math.Sin(angle));
                        if (x < 0)
                        {
                            x = 0 - x;
                        }
                        if (y<0)
                        {
                            y = 0 - y;
                        }
                        Point randomPoint = new Point(x, y);
                        points.Add(randomPoint);
                    }
                    return points;
                default:
                    for (int pointIndex = 0; pointIndex < numberOfPoints; pointIndex++)
                    {
                        int distance = Program.Random.Next(radius);
                        double angle = Program.Random.Next(360) / (2 * Math.PI); //angle in radians

                        int x = (int)(distance * Math.Cos(angle));
                        int y = (int)(distance * Math.Sin(angle));

                        Point randomPoint = new Point(x, y);
                        points.Add(randomPoint);
                    }
                    return points;
            }
            
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
                var utils = new Utilities();
                utils.Swap(ref  ellipseWidth,ref ellipseHeight);
            }
            List<Point> points = new List<Point>();
            for (int pointIndex = 0; pointIndex < numberOfPoints; pointIndex++)
            {
                //K = 2π*[1/2(a^2+b^2)]^1/2
                int distance = Program.Random.Next(ellipseWidth);
                //double angle = Program.Random.Next(360)/(2*Math.PI);
                double angle = Program.Random.Next(360)*(2*Math.PI);

                int x = (int) (distance*Math.Cos(angle));
                int y = (int) (distance*Math.Sin(angle));

                Point randomPoint = new Point(x, y);
                points.Add(randomPoint);
            }
            return points;
        }

        public List<Point> GetPointInsideRectangle(int rectangleWidth, int rectangleHeight, int numberOfPoints)
        {
            var centerX = rectangleWidth/2;
            var centerY = rectangleHeight/2;

            List<Point> points = new List<Point>();
            for (int pointIndex = 0; pointIndex < numberOfPoints; pointIndex++)
            {
                int x = Program.Random.Next(0, rectangleWidth - 0);
                int y = Program.Random.Next(0, rectangleHeight - 0);

                Point randomPoint = new Point(x, y);
                points.Add(randomPoint);
            }

            #region DEBUG console log

#if DEBUG
            Console.WriteLine($"number of points generated {points.Count}\ngenerated within width: {rectangleWidth} height: {rectangleHeight}\nPoints:");
            foreach (var point in points)
            {
                Console.WriteLine($"{point.X}\t{point.Y} ");
            }
#endif

            #endregion
            return points;
        }
    }
}
