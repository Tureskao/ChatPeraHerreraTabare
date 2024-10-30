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
        private bool conectado; // Bandera que marca si estamos conectados
        private Thread subproceso;

        private void UDP_ClienteChatForm_Load(object sender, EventArgs e)
        {
            ConfiguracionesForm configTcpServ = new ConfiguracionesForm("UDP", "Client");
            configTcpServ.ShowDialog();

            IPAddress clientIP = IPAddress.Parse(VariablesDefaultChat.UDP_Client_IP);
            int clientPort = int.Parse(VariablesDefaultChat.UDP_Client_Port);

            puntoRecepcion = new IPEndPoint(clientIP, clientPort);
            cliente = new UdpClient(clientPort);

            subproceso = new Thread(new ThreadStart(EsperarPaquetes));
            subproceso.Start();
        } //fin de la asignación de puertos mediante nuestra variable


      

        private void ServidorPaquetesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (subproceso.IsAlive)
            {
                conectado = false;
                subproceso.Join();
            }
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

        private void ClearInput(string Mensaje)
        {
            if (mostrarTextBox.InvokeRequired)
            {
                Invoke(new DisplayDelegate(ClearInput),
                new object[] {Mensaje});
            }
            else
            {
                mostrarTextBox.Text = "";
            }
        }


        // Delegado que permite llamar al método DeshabilitarSalida
        // en el subproceso que crea y mantiene la GUI
        private delegate void EnableButtonDelegate(bool value);

        // El método Deshabilitar Entrada establece la propiedad ReadOnly de entradaTextBox
        // de una manera segura para los subprocesos
        private void HabilitarEnviar(bool valor)
        {
            if (btnEnviar.InvokeRequired)
            {
                Invoke(new EnableButtonDelegate(HabilitarEnviar),
                    new object[] { valor });
            }

            else
            {
                btnEnviar.Enabled = valor;
            }
        }

        /*
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
        */
        public void EsperarPaquetes()
        {
            MostrarMensaje("Cliente iniciado, se inicia un intento de conexión al servidor en: " + VariablesDefaultChat.UDP_Client_IP + ":" + VariablesDefaultChat.TCP_Client_Port);
            try
            {
                conectado = iniciarConexion();
                while (conectado)
                {
                    byte[] datos = cliente.Receive(ref puntoRecepcion);
                    string mensaje = System.Text.Encoding.ASCII.GetString(datos);
                    MostrarMensaje($"\r\nPaquete recibido:\r\nLongitud: {datos.Length}\r\nContenido: {mensaje}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al recibir paquete: " + ex.Message);
                HabilitarEnviar(false);
            }
            HabilitarEnviar(false);
            return;
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
        
        private void btnEnviar_Click(object sender, EventArgs e)
        { 

            string paquete = "CLIENTE>>> " + entradaTextBox.Text;
            if(paquete == "CLIENTE>>> Terminar conexion")
            {
                conectado = false;
            }
            MostrarMensaje("\r\nEnviado paquete que contiene: " + paquete);

            byte[] datos = System.Text.Encoding.ASCII.GetBytes(paquete);

            // Utiliza las variables de la clase para obtener la IP y puerto del servidor
            IPAddress serverIP = IPAddress.Parse(VariablesDefaultChat.UPD_Server_IP);
            int serverPort = int.Parse(VariablesDefaultChat.UDP_Server_Port);

            cliente.Send(datos, datos.Length, serverIP.ToString(), serverPort);
            MostrarMensaje( "\r\nPaquete enviado\r\n");
            ClearInput("");
        }

        private bool iniciarConexion()
        {
            bool conexionExitosa = false;
            try
            {
                string paquete = "CLIENTE>>> Iniciar Conexion";
                MostrarMensaje("\r\nEnviado paquete para iniciar la conexión: " + paquete);

                byte[]  datos = System.Text.Encoding.ASCII.GetBytes(paquete);

                // Utiliza las variables de la clase para obtener la IP y puerto del servidor
                IPAddress serverIP = IPAddress.Parse(VariablesDefaultChat.UPD_Server_IP);
                int serverPort = int.Parse(VariablesDefaultChat.UDP_Server_Port);

                cliente.Send(datos, datos.Length, serverIP.ToString(), serverPort);
                MostrarMensaje( "\r\nPaquete enviado\r\n");
                ClearInput("");
                conexionExitosa = true;
                HabilitarEnviar(true);

            } 
            catch (Exception)
            {
                    MessageBox.Show("No pudo conectarse a: " + VariablesDefaultChat.UDP_Client_IP + ":" + VariablesDefaultChat.UDP_Client_Port,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            return conexionExitosa;
        }
    }
}
