using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grimoire.Core;
using Grimoire.Processors;

namespace Grimoire.Interfaces
{
    public interface IAmBehavable
    {
        bool Act(Enemy enemy, Commands commands);
    }
}
