using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;


namespace SendMessageTcpTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(this.txtMessage.Text))
            {
                SendMessage(this.txtMessage.Text).Wait();
            }
        }

        private async Task<IPEndPoint> GetTargetIPEndPoint()
        {
            var address = await Dns.GetHostAddressesAsync(Dns.GetHostName());
            var endpoint = new IPEndPoint(address[0], 8899);
            return endpoint;
        }

        private async Task SendMessage(string message)
        {
            var remoteServer = "10.86.160.157";
            var client = new TcpClient(remoteServer,8899);
            var stream = client.GetStream();
            var bytes = System.Text.Encoding.ASCII.GetBytes(message);
            this.txtMessage.Text = "send message:" + message;
            await stream.WriteAsync(bytes, 0, bytes.Length);

           

            //var bytesToRead = new byte[256];
            //string response = string.Empty;
            //int i = 0;
            //while((i=await stream.ReadAsync(bytesToRead, 0, bytesToRead.Length))!=0)
            //{
            //    response += System.Text.Encoding.ASCII.GetString(bytesToRead);
            //}

            //this.txtMessage.Text += response;
            stream.Close();
            client.Close();
        }

        private void Receive()
        {

        }
    }
}
