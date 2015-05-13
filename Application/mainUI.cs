using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AppUI
{
    public partial class Form1 : Form
    {
        private string selectedFolder;        

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
            taskName.Text = "Preprocessing";
            Preprocessing.VectorSpaceModel.extractFeatures(selectedFolder, 10);
            MessageBox.Show("Preprocessing complete.");

            

            // Open file articles.feat
            List<List<double>> matrixObj = new List<List<double>>();
            matrixObj = openFile();




            /* Show statistics information */
            // Clustering            
            taskName.Text = "Clustering";

            /* Switch case for clustering algorithms */
            switch (cbxAlg.SelectedIndex)
            {
                case 0:
                    //Clustering.CLARA.
                    break;
                default:
                    //Clustering. OPTICS
                    break;
            }
            
            // Clear progress info
            taskName.Text = "";
            MessageBox.Show("Your collection is clustered.");
        }

        private List<List<double>> openFile()
        {
            string fileName = "articles.feat";
            List<List<double>> matrixObj = new List<List<double>>();

            using (StreamReader file = new StreamReader(fileName))
            { 
                string line = file.ReadLine();
                int row = int.Parse(line);
                line = file.ReadLine();
                int col = int.Parse(line);

                line = file.ReadLine();

                for(int i = 0; i < row; i++)
                {
                    List<double> lstOnRow = new List<double>();
                    line = file.ReadLine();
                    string[] words = line.Split(' ');
                    for (int j = 0; j < col; j++)
                    {
                        double dtemp = double.Parse(words[i]);
                        lstOnRow.Add(dtemp);
                    }

                    matrixObj.Add(lstOnRow);
                }

            }
            return matrixObj;
        }
    }
}
