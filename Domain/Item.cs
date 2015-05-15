using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Item
    {
        private DocVector vector; // Feature of item
        public DocVector Vector
        {
            get { return vector; }
            set { vector = value; }
        }

        private int label; // Real label of item
        public int Label
        {
            get { return label; }
            set { label = value; }
        }

        private int tmpLabel; // Temporary label
        public int TmpLabel
        {
            get { return tmpLabel; }
            set { tmpLabel = value; }
        }

        public Item(int M)
        {
            vector = new DocVector(M);
            label = -1;
            tmpLabel = -1;
        }
    }
}
   
