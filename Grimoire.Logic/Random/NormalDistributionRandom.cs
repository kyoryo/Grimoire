using System;
using Grimoire.Logic.Interfaces;
using RandomState = Grimoire.Logic.Interfaces.RandomState;

namespace Grimoire.Logic.Random
{
    public class NormalDistributionRandom : IRandomNumber
    {
        private int _seed;
        private System.Random _random;
        private long _numberGenerated;
        private double _nextRandom;
        private bool _uselast = true;

        public NormalDistributionRandom():this(Environment.TickCount)
        {
           
        }

        public NormalDistributionRandom(int seed)
        {
            _seed = seed;
            _random = new System.Random(_seed);
        }

        public int Next(int maxValue)
        {
            return Next(0, maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            _numberGenerated++;
            double deviations = 3.5;
            var r = (int) Calculate(minValue + (maxValue - minValue)/2.0, (maxValue - minValue)/2.0/deviations);
            if (r > maxValue)
            {
                r = maxValue;
            }
            else if (r<minValue)
            {
                r = minValue;
            }
            return r;
        }

        

        public RandomState Save()
        {
            var result = new RandomState
            {
                NumberGenerated = _numberGenerated,
                Seed = new[] {_seed}
            };
            return result;
        }

        public void Restore(RandomState state)
        {
            if (state == null)
            {
                throw new ArgumentException("state","RandomState cannot be null");
            }
            _seed = state.Seed[0];
            _random =new System.Random(_seed);
            _numberGenerated = default(long);
            _nextRandom = default(double);
            _uselast = true;
            for (long i = 0; i < state.NumberGenerated; i++)
            {
                Next(1);
            }
        }

        private double Calculate()
        {
            if (_uselast)
            {
                _uselast = false;
                return _nextRandom;
            }
            else
            {
                double x1, x2, s;
                do
                {
                    x1 = 2.0*_random.NextDouble() - 1.0;
                    x2 = 2.0*_random.NextDouble() - 1.0;
                    s = x1*x1 + x2*x2;
                } while (s>=1.0||s==0);

                s = Math.Sqrt((-2.0*Math.Log(s))/s);

                _nextRandom = x2*s;
                _uselast = true;
                return x1*s;
            }
        }
        private double Calculate(double mean, double standardDeviation)
        {
            return mean + Calculate()*standardDeviation;
        }
    }
}
