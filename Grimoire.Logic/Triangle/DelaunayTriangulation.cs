using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grimoire.Logic.Models;

namespace Grimoire.Logic.Triangle
{
    public class DelaunayTriangulation
    {
        /// <summary>
        /// list of all points
        /// </summary>
        private List<Point> points;
        /// <summary>
        /// List of point that has not been triangulated
        /// </summary>
        private List<Point> leftOverPoints = new List<Point>();
        /// <summary>
        /// List of triangulated points
        /// </summary>
        private List<Point> pointsUsed =new List<Point>();
        /// <summary>
        /// List of triangles to be returned by Triangulate Method
        /// </summary>
        internal List<Triangle> res = new List<Triangle>();

        public DelaunayTriangulation(List<Point> points )
        {
            this.points = points;
            for (int i = 0; i < points.Count; i++)
            {
                leftOverPoints.Add((Point)points[i].Clone());
            }
        }

        public List<Triangle> Triangulate()
        {
            res.Clear();
            Triangle rootTriangle=new Triangle(); //1st triangle
            Pair pair;
            List<Point> leftOverPointsTemp = new List<Point>(leftOverPoints);
            List<Point> leftOverPointsTemp1 = new List<Point>(leftOverPoints);
            foreach (Point[] point in GetRandomPoints())
            {
                if (!HasPointsInside(point[0], point[1], point[2]))
                {
                    rootTriangle = new Triangle(point[0], point[1], point[2]);
                    res.Add(rootTriangle);
                    leftOverPointsTemp.Remove(point[0]);
                    leftOverPointsTemp.Remove(point[1]);
                    leftOverPointsTemp.Remove(point[2]);

                    //Adds to the list of points used, the points in p
                    pointsUsed.Add(point[0]);
                    pointsUsed.Add(point[1]);
                    pointsUsed.Add(point[2]);

                    pair = Triangulate(leftOverPointsTemp, res);
                    if (pair.ok)
                    {
                        return pair.res;
                    }
                    else
                    {
                        //Removes from the last parameter position
                        res.RemoveAt(0);
                        pointsUsed.RemoveAt(pointsUsed.Count - 1);
                        pointsUsed.RemoveAt(pointsUsed.Count - 1);
                        pointsUsed.RemoveAt(pointsUsed.Count - 1);

                        leftOverPointsTemp = leftOverPointsTemp1;
                    }
                }
            }
            return null;

        }

        private Pair Triangulate(List<Point> leftOverPoints, List<Triangle> res)
        {
            List<Triangle> temp = null;
            Point newPoint = new Point();
            while (leftOverPoints.Count>0)
            {
                for (int i = 0; i < leftOverPoints.Count; i++)
                {
                    temp = GetNextTriangle(leftOverPoints[i]);
                    if (temp.Count>0)
                    {
                        res.AddRange(temp);
                        newPoint = leftOverPoints[i];
                        leftOverPoints.RemoveAt(i);
                        pointsUsed.Add(newPoint);
                        Pair p = Triangulate(leftOverPoints, res);
                        if (p.ok)
                        {
                            return p;
                        }
                        else
                        {
                            res.RemoveRange(res.Count-temp.Count,temp.Count);
                            leftOverPoints.Insert(i,newPoint);
                            pointsUsed.Remove(newPoint);
                        }
                    }
                }
                return new Pair(res,false);
                
            }
            return new Pair(res,true);
        }

        private List<Triangle> GetNextTriangle(Point newPoint)
        {
            List<Triangle> result = new List<Triangle>();
            foreach (Point[] point in GetPairRandomPoints())
            {
                if (!point[0].Equals(newPoint) && !point[1].Equals(newPoint))
                {
                    if (!HasPointsInside(point[0], point[1], newPoint))
                    {
                        result.Add(new Triangle(point[0], point[1], newPoint));
                    }
                }
                
            }
            return result;
        }
        //This function is testing all pairs of possible points
        private IEnumerable<Point[]> GetPairRandomPoints()
        {
            for (int i = 0; i < pointsUsed.Count; i++)
                for (int j = i + 1; j < pointsUsed.Count; j++)
                    yield return new Point[2] { pointsUsed[i], pointsUsed[j] };
        }

        private bool HasPointsInside(Point point, Point point1, Point point2)
        {
            List<Triangle> listTriangles = new List<Triangle>();
            Triangle triangle = new Triangle(point, point1, point2);
            listTriangles.Add(triangle);
            Dictionary<Triangle, Circle> dictionary = GetCircumference(listTriangles);
            foreach (Point p in points)
            {
                if (!p.Equals(point) && !p.Equals(point1) && !p.Equals(point2))
                {
                    var rad = Math.Pow(p.X - dictionary[triangle].center.X, 2) +
                              Math.Pow(p.Y - dictionary[triangle].center.Y, 2);

                    if (rad <= Math.Pow(dictionary[triangle].radius,2))
                        return true;
                }
            }
            return false;
        }

       

        /// <summary>
        /// Method to test possible triangles
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Point[]> GetRandomPoints()
        {
            for (int i = 0; i < points.Count; i++)
                for (int j = i+1; j < points.Count; j++)
                    for (int k = j+1; k < points.Count; k++)
                        //The "yield" instruction followed by the "return" instruction is to "freeze or stop" the execution of this method. It returns what it says in the return and stops the execution of the method until it is called again. When it is called again, it continues the execution of the method by which it had remained
                        yield return new Point[3]
                        {
                            points[i],
                            points[j],
                            points[k]
                        };
        }
        private Dictionary<Triangle, Circle> GetCircumference(List<Triangle> listTriangles)
        {
            Dictionary<Triangle,Circle> res = new Dictionary<Triangle, Circle>();
            Rect r1, r2, r3, r4;
            Point intersection;
            float radius;
            if (listTriangles != null)
            {
                foreach (Triangle triangle in listTriangles)
                {
                    r1 = GetRectEquation(triangle.a, triangle.b);
                    r2 = GetMatrixEquation(triangle.a, triangle.b, r1);
                    r3 = GetRectEquation(triangle.b, triangle.c);
                    r4 = GetMatrixEquation(triangle.b, triangle.c, r3);
                    intersection = GetIntersectPoint(r2, r4);
                    radius = (float) GetDistance(triangle.a, intersection);
                    res[triangle] =new Circle(radius,(Point)intersection.Clone());
                }
            }
            return res;
        }

        private Rect GetRectEquation(Point p1, Point p2)
        {
            var numerator = (int) (p1.Y - p2.Y);
            var denominator = (int) (p1.X - p2.Y);
            var n = -1*p1.X*((float) (p1.Y - p2.Y)/(float) (p1.X - p2.Y)) + p1.Y;
            var m = new Rational(numerator, denominator);
            var result = new Rect(m, n);
            return result;
        }

        private Rect GetMatrixEquation(Point p1, Point p2, Rect r)
        {
            var halfX = (int) (p1.X + p2.X)/2; ///TODO FLOAT
            var halfY = (int) (p1.Y + p2.Y)/2; ///TODO FLOAT
            Point halfPoint = new Point(halfX,halfY);
            Rational m = new Rational(r.M._denominator, r.M._numerator * -1);
            Rect resutlRect = new Rect(m, -1*m.Eval()*halfPoint.X + halfPoint.Y);
            return resutlRect;
        }

        private Point GetIntersectPoint(Rect r1, Rect r2)
        {
            float x = (r2.N - r1.N)/(float) (r1.SlopeEval() - r2.SlopeEval());
            float y = r1.Eval(x);
            return new Point((int)x,(int)y); ///TODO FLOAT
        }

        private float GetDistance(Point point, Point point1)
        {
            //With the Euclidean norm
            var result = (float) Math.Sqrt(Math.Pow(point.X - point1.X, 2) + Math.Pow(point.Y - point1.Y, 2));
            return result;
        }

    }
}
