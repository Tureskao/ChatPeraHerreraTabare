using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ChatServidor_PeraHerreraTabare
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Socket conexion; //Socket para aceptar una conexion
        private Thread lecturaThread; //Thread para procesar los mensajes entrantes
        private NetworkStream socketStream; //flujo de datos de red
        private BinaryWriter escrito; //facilita la escritura en el flujo
        private BinaryReader lector; //facilita la lectra de flujo
        private void Form1_Load(object sender, EventArgs e)
        { //inicializa el subproceso para la lectura
            lecturaThread = new Thread(new ThreadStart(EjecutarServidor));
            lecturaThread.Start();
        }
        private void
    }
}
