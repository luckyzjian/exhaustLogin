using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace exhaustLogin
{
    public partial class personelInf : Form
    {
        SYS_MODEL.staffModel staffmodel = new SYS_MODEL.staffModel();
        public personelInf()
        {
            InitializeComponent();
        }

        private void personelInf_Load(object sender, EventArgs e)
        {
            DataTable dt = null;
            dt = mainPanel.logininfcontrol.getComBoBoxItemsInf("职位");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBoxClass.Items.Add(dt.Rows[i]["MC"].ToString());
            }
            comboBoxClass.SelectedIndex = 0;
            labelUsername.Text = mainPanel.nowUser.userName;
            staffmodel = mainPanel.logininfcontrol.GetStaffInf(mainPanel.nowUser.userID);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            loadStaffInf();            
            panelPassword.Visible = false;
            Thread.Sleep(100);
            panelInf.Location = new Point(442, 76);
            panelInf.Visible = true;
            
            panelPassword.Location = new Point(734, 76);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadPassword();
            panelInf.Visible = false;
            Thread.Sleep(100);
            panelPassword.Location = new Point(442, 76);
            panelPassword.Visible = true;
            
            panelInf.Location = new Point(734, 76);
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
            return true;
        }
        private void loadStaffInf()
        {
            textBoxNAME.Text = staffmodel.NAME;
            textBoxID.Text = staffmodel.ID;
            dateBIRTHDAY.Text = staffmodel.BIRTHDAY;
            comboBoxClass.Text = mainPanel.logininfcontrol.getPostNamebyPostID(staffmodel.POSTID);
            textBoxADD.Text = staffmodel.ADDRESS;
            textBoxAGE.Text = staffmodel.AGE;
            textBoxTEL.Text = staffmodel.PHONE;
            textBoxQQ.Text = staffmodel.QQ;
            textBoxEDUCATION.Text = staffmodel.EDUCATION;
            textBoxMAIL.Text = staffmodel.EMAIL;
            if (staffmodel.SEX == "男") radioButtonMALE.Checked=true;
            else radioButtonFEMALE.Checked = true;
            if (staffmodel.MARRIED == "已婚") radioButtonMARRIED.Checked = true;
            else radioButtonNOTMARRIED.Checked = true;
        }
        private void loadPassword()
        {
            textBoxOLDPASSWORD.Text = "";
            textBoxNEWPASSWORD.Text = "";
            textBoxNEWPASSWORD2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(!checkInfIsRight())
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
            if (radioButtonMALE.Checked) staffmodel.SEX = "男";
            else staffmodel.SEX = "女";
            if (radioButtonMARRIED.Checked) staffmodel.MARRIED = "已婚";
            else staffmodel.MARRIED = "未婚";
            if (mainPanel.logininfcontrol.updateStaffInf(staffmodel))
                MessageBox.Show("用户信息更改成功", "系统提示");
            else
                MessageBox.Show("未知原因导致用户信息更改失败", "系统提示");
        }
        private bool checkPasswordIsRight()
        {
            if (textBoxOLDPASSWORD.Text != staffmodel.PASSWORD)
            {
                MessageBox.Show("旧密码输入不正确", "系统提示");
                return false;
            }
            if (textBoxNEWPASSWORD.Text != textBoxNEWPASSWORD2.Text)
            {
                MessageBox.Show("两次密码输入不一致", "系统提示");
                return false;
            }
            return true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!checkPasswordIsRight())
                return;
            if(mainPanel.logininfcontrol.reWritePassword(mainPanel.nowUser.userID,textBoxNEWPASSWORD.Text))
                MessageBox.Show("密码更改成功", "系统提示");
            else
                MessageBox.Show("未知原因导致密码更改失败", "系统提示");
        }

    }
}
