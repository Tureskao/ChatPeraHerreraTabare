using System;
using System.Collections.Generic;
using System.Text;

namespace ChatServidor_PeraHerreraTabare
{
    static class VariablesDefaultChat
    {
        public static string TCP_Server_IP { get; set; } = "127.0.0.1";
        public static string UPD_Server_IP { get; set; } = "127.0.0.1";
        public static string TCP_Client_IP { get; set; } = "127.0.0.1";
        public static string UDP_Client_IP { get; set; } = "127.0.0.1";

        public static string TCP_Server_Port { get; set; }  = "50000";
        public static string UDP_Server_Port { get; set; }  = "50001";
        public static string TCP_Client_Port { get; set; }  = "50000";
        public static string UDP_Client_Port { get; set; }  = "50001";

        static VariablesDefaultChat() {
        TCP_Server_IP = "127.0.0.1";
        UPD_Server_IP = "127.0.0.1";
        TCP_Client_IP = "127.0.0.1";
        UDP_Client_IP = "127.0.0.1";

        TCP_Server_Port = "50000";
        UDP_Server_Port = "50001";
        TCP_Client_Port = "50000";
        UDP_Client_Port = "50000";
        }

       

    }
}
