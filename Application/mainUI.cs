using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Domain;
using Clustering;

namespace AppUI
{
    public partial class Form1 : Form
    {
        private string selectedFolder;
        private static int N = 0;
        private static int M = 0;
        private static string keywords;
        private static int numError;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            if (fbdDataFolder.ShowDialog() == DialogResult.OK)
            {
                selectedFolder = fbdDataFolder.SelectedPath;                
            }
        }

        private void btnExe_Click(object sender, EventArgs e)
        {                        
            // Load features         
            List<Item> data = loadFeature();

            /* Show statistics information */
            statInfo.Text = "No of documents: " + N + "\n"
                            + "No of keywords: " + M + "\n"
                            + "List of keywords: " + keywords + "\n";            

            // Clustering            
            taskName.Text = "Clustering";
            Stopwatch stopwatch = Stopwatch.StartNew();
            /* Switch case for clustering algorithms */
            switch (cbxAlg.SelectedIndex)
            {
                case 0:
                    CLARA.Clara(ref data, 5, 40 + 2 * 5);
                    break;
                default:
                    //Clustering. OPTICS
                    OPTICS optics = new OPTICS(ref data, 0.05f, 90);
                    break;
            }
            stopwatch.Stop();

            storeResult(data);
            // Clear progress info            
            taskName.Text = "Clustering complete.";
            MessageBox.Show("Your collection is clustered.");

            // Show result information
            lbElapsed.Text = Convert.ToString(stopwatch.ElapsedMilliseconds) + " ms";
            lbError.Text = Convert.ToString(Math.Round(numError * 1.0 / N * 100, 2)) + " %";
        }

        // Load feature
        private List<Item> loadFeature(string featFile = "articles.feat")
        {
            List<Item> dataSet = new List<Item>();

            dataSet.Clear();
            using (StreamReader reader = File.OpenText(featFile))
            {
                N = int.Parse(reader.ReadLine()); // Read number of items
                M = int.Parse(reader.ReadLine()); // Read number of keywords

                keywords = reader.ReadLine(); // Read keywords
                            
                for (int i = 0; i < N; ++i)
                {
                    Item tmp = new Item(M);    
                    string[] values = reader.ReadLine().Split(' ');
                    for (int j = 0; j < M; ++j)
                        tmp.Vector.Tf_idf[j] = float.Parse(values[j]);
                    tmp.Label = i / 100;

                    dataSet.Add(tmp);
                }
            }

            return dataSet;
        }        

        // Store result
        private void storeResult(List<Item> data, string resultFile = "clustering.txt")
        {
            numError = 0;
            using (StreamWriter writer = new StreamWriter(resultFile, false))
            {
                for (int i = 0; i < data.Count; ++i)
                {
                    if (data[i].Label != data[i].TmpLabel)
                        ++numError;
                    writer.WriteLine(data[i].TmpLabel + 1);
                }
            }            
        }

        private void btnPrep_Click(object sender, EventArgs e)
        {
            M = Convert.ToInt32(txtNumClusters.Text);
            // Preprocessing
            taskName.Text = "Preprocessing...";
            Preprocessing.VectorSpaceModel.extractFeatures(selectedFolder, M);
            taskName.Text = "Preprocessing complete.";
            MessageBox.Show("Preprocessing complete.");
        }
    }
}
