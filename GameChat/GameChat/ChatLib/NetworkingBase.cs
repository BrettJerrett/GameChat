using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameChat.ChatLib
{
    public abstract class NetworkingBase
    {
        //Setting common properties to be used for connections.
        public const int PORT = 5555;
        protected Int32 port = PORT;
        public const string IPADDRESS = "127.0.0.1";

        //Setting NetworkStream and TcpClient properties here so they can be accessed by Client and Server via inheritance. 
        protected NetworkStream networkStream = null;
        protected TcpClient tcpClient = null;

        //Abstract methods to start and disconnect that will be overriden by the Client and Server class appropriately
        public abstract void Start();

        public abstract void Disconnect();

        //StartStream method to return the network stream via tcpClient
        public void StartStream()
        {
            networkStream = tcpClient.GetStream();
        }

        //writeStream method that takes in a byte array and writes the data to the NetworkStream
        public void writeStream(Byte[] msgOut)
        {
            try
            {
                networkStream.Write(msgOut, 0, msgOut.Length);
            }
            catch (SocketException e)
            {
                throw new Exception("Socket Exception: " + e.Message);
            }
        }
        //<summary>
        //readBytes method that takes in a byte array and returns the 
        //number of bytes read by reading the networkStream
        //</summary>
        public int readBytes(Byte[] msg)
        {
            try
            {
                return networkStream.Read(msg, 0, msg.Length);
            }
            catch (SocketException e)
            {
                throw new Exception("Socket Exception: " + e.Message);
            }
        }
        //<summary>
        //Boolean function that returns true or false against the .DataAvailable to see if there is data to be read
        //Seems to work easier than with the original while loop against the length of byte read.
        //</summary>
        public bool CheckData()
        {
            try
            {
                return networkStream.DataAvailable;
            }
            catch (SocketException e)
            {
                throw new Exception("Socket Exception " + e.Message);
            }
        }



    }
}

