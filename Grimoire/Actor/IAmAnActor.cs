using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Actor
{
    public interface IAmAnActor
    {
        string Name { get; set; }
        int FOV { get; set; }
    }
}
