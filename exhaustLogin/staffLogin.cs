using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SYS_DAL;
namespace exhaustLogin
{
    public partial class staffLogin : Form
    {
        //private loginInfModel logininfmodel = new loginInfModel();
        private loginInfControl logininfcontrol = new loginInfControl();
        private baseControl basecontrol = new baseControl();
        public staffLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userid = logininfcontrol.checkUserIsAlive(comboBoxUser.Text, textBoxPassword.Text);
            if (userid != "-2")
            {
                string postid = basecontrol.getIDByName(comboBoxClass.Text, "职位", "POSTID");
                mainPanel.nowUser.postID = postid;
                mainPanel.nowUser.userID = userid;
                mainPanel.nowUser.postName = comboBoxClass.Text;
                mainPanel.nowUser.userName = comboBoxUser.Text;
                mainPanel.nowUser.password = textBoxPassword.Text;
                mainPanel.isNetUsed = radioButtonNet.Checked;
                mainPanel.userLoginSuccess = true;
                this.Close();
            }
            else
            {
                mainPanel.userLoginSuccess = false;
                MessageBox.Show("用户名不存在或输入密码错误,请检查");
            }
        }

        private void staffLogin_Load(object sender, EventArgs e)
        {
            radioButtonNet.Checked = mainPanel.isNetUsed;
            radioButtonLocal.Checked = !mainPanel.isNetUsed;
            DataTable dt = null;
            dt = logininfcontrol.getComBoBoxItemsInf("职位");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBoxClass.Items.Add(dt.Rows[i]["MC"].ToString());
            }
            comboBoxClass.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainPanel.userLoginSuccess = false;
            this.Close();
        }

        private void comboBoxClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxUser.Items.Clear();
            DataTable dt = null;
            string postid = basecontrol.getIDByName(comboBoxClass.Text, "职位", "POSTID");
            dt = logininfcontrol.getStaffByPost(postid);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBoxUser.Items.Add(dt.Rows[i]["NAME"].ToString());
            }
        }
    }
}
