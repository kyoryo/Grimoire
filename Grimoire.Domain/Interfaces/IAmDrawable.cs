using Grimoire.Domain.Model;
using RLNET;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Domain.Interfaces
{
    public interface IAmDrawable<T> where T : ObjectDrawingModel
    {
        void Draw(RLConsole console, IMap map);
    }
}
