using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;

namespace Grimoire.Logic.Generator
{
    public class PointsCalculator
    {
        //uniformly
        static readonly Random rand = new Random();
        //https://stackoverflow.com/questions/29060069/random-points-inside-a-circle
        public List<Point> GetPointInsideCircle(int radius, int numberOfPoints)
        {
            List<Point> points = new List<Point>();
            for (int pointIndex = 0; pointIndex < numberOfPoints; pointIndex++)
            {
                int distance = rand.Next(radius);
                double angle = rand.Next(360)/(2*Math.PI); //angle in radians

                int x = (int) (distance*Math.Cos(angle));
                int y = (int) (distance*Math.Sin(angle));

                Point randomPoint = new Point(x,y);
                points.Add(randomPoint);
            }
            return points;
        }
    }
}
