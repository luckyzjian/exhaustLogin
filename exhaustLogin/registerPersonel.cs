using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace exhaustLogin
{
    public partial class registerPersonel : Form
    {
        DataTable dt_wait = null;
        public registerPersonel()
        {
            InitializeComponent();
        }
        private bool checkInfIsRight()
        {
            if (textBoxNAME.Text == "")
            {
                MessageBox.Show("请填写用户姓名", "系统提示");
                return false;
            }
            if (comboBoxClass.Text == "")
            {
                MessageBox.Show("请选择用户职位", "系统提示");
                return false;
            }
            if (textBoxNEWPASSWORD.Text == "")
            {
                MessageBox.Show("请输入初始密码", "系统提示");
                return false;
            }
            if (textBoxNEWPASSWORD.Text != textBoxNEWPASSWORD2.Text)
            {
                MessageBox.Show("两次密码输入不一致", "系统提示");
                return false;
            }
            return true;
        }
        private void buttonSAVEINF_Click(object sender, EventArgs e)
        {
            if (!checkInfIsRight()) return;
            SYS_MODEL.staffModel staffmodel = new SYS_MODEL.staffModel();
            staffmodel.STAFFID = textBoxID.Text;
            staffmodel.NAME = textBoxNAME.Text;
            staffmodel.ID = textBoxID.Text;
            staffmodel.POSTID = mainPanel.logininfcontrol.getPostIDbyPostName(comboBoxClass.Text);
            staffmodel.BIRTHDAY = dateBIRTHDAY.Value.ToString();
            staffmodel.ADDRESS = textBoxADD.Text;
            staffmodel.AGE = textBoxAGE.Text;
            staffmodel.PHONE = textBoxTEL.Text;
            staffmodel.QQ = textBoxQQ.Text;
            staffmodel.EMAIL = textBoxMAIL.Text;
            staffmodel.EDUCATION = textBoxEDUCATION.Text;
            staffmodel.PASSWORD = textBoxNEWPASSWORD.Text;
            if (radioButtonMALE.Checked) staffmodel.SEX = "男";
            else staffmodel.SEX = "女";
            if (radioButtonMARRIED.Checked) staffmodel.MARRIED = "已婚";
            else staffmodel.MARRIED = "未婚";
            if (mainPanel.logininfcontrol.checkUserIsAlive(staffmodel.STAFFID))
            {
                MessageBox.Show("该用户存在，不能重复注册", "系统提示");
                return;
            }
            if (mainPanel.logininfcontrol.saveStaffInf(staffmodel))
                MessageBox.Show("用户注册成功", "系统提示");
            else
                MessageBox.Show("未知原因导致用户注册失败", "系统提示");
            ref_Staff();
        }

        private void registerPersonel_Load(object sender, EventArgs e)
        {
            init_staff();
            DataTable dt = null;
            dt = mainPanel.logininfcontrol.getComBoBoxItemsInf("职位");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBoxClass.Items.Add(dt.Rows[i]["MC"].ToString());
            }
            comboBoxClass.SelectedIndex = 0;
        }
        private void init_staff()
        {
            init_datagrid();
            ref_Staff();
        }
        private void init_datagrid()
        {
            dt_wait = new DataTable();
            dt_wait.Columns.Add("编号");
            dt_wait.Columns.Add("姓名");
            dt_wait.Columns.Add("职务ID");
            dt_wait.Columns.Add("职务");
            dataGridViewStaff.DataSource = dt_wait;
            dataGridViewStaff.Columns["职务ID"].Visible = false;
            dataGridViewStaff.Columns["编号"].Width = 80;
            dataGridViewStaff.Columns["姓名"].Width = 80;
            dataGridViewStaff.Columns["职务"].Width = 75;
        }
        public void ref_Staff()
        {
            try
            {
                dt_wait.Rows.Clear();
                DataTable dt = mainPanel.logininfcontrol.getAllStaff();
                DataRow dr = null;
                if (dt != null)
                {
                    foreach (DataRow dR in dt.Rows)
                    {
                        dr = dt_wait.NewRow();
                        dr["编号"] = dR["STAFFID"].ToString();
                        dr["姓名"] = dR["NAME"].ToString();
                        dr["职务ID"] = dR["POSTID"].ToString();
                        dr["职务"] = mainPanel.logininfcontrol.getPostNamebyPostID(dR["POSTID"].ToString());
                        dt_wait.Rows.Add(dr);
                    }
                }
                dataGridViewStaff.DataSource = dt_wait;
                dataGridViewStaff.Sort(dataGridViewStaff.Columns["职务ID"], ListSortDirection.Ascending);
            }
            catch (Exception)
            {

            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string selectedStaff = "";
            if (dataGridViewStaff.SelectedRows.Count > 0)
            {
                if (dataGridViewStaff.SelectedRows.Count == 1)
                {
                    selectedStaff = dataGridViewStaff.SelectedRows[0].Cells["编号"].Value.ToString();
                    mainPanel.logininfcontrol.deleteOnePerson(selectedStaff);
                    MessageBox.Show("删除成功", "系统提示");
                    ref_Staff();
                }
            }
        }





    }
}
