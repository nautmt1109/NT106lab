using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace tcpserver
{
    public partial class Form1 : Form
    {
        Thread a;
        TcpListener listener = null;
        List<TcpClient> connectedClients = new List<TcpClient>();
        Thread Listenformclient;
        StreamWriter streamWriter;
        public Form1()
        {
            InitializeComponent();
        }
        public void Listen()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            listener = new TcpListener(ip, 8000);
            listener.Start();
            lbMessage.Items.Add("server is listening on " + listener.LocalEndpoint);
            lbMessage.Items.Add("waiting for connection...");
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                lbMessage.Items.Add("Accepted new client connection");
                connectedClients.Add(client);
                Listenformclient = new Thread(new ParameterizedThreadStart(LFC));
                Listenformclient.Start(client);

            }
        }
        public void LFC(object client)
        {
            TcpClient tcpClient = (TcpClient)client;
            StreamReader streamReader = new StreamReader(tcpClient.GetStream());

            try
            {
                while (true)
                {
                    if (tcpClient.Connected)
                    {
                        string s = streamReader.ReadLine();
                        if (s != null)
                        {
                            lbMessage.Items.Add(s);
                            foreach (TcpClient otherClient in connectedClients)
                            {
                                if (otherClient.Connected && otherClient != tcpClient)
                                {
                                    StreamWriter otherWriter = new StreamWriter(otherClient.GetStream());
                                    otherWriter.WriteLine(s);
                                    otherWriter.Flush();
                                }
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (IOException)
            {
                // Xử lý ngoại lệ khi đóng kết nối bởi remote host
                lbMessage.Items.Add("Connection closed by remote host");
            }
            finally
            {
                // Đảm bảo đóng kết nối, stream và TcpClient
                streamReader.Close();
                streamWriter.Close();
                tcpClient.Close();
            }

        }
        private void lbMessage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            btnListen.Enabled = false;
            a = new Thread(new ThreadStart(Listen));
            a.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            //if (a != null && a.IsAlive)
            //{
            //    a.Abort();
            //}
            //if (Listenformclient != null && Listenformclient.IsAlive)
            //{
            //    Listenformclient.Abort();
            //}
            //if (listener != null)
            //{
            //    listener.Stop();
            //}

        }
    }
}