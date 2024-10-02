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
        }

        private NetworkStream salida; // Flujo para recibir datos
        private BinaryWriter escritor; // Facilita la escritura en el flujo
        private BinaryReader lector; // Facilita la lectura en el flujo
        private Thread lecturaThread; // Thread para procesar mensajes entrantes
        private string mensaje = "";

        private void TCP_ClienteChatForm_Load(object sender, EventArgs e)
        {
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

        private void DeshabilitarSalida(bool valor)
        {
            if (entradaTextBox.InvokeRequired)
            {
                Invoke(new DisableInputDelegate(DeshabilitarSalida),
                new object[] { valor });
            }
            else
            {
                entradaTextBox.ReadOnly = valor;
            }
        }

        private void entradaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter && entradaTextBox.ReadOnly == false)
                {
                    escritor.Write("CLIENTE>>> " + entradaTextBox.Text);
                    mostrarTextBox.Text += "\r\nCLIENTE>>> " + entradaTextBox.Text;
                    entradaTextBox.Clear();
                }
            }
            catch (SocketException)
            {
                mostrarTextBox.Text += "\nError al escribir objeto";
            }
        }

        public void EjecutarCliente()
        {
            TcpClient cliente;
            try
            {
                // Paso 1
                cliente = new TcpClient();
                cliente.Connect("127.0.0.1", 50000);

                // Paso 2
                salida = cliente.GetStream();

                escritor = new BinaryWriter(salida);
                lector = new BinaryReader(salida);

                MostrarMensaje("\r\nSe recibieron flujos de E/S\r\n");
                DeshabilitarSalida(false);

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
                        System.Environment.Exit(System.Environment.ExitCode);
                    }
                } while (mensaje != "SERVIDOR>>> TERMINAR");

                // Paso 4
                escritor.Close();
                lector.Close();
                salida.Close();
                cliente.Close();

                Application.Exit();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString(), "Error en la conexión",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Environment.Exit(System.Environment.ExitCode);
            }
        }
    }
}
