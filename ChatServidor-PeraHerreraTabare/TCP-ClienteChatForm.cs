using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ChatServidor_PeraHerreraTabare
{
    public partial class TCP_ClienteChatForm : Form
    {
        public TCP_ClienteChatForm()
        {
            InitializeComponent();
            lecturaThread = new Thread(new ThreadStart(EjecutarCliente));
            lecturaThread.Start();
        }

        private NetworkStream salida; // Flujo para recibir datos
        private BinaryWriter escritor; // Facilita la escritura en el flujo
        private BinaryReader lector; // Facilita la lectura en el flujo
        private Thread lecturaThread; // Thread para procesar mensajes entrantes
        private string mensaje = "";

        private void TCP_ClienteChatForm_Load(object sender, EventArgs e)
        {
            ConfiguracionesForm configTCPC = new ConfiguracionesForm("TCP", "Client");
            configTCPC.ShowDialog();
            lecturaThread = new Thread(new ThreadStart(EjecutarCliente));
            lecturaThread.Start();
        }

        private void TCP_ClienteChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }

        private delegate void DisplayDelegate(string message);

        private void MostrarMensaje(string mensaje)
        {
            if (mostrarTextBox.InvokeRequired)
            {
                Invoke(new DisplayDelegate(MostrarMensaje),
                new object[] { mensaje });
            }
            else
            {
                mostrarTextBox.Text += mensaje;
            }
        }

        private delegate void DisableInputDelegate(bool value);

        private void HabilitarEnviar(bool valor)
        {
            if (entradaTextBox.InvokeRequired)
            {
                Invoke(new DisableInputDelegate(HabilitarEnviar),
                new object[] { valor });
            }
            else
            {
                btnEnviar.Enabled = valor;
            }
        }

        public void EjecutarCliente()
        {
            TcpClient cliente;
            try
            {
                // Paso 1
                cliente = new TcpClient();

                try
                {
                    cliente.Connect("127.0.0.1", 50000);
                }
                catch (Exception e)
                {
                    MessageBox.Show("No pudo conectarse a: " + VariablesDefaultChat.TCP_Client_IP + ":" + VariablesDefaultChat.TCP_Client_Port, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Paso 2
                salida = cliente.GetStream();

                escritor = new BinaryWriter(salida);
                lector = new BinaryReader(salida);

                MostrarMensaje("\r\nSe recibieron flujos de E/S\r\n");
                HabilitarEnviar(true);

                // Paso 3
                do
                {
                    try
                    {
                        //lee mensaje del servidor
                        mensaje = lector.ReadString();
                        MostrarMensaje("\r\n" + mensaje);
                    }
                    catch (Exception)
                    {
                        break;
                    }
                } while (cliente.Connected);

                // Paso 4
                escritor.Close();
                lector.Close();
                salida.Close();
                cliente.Close();

                MostrarMensaje("\r\nEl servidor terminó la conexión\r\n");

                HabilitarEnviar(false);

            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString(), "Error en la conexión",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(System.Environment.ExitCode);
            }
        }


        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                escritor.Write("CLIENTE>>> " + entradaTextBox.Text);
                mostrarTextBox.Text += "\r\nCLIENTE>>> " + entradaTextBox.Text;
                entradaTextBox.Clear();
            }
            catch (SocketException)
            {
                mostrarTextBox.Text += "\nError al escribir objeto";
            }
        }

        private void configurarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfiguracionesForm configTcpServ = new ConfiguracionesForm("TCP", "Client");
            configTcpServ.ShowDialog();
        }
    }
}
