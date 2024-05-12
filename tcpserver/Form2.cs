using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tcpserver
{
    public partial class Form2 : Form
    {
        TcpClient tcpClient;
        StreamWriter streamWriter;
        StreamReader streamReader;
        Thread a;
        public Form2()
        {
            InitializeComponent();
            try
            {
                tcpClient = new TcpClient("127.0.0.1", 8000);
                streamWriter = new StreamWriter(tcpClient.GetStream());
                a = new Thread(new ParameterizedThreadStart(Listen));
                a.Start(tcpClient);
            }
            catch (Exception)
            {
                MessageBox.Show("Không có server để kết nối");
            }
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (tcpClient != null)
            {
                string s = string.Empty;
                string name ;
                if (textBox2.Text.ToString() == "")
                    name = "Giấu tên";
                else
                    name = textBox2.Text.ToString();
                s =name+": "+ textBox1.Text.ToString();
                streamWriter.WriteLine(s);
                streamWriter.Flush();
                string selfs = "Bạn: " + textBox1.Text.ToString();
                lbMessage.Items.Add(selfs);
            }
            else
            {
                lbMessage.Items.Add("tcpclient is null");
            }


        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
        public void Listen(object tcpClient)
        {
            TcpClient tcpClient1 = (TcpClient)tcpClient;
            streamReader = new StreamReader(tcpClient1.GetStream());
            while (true)
            {
                if (tcpClient1.Connected)
                {
                    string s = streamReader.ReadLine();
                    if (s != null)
                    {
                        lbMessage.Items.Add(s);
                    }
                }

            }

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tcpClient != null) { tcpClient.Close(); }
            if (streamWriter != null) { streamWriter.Close(); }
            if (streamWriter != null) { streamWriter.Close(); }
        }
    }
}
