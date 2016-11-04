using Grimoire.Logic.Generator.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp.Random;

namespace Grimoire.Logic.Generator
{
    public class RandomNumber : IRandomNumber, IRandom
    {
        public int Next()
        {
            throw new NotImplementedException();
        }

        public int Next(int maxValue)
        {
            throw new NotImplementedException();
        }

        public int Next(int minValue, int maxValue)
        {
            throw new NotImplementedException();
        }

        public void Restore(RandomState state)
        {
            throw new NotImplementedException();
        }

        public RandomState Save()
        {
            throw new NotImplementedException();
        }
    }
}
