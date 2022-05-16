/*Answering support questions Application
 * Name: SocketListener.cs
 * 
 * Written by: Tobi Akinnola and Chigozie Muonagolu
 * 
 * Purpose: To store the application server functions
 * 
 * Uasage: call in the main_program
 * 
 * Description of parameters: none
 * 
 * Namespaces required: See below
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SupportQuestionsApplication
{
    public class SocketListener
    {
        //Creates a listening socket for the sender socket to connect to
        //Gets the host info, so the IP address can be retrived
        static IPHostEntry _hostInfo = Dns.GetHostEntry(Dns.GetHostName());

        //Gets the host IP addressss from the Host Info
        static IPAddress IpAddress = _hostInfo.AddressList[0];
        //Using the IP Address from the host information and a port number, establishes an end point
        static IPEndPoint localEndPoin = new IPEndPoint(IpAddress, 11000);

        bool keepRunning = true;

        //Creates a listener Socket
        static Socket listener = new Socket(IpAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        // Bind the socket to the local endpoint and
        internal Socket startListening()
        {
            // listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoin);
                listener.Listen(10);
                return listener;
            }
            catch (SocketException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}