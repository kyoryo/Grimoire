using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Logic.Triangle
{
    public class Rational
    {
        public int _numerator, _denominator;

        public Rational(int numerator, int denominator)
        {
            _numerator = numerator;
            _denominator = denominator;
        }

        public float Eval()
        {
            if (_denominator == 0)
                return 0;
            return (float) _numerator/(float) _denominator;
            
        }
    }
}
