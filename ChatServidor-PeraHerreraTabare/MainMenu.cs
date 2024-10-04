using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChatServidor_PeraHerreraTabare
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private string[] protocols = new string[] {"TCP", "UDP"};
        private string[] mode = new string[] { "Servidor", "Cliente" };
        private void initialize_cbbProtocol()
        {
            
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_launch_Click(object sender, EventArgs e)
        {
            if(cbb_protocol.SelectedItem.ToString() == "TCP")
            {
                if (cbb_mode.SelectedItem.ToString() == "Servidor")
                {
                    TCP_ServidorChatForm TCP_SCForm = new TCP_ServidorChatForm();
                    TCP_SCForm.Show();
                }
                else
                {
                    TCP_ClienteChatForm TCP_CCForm = new TCP_ClienteChatForm();
                    TCP_CCForm.Show();
                }
            }
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void MainMenu_Close(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
