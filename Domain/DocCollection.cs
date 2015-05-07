using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class DocCollection
    {
        private List<string> rawDocs; // Collection of raw documents

        public DocCollection()
        {
            rawDocs = new List<string>();
        }
        
        public List<string> RawDocs
        {
            get { return rawDocs; }
            set { rawDocs = value; }
        }

        // Collect new document
        public void collect(string newDoc)
        {
            rawDocs.Add(newDoc);
        }
    }
}
