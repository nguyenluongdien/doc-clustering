namespace AppUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbxAlg = new System.Windows.Forms.ComboBox();
            this.btnExe = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbError = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNumClusters = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.statInfo = new System.Windows.Forms.RichTextBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.fbdDataFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.taskName = new System.Windows.Forms.Label();
            this.lbElapsed = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxAlg
            // 
            this.cbxAlg.FormattingEnabled = true;
            this.cbxAlg.Items.AddRange(new object[] {
            "CLARA",
            "OPTICS"});
            this.cbxAlg.Location = new System.Drawing.Point(6, 50);
            this.cbxAlg.Name = "cbxAlg";
            this.cbxAlg.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbxAlg.Size = new System.Drawing.Size(165, 21);
            this.cbxAlg.TabIndex = 0;
            this.cbxAlg.SelectedIndex = 0;
            // 
            // btnExe
            // 
            this.btnExe.Location = new System.Drawing.Point(12, 204);
            this.btnExe.Name = "btnExe";
            this.btnExe.Size = new System.Drawing.Size(187, 23);
            this.btnExe.TabIndex = 1;
            this.btnExe.Text = "Execute";
            this.btnExe.UseVisualStyleBackColor = true;
            this.btnExe.Click += new System.EventHandler(this.btnExe_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbError);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lbElapsed);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(205, 154);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 73);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Result";
            // 
            // lbError
            // 
            this.lbError.AutoSize = true;
            this.lbError.Location = new System.Drawing.Point(58, 50);
            this.lbError.Name = "lbError";
            this.lbError.Size = new System.Drawing.Size(0, 13);
            this.lbError.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Error rate:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Elapsed time:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtNumClusters);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbxAlg);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(187, 136);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Input";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Algorithms:";
            // 
            // txtNumClusters
            // 
            this.txtNumClusters.Location = new System.Drawing.Point(6, 108);
            this.txtNumClusters.Name = "txtNumClusters";
            this.txtNumClusters.Size = new System.Drawing.Size(165, 20);
            this.txtNumClusters.TabIndex = 4;
            this.txtNumClusters.Text = "20";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Number of keywords:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.statInfo);
            this.groupBox3.Location = new System.Drawing.Point(205, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(270, 136);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Statistics";
            // 
            // statInfo
            // 
            this.statInfo.Location = new System.Drawing.Point(6, 19);
            this.statInfo.Name = "statInfo";
            this.statInfo.ReadOnly = true;
            this.statInfo.Size = new System.Drawing.Size(258, 111);
            this.statInfo.TabIndex = 0;
            this.statInfo.Text = "";
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(12, 169);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(187, 23);
            this.btnSelectFolder.TabIndex = 5;
            this.btnSelectFolder.Text = "Select folder";
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // fbdDataFolder
            // 
            this.fbdDataFolder.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // taskName
            // 
            this.taskName.AutoSize = true;
            this.taskName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taskName.Location = new System.Drawing.Point(9, 235);
            this.taskName.Name = "taskName";
            this.taskName.Size = new System.Drawing.Size(70, 16);
            this.taskName.TabIndex = 6;
            this.taskName.Text = "taskName";
            // 
            // lbElapsed
            // 
            this.lbElapsed.AutoSize = true;
            this.lbElapsed.Location = new System.Drawing.Point(71, 25);
            this.lbElapsed.Name = "lbElapsed";
            this.lbElapsed.Size = new System.Drawing.Size(0, 13);
            this.lbElapsed.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 258);
            this.Controls.Add(this.taskName);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnExe);
            this.Name = "Form1";
            this.Text = "Text Clustering";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxAlg;
        private System.Windows.Forms.Button btnExe;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNumClusters;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.FolderBrowserDialog fbdDataFolder;
        private System.Windows.Forms.Label taskName;
        private System.Windows.Forms.RichTextBox statInfo;
        private System.Windows.Forms.Label lbError;
        private System.Windows.Forms.Label lbElapsed;
    }
}

