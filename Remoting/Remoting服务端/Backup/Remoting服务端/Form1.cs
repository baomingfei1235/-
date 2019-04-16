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

        string today = "";
        public frmRemotingServer()
        {
            InitializeComponent();
            frmPortSet fc = new frmPortSet();
            fc.ShowDialog();
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
            TimeSpan ff = new TimeSpan(0);
            RemotingConfiguration.CustomErrorsMode = CustomErrorsModes.Off;
            RemotingConfiguration.CustomErrorsEnabled(false);
            System.Runtime.Remoting.Lifetime.LifetimeServices.LeaseTime = ff;
            BinaryServerFormatterSinkProvider provider = new BinaryServerFormatterSinkProvider();
            provider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;            
            IDictionary   props   =   new  Hashtable();
            props["port"] = Port;
            TcpChannel chan = new TcpChannel(props, null, provider);
            ChannelServices.RegisterChannel(chan, false);
            Oral Operater = new Oral();     
            ObjRef obj = RemotingServices.Marshal(Operater, "Tcpservice");            
            RemotingServices.Unmarshal(obj);
            notifyIcon1.Text = "端口号：" + Port.ToString() + "remoting服务正在运行中";
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
                if (today == "")
                {
                    today = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
                }
                else if (today != DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString())
                {
                    today = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
                    XmlDocument doc = new XmlDocument();
                    doc.Load(Application.StartupPath + @"\Operator\Users.xml");
                    doc.SelectSingleNode("Users").RemoveAll();
                    doc.Save(Application.StartupPath + @"\Operator\Users.xml");
                }
            }
            catch
            { }
        }
    }
}