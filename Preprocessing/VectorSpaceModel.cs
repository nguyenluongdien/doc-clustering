using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Domain;
using System.Collections;

namespace Preprocessing
{
    public class VectorSpaceModel
    {                     
        private static Stemmer stemmer = new Stemmer();
        private static List<string> keywords;
        //private static Regex r = new Regex("([ \\t{}()\",:;. \n])");

        private static List<DocVector> collectionProcessing(DocCollection collection, int M)
        {
            Dictionary<string, int>  globalTerms = new Dictionary<string, int>();            
            
            /*
             * Remove stopwords
             * Stemming
             * Chose M keywords
             * Compute tf-idf
             */

            // Chose global keywords
            foreach (string documentContent in collection.RawDocs)
            {
                // Remove stopwords
                string[] words = StopwordRemoval.removeStopword(documentContent);
                // Stemming
                words = stemmer.stem_list(words);
                
                // Construct a set of distinct terms of current document
                HashSet<string> distinctTerms = new HashSet<string>();
                foreach (string word in words)
                    distinctTerms.Add(word);

                // Update represented value of document frequency for found distinct terms
                foreach (string term in distinctTerms)
                {
                    if (globalTerms.ContainsKey(term))
                        globalTerms[term]++;
                    else
                        globalTerms.Add(term, 1);
                }
            }

            // Sort globalTerms dictionary and use the top M pair as keywords
            var keywords_df = from pair in globalTerms
                           orderby Math.Abs(pair.Value - collection.RawDocs.Count / 5) ascending
                           select pair;    

            List<DocVector> docVectorSpace = new List<DocVector>();
            DocVector _docVector;
            float[] space = new float[M];
            foreach (string document in collection.RawDocs)
            {                 
                // Calculate tf-idf
                int i = 0;
                foreach (KeyValuePair<string, int> pair in keywords_df)
                {
                    space[i] = calc_tf(document, pair.Key);
                }

                _docVector = new DocVector();                
                _docVector.Tf_idf = space;
                docVectorSpace.Add(_docVector);
            }            

            // Store keywords
            keywords.Clear();
            foreach (KeyValuePair<string, int> pair in keywords_df)
                keywords.Add(pair.Key);

            // Return vector space of whole collection
            return docVectorSpace;
        }

        private static float calc_tf(string document, string term)
        {
            // Remove stopwords
            string[] words = StopwordRemoval.removeStopword(document);
            // Stemming
            words = stemmer.stem_list(words);

            int count = 0;
            for (int i = 0; i < words.Count(); ++i)
                if (words[i] == term)
                    ++count;
            
            return (float)(count * 1.0 / words.Count());
        }

        // Extract and store features
        public static void extractFeatures(string folderPath, string output = "articles.feat")
        {

        }
        // Construct list of keywords, store vector space model
    }
}
