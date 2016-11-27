using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Logic.Triangle
{
    public class Rect
    {
        public Rational M; //slope of the line
        public float N; //value of y when this line intersect with y-axis

        public Rect(Rational M, float N)
        {
            this.M = M;
            this.N = N;
        }

        public float SlopeEval()
        {
            return M.Eval();
        }

        public float Eval(float x)
        {
            return x*SlopeEval() + N;
        }
    }
}
