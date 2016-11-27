using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Logic.Triangle
{
    public class Pair
    {
        public List<Triangle> res;
        public bool ok = false;

        public Pair(List<Triangle> res, bool ok)
        {
            this.res = res;
            this.ok = ok;
        }
    }
}
