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
        puntoRecepcion = new IPEndPoint(new IPAddress(0), 0);
        cliente = new UdpClient(50001);
        Thread subproceso =
            new Thread(new ThreadStart(EsperarPaquetes));
        subproceso.Start();
    }

    private void ServidorPaquetesForm_FormClosing(object sender, FormClosingEventArgs e)
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

    private void entradaTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            string paquete = entradaTextBox.Text;
            mostrarTextBox.Text +=
                "\r\nEnviado paquete que contiene: " + paquete;

            byte[] datos = System.Text.Encoding.ASCII.GetBytes(paquete);

            cliente.Send(datos, datos.Length, "127.0.0.1", 50000);
            mostrarTextBox.Text += "\r\nPaquete enviado\r\n";
            entradaTextBox.Clear();
        }

    }

    public void EsperarPaquetes()
    {
        while (true)
        {
            byte[] datos = cliente.Receive(ref puntoRecepcion);
            MostrarMensaje("\r\nPaquete recibido:" +
            "\r\nLongitud: " + datos.Length +
            "\r\nContenido: " + System.Text.Encoding.ASCII.GetString(datos))
        }
    }
}
}
