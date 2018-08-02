using System;
namespace SYS.Model
{
	/// <summary>
	/// SBXXB:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class BDSX
	{
        public BDSX()
		{}
		#region Model
        private string hxsx;//加载滑行标定时限

        public string HXSX
        {
            get { return hxsx; }
            set { hxsx = value; }
        }
        private string jsglsx;//寄生功率标定时限

        public string JSGLSX
        {
            get { return jsglsx; }
            set { jsglsx = value; }
        }
        private string glsx;//惯量标定时限

        public string GLSX
        {
            get { return glsx; }
            set { glsx = value; }
        }
        private string fxysx;//废气仪标定时限

        public string FXYSX
        {
            get { return fxysx; }
            set { fxysx = value; }
        }
        private string lljsx;//流量计标定时限

        public string LLJSX
        {
            get { return lljsx; }
            set { lljsx = value; }
        }
		#endregion Model

	}
}

