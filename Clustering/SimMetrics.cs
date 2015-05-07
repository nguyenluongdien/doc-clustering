using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clustering
{
    public class SimMetrics
    {
        public static float cosSim(float[] va, float[] vb)
        {
            // Calculate dot product of two vectors
            float result = DotProduct(va, vb) / (Magnitude(va) * Magnitude(vb));

            if (float.IsNaN(result))
                return 0;
            return result;
        }

        public static float DotProduct(float[] va, float[] vb)
        {
            float result = 0;
            for (int i = 0; i < va.Length; ++i)
                result += va[i] * vb[i];

            return result;
        }

        public static float Magnitude(float[] v)
        {
            return (float)Math.Sqrt(DotProduct(v, v));
        }
    }
}
