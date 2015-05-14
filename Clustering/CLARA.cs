using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;

namespace Clustering
{
    public class CLARA
    {
        public static List<Item> Clara(ref List<Item> data, int k, int m, int q = 5)
        {
            float maxSim = float.MinValue;
            List<Item> bestMedoids = new List<Item>();
            int i = 0;

            while (i < q)
            {
                List<Item> sample = new List<Item>();
                Utility.randomSelection(data, ref sample, m);
                List<Item> medoids = PAM.Pam(ref sample, k);

                float sim = Utility.quality(data, medoids);
                if (sim > maxSim)
                {
                    maxSim = sim;
                    bestMedoids = medoids;
                }

                ++i;
            }

            return bestMedoids;
        }
    }
}
