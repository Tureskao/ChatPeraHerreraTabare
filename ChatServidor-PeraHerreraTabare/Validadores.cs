using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace ChatServidor_PeraHerreraTabare
{
    static class Validadores
    {

        public static bool ValidarDireccionIP(string dirIP)
        {
            try
            {
                IPAddress.Parse(dirIP);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidarPuerto(string port)
        {
            try
            {
                int intPort = int.Parse(port);
                if (intPort > 65535)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

        }
    }
}
