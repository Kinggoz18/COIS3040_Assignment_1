/*Answering support questions Application
 * Name: socketSender_client.cs
 * 
 * Written by: Tobi Akinnola and Chigozie Muonagolu
 * 
 * Purpose: To help establish a connection to the server program
 * 
 * Uasage: none
 * 
 * Description of parameters: none
 * 
 * Namespaces required: See below
*/
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
namespace SupportQuestionsApplication_Client
{
    public class socketSender_client
    {
        private IPHostEntry ipHostEntry;
        private IPAddress ipAddress;
        private IPEndPoint ipEndPoint;
        private Socket sender;
        //Creates a sender socket for the server
        public Socket startSending()
        {
            ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            ipAddress = ipHostEntry.AddressList[0];
            ipEndPoint= new IPEndPoint(ipAddress, 11000);
            sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //Starts the connection to the remote endpoint
            try
            {
                sender.Connect(ipEndPoint);
                Console.WriteLine("Connection Established to: {0}", sender.RemoteEndPoint.ToString());
                return sender;
            }
            catch(SocketException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

    }
}