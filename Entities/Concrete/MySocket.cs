﻿using System.IO;
using System.Net.Sockets;

namespace Entities.Concrete
{
    public class MySocket
    {
        public TcpClient tcpClient;
        public TcpListener tcpListener;
        public Socket socket;
        public NetworkStream networkStream;
        public StreamReader streamReader;
        public StreamWriter streamWriter;
    }
}