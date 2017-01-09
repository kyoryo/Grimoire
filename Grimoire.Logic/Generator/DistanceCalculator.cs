using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace Grimoire.Logic.Generator
{
    public class DistanceCalculator
    {
        public double Distance(int x1, int y1, int x2, int y2)
        {
            double x = x2 - x1;
            double y = y2 - y1;
            var result = Math.Sqrt(Math.Pow(x, 2)+Math.Pow(y, 2));
            return result;
        }
    }
}
