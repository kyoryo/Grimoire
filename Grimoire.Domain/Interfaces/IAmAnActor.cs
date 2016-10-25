using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Domain.Actor
{
    public interface IAmAnActor
    {
        string Name { get; set; }
        int FOV { get; set; }
    }
}
