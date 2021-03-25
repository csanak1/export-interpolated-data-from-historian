
namespace ExportHistorianTagDataToCSV
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnServerConn = new System.Windows.Forms.Button();
            this.lblServerName = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblFilteredTagCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboTagList = new System.Windows.Forms.ComboBox();
            this.btnFilterTag = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTagFilterInput = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numSampleNr = new System.Windows.Forms.NumericUpDown();
            this.numIntervalSampleNr = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.comboSamplingTimeUnit = new System.Windows.Forms.ComboBox();
            this.rbSamplingType2 = new System.Windows.Forms.RadioButton();
            this.rbSamplingType1 = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtEndDateTime = new System.Windows.Forms.DateTimePicker();
            this.dtStartDateTime = new System.Windows.Forms.DateTimePicker();
            this.btnExport = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSampleNr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIntervalSampleNr)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnServerConn);
            this.groupBox1.Controls.Add(this.lblServerName);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 44);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Historian Server";
            // 
            // btnServerConn
            // 
            this.btnServerConn.Location = new System.Drawing.Point(221, 15);
            this.btnServerConn.Name = "btnServerConn";
            this.btnServerConn.Size = new System.Drawing.Size(123, 23);
            this.btnServerConn.TabIndex = 1;
            this.btnServerConn.Text = "Connect to server";
            this.btnServerConn.UseVisualStyleBackColor = true;
            this.btnServerConn.Click += new System.EventHandler(this.btnServerConn_Click);
            // 
            // lblServerName
            // 
            this.lblServerName.AutoSize = true;
            this.lblServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblServerName.ForeColor = System.Drawing.Color.Red;
            this.lblServerName.Location = new System.Drawing.Point(7, 20);
            this.lblServerName.Name = "lblServerName";
            this.lblServerName.Size = new System.Drawing.Size(134, 13);
            this.lblServerName.TabIndex = 0;
            this.lblServerName.Text = "Historian Server Name";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblFilteredTagCount);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.comboTagList);
            this.groupBox2.Controls.Add(this.btnFilterTag);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tbTagFilterInput);
            this.groupBox2.Location = new System.Drawing.Point(13, 64);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(350, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tag Filter Options";
            // 
            // lblFilteredTagCount
            // 
            this.lblFilteredTagCount.AutoSize = true;
            this.lblFilteredTagCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblFilteredTagCount.Location = new System.Drawing.Point(74, 81);
            this.lblFilteredTagCount.Name = "lblFilteredTagCount";
            this.lblFilteredTagCount.Size = new System.Drawing.Size(0, 9);
            this.lblFilteredTagCount.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Taglist:";
            // 
            // comboTagList
            // 
            this.comboTagList.FormattingEnabled = true;
            this.comboTagList.Location = new System.Drawing.Point(74, 55);
            this.comboTagList.Name = "comboTagList";
            this.comboTagList.Size = new System.Drawing.Size(254, 21);
            this.comboTagList.TabIndex = 3;
            // 
            // btnFilterTag
            // 
            this.btnFilterTag.Location = new System.Drawing.Point(250, 18);
            this.btnFilterTag.Name = "btnFilterTag";
            this.btnFilterTag.Size = new System.Drawing.Size(75, 23);
            this.btnFilterTag.TabIndex = 2;
            this.btnFilterTag.Text = "Filter";
            this.btnFilterTag.UseVisualStyleBackColor = true;
            this.btnFilterTag.Click += new System.EventHandler(this.btnFilterTag_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tag Filter:";
            // 
            // tbTagFilterInput
            // 
            this.tbTagFilterInput.Location = new System.Drawing.Point(74, 20);
            this.tbTagFilterInput.Name = "tbTagFilterInput";
            this.tbTagFilterInput.Size = new System.Drawing.Size(147, 20);
            this.tbTagFilterInput.TabIndex = 0;
            this.tbTagFilterInput.Text = "BC.**";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.numSampleNr);
            this.groupBox3.Controls.Add(this.numIntervalSampleNr);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.comboSamplingTimeUnit);
            this.groupBox3.Controls.Add(this.rbSamplingType2);
            this.groupBox3.Controls.Add(this.rbSamplingType1);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.dtEndDateTime);
            this.groupBox3.Controls.Add(this.dtStartDateTime);
            this.groupBox3.Location = new System.Drawing.Point(13, 171);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(350, 192);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Query Options";
            // 
            // numSampleNr
            // 
            this.numSampleNr.Location = new System.Drawing.Point(242, 153);
            this.numSampleNr.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numSampleNr.Name = "numSampleNr";
            this.numSampleNr.Size = new System.Drawing.Size(86, 20);
            this.numSampleNr.TabIndex = 14;
            // 
            // numIntervalSampleNr
            // 
            this.numIntervalSampleNr.Location = new System.Drawing.Point(128, 123);
            this.numIntervalSampleNr.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numIntervalSampleNr.Name = "numIntervalSampleNr";
            this.numIntervalSampleNr.Size = new System.Drawing.Size(91, 20);
            this.numIntervalSampleNr.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(128, 154);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 13);
            this.label11.TabIndex = 12;
            this.label11.Text = "Number of samples:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.Location = new System.Drawing.Point(225, 124);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(13, 17);
            this.label10.TabIndex = 11;
            this.label10.Text = "/";
            // 
            // comboSamplingTimeUnit
            // 
            this.comboSamplingTimeUnit.DisplayMember = "Minutes";
            this.comboSamplingTimeUnit.FormattingEnabled = true;
            this.comboSamplingTimeUnit.Items.AddRange(new object[] {
            "Minutes",
            "Hours"});
            this.comboSamplingTimeUnit.Location = new System.Drawing.Point(242, 123);
            this.comboSamplingTimeUnit.Name = "comboSamplingTimeUnit";
            this.comboSamplingTimeUnit.Size = new System.Drawing.Size(86, 21);
            this.comboSamplingTimeUnit.TabIndex = 10;
            this.comboSamplingTimeUnit.SelectedIndexChanged += new System.EventHandler(this.comboSamplingTimeUnit_SelectedIndexChanged);
            // 
            // rbSamplingType2
            // 
            this.rbSamplingType2.AutoSize = true;
            this.rbSamplingType2.Location = new System.Drawing.Point(23, 151);
            this.rbSamplingType2.Name = "rbSamplingType2";
            this.rbSamplingType2.Size = new System.Drawing.Size(74, 17);
            this.rbSamplingType2.TabIndex = 8;
            this.rbSamplingType2.TabStop = true;
            this.rbSamplingType2.Text = "by Sample";
            this.rbSamplingType2.UseVisualStyleBackColor = true;
            this.rbSamplingType2.CheckedChanged += new System.EventHandler(this.rbSamplingType2_CheckedChanged);
            // 
            // rbSamplingType1
            // 
            this.rbSamplingType1.AutoSize = true;
            this.rbSamplingType1.Location = new System.Drawing.Point(23, 124);
            this.rbSamplingType1.Name = "rbSamplingType1";
            this.rbSamplingType1.Size = new System.Drawing.Size(74, 17);
            this.rbSamplingType1.TabIndex = 7;
            this.rbSamplingType1.TabStop = true;
            this.rbSamplingType1.Text = "by Interval";
            this.rbSamplingType1.UseVisualStyleBackColor = true;
            this.rbSamplingType1.CheckedChanged += new System.EventHandler(this.rbSamplingType1_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 105);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Sampling:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "End Time:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Start Time:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(128, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 15);
            this.label6.TabIndex = 3;
            this.label6.Text = "Interpolated";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Query Type:";
            // 
            // dtEndDateTime
            // 
            this.dtEndDateTime.CustomFormat = "yyyy.MM.dd. HH:m:ss";
            this.dtEndDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEndDateTime.Location = new System.Drawing.Point(128, 70);
            this.dtEndDateTime.MaxDate = new System.DateTime(2022, 9, 1, 0, 0, 0, 0);
            this.dtEndDateTime.Name = "dtEndDateTime";
            this.dtEndDateTime.Size = new System.Drawing.Size(200, 20);
            this.dtEndDateTime.TabIndex = 1;
            this.dtEndDateTime.Value = new System.DateTime(2021, 3, 25, 0, 0, 0, 0);
            // 
            // dtStartDateTime
            // 
            this.dtStartDateTime.CustomFormat = "yyyy.MM.dd. HH:m:ss";
            this.dtStartDateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStartDateTime.Location = new System.Drawing.Point(128, 43);
            this.dtStartDateTime.MaxDate = new System.DateTime(2022, 9, 1, 0, 0, 0, 0);
            this.dtStartDateTime.Name = "dtStartDateTime";
            this.dtStartDateTime.Size = new System.Drawing.Size(200, 20);
            this.dtStartDateTime.TabIndex = 0;
            this.dtStartDateTime.Value = new System.DateTime(2021, 3, 25, 0, 0, 0, 0);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(13, 386);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(350, 37);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Export .csv";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 436);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.Text = "Historian Tag data export to CSV";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSampleNr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIntervalSampleNr)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnServerConn;
        private System.Windows.Forms.Label lblServerName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboTagList;
        private System.Windows.Forms.Button btnFilterTag;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbTagFilterInput;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboSamplingTimeUnit;
        private System.Windows.Forms.RadioButton rbSamplingType2;
        private System.Windows.Forms.RadioButton rbSamplingType1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtEndDateTime;
        private System.Windows.Forms.DateTimePicker dtStartDateTime;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblFilteredTagCount;
        private System.Windows.Forms.NumericUpDown numSampleNr;
        private System.Windows.Forms.NumericUpDown numIntervalSampleNr;
    }
}

