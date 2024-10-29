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

        private void UDP_ServidorChatForm_Load(object sender, EventArgs e)
        {
            IPAddress serverIP = IPAddress.Parse(VariablesDefaultChat.UPD_Server_IP);
            int serverPort = int.Parse(VariablesDefaultChat.UDP_Server_Port);

            cliente = new UdpClient(serverPort);
            puntoRecepcion = new IPEndPoint(new IPAddress(0), 0);

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
            while (true)
            {
                byte[] datos = cliente.Receive(ref puntoRecepcion);
                MostrarMensaje("\r\nSe recibió paquete:" + "\r\nLongitud: " + datos.Length +
                "\r\nContenido: " +
                System.Text.Encoding.ASCII.GetString(datos));
                MostrarMensaje("\r\n\r\nEnviando de vuelta datos al cliente...");
                cliente.Send(datos, datos.Length, puntoRecepcion);
                MostrarMensaje("\r\nPaquete enviado\r\n");
            }
        }


    }
}
