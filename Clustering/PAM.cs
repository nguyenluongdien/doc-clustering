using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace Clustering
{
    public class PAM
    {
        public static List<Item> Pam(ref List<Item> data, int k)
        {
            // Select k medoids randomly
            List<Item> medoids = new List<Item>();
            HashSet<int> indices = Utility.randomSelection(data, ref medoids, k);

            // Clustering
            Utility.doClustering(medoids, ref data);
            bool change = true;
            while (change)
            {
                change = false;
                float maxTS = 0; // Maximum value of total similarity
                int m = 0, p = 0;
                for (int i = 0; i < medoids.Count; ++i)
                    for (int j = 0; j < data.Count; ++j)
                    {
                        if (!indices.Contains(j)) // Check if item[j] is not a medoid
                        {
                            float TS = 0;
                            for (int u = 0; u < data.Count; ++u)
                                if (!indices.Contains(u))
                                    TS += Sjmp(data, medoids, u, i, j);

                            if (TS > maxTS)
                            {
                                maxTS = TS;
                                m = i;
                                p = j;
                            }
                        }
                    }

                if (maxTS > 0)
                {
                    change = true;
                    medoids.RemoveAt(m);
                    medoids.Add(data[p]);
                    Utility.doClustering(medoids, ref data);
                }
            }

            return medoids;
        }

        // Calculate the change of similarity
        private static float Sjmp(List<Item> data, List<Item> medoids, int j, int m, int p)
        {
            // Find Om2
            int m2 = data[j].Label;
            if (m2 == m)
            {
                float maxSim = -1;
                for (int i = 0; i < medoids.Count; ++i)
                    if (i != m)
                    {
                        float sim = SimMetrics.cosSim(data[j].Vector.Tf_idf, medoids[i].Vector.Tf_idf);
                        if (sim > maxSim)
                        {
                            maxSim = sim;
                            m2 = i;
                        }
                    }        
            }

            float simjm = SimMetrics.cosSim(data[j].Vector.Tf_idf, medoids[m].Vector.Tf_idf);
            float simjp = SimMetrics.cosSim(data[j].Vector.Tf_idf, data[p].Vector.Tf_idf);
            float simjm2 = SimMetrics.cosSim(data[j].Vector.Tf_idf, medoids[m2].Vector.Tf_idf);

            // Evaluate in 4 cases
            if (data[j].Label == m) // Item[j] belongs to cluster m
            {                
                if (simjp <= simjm2)
                    return (simjm2 - simjm); // Item[j] leaves cluster m and joins in cluster m2
                return (simjp - simjm); // Item[j] leaves cluster m and joins in cluster p
            }
            else // Item[j] belong to cluster m2
            {
                if (simjp <= simjm2)
                    return 0; // Item[j] doesn't change cluster
                return (simjp - simjm2);
            }
        }
    }
}
