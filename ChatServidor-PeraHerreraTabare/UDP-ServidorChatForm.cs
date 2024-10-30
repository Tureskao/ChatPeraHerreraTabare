using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace ChatServidor_PeraHerreraTabare
{
    public partial class UDP_ServidorChatForm : Form
    {
        public UDP_ServidorChatForm()
        {
            InitializeComponent();
        }

        private UdpClient cliente; //clase que maneja la comunicación a través de UDP. Se encarga de enviar y recibir los paquetes.
        private IPEndPoint puntoRecepcion; //almacenar la dirección IP y el número de puerto de una máquina
        private bool conectado; // Bandera que marca que seguimos esperando paquetes

        private void UDP_ServidorChatForm_Load(object sender, EventArgs e)
        {
            ConfiguracionesForm configTcpServ = new ConfiguracionesForm("UDP", "Server");
            configTcpServ.ShowDialog(); 

            IPAddress serverIP = IPAddress.Parse(VariablesDefaultChat.UPD_Server_IP);
            int serverPort = int.Parse(VariablesDefaultChat.UDP_Server_Port);

            cliente = new UdpClient(serverPort);
            puntoRecepcion = new IPEndPoint(new IPAddress(0), 0);

            conectado = true;

            Thread lecturaThread = new Thread(new ThreadStart(EsperarPaquetes));
            lecturaThread.Start();
        }

        private void UDP_ServidorChatForm_FormClosing(object sender, FormClosingEventArgs e)
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

        public void EsperarPaquetes()
        {
            MostrarMensaje("Servidor iniciado, esperando mensajes de clientes\r\n Se enviará un eco del mensaje recibido al cliente");
            while (conectado)
            {
                byte[] datos = cliente.Receive(ref puntoRecepcion);
                string mensaje = System.Text.Encoding.ASCII.GetString(datos);
                MostrarMensaje("\r\nSe recibió paquete:" + "\r\nLongitud: " + datos.Length +
                "\r\nContenido: " + mensaje);
                if(mensaje == "CLIENTE>>> Terminar conexion")
                {
                    conectado = false;
                    break;
                }
                MostrarMensaje("\r\n\r\nEnviando de vuelta datos al cliente...");
                cliente.Send(datos, datos.Length, puntoRecepcion);
                MostrarMensaje("\r\nPaquete enviado\r\n");
            }
            MostrarMensaje("\r\n\r\nSe terminó la conexión con el cliente, cerrando el programa...");
            Thread.Sleep(5000);
            System.Environment.Exit(System.Environment.ExitCode);
        }
    }
}
