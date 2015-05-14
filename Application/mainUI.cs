using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
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

        public Form1()
        {
            InitializeComponent();
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Change to value of progress bar
            progressBar.Value = e.ProgressPercentage;
            // Update the text
            completedPert.Text = e.ProgressPercentage.ToString();
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
            // Preprocessing
            taskName.Text = "Preprocessing...";
            Preprocessing.VectorSpaceModel.extractFeatures(selectedFolder, 5);
            taskName.Text = "Preprocessing complete.";
            MessageBox.Show("Preprocessing complete.");
            
            // Load features         
            List<Item> data = loadFeature();

            /* Show statistics information */
            statInfo.Text = "No of documents: " + N + "\n"
                            + "No of keywords: " + M + "\n"
                            + "List of keywords: " + keywords + "\n";            

            // Clustering            
            taskName.Text = "Clustering";
            /* Switch case for clustering algorithms */
            switch (cbxAlg.SelectedIndex)
            {
                case 0:
                    CLARA.Clara(ref data, 5, 40 + 2 * 5);
                    break;
                default:
                    //Clustering. OPTICS
                    break;
            }

            storeResult(data);
            // Clear progress info            
            MessageBox.Show("Your collection is clustered.");
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

                Item tmp = new Item(M);                
                for (int i = 0; i < N; ++i)
                {
                    string[] values = reader.ReadLine().Split(' ');
                    for (int j = 0; j < M; ++j)
                        tmp.Vector.Tf_idf[j] = float.Parse(values[j]);

                    dataSet.Add(tmp);
                }
            }

            return dataSet;
        }        

        // Store result
        private void storeResult(List<Item> data, string resultFile = "clustering.txt")
        {
            using (StreamWriter writer = new StreamWriter(resultFile, false))
            {
                for (int i = 0; i < data.Count; ++i)
                    writer.WriteLine(data[i].Label);
            }
        }
    }
}
