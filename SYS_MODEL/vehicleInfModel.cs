using System;
namespace SYS.Model
{
	/// <summary>
	/// BJCLXXB:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class vehicleInfModel
	{
        public vehicleInfModel()
		{}
		#region Model
        private int id;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        private string clxh;

        public string CLXH
        {
            get { return clxh; }
            set { clxh = value; }
        }
        private string clmc;

        public string CLMC
        {
            get { return clmc; }
            set { clmc = value; }
        }
        private string fdjxh;

        public string FDJXH
        {
            get { return fdjxh; }
            set { fdjxh = value; }
        }
        private string fdjscc;

        public string FDJSCC
        {
            get { return fdjscc; }
            set { fdjscc = value; }
        }
        private string manuf;

        public string MANUF
        {
            get { return manuf; }
            set { manuf = value; }
        }

        private string clsb;

        public string CLSB
        {
            get { return clsb; }
            set { clsb = value; }
        }
        private string filename;

        public string FILENAME
        {
            get { return filename; }
            set { filename = value; }
        }
        private string pf;

        public string PF
        {
            get { return pf; }
            set { pf = value; }
        }
       
        private string cllb;

        public string CLLB
        {
            get { return cllb; }
            set { cllb = value; }
        }
        private string vin;

        public string VIN
        {
            get { return vin; }
            set { vin = value; }
        }
        private string clzl;

        public string CLZL
        {
            get { return clzl; }
            set { clzl = value; }
        }

        private string czdz;
        private string jcfs;
        private string jclb;
        private string ssxq;
        private string sfwdzr;
        private string sfyqbf;
        private string fdjscqy;
        private string qdltqy;
        private string ryph;
        public string CZDZ
        {
            get { return czdz; }
            set { czdz = value; }
        }
        public string JCFS
        {
            get { return jcfs; }
            set { jcfs = value; }
        }
        public string JCLB
        {
            get { return jclb; }
            set { jclb = value; }
        }
        public string SSXQ
        {
            get { return ssxq; }
            set { ssxq = value; }
        }
        public string FDJSCQY
        {
            get { return fdjscqy; }
            set { fdjscqy = value; }
        }
        public string QDLTQY
        {
            get { return qdltqy; }
            set { qdltqy = value; }
        }
        public string RYPH
        {
            get { return ryph; }
            set { ryph = value; }
        }
        public string SFWDZR
        {
            get { return sfwdzr; }
            set { sfwdzr = value; }
        }
        public string SFYQBF
        {
            get { return sfyqbf; }
            set { sfyqbf = value; }
        }
        #endregion Model
    }
}

