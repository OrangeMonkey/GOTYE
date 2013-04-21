using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace GOTYE
{
    class Stage
    {
        public static Stage GenerateStage(int stagenumber)
        {
            var stage = new Stage();

            for (int i = 0; i < stagenumber + 1; ++i)
            {
                stage.AddObstacle<Roid>(Program.Rand.NextDouble());
            }

            return stage;
        }

        List<KeyValuePair<Type, double>> spawntimes;

        private Stage()
        {
            spawntimes = new List<KeyValuePair<Type, double>>(); 
        }

        private void AddObstacle<T>(double time)
            where T : SpaceJunk
        {
            if (spawntimes.Count == 0)
            {
                spawntimes.Add(new KeyValuePair<Type, double>(typeof(T), time));
            }

            else
            {
                spawntimes.Add(new KeyValuePair<Type, double>(typeof(T), time + spawntimes.Last().Value));
            }
        }

        public SpaceJunk ChooseNextObstacle(double time, float x, float miny, float maxy)
        {
            if (spawntimes.Count == 0)
            {
                return null;
            }

            if (spawntimes.First().Value < time)
            {
                var pair = spawntimes.First();
                spawntimes.Remove(pair);
                var c = pair.Key.GetConstructor(new Type[] { typeof(float), typeof(float), typeof(float) });
                if (c == null)
                {
                    return null;
                }

                return (SpaceJunk) c.Invoke(new object[] { x, miny, maxy });
            }

            return null;

        }

        public bool HasEnded()
        {
            return spawntimes.Count == 0;
        }

    }
}
