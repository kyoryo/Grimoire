using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grimoire.Logic.Generator
{
    public class SeedGenerator
    {
        //( seed* 16807 ) % 2147483647 
        // (seed* 16807) % 2147483647) / 0x7FFFFFFF + 0.000000000233;
        private static readonly int multiplier = 16807;
        private static readonly int modulus = 2147483647;

        protected long initialSeed;
        protected long currentSeed;

        protected int useCount = 0;
        public static bool reportUsecountFlag = false;

        #region ctor
        public SeedGenerator(uint seed = 1)
        {
            currentSeed = initialSeed = seed;
        }

        public SeedGenerator(int seed = 1)
        {
            currentSeed = initialSeed = seed;
        }
        #endregion
        /// <summary>
        /// test
        /// </summary>
        /// <param name="seed"></param>
        public void setSeed(uint seed)
        {
            if (reportUsecountFlag)
            {
                ReportUseCount();
            }
            currentSeed = initialSeed = seed;
            useCount = 0;
        }

        /// <summary>
        /// test
        /// </summary>
        public void ReportUseCount()
        {
            Console.WriteLine("Random has been used " + useCount + " times since last set seed.");
        }


        /// <summary>
        /// limit the exclusive upper limit to result ( positive is a must)
        /// </summary>
        /// <returns>
        /// Returns an integer 0 <= n < int.maxValue
        /// or 0 <= n  < limit if specified
        /// </returns>
        public int Next()
        {
            useCount++;
            currentSeed = (currentSeed * multiplier) % modulus;
            return (int)currentSeed;
        }

        /// <summary>
        /// limit the exclusive upper limit to the result ( must be positive )
        /// </summary>
        /// <returns>
        /// 0 <= n< 1,
        /// or 0 <= n<limit if specified
        /// </returns>
        public float NextFloat()
        {
            return (float)Next() / (int.MaxValue - 1);
        }

        //Unity.Engine getter
        public float value { get { return NextFloat(); } private set { } }

        public int Range(int min, int max)
        {
            // min is less than max
            if (min > max)
            {
                int tmp = max;
                max = min;
                min = tmp;
            }

            if (min == max)
            {
                return min;
            }

            return (Next() % (max - min)) + min;
        }

        public float Range(float min, float max)
        {
            // min is less than max
            if (min > max)
            {
                float tmp = max;
                max = min;
                min = tmp;
            }
            return (NextFloat() * (max - min)) + min;
        }

        /// <summary>
        /// For assigning a random sign to a value
        /// </summary>
        /// <returns>
        /// either -1 or 1
        /// </returns>
        public int sign()
        {
            if (NextFloat() < 0.5)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

    }
}
