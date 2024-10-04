using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace ChatServidor_PeraHerreraTabare
{
    public partial class ConfiguracionesForm : Form
    {
        private string propiedadIP;
        private string propiedadPort;
        public ConfiguracionesForm(string Protocolo, string Modo)
        {
            InitializeComponent();
            propiedadIP = Protocolo + "_" + Modo + "_IP";
            propiedadPort = Protocolo + "_" + Modo + "_Port";
            lbl_TipoChat.Text = Modo + " " + Protocolo;
            lbl_PropiedadPort.Text = propiedadIP;
            lbl_PropPort.Text = propiedadPort;
            txb_DireccionIp.Text = VariablesDefaultChat.TCP_Server_IP;
            txb_Puerto.Text = leerConfiguracion(propiedadPort);
        }

        private string leerConfiguracion(string nombreConfiguracion)
        {
            string respuesta = "";
            Type tipoConfiguracion = typeof(VariablesDefaultChat);
            PropertyInfo propiedad = tipoConfiguracion.GetProperty(nombreConfiguracion);
            if (propiedad != null)
            {
                respuesta = (string)propiedad.GetValue(null);
            }
            return respuesta;

        }

        private void actualizarConfiguracion(string nombreConfiguracion, string nuevoValor)
        {
            Type tipoConfiguracion = typeof(VariablesDefaultChat);
            PropertyInfo propiedad = tipoConfiguracion.GetProperty(nombreConfiguracion);
            if (propiedad != null)
            {
                propiedad.SetValue(null, nuevoValor);
            }
        }

        private void ConfiguracionesForm_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Validadores.ValidarDireccionIP(txb_DireccionIp.Text))
            {
                actualizarConfiguracion(propiedadIP, txb_DireccionIp.Text);
            } else
            {
                MessageBox.Show("Debe colocar una dirección IP correcta; con 4 valores entre 0 a 255 separados por puntos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (Validadores.ValidarPuerto(txb_Puerto.Text))
            {
                actualizarConfiguracion(propiedadPort, txb_Puerto.Text);
            } else
            {
                MessageBox.Show("Debe colocar un valor de Puerto correcto; entre 0 y 65535", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
