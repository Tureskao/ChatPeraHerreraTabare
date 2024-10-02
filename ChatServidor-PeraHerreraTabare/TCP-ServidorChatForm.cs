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
        //Constructor vacío
        public TCP_ServidorChatForm()
        {
            InitializeComponent();
        }

        private Socket conexion; //Socket para aceptar una conexión
        private Thread lecturaThread; // Thread para procesar los mensajes entrantes
        private NetworkStream socketStream; //Flujo para datos de la red
        private BinaryWriter escritor; //Facilita la escritura en el flujo
        private BinaryReader lector; //Facilita la lectura del flujo

        // Inicializa el hilo para la lectura
        private void TCP_ServidorChatForm_Load(object sender, EventArgs e)
        {
            lecturaThread = new Thread(new ThreadStart(EjecutarServidor));
            lecturaThread.Start();
        }

        // Cierra todos los subprocesos asociados con esta aplicacion
        private void TCP_ServidorChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
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

        
        // Delegado que permite llamar al método DeshabilitarSalida
        // en el subproceso que crea y mantiene la GUI
        private delegate void DisableInputDelegate(bool value);

        // El método DeshabilitarEntrada establece la propiedad ReadOnly de entradaTextBox
        // de una manera segura para los subprocesos
        private void DeshabilitarEntrada(bool valor)
        {
            if (entradaTextBox.InvokeRequired)
            {
                Invoke(new DisableInputDelegate(DeshabilitarEntrada),
                    new object[] { valor });
            }

            else
            {
                entradaTextBox.ReadOnly = valor;
            }
        }

    // Envía al cliente el texto escrito en el Servidor
    private void entradaTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        // Envía el texto al Cliente
        try
        {
            if (e.KeyCode == Keys.Enter && entradaTextBox.ReadOnly == false)
            {
                escritor.Write("SERVIDOR>>> " + entradaTextBox.Text);
                mostrarTextBox.Text += "\r\nSERVIDOR>>> " + entradaTextBox.Text;
                if (entradaTextBox.Text == "TERMINAR")
                {
                    conexion.Close();
                }
                entradaTextBox.Clear();
            }
        }
        catch (SocketException)
        {
            mostrarTextBox.Text += "\nError al escribir objeto";
        }
    }

    public void EjecutarServidor()
    {
        TcpListener oyente;
        int contador = 1;
        try
        {
            // Paso 1
            IPAddress local = IPAddress.Parse("127.0.0.1");
            oyente = new TcpListener(local, 50000);

            // Paso 2
            oyente.Start();

            // Paso 3
            while (true)
            {
                MostrarMensaje("Esperando una conexión \r\n");

                conexion = oyente.AcceptSocket();
                socketStream = new NetworkStream(conexion);
                escritor = new BinaryWriter(socketStream);
                lector = new BinaryReader(socketStream);

                MostrarMensaje("Conexion " + contador + " recibida.\r\n");

                escritor.Write("SERVIDOR>>> Conexión exitosa");

                DeshabilitarEntrada(false); //habilita entradaTextBox

                string laRespuesta = "";

                // Paso 4
                do
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
                } while (laRespuesta != "CLIENTE>>> TERMINAR" && conexion.Connected);

                MostrarMensaje("\r\nEl usuario terminó la conexión\r\n");

                //Paso 5
                escritor.Close();
                lector.Close();
                socketStream.Close();
                conexion.Close();

                DeshabilitarEntrada(true);
                contador++;
            }
        }
        catch (Exception error)
        {
            MessageBox.Show(error.ToString());
        }
    }
}

}
