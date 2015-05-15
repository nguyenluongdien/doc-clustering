using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Domain;
using System.Collections;
using System.IO;

namespace Preprocessing
{
    public class VectorSpaceModel
    {
        private static Stemmer stemmer = new Stemmer();
        private static List<string> keywords = new List<string>();
        //private static Regex r = new Regex("([ \\t{}()\",:;. \n])");

        private static List<DocVector> collectionProcessing(DocCollection collection, int M)
        {
            Dictionary<string, int> globalTerms = new Dictionary<string, int>();

            /*
             * Remove stopwords
             * Stemming
             * Chose M keywords
             * Compute tf-idf
             */

            // Chose global keywords base on document frequency value
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
           
            float[] space = new float[M];
            foreach (string document in collection.RawDocs)
            {
                // Calculate tf-idf
                int topRange = M * 5;
                HashSet<int> indices = new HashSet<int>();
                Random r = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

                do
                {
                    int pos = r.Next(0, topRange);
                    indices.Add(pos);
                } while (indices.Count < M);

                DocVector _docVector = new DocVector(M);
                int i = 0;
                foreach (int pos in indices)
                {
                    KeyValuePair<string, int> pair = keywords_df.ElementAt(pos);
                    _docVector.Tf_idf[i] = (float)(Math.Log(collection.RawDocs.Count * 1.0 / pair.Value) * calc_tf(document, pair.Key));
                    ++i;                    
                }

                //DocVector _docVector = new DocVector(M);
                //int i = 0;
                //foreach (KeyValuePair<string, int> pair in keywords_df)
                //{
                //    _docVector.Tf_idf[i] = (float)(Math.Log(collection.RawDocs.Count * 1.0 / pair.Value) * calc_tf(document, pair.Key));
                //    ++i;
                //    if (i >= M) 
                //        break;
                //}                                                
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
        public static void extractFeatures(string folderPath, int M, string output = "articles.feat")
        {
            DocCollection collection = new DocCollection();

            // Collect all files
            foreach (string filepath in Directory.GetFiles(folderPath, "*.txt"))
            {
                string content = File.ReadAllText(filepath);
                collection.collect(content);
            }

            List<DocVector> docVect = collectionProcessing(collection, 20);

            // Store features
            using (StreamWriter writer = new StreamWriter(output, false))
            {
                writer.WriteLine(Directory.GetFiles(folderPath, "*.txt").Count()); // Number of files
                writer.WriteLine(M); // Number of keywords

                // Store bag of words
                for (int i = 0; i < M; ++i)
                    writer.Write(keywords[i] + " ");
                writer.WriteLine();

                // Store feature for each document
                foreach (DocVector vector in docVect)
                {
                    for (int i = 0; i < M; ++i)
                        writer.Write(vector.Tf_idf[i] + " ");
                    writer.WriteLine();
                }
            }
        }        
    }
}
