using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace Clustering
{
    public class Utility
    {
        // Randomly select k items from data set
        public static HashSet<int> randomSelection(List<Item> data, ref List<Item> randomSet, int k)
        {
            randomSet.Clear();
            HashSet<int> indices = new HashSet<int>();
            Random r = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

            do
            {
                int pos = r.Next(0, data.Count);
                indices.Add(pos);
            } while (indices.Count < k);

            foreach (int pos in indices)
            {
                randomSet.Add(data[pos]);
            }

            return indices;
        }        

        // Cluster the data set base on medoid set
        public static void doClustering(List<Item> medoids, ref List<Item> data)
        {
            for (int i = 0; i < data.Count; ++i)
            {
                float maxSim = -1;
                for (int j = 0; j < medoids.Count; ++j)
                {
                    float sim = SimMetrics.cosSim(data[i].Vector.Tf_idf, medoids[j].Vector.Tf_idf);
                    if (sim > maxSim)
                    {
                        maxSim = sim;
                        data[i].TmpLabel = j;
                    }
                }
            }
        }

        // Evaluate the quality of clustering
        public static float quality(List<Item> data, List<Item> medoids)
        {
            float result = 0;
            for (int i = 0; i < data.Count; ++i)
                result += SimMetrics.cosSim(data[i].Vector.Tf_idf, medoids[data[i].TmpLabel].Vector.Tf_idf);

            return result;
        }
    }
}
