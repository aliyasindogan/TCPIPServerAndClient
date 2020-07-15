using Entities.Concrete;
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
        private MySocket mySocket = new MySocket();

        public delegate void WriteOnScreenHandler(string message);

        public static event WriteOnScreenHandler WriteOnScreenEvent;

        public async Task<bool> SendMessageAsync(ServerSendMessageRequest sendMessage)

        {
            try
            {
                mySocket.streamWriter = new StreamWriter(mySocket.networkStream);
                await mySocket.streamWriter.WriteLineAsync(sendMessage.SendMessage);
                await mySocket.streamWriter.FlushAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool StartListening(ServerStartListeningRequest request)
        {
            try
            {
                string ipAddress = request.IpAddress;
                IPAddress myIpAddress = IPAddress.Parse(ipAddress);
                mySocket.tcpListener = new TcpListener(myIpAddress, request.Port);
                mySocket.tcpListener.Start();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async void StartReadAsync()
        {
            mySocket.socket = await mySocket.tcpListener.AcceptSocketAsync();
            mySocket.networkStream = new NetworkStream(mySocket.socket);
            mySocket.streamReader = new StreamReader(mySocket.networkStream);
            while (true)
            {
                try
                {
                    string client = "Cilent: ";
                    string text = client;
                    text = await mySocket.streamReader.ReadLineAsync();
                    WriteOnScreenEvent(text);
                }
                catch (Exception ex)
                {
                    WriteOnScreenEvent("Hata: " + ex.Message.ToString());
                }
            }
        }

        //public async Task<bool> StopServer()
        //{
        //    try
        //    {
        //        var task = Task.Run(() =>
        //        {
        //            networkStream.Close();
        //            return true;
        //        });
        //        return await task;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
    }
}