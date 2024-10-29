using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;


namespace ChatServidor_PeraHerreraTabare
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
            Initialize_cbbProtocol();
            Initialize_cbbMode();
        }

        private string[] protocols = new string[] {"TCP", "UDP"};
        private string[] mode = new string[] { "Servidor", "Cliente" };
        private void Initialize_cbbProtocol()
        {
            cbb_protocol.SelectedIndex = 0;
        }
        private void Initialize_cbbMode()
        {
            cbb_mode.SelectedIndex = 0;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_launch_Click(object sender, EventArgs e)
        {
            if (cbb_protocol.SelectedItem.ToString() == "TCP")
            {
                // Modo TCP
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
            else if (cbb_protocol.SelectedItem.ToString() == "UDP")
            {
                // Modo UDP
                if (cbb_mode.SelectedItem.ToString() == "Servidor")
                {
                    UDP_ServidorChatForm UDP_SCForm = new UDP_ServidorChatForm();
                    UDP_SCForm.Show();
                }
                else
                {
                    UDP_ClienteChatForm UDP_CCForm = new UDP_ClienteChatForm();
                    UDP_CCForm.Show();
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

        private void MainMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }
    }
}
    

