using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExportHistorianTagDataToCSV
{
    public partial class Main : Form
    {
        private HistorianDA histDA = new HistorianDA();

        public Main()
        {
            InitializeComponent();

            InitializeForm();
        }

        private void InitializeForm()
        {
            lblServerName.Text = histDA.IHistSrv;
            rbSamplingType1.Checked = true;
            dtStartDateTime.MaxDate = DateTime.Now;
            dtEndDateTime.MaxDate = DateTime.Now;
            comboSamplingTimeUnit.SelectedIndex = 0;
        }

        private void btnServerConn_Click(object sender, EventArgs e)
        {
            if (!histDA.IsConnected)
            {
                histDA.Connect();

                lblServerName.ForeColor = Color.Green;
                btnServerConn.Text = "Disconnect";
            }
            else
            {
                lblServerName.ForeColor = Color.Red;
                btnServerConn.Text = "Connect";

                histDA.Disconnect();
            }
        }

        private void btnFilterTag_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            if (histDA.IsConnected)
            {
                string filterText = tbTagFilterInput.Text;

                if (!string.IsNullOrWhiteSpace(filterText))
                {
                    var tagList = histDA.QueryFilteredTags(filterText);

                    if (tagList != null && tagList.Count != 0)
                    {
                        lblFilteredTagCount.Text = tagList.Count + " tag have been found";
                        comboTagList.DataSource = tagList.ToList();
                    }
                    else
                    {
                        MessageBox.Show("No tags have been found!");
                    }
                }
                else
                {
                    MessageBox.Show("The tag filter textbox is empty! Use the mask BC.* to query all the tags.");
                }
            }
            else
            {
                MessageBox.Show("First, establish the connection with the server.");
            }

            Cursor = Cursors.Default;
        }

        private void rbSamplingType1_CheckedChanged(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;

            if (rb.Checked)
            {
                numIntervalSampleNr.Enabled = true;
                comboSamplingTimeUnit.Enabled = true;
                numSampleNr.Value = 0;
                numSampleNr.Enabled = false;
            }
        }

        private void rbSamplingType2_CheckedChanged(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;

            if (rb.Checked)
            {
                numIntervalSampleNr.Value = 0;
                numIntervalSampleNr.Enabled = false;
                comboSamplingTimeUnit.Enabled = false;
                numSampleNr.Enabled = true;
            }
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;

            btn.Enabled = false;
            btn.Text = "Please wait...";

            try
            {
                DateTime queryStartTime, queryEndTime;
                int numOfSamples = 0;
                string tagName;

                queryStartTime = dtStartDateTime.Value;
                queryEndTime = dtEndDateTime.Value;
                tagName = (string)comboTagList.SelectedValue;

                if (!string.IsNullOrEmpty(tagName))
                {
                    if (queryStartTime < queryEndTime)
                    {
                        if (rbSamplingType1.Checked)
                        {
                            string samplingTimeUnit = comboSamplingTimeUnit.Text;

                            if (!samplingTimeUnit.In("Minutes", "Hours")) return;

                            numOfSamples = (int)numIntervalSampleNr.Value;

                            if (numOfSamples == 0)
                            {
                                MessageBox.Show("Please specify the number of the samples!");

                                return;
                            }

                            if (samplingTimeUnit == "Minutes")
                            {
                                numOfSamples = (int)(numOfSamples * (queryEndTime - queryStartTime).TotalMinutes);
                            }

                            if (samplingTimeUnit == "Hours")
                            {
                                numOfSamples = (int)(numOfSamples * (queryEndTime - queryStartTime).TotalHours);
                            }
                        }

                        if (rbSamplingType2.Checked)
                        {
                            numOfSamples = (int)numSampleNr.Value;

                            if (numOfSamples == 0)
                            {
                                MessageBox.Show("Please specify the number of the samples!");

                                return;
                            }
                        }

                        var tagData = await InterpolatedDataQueryAsync(queryStartTime, queryEndTime, numOfSamples, tagName);

                        ConvertAndSaveToCSV(tagData);
                    }
                    else
                    {
                        MessageBox.Show("Please specify the query start and end dates correctly!");

                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please specify a tag!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error during processing data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btn.Enabled = true;
                btn.Text = "Export .csv";
            }
        }

        private void comboSamplingTimeUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            var combo = sender as ComboBox;

            if(combo.Text == "Minutes")
            {
                numIntervalSampleNr.Maximum = 240;
            }
            else
            {
                numIntervalSampleNr.Maximum = 14400;
            }
        }

        private async Task<DataTable> InterpolatedDataQueryAsync(DateTime st, DateTime et, int samples, string tagName)
        {
            try
            {
                return await Task.Run(() => histDA.QueryTagInterpolatedValues(st, et, samples, tagName));
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error happened during tag data query: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return null;
            }
        }

        private void ConvertAndSaveToCSV(DataTable tagData)
        {
            const string header = "Tagname;Value;Quality;Timestamp";
            const string filter = "CSV file (*.csv)|*.csv| All Files (*.*)|*.*";

            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = filter
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;

                using (StreamWriter swr =
                        new StreamWriter(File.Open(filename, FileMode.CreateNew), Encoding.Default, 1000000))
                {
                    swr.WriteLine(header);

                    foreach (DataRow dr in tagData.Rows)
                    {
                        swr.WriteLine(string.Join(";", dr.ItemArray));
                    }

                    swr.Close();
                }

                MessageBox.Show("File sucessfully saved.", "File saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
