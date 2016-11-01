using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Interfaces
{
    public interface IAmAnActor
    {
        string Name { get; set; }
        int FieldOfView { get; set; }
    }
}
