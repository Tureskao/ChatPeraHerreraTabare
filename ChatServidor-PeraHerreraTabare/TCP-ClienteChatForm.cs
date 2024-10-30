
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
using System.Drawing.Text;

namespace ChatServidor_PeraHerreraTabare
{
    public partial class TCP_ClienteChatForm : Form
    {
        public TCP_ClienteChatForm()
        {
            InitializeComponent();
        }

        static private bool conectado; // Bandera bool que señaliza si estamos conectados
        static private bool intentandoConectar; // Bandera bool que señaliza si estamos intentando conectarnos
        private NetworkStream salida; // Flujo para recibir datos
        private BinaryWriter escritor; // Facilita la escritura en el flujo
        private BinaryReader lector; // Facilita la lectura en el flujo
        private Thread lecturaThread; // Thread para procesar mensajes entrantes
        private string mensaje = "";
        private CancellationTokenSource cts = new CancellationTokenSource();


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

            TcpClient cliente = new TcpClient();
            intentandoConectar = true;
            conectado = false;
                try
                {
                    // Paso 1
                    
                        try
                        {
                            cliente.Connect(VariablesDefaultChat.TCP_Client_IP, int.Parse(VariablesDefaultChat.TCP_Client_Port));
                            conectado = true;
                            intentandoConectar = false;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("No pudo conectarse a: " + VariablesDefaultChat.TCP_Client_IP + ":" + VariablesDefaultChat.TCP_Client_Port, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            intentandoConectar = false;
                            conectado = false;
                            terminarHiloCliente(cts);
                        }
                    
                    // Paso 2
                    if (conectado)
                    {
                        salida = cliente.GetStream();

                        escritor = new BinaryWriter(salida);
                        lector = new BinaryReader(salida);

                        MostrarMensaje("\r\nSe recibieron flujos de E/S\r\n");
                        HabilitarEnviar(true);
                        HabilitarConectar(false);
                        HabilitarDesconectar(true);

                      // Paso 3
                    while (conectado && cliente.Connected)
                    {
                            do
                            {
                                try
                                {
                                    mensaje = lector.ReadString();
    
                                    if (mensaje == "SERVIDOR>>> TERMINAR CONEXIÓN")
                                    {
                                        conectado = false;
                                        terminarConexion();
                                        break;
                                    }
                                    else
                                    {
                                        MostrarMensaje("\r\n" + mensaje);
                                    }
                                }
                                catch (Exception)
                                {
                                    conectado = false;
                                    break;
                                }   
                            } while (conectado);

                        }
                    }
                        // Paso 4
                        try
                        {
                            lector?.Close();
                            escritor?.Close();
                            salida?.Close();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Se produjo un error al terminar la conexión: " + e);
                        }
                        cliente?.Close();
                        MostrarMensaje("\r\nSe terminó la conexión\r\n");
                        HabilitarEnviar(false);
                        cliente?.Dispose();
                        conectado = false;
                        return;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.ToString(), "Error en la conexión",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    HabilitarEnviar(false);
                    HabilitarDesconectar(false);
                    HabilitarConectar(true);
                    terminarHiloCliente(cts);
                }
            
        }

        private void terminarHiloCliente(CancellationTokenSource cts)
        {
            cts.Cancel();
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

            try
            {
                escritor?.Write("CLIENTE>>> TERMINAR CONEXIÓN");
            }
            catch (Exception e)
            {
                Console.WriteLine("Se produjo un error al enviar el mensaje de terminar la conexión: " + e);
            }
            MostrarMensaje("\r\nCLIENTE>>> Conexión Terminada\r\n");
            HabilitarDesconectar(false);
            HabilitarEnviar(false);
            try
            {
                lector?.Close();
                escritor?.Close();
                salida?.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Se produjo un error al terminar la conexión: " + e);
            }
            CambiarNombreVentana(this.Text.Substring(0, 22));
            HabilitarConectar(true);

        }

        private void terminarThread(Thread thread)
        {
            while (thread != null && thread.IsAlive)
            {
                try
                {
                    thread.Interrupt();
                    thread.Join(5000);
                }
                catch (ThreadInterruptedException)
                {
                    try
                    {
                        lector?.Close();
                        lector.Dispose();
                        escritor?.Close();
                        escritor?.Dispose();
                        salida?.Close();
                        salida.Dispose();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Se produjo un error al terminar la conexión: " + e);
                    }
                    thread.Join(5000);
                }
            }
        }

        private void iniciarConexion()
        {
            if (lecturaThread == null)
            {
                intentandoConectar = true;
                ConfiguracionesForm configTcpServ = new ConfiguracionesForm("TCP", "Client");
                configTcpServ.ShowDialog();
                lecturaThread = new Thread(new ThreadStart(EjecutarCliente));
                lecturaThread.Start();
            }
            else if (!conectado)
            {
                intentandoConectar = false;
                lecturaThread.Join();
                lecturaThread = null;
                iniciarConexion();
            }
            else
            {
                DialogResult dr = MessageBox.Show("Usted ya está intentando conectarse a: " + VariablesDefaultChat.TCP_Client_IP + ":" + VariablesDefaultChat.TCP_Client_Port + "\n ¿Desea terminar la conexion?",
                    "Error",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);
                switch (dr)
                {
                    case DialogResult.Yes:
                        lecturaThread.Join();
                        break;
                    case DialogResult.No:
                        break;
                }
            }
        }


        private void btnConectar_Click(object sender, EventArgs e)
        {
            iniciarConexion();
        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            conectado = false;
            intentandoConectar = false;
            terminarHiloCliente(cts);
            terminarConexion();
        }
    }
}
