﻿using Entities.Concrete;
using Entities.Concrete.Request;
using Services.Abstract;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class ServerManager : IServerService
    {
        private MySocket serverSocket = new MySocket();

        public delegate void WriteOnScreenHandler(string message);

        public static event WriteOnScreenHandler WriteOnScreenEvent;

        public async Task<bool> SendMessageAsync(ServerSendMessageRequest sendMessage)
        {
            try
            {
                serverSocket.streamWriter = new StreamWriter(serverSocket.networkStream);
                await serverSocket.streamWriter.WriteLineAsync(sendMessage.SendMessage);
                await serverSocket.streamWriter.FlushAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool StartListening(BaseRequest request)
        {
            try
            {
                string ipAddress = request.IpAddress;
                IPAddress myIpAddress = IPAddress.Parse(ipAddress);
                serverSocket.tcpListener = new TcpListener(myIpAddress, request.Port);
                serverSocket.tcpListener.Start();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task StartReadAsync()
        {
            serverSocket.socket = await serverSocket.tcpListener.AcceptSocketAsync();
            serverSocket.networkStream = new NetworkStream(serverSocket.socket);
            serverSocket.streamReader = new StreamReader(serverSocket.networkStream);
            while (true)
            {
                try
                {
                    string client = "Cilent: ";
                    string text = client;
                    text = await serverSocket.streamReader.ReadLineAsync();
                    WriteOnScreenEvent(text);
                }
                catch (Exception ex)
                {
                    WriteOnScreenEvent("Exception: " + ex.Message.ToString());
                }
            }
        }

        public bool StopServer()
        {
            try
            {
                serverSocket.networkStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}