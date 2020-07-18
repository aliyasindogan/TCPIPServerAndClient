﻿using Entities.Concrete;
using Entities.Concrete.Request;
using Services.Abstract;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class ClientManager : IClientService
    {
        private MySocket clientSocket = new MySocket();

        public delegate void WriteOnScreenHandler(string message);

        public static event WriteOnScreenHandler WriteOnScreenEvent;

        public async Task ConnectionAndStartReadyAsync(BaseRequest baseRequest)
        {
            clientSocket.tcpClient = new TcpClient(baseRequest.IpAddress, baseRequest.Port);
            clientSocket.networkStream = clientSocket.tcpClient.GetStream();
            clientSocket.streamReader = new StreamReader(clientSocket.networkStream);
            while (true)
            {
                try
                {
                    string client = "Server: ";
                    string text = client;
                    text = await clientSocket.streamReader.ReadLineAsync();
                    WriteOnScreenEvent(text);
                }
                catch (Exception ex)
                {
                    WriteOnScreenEvent("Hata: " + ex.Message.ToString());
                }
            }
        }

        public async Task<bool> SendMessageAsync(ClientSendMessageRequest sendMessage)
        {
            try
            {
                clientSocket.streamWriter = new StreamWriter(clientSocket.networkStream);
                await clientSocket.streamWriter.WriteLineAsync(sendMessage.SendMessage);
                await clientSocket.streamWriter.FlushAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}