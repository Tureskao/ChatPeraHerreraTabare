
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

        static private bool conectado; // Bandera bool que señaliza si estamos conectados
        static private bool threadRunning; // Bandera bool que señaliza si el thread de lectura esta corriendo
        private NetworkStream salida; // Flujo para recibir datos
        private BinaryWriter escritor; // Facilita la escritura en el flujo
        private BinaryReader lector; // Facilita la lectura en el flujo
        private Thread lecturaThread; // Thread para procesar mensajes entrantes
        private string mensaje = "";

        private void TCP_ClienteChatForm_Load(object sender, EventArgs e)
        {

        }

        private void TCP_ClienteChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {

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

        private void HabilitarConectar(bool valor)
        {
            if (entradaTextBox.InvokeRequired)
            {
                Invoke(new DisableInputDelegate(HabilitarConectar),
                new object[] { valor });
            }
            else
            {
                btnConectar.Enabled = valor;
            }
        }

        private void HabilitarDesconectar(bool valor)
        {
            if (entradaTextBox.InvokeRequired)
            {
                Invoke(new DisableInputDelegate(HabilitarDesconectar),
                new object[] { valor });
            }
            else
            {
                btnDesconectar.Enabled = valor;
            }
        }

        public void EjecutarCliente()
        {
            while (threadRunning)
            {
                TcpClient cliente;
                try
                {
                    // Paso 1
                    cliente = new TcpClient();

                    try
                    {
                        cliente.Connect(VariablesDefaultChat.TCP_Client_IP, int.Parse(VariablesDefaultChat.TCP_Client_Port));
                        conectado = true;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("No pudo conectarse a: " + VariablesDefaultChat.TCP_Client_IP + ":" + VariablesDefaultChat.TCP_Client_Port, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        threadRunning = false;
                        break;
                    }

                    if (cliente.Connected)
                    {
                        // Paso 2
                        salida = cliente.GetStream();

                        escritor = new BinaryWriter(salida);
                        lector = new BinaryReader(salida);

                        MostrarMensaje("\r\nSe recibieron flujos de E/S\r\n");
                        HabilitarEnviar(true);
                        HabilitarConectar(false);
                        HabilitarDesconectar(true);


                        // Paso 3
                        while (conectado)
                        {
                            try
                            {
                                mensaje = lector.ReadString();
                                if (mensaje == "SERVIDOR>>> TERMINAR CONEXIÓN")
                                {
                                    conectado = false;
                                }
                                MostrarMensaje("\r\n" + mensaje);
                            }
                            catch (Exception)
                            {
                                break;
                            }
                        }

                        // Paso 4

                        terminarConexion();
                        cliente.Close();
                        MostrarMensaje("\r\nSe terminó la conexión\r\n");
                        HabilitarEnviar(false);
                        conectado = false;
                    } else
                    {
                        terminarThread(lecturaThread);
                    }

                }
                catch (Exception error)
                {
                    MessageBox.Show(error.ToString(), "Error en la conexión",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    HabilitarEnviar(false);
                    HabilitarDesconectar(false);
                    HabilitarConectar(true);
                }
            }
            terminarThread(lecturaThread);
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

        private delegate void displayDelegate(string mensaje);
        private void CambiarNombreVentana(string mensaje)
        {
            if (this.InvokeRequired)
            {
                Invoke(new displayDelegate(CambiarNombreVentana),
                    new object[] { mensaje });
            }
            else
            {
                this.Text = mensaje;
            }
        }

        private void terminarConexion()
        {
            if (lecturaThread != null)
            {
                try
                {
                    escritor.Write("CLIENTE>>> TERMINAR CONEXIÓN");
                }
                catch (Exception) { }
                MostrarMensaje("\r\nCLIENTE>>> Conexión Terminada\r\n");
                HabilitarDesconectar(false);
                HabilitarEnviar(false);
                lector.Close();
                escritor.Close();
                salida.Close();
                CambiarNombreVentana(this.Text.Substring(0, 22));
                HabilitarConectar(true);
            }
        }

        private void terminarThread(Thread thread)
        { 
            if (thread != null && thread.IsAlive)
            {
                thread.Join();
            }
        }

        private void iniciarConexion()
        {
            if (lecturaThread == null)
            {
                conectado = true;
                threadRunning = true;
                ConfiguracionesForm configTcpServ = new ConfiguracionesForm("TCP", "Client");
                configTcpServ.ShowDialog();
                lecturaThread = new Thread(new ThreadStart(EjecutarCliente));
                lecturaThread.Name = "lecturaThread";
                lecturaThread.Start();
            }
            else if (threadRunning)
            {
                threadRunning = false;
                lecturaThread.Join();
                lecturaThread = null;
                iniciarConexion();
            }
            else
            {
                MessageBox.Show("Usted ya está intentando conectarse a: " + VariablesDefaultChat.TCP_Client_IP + ":" + VariablesDefaultChat.TCP_Client_Port,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }


        private void btnConectar_Click(object sender, EventArgs e)
        {
            iniciarConexion();
        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            conectado = false;
            threadRunning = false;
            terminarConexion();
        }
    }
}
