using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _22521206_lab3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        UdpClient udpClient ;
        Thread thread;
        private void btnListen_Click(object sender, EventArgs e)
        {
            thread = new Thread(new ThreadStart(Serverthread));
            thread.Start();
        }
        public void Serverthread()
        {
            udpClient = new UdpClient(Convert.ToInt32(tbPort.Text));
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, Convert.ToInt32(tbPort.Text));

            while (true)
            {
                byte[] data = new byte[32];
                data = udpClient.Receive(ref iep);
                string returndata =iep.Address.ToString()+": "+ Encoding.UTF8.GetString(data);
                Showmessage(returndata);
            }

        }
        public void Showmessage(string returndata)
        {
            tbMessage.Text += returndata+"\r\n";
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
          udpClient.Close();
          thread.Abort();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }

    }