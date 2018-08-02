using System;
namespace SYS.Model
{
	/// <summary>
	/// BJCLXXB:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class CARSAFEDETECTED
	{
        public CARSAFEDETECTED()
		{}
		#region Model
        private string clid;//车辆ID

        public string CLID
        {
            get { return clid; }
            set { clid = value; }
        }
        
        private DateTime dlsj;//登录时间

        public DateTime DLSJ
        {
            get { return dlsj; }
            set { dlsj = value; }
        }

        private DateTime jcsj;//登录时间

        public DateTime JCSJ
        {
            get { return jcsj; }
            set { jcsj = value; }
        }
        private string jcjg;

        public string JCJG
        {
            get { return jcjg; }
            set { jcjg = value; }
        }
        private string jccs;//检测次数

        public string JCCS
        {
            get { return jccs; }
            set { jccs = value; }
        }
        
        private string clhp;

        public string CLHP
        {
            get { return clhp; }
            set { clhp = value; }
        }

        private string jcfy;
        public string JCFY
        {
            get { return jcfy; }
            set { jcfy = value; }
        }
        private string sfjf;

        public string SFJF
        {
            get { return sfjf; }
            set { sfjf = value; }
        }
        private string sfjs;

        public string SFJS
        {
            get { return sfjs; }
            set { sfjs = value; }
        }

        private string sflq;

        public string SFLQ
        {
            get { return sflq; }
            set { sflq = value; }
        }
        #endregion Model
    }
}

