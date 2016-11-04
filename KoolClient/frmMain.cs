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

namespace KoolClient
{
    public partial class frmMain : Form
    {
        public byte[] bytes;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text = "KoolRat v" + Application.ProductVersion;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                IPHostEntry ipHost = Dns.Resolve(Dns.GetHostName());
                IPAddress ip = ipHost.AddressList[0];
                IPEndPoint remote = new IPEndPoint(ip, 962);

                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    sock.Connect(remote);

                    byte[] msg = Encoding.ASCII.GetBytes("notepad" + Environment.NewLine);
                    int bytesSent = sock.Send(msg);

                    sock.Shutdown(SocketShutdown.Both);
                    sock.Close();

                }
                catch (SocketException sex)
                {
                    MessageBox.Show(sex.Message, "Socket Exception");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Gneral Exception");
                }
            }
            catch (SocketException sex)
            {
                MessageBox.Show(sex.Message, "Socket Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "General Exception");
            }
        }
    }
}
