using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _22521206_lab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        UdpClient udp;
        private void btnSend_Click(object sender, EventArgs e)
        {
            udp= new UdpClient();
            byte[] data=new byte[1024];
            IPEndPoint ipep=new IPEndPoint(IPAddress.Parse(tbIP.Text),int.Parse(tbPort.Text));
            data=Encoding.UTF8.GetBytes(tbMessage.Text.ToString());
            udp.Send(data, data.Length, ipep);
            tbMessage.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            udp.Close();
        }
    }
}
