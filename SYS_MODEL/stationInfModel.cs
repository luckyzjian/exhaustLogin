using System;
using System.Collections.Generic;
using System.Text;

namespace SYS_MODEL
{
    public partial class stationInfModel
    {
        #region model
        private string stationid;

        public string STATIONID
        {
            get { return stationid; }
            set { stationid = value; }
        }
        private string stationname;

        public string STATIONNAME
        {
            get { return stationname; }
            set { stationname = value; }
        }
        private string stationadd;

        public string STATIONADD
        {
            get { return stationadd; }
            set { stationadd = value; }
        }
        private string stationphone;

        public string STATIONPHONE
        {
            get { return stationphone; }
            set { stationphone = value; }
        }
        private string stationperson;

        public string STATIONPERSON
        {
            get { return stationperson; }
            set { stationperson = value; }
        }
        private string stationdate;

        public string STATIONDATE
        {
            get { return stationdate; }
            set { stationdate = value; }
        }
        private string stationjcff;

        public string STATIONJCFF
        {
            get { return stationjcff; }
            set { stationjcff = value; }
        }
        private string standard;

        public string STANDARD
        {
            get { return standard; }
            set { standard = value; }
        }
        private string stationnet;
        public string STATIONNET
        {
            get { return stationnet; }
            set { stationnet = value; }
        }
        public string StationCompany { set; get; }
        #endregion
    }
}
