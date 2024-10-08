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
    public partial class TCP_ServidorChatForm : Form
    {
        private ConfiguracionesForm configTcpServ;

        //Constructor vacío
        public TCP_ServidorChatForm()
        {
            InitializeComponent();
        }

        private Socket conexion; //Socket para aceptar una conexión
        private static bool conectado; // Booleano que marca que estamos conectados
        private Thread lecturaThread; // Thread para procesar los mensajes entrantes
        private NetworkStream socketStream; //Flujo para datos de la red
        private BinaryWriter escritor; //Facilita la escritura en el flujo
        private BinaryReader lector; //Facilita la lectura del flujo
        private int contador = 1;

        // Inicializa el hilo para la lectura
        /*
        private void TCP_ServidorChatForm_Load(object sender, EventArgs e)
        {
            ConfiguracionesForm configTcpServ = new ConfiguracionesForm("TCP", "Server");
            configTcpServ.ShowDialog();
            if (lecturaThread == null)
            {
                lecturaThread = new Thread(new ThreadStart(EjecutarServidor));
            }
            lecturaThread.Start();
        }
        */

        // Cierra todos los subprocesos asociados con esta aplicacion
        private void TCP_ServidorChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (escritor != null && lector != null)
                {
                    escritor.Close();
                    lector.Close();
                }
                if (socketStream != null)
                {
                    socketStream.Close();
                }
                if (conexion != null)
                {

                    conexion.Shutdown(SocketShutdown.Both);
                    conexion.Close();
                    conexion = null;
                }
                if (lecturaThread != null)
                {
                    lecturaThread.Join();
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        // Delegado que permite que se haga una llamada al método MostrarMensaje en el hilo
        // que crea y mantiene la GUI
        private delegate void displayDelegate(string mensaje);

        // El método DisplayDelegate establece la propiedad Text de mostrarTextBox
        // en forma segura para los subprocesos
        private void MostrarMensaje(string mensaje)
        {
            if (mostrarTextBox.InvokeRequired)
            {
                Invoke(new displayDelegate(MostrarMensaje),
                    new object[] { mensaje });
            }
            else
            {
                mostrarTextBox.Text += mensaje;
            }
        }

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


        // Delegado que permite llamar al método DeshabilitarSalida
        // en el subproceso que crea y mantiene la GUI
        private delegate void EnableButtonDelegate(bool value);

        // El método Deshabilitar Entrada establece la propiedad ReadOnly de entradaTextBox
        // de una manera segura para los subprocesos
        private void HabilitarEnviar(bool valor)
        {
            if (entradaTextBox.InvokeRequired)
            {
                Invoke(new EnableButtonDelegate(HabilitarEnviar),
                    new object[] { valor });
            }

            else
            {
                btnEnviar.Enabled = valor;
            }
        }

        private void HabilitarDesconectar(bool valor)
        {
            if (entradaTextBox.InvokeRequired)
            {
                Invoke(new EnableButtonDelegate(HabilitarDesconectar),
                    new object[] { valor });
            }

            else
            {
                btnDesconectar.Enabled = valor;
            }
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                if (entradaTextBox.ReadOnly == false)
                {
                    escritor.Write("SERVIDOR>>> " + entradaTextBox.Text);
                    mostrarTextBox.Text += "\r\nSERVIDOR>>> " + entradaTextBox.Text;
                    entradaTextBox.Clear();
                }
            }
            catch (SocketException)
            {
                mostrarTextBox.Text += "\nError al escribir objeto";
            }
        }

        private void configurarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            configTcpServ.ShowDialog();
        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            if (conexion != null)
            {
                escritor.Write("SERVIDOR>>> TERMINAR CONEXIÓN");
                MostrarMensaje("\r\nSERVIDOR>>> Conexión Terminada\r\n");
                btnDesconectar.Enabled = false;
                btnEnviar.Enabled = false;
                lector.Close();
                escritor.Close();
                socketStream.Close();
                conexion.Shutdown(SocketShutdown.Both);
                CambiarNombreVentana(this.Text.Substring(0,22));
            }
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {
            iniciarConexion();
        }

        private void iniciarConexion()
        {
            if (lecturaThread == null)
            {
                conectado = true;
                ConfiguracionesForm configTcpServ = new ConfiguracionesForm("TCP", "Server");
                configTcpServ.ShowDialog();
                lecturaThread = new Thread(new ThreadStart(EjecutarServidor));
                lecturaThread.Start();
            }
            else if (!conectado)
            {
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

        public void EjecutarServidor()
        {
            TcpListener oyente;
            try
            {
                
                    // Paso 1
                    IPAddress local = IPAddress.Parse(VariablesDefaultChat.TCP_Server_IP);
                    oyente = new TcpListener(local, int.Parse(VariablesDefaultChat.TCP_Server_Port));

                    // Paso 2
                    oyente.Start();

                    // Paso 3
                while (true && conectado)
                {

                    MostrarMensaje("Esperando una conexión \r\n");
                    conexion = oyente.AcceptSocket();
                    socketStream = new NetworkStream(conexion);
                    escritor = new BinaryWriter(socketStream);
                    lector = new BinaryReader(socketStream);

                    MostrarMensaje("Conexion " + contador + " recibida.\r\n");

                    escritor.Write("SERVIDOR>>> Conexión exitosa");
                    CambiarNombreVentana(this.Text + " - Conectado a" + VariablesDefaultChat.TCP_Client_IP + ":" + VariablesDefaultChat.TCP_Client_Port);
                    conectado = true;

                    HabilitarDesconectar(true);
                    HabilitarEnviar(true); //habilita entradaTextBox

                    string laRespuesta = "";

                    // Paso 4

                    while (conexion != null && conexion.Connected)
                    {
                        try
                        {
                            laRespuesta = lector.ReadString();
                            MostrarMensaje("\r\n" + laRespuesta);
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }

                    oyente.Stop();
                    MostrarMensaje("\r\nSe terminó la conexión\r\n");
                    contador++;
                    conectado = false;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }
    }

}
