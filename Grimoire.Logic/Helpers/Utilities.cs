using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Logic.Helpers
{
    public class Utilities
    {
        public void Swap<T>(ref T x, ref T y)
        {
            T t = y;
            y = x;
            x = t;
        }
    }
}
