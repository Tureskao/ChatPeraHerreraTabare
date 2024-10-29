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
    public partial class UDP_ClienteChatForm : Form
    {
        public UDP_ClienteChatForm()
        {
            InitializeComponent();
        }

        private UdpClient cliente;
        private IPEndPoint puntoRecepcion;

        private void UDP_ClienteChatForm_Load(object sender, EventArgs e)
        {
            IPAddress clientIP = IPAddress.Parse(VariablesDefaultChat.UDP_Client_IP);
            int clientPort = int.Parse(VariablesDefaultChat.UDP_Client_Port);

            puntoRecepcion = new IPEndPoint(clientIP, clientPort);
            cliente = new UdpClient(clientPort);

            Thread subproceso = new Thread(new ThreadStart(EsperarPaquetes));
            subproceso.Start();
        } //fin de la asignación de puertos mediante nuestra variable


      

        private void ServidorPaquetesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);
        }//fin del proceso de la aplicación
         //y libera todos los recursos utilizados por ella,
         //incluyendo los sockets.

       
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

        private void entradaTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                entradaTextBox.KeyDown += entradaTextBox_KeyDown; //ver si esta bien

                string paquete = entradaTextBox.Text;
                mostrarTextBox.Text += "\r\nEnviado paquete que contiene: " + paquete;

                byte[] datos = System.Text.Encoding.ASCII.GetBytes(paquete);

                // Utiliza las variables de la clase para obtener la IP y puerto del servidor
                IPAddress serverIP = IPAddress.Parse(VariablesDefaultChat.UPD_Server_IP);
                int serverPort = int.Parse(VariablesDefaultChat.UDP_Server_Port);

                cliente.Send(datos, datos.Length, serverIP.ToString(), serverPort);
                mostrarTextBox.Text += "\r\nPaquete enviado\r\n";
                entradaTextBox.Clear();

            }

        }
        public void EsperarPaquetes()
        {
            try
            {
                while (true)
                {
                    byte[] datos = cliente.Receive(ref puntoRecepcion);
                    string mensaje = System.Text.Encoding.ASCII.GetString(datos);
                    MostrarMensaje($"\r\nPaquete recibido:\r\nLongitud: {datos.Length}\r\nContenido: {mensaje}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al recibir paquete: " + ex.Message);
            }
        }

        /*
        public void EsperarPaquetes()
        {
            while (true)
            {
                byte[] datos = cliente.Receive(ref puntoRecepcion);
                MostrarMensaje("\r\nPaquete recibido:" +
                "\r\nLongitud: " + datos.Length +
                "\r\nContenido: " + System.Text.Encoding.ASCII.GetString(datos));
            }
        }
        */
        private void UDP_ClienteChatForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
