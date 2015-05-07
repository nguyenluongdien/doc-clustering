using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class DocVector
    {
        private float[] tf_idf; // vector of tf-idf coefficents

        public DocVector(int M)
        {
            tf_idf = new float[M];
        }

        public float[] Tf_idf
        {
            get { return tf_idf; }
            set { tf_idf = value; }
        }


    }
}
