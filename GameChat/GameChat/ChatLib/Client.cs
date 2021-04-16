using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace GameChat.ChatLib
{
    class Client : NetworkingBase
    {
        override public void Disconnect()
        {
            try
            {
                //If networkStream is not empty, close the networkStream
                if (networkStream != null)
                {
                    networkStream.Close();
                }
                //If tcpClient is not empty, close the tcpClient
                if (tcpClient != null)
                {
                    tcpClient.Close();
                }
            }
            catch (SocketException e)
            {
                throw new Exception("Socket Exception: " + e.Message);
            }
        }

        override public void Start()
        {
            try
            {
                tcpClient = new TcpClient(IPADDRESS, port);
                StartStream();
            }
            catch (SocketException e)
            {
                throw new Exception("Socket Exception: " + e.Message);
            }
        }
    }
}
