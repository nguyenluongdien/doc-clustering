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

        private int label; // Label of item
        public int Label
        {
            get { return label; }
            set { label = value; }
        }        

        public Item(int M)
        {
            vector = new DocVector(M);
            label = -1;            
        }
    }
}
   
