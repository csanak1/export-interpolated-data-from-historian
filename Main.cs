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
        private readonly HistorianDA histDA = new HistorianDA();

        public class DateRange
        {
            public int Id { get; set; }
            public DateTime From { get; set; }
            public DateTime To { get; set; }
        }

        public List<DateRange> dateRangeList { get; set; }

        public List<DateRange> DateRangeList
        {
            get { return dateRangeList; }
            set { dateRangeList = value; }
        }

        public List<string> tagNameList { get; set; }

        public List<string> TagNameList
        {
            get { return tagNameList; }
            set { tagNameList = value; }
        }

        public class ComboBoxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        public Main()
        {
            InitializeComponent();
            InitializeForm();

            TagNameList = new List<string>();
            DateRangeList = new List<DateRange>();
        }

        private void InitializeForm()
        {
            groupFilterControls.Enabled = false;
            groupQueryOptions.Enabled = false;
            btnExport.Enabled = false;
            lblServerName.Text = histDA.IHistSrv;
            rbSamplingType1.Checked = true;
            dtStartDateTime.MaxDate = DateTime.Now.Date;
            dtEndDateTime.MaxDate = DateTime.Now.Date;
            dtStartDateTime.Value = DateTime.Now.Date.AddDays(-1);
            dtEndDateTime.Value = DateTime.Now.Date;
            comboSamplingTimeUnit.SelectedIndex = 0;
        }

        private void btnServerConn_Click(object sender, EventArgs e)
        {
            if (!histDA.IsConnected)
            {
                histDA.Connect();

                if (histDA.IsConnected)
                {
                    lblServerName.ForeColor = Color.Green;
                    btnServerConn.Text = "Disconnect";
                    groupFilterControls.Enabled = true;
                    groupQueryOptions.Enabled = true;
                    btnExport.Enabled = true;
                }
            }
            else
            {
                histDA.Disconnect();

                if (!histDA.IsConnected)
                {
                    lblServerName.ForeColor = Color.Red;
                    btnServerConn.Text = "Connect";
                    groupFilterControls.Enabled = false;
                    groupQueryOptions.Enabled = false;
                    btnExport.Enabled = false;
                }
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
                        btnAddToQueryList.Enabled = true;
                    }
                    else
                    {
                        btnAddToQueryList.Enabled = false;

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
                uint numOfSamples = 0;

                if(DateRangeList.Count > 0 && TagNameList.Count > 0)
                {
                    string samplingTimeUnit = comboSamplingTimeUnit.Text;

                    if (!samplingTimeUnit.In("Minutes", "Hours"))
                        throw new InvalidOperationException("Sampling unit is not set!");

                    int numIntervalSamples = (int)numIntervalSampleNr.Value;

                    if (numIntervalSamples != 0)
                    {
                        if (rbSamplingType1.Checked)
                        {
                            foreach (var dateInterval in DateRangeList)
                            {
                                if (samplingTimeUnit == "Minutes")
                                {
                                    numOfSamples = (uint)(numIntervalSamples * (dateInterval.To - dateInterval.From).TotalMinutes);
                                }

                                if (samplingTimeUnit == "Hours")
                                {
                                    numOfSamples = (uint)(numIntervalSamples * (dateInterval.To - dateInterval.From).TotalHours);
                                }
                            }

                            if (numOfSamples < 16777216)
                            {
                                foreach (string tagName in TagNameList)
                                {
                                    var tagData = new DataTable();

                                    foreach (var dateInterval in DateRangeList)
                                    {
                                        if (!samplingTimeUnit.In("Minutes", "Hours")) return;

                                        numOfSamples = (uint)numIntervalSampleNr.Value;

                                        if (numOfSamples == 0)
                                        {
                                            MessageBox.Show("Please specify the number of the samples!");

                                            return;
                                        }

                                        if (samplingTimeUnit == "Minutes")
                                        {
                                            numOfSamples = (uint)(numIntervalSamples * (dateInterval.To - dateInterval.From).TotalMinutes);
                                        }

                                        if (samplingTimeUnit == "Hours")
                                        {
                                            numOfSamples = (uint)(numIntervalSamples * (dateInterval.To - dateInterval.From).TotalHours);
                                        }

                                        tagData.Merge(await InterpolatedDataQueryAsync(dateInterval.From, dateInterval.To, numOfSamples, tagName));
                                    }

                                    ConvertAndSaveToCSV(tagData, tagName);
                                }
                            }
                            else
                            {
                                MessageBox.Show("The number of samles are too high, over 16 million! Change the query interval shorter or the number of smaples lower.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("The number of samles is zero!");
                    }

                    if (rbSamplingType2.Checked)
                    {
                        numOfSamples = (uint)numSampleNr.Value;

                        if (numOfSamples != 0 && numOfSamples < 16777216)
                        {
                            foreach (string tagName in TagNameList)
                            { 
                                var tagData = await InterpolatedDataQueryAsync(DateRangeList[0].From, DateRangeList[0].To, numOfSamples, tagName);

                                ConvertAndSaveToCSV(tagData, tagName);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please specify the number of the samples!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please specify at least one tag and one date interval!");
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

        private async Task<DataTable> InterpolatedDataQueryAsync(DateTime st, DateTime et, uint samples, string tagName)
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

        private void ConvertAndSaveToCSV(DataTable tagData, string tagName)
        {
            const string header = "Tagname;Value;Quality;Timestamp";
            const string filter = "CSV file (*.csv)|*.csv| All Files (*.*)|*.*";

            SaveFileDialog saveFileDialog1 = new SaveFileDialog
            {
                Filter = filter,
                FileName = tagName
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

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            histDA.Disconnect();
        }

        private void btnAddToQueryList_Click(object sender, EventArgs e)
        {
            string tagName = (string)comboTagList.SelectedValue;

            if (!string.IsNullOrEmpty(tagName))
            {
                if (!TagNameList.Contains(tagName))
                {
                    TagNameList.Add(tagName);

                    comboQueryTagList.DataSource = new BindingSource(TagNameList, null);

                    if (TagNameList.Count > 0)
                    {
                        btnRemoveFromQueryList.Enabled = true;
                    }

                    lblQueryTagCount.Text = TagNameList.Count.ToString() + " tag(s) have been added";
                }
            }
            else
            {
                MessageBox.Show("Please specify a tag!");
            }
        }

        private void btnRemoveFromQueryList_Click(object sender, EventArgs e)
        {
            if (comboQueryTagList.SelectedIndex > -1)
            {
                string tagName = (string)comboQueryTagList.SelectedValue;

                if (!string.IsNullOrEmpty(tagName))
                {
                    if (TagNameList.Contains(tagName))
                    {
                        TagNameList.Remove(tagName);

                        comboQueryTagList.DataSource = new BindingSource(TagNameList, null);

                        if (TagNameList.Count == 0)
                        {
                            comboQueryTagList.Text = string.Empty;
                            btnRemoveFromQueryList.Enabled = false;
                        }

                        lblQueryTagCount.Text = "Query tag count: " + TagNameList.Count.ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Please select a tag from the list!");
                }
            }
        }

        private void btnAddIntervalToQueryList_Click(object sender, EventArgs e)
        {
            DateTime queryStartTime, queryEndTime;

            queryStartTime = dtStartDateTime.Value;
            queryEndTime = dtEndDateTime.Value;

            if(queryStartTime < queryEndTime)
            {
                if(DateRangeList.Count == 0 || !IsThereOverlap(DateRangeList, queryStartTime, queryEndTime))
                {
                    var dateRange = new DateRange()
                    {
                        Id = DateRangeList.Count == 0 ? 1 : DateRangeList.Max(x => x.Id) + 1,
                        From = queryStartTime,
                        To = queryEndTime
                    };

                    DateRangeList.Add(dateRange);

                    comboQueryDateRangeList.Items.Clear();

                    foreach (DateRange dr in DateRangeList.OrderBy(x => x.From))
                    {
                        var queryDateRangeItem = new ComboBoxItem
                        {
                            Value = dr.Id,
                            Text = $"{dr.From} - {dr.To}"
                        };

                        comboQueryDateRangeList.Items.Add(queryDateRangeItem);
                    }

                    comboQueryDateRangeList.SelectedIndex = 0;
                    lblFilteredDateIntervalCount.Text = "Date range count: " + DateRangeList.Count.ToString();
                    btnRemoveIntervalFromQueryList.Enabled = true;
                }
                else
                {
                    MessageBox.Show("There are overlapping dates! Please check the selected date interval.");
                }
            }
            else
            {
                MessageBox.Show("Please specify the query start and end dates correctly! Start date must be smaller than end date.");
            }
        }

        private void btnRemoveIntervalFromQueryList_Click(object sender, EventArgs e)
        {
            if (comboQueryDateRangeList.SelectedIndex > -1)
            {
                var dateIntervalId = (ComboBoxItem)comboQueryDateRangeList.SelectedItem;

                DateRangeList.RemoveAll(x => x.Id == (int)dateIntervalId.Value);

                comboQueryDateRangeList.Items.Clear();

                if (DateRangeList.Count > 0)
                {
                    foreach (DateRange dr in DateRangeList.OrderBy(x => x.From))
                    {
                        var queryDateRangeItem = new ComboBoxItem
                        {
                            Value = dr.Id,
                            Text = $"{dr.From} - {dr.To}"
                        };

                        comboQueryDateRangeList.Items.Add(queryDateRangeItem);
                    }

                    comboQueryDateRangeList.SelectedIndex = 0;
                }
                else
                {
                    comboQueryDateRangeList.Text = string.Empty;
                    btnRemoveIntervalFromQueryList.Enabled = false;
                }

                lblFilteredDateIntervalCount.Text = "Date range count: " + DateRangeList.Count.ToString();
            }
        }

        public bool IsThereOverlap(List<DateRange> dateList, DateTime start, DateTime end)
        {
            foreach(DateRange dr in dateList.OrderBy(x => x.From))
            {
                return Min(dr.From, dr.To) < Max(start, end) && Max(dr.From, dr.To) > Min(start, end);
            }
            return false;
        }

        public static DateTime Max(DateTime d1, DateTime d2)
        {
            return d1 > d2 ? d1 : d2;
        }

        public static DateTime Min(DateTime d1, DateTime d2)
        {
            return d2 > d1 ? d1 : d2;
        }
    }
}
