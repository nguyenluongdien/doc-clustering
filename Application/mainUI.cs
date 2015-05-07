using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
    }
}
