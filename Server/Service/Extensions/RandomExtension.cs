using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Extensions
{
    public static class RandomExtension
    {
        public static T Random<T>(this IEnumerable<T> source)
        {
            return source.Random(1).Single();
        }

        public static IEnumerable<T> Random<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(x => Guid.NewGuid());
        }

        public static IEnumerable<T> RandomNoShuffle<T>(this IEnumerable<T> source, int count)
        {
            var materials = source.ToList();
            var pick = new List<T>();
            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                var index = random.Next(0, source.Count() - 1);
                pick.Add(materials[index]);
            }

            return pick;
        }

        public static int[] RandomRate<T>(this T[] rate, int count)
        {
            var doubleRate = rate.Select(x => Convert.ToDouble(x)).ToArray();
            return doubleRate.RandomRate(count);
        }

        public static int[] RandomRate(this double[] rate, int count)
        {
            var output = new int[rate.Length];
            var random = new Random((int)DateTime.UtcNow.Ticks);
            var totalRate = rate.Sum(x => x);

            for (int i = 0; i < count; i++)
            {
                var randomValue = random.NextDouble(0, totalRate);
                var curRate = .0;

                for (byte j = 0; j < rate.Length; j++)
                {
                    if (rate[j] == 0)
                        continue;

                    curRate += rate[j];

                    if (curRate >= randomValue)
                    {   //뽑힘
                        output[j] += 1;
                        break;
                    }
                }
            }

            return output;
        }

        public static bool RandomRate(this double rate, double maxValue)
        {
            var random = new Random((int)DateTime.UtcNow.Ticks);
            var randomValue = random.NextDouble(0, maxValue);

            return rate >= randomValue;
        }

        public static double NextDouble(this Random random, double minValue, double maxValue)
        {
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }
    }
}
