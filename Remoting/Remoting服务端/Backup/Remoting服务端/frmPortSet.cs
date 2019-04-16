using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Remoting服务端
{
    public partial class frmPortSet : Form
    {
        public frmPortSet()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {            

            try
            {
                frmRemotingServer.Port = Convert.ToInt32(textBox1.Text);
                this.Close();
            }
            catch
            {
                MessageBox.Show("信息提示", "请输入数值类型！");
                this.textBox1.Text = "9999";
            }
        }
    }
}