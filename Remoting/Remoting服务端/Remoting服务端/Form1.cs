using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Services;
using System.Runtime.Remoting.Channels.Tcp;
using System.Collections;
using System.Xml;
using DataOperater;

namespace Remoting服务端
{
    public partial class frmRemotingServer : Form
    {
        public static int Port = 9999;

        //string today = "";
        Oral Operater;
        
        public frmRemotingServer()
        {
            //this.Hide();
            InitializeComponent();
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出吗？","信息提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {               
                Application.Exit();
            }
        }

        private void 还原ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ShowDialog();
        }
      
        private void frmRemotingServer_Load(object sender, EventArgs e)
        {
          
            frmPortSet fc = new frmPortSet();
            fc.ShowDialog();
            TimeSpan ff = new TimeSpan(0);
            RemotingConfiguration.CustomErrorsMode = CustomErrorsModes.Off;
            RemotingConfiguration.CustomErrorsEnabled(false);
            System.Runtime.Remoting.Lifetime.LifetimeServices.LeaseTime = ff;
            BinaryServerFormatterSinkProvider provider = new BinaryServerFormatterSinkProvider();
            provider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;
            IDictionary props = new Hashtable();
            props["port"] = Port;
            TcpChannel chan = new TcpChannel(props, null, provider);
            ChannelServices.RegisterChannel(chan, false);
            Operater = new Oral();
            ObjRef obj = RemotingServices.Marshal(Operater, "Tcpservice");
            RemotingServices.Unmarshal(obj);
            notifyIcon1.Text = "端口号：" + Port.ToString() + "remoting服务正在运行中";
            this.Visible = false;

            //DataSet dsdoc = Operater.GetDataSet("select * from t_patient_doc_colb where tid=7017004");
            //string Xmldoc = dsdoc.Tables[0].Rows[0]["CONTENT"].ToString();
            //byte[] imgs = Operater.GetDocImage(Xmldoc);
            //Image tt = imgs[0];
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出吗？", "信息提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {              
                Application.Exit();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                //if (today == "")
                //{
                //    today = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
                //}
                //else if (today != DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString())
                //{
                //    today = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
                //    XmlDocument doc = new XmlDocument();
                //    doc.Load(Application.StartupPath + @"\Operator\Users.xml");
                //    doc.SelectSingleNode("Users").RemoveAll();
                //    doc.Save(Application.StartupPath + @"\Operator\Users.xml");
                //}
                this.Visible = false;               
                Oral.RemoveOutLimited();
                richTextBox1.Text = "";
                for (int i = 0; i < Oral.ArrCients.Count; i++)
                {
                    client_obj tb = (client_obj)Oral.ArrCients[i];
                    tb.LinkCount++;
                    string strval = tb.Ip + " " + tb.UserName + " " + tb.ZhiWu + " " + tb.ZhiCheng + " " + tb.Account_Name;
                    if (richTextBox1.Text.Trim() == "")
                    {
                        richTextBox1.Text = strval;
                    }
                    else
                    {
                        richTextBox1.Text = richTextBox1.Text + "\n" + strval;
                    }
                }                
            }
            catch
            { }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;                
            }
            else
            {
                this.WindowState = FormWindowState.Minimized;
            }

        }

        private void notifyIcon1_BalloonTipShown(object sender, EventArgs e)
        {
           
        }

        private void notifyIcon1_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            //notifyIcon1.ShowBalloonTip(5000, "remoting提示消息", "端口号：" + Port.ToString() + "remoting服务正在运行中", ToolTipIcon.Info);
        }

        private void frmRemotingServer_MinimumSizeChanged(object sender, EventArgs e)
        {
            
        }

        private void frmRemotingServer_Resize(object sender, EventArgs e)
        {          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Oral ff = new Oral();
            ////DBParameter[] tt = new DBParameter[1];
            ////tt[0] = new DBParameter();
            ////tt[0].DBType =System.Data.OracleClient.OracleType.VarChar ;
            ////tt[0].ParameterName = "ttt";
            ////tt[0].Value = "3";
            ////ff.RunProcedure("TEST", tt);

            //DBParameter[] tt = new DBParameter[2];
            //tt[0] = new DBParameter();
            //tt[0].DBType = System.Data.OracleClient.OracleType.Number;
            //tt[0].ParameterName = "us_id";
            //tt[0].Value = "3";


            //tt[1] = new DBParameter();
            //tt[1].DBType = System.Data.OracleClient.OracleType.Cursor;
            //tt[1].ParameterName = "cur_name";
            //tt[1].Direction = ParameterDirection.Output;
            //DataSet ds = ff.RunProcedureGetData("PKG_select_text.Getusername", tt);
           


        }

        private void frmRemotingServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon1.Visible = false;
        }
    }
}