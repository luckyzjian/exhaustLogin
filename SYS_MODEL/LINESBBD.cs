using System;
namespace SYS.Model
{
	/// <summary>
	/// SBXXB:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class LINESBBD
	{
        public LINESBBD()
		{}
		#region Model
        private string stationID;

        public string STATIONID
        {
            get { return stationID; }
            set { stationID = value; }
        }
        private string lineID;

        public string LINEID
        {
            get { return lineID; }
            set { lineID = value; }
        }
        private string hxbd;

        public string HXBD
        {
            get { return hxbd; }
            set { hxbd = value; }
        }
        private string hxenable;

        public string HXENABLE
        {
            get { return hxenable; }
            set { hxenable = value; }
        }
        private DateTime yrdate;

        public DateTime YRDATE
        {
            get { return yrdate; }
            set { yrdate = value; }
        }
        private string yrenable;

        public string YRENABLE
        {
            get { return yrenable; }
            set { yrenable = value; }
        }
        private string jsglbd;

        public string JSGLBD
        {
            get { return jsglbd; }
            set { jsglbd = value; }
        }
        private string jsglenable;

        public string JSGLENABLE
        {
            get { return jsglenable; }
            set { jsglenable = value; }
        }
        private string glbd;

        public string GLBD
        {
            get { return glbd; }
            set { glbd = value; }
        }
        private string glenable;

        public string GLENABLE
        {
            get { return glenable; }
            set { glenable = value; }
        }
        private string fxybd;

        public string FXYBD
        {
            get { return fxybd; }
            set { fxybd = value; }
        }
        private string fxyenalbe;

        public string FXYENALBE
        {
            get { return fxyenalbe; }
            set { fxyenalbe = value; }
        }
        private string lljbd;

        public string LLJBD
        {
            get { return lljbd; }
            set { lljbd = value; }
        }
        private string lljenable;

        public string LLJENABLE
        {
            get { return lljenable; }
            set { lljenable = value; }
        }
		#endregion Model

	}
}

