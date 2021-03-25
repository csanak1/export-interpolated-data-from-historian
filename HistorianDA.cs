using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Historian = Proficy.Historian.ClientAccess.API; // v7.1

namespace ExportHistorianTagDataToCSV
{
    class HistorianDA
    {
        internal class TagData
        {
            public string TagName { get; set; }
            public string Value { get; set; }
            public string Quality { get; set; }
            public string TimeStamp { get; set; }
        }

        public struct TagSpecifics
        {
            public string Tagname;
            public string[] DependencyTagnames;
            public delegate double Formula(double[] formulaMembers);
            public Formula CalcFormula;
        }

        private const string iHistSrv = "yourHistorianServer";

        public string IHistSrv
        {
            get
            {
                return iHistSrv;
            }

            //TODO: setter
        }

        //variable to hold the Historian server connection
        Historian.ServerConnection sc;

        public bool IsConnected
        {
            get { return sc != null && sc.IsConnected(); }
        }

        public HashSet<string> QueryFilteredTags(string tagFilter)
        {
            if (sc == null)
            {
                MessageBox.Show("First, establish the connection with the Historian server");

                return null;
            }
            else
            {
                Connect();

                if (!IsConnected)
                    Connect();

                try
                {
                    Historian.TagQueryParams queryTags = new Historian.TagQueryParams { PageSize = 100 }; // PageSize is the batch size of the while loop below, not recommended to set higher than 500
                    Historian.ItemErrors itemErrors = new Historian.ItemErrors();
                    Historian.DataSet dataSet = new Historian.DataSet();

                    List<Historian.Tag> filteredTags = new List<Historian.Tag>();
                    List<Historian.Tag> tempTags;

                    queryTags.Criteria.TagnameMask = tagFilter; //filtering tags 
                    //queryTags.Criteria.DataType = Historian.Tag.NativeDataType.VariableString; //

                    //execute the query and populate the list datatype
                    while (sc.ITags.Query(ref queryTags, out tempTags))
                        filteredTags.AddRange(tempTags);
                    filteredTags.AddRange(tempTags);

                    //impossible to have two tags with the same name, but... using distinct anyway...
                    return new HashSet<string>(filteredTags.Select(e => e.Name).Distinct().ToList());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Tag query error: " + ex.Message);

                    return null;
                }
            }
        }

        public DataTable QueryTagInterpolatedValues(DateTime queryStart, DateTime queryEnd, int samples, string tagName)
        {
            //int totalSamples = 0;

            Connect();

            if (!IsConnected)
                Connect();

            Historian.DataQueryParams query =
                    new Historian.InterpolatedQuery(queryStart, queryEnd, (uint)samples, tagName) { Fields = Historian.DataFields.Time | Historian.DataFields.Value | Historian.DataFields.Quality };
            Historian.DataSet set = new Historian.DataSet();

            query.Criteria.SamplingMode = Historian.DataCriteria.SamplingModeType.Interpolated;

            sc.IData.Query(
                    ref query,
                    out set,
                    out _);

            //totalSamples += set.TotalSamples;

            var tagData = new List<TagData>();

            for (int i = 0; i < set[tagName].Count(); i++)
            {
                var data = new TagData
                {
                    TagName = tagName,
                    Value = set[tagName].GetValue(i) == null ? string.Empty : set[tagName].GetValue(i).ToString(),
                    Quality = set[tagName].GetQuality(i).PercentGood().ToString(),
                    TimeStamp = set[tagName].GetTime(i).ToString("yyyy.MM.dd. HH:mm:ss")
                };

                tagData.Add(data);
            }

            return Extensions.ConvertToDataTable(tagData);
        }

        // TODO:
        public DataTable QueryTagInterpolatedValues(DateTime queryStart, DateTime queryEnd, int samples, int queryType, params TagSpecifics[] tags)
        {
            // TODO: InterpolatedQuery, CalculatedQuery, RawByTimeQuery

            //int totalSamples = 0;


            return null;
        }

        public void Connect()
        {
            if (sc == null)
            {
                sc = new Historian.ServerConnection(new Historian.ConnectionProperties
                {
                    ServerHostName = IHistSrv,
                    OpenTimeout = new TimeSpan(0, 0, 10),
                    ServerCertificateValidationMode = Historian.CertificateValidationMode.None
                });
            }

            if (!sc.IsConnected())
            {
                try
                {
                    sc.Connect();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error at connecting to Historian server: " + ex.Message);
                }
            }
        }

        public void Disconnect()
        {
            if (sc.IsConnected())
            {
                try
                {
                    sc.Disconnect();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error at disconnecting from Historian server: " + ex.Message);
                }
            }

            Dispose();
        }

        private void Dispose()
        {
            ((IDisposable)sc).Dispose();
        }
    }
}
