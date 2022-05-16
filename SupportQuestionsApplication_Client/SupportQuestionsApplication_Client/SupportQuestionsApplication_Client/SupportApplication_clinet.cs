/*Answering support questions Application
 * Name: SupportApplication_clinet.cs
 * 
 * Written by: Tobi Akinnola and Chigozie Muonagolu
 * 
 * Purpose: To help send information to the server and display messages from the server
 * 
 * Uasage: none
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
namespace SupportQuestionsApplication_Client
{
    public class SupportApplication_clinet
    {

        socketSender_client Connection;
        Socket sender;
        byte[] bytes;
        //Default supportApplication
        public SupportApplication_clinet()
        {
            this.Connection = new socketSender_client();
            this.bytes = new byte[1024];
            //Starts a connection when the constructor is called
            sender = Connection.startSending();
        }
        //Gets response from the server
        public string getResponse()
        {
            bool moreData = true;
            string response = "";
            while (moreData)
            {
                // Receive the response from the remote device.  
                int receivedBytes = sender.Receive(bytes);
                response += Encoding.ASCII.GetString(bytes, 0, receivedBytes);
                if (response.IndexOf('.') > -1)
                {
                    moreData = false;
                }
            }
            return response;
        }
        //Terminates a connection
        public void endConnection()
        {
            sender.Close();
        }
        //Sends questions to the server
        public void sendQuestion(string question)
        {
                question += "?";
            // Encode the data string into a byte array for seding across the network.  
            byte[] msg;

            msg = Encoding.ASCII.GetBytes(question);
            sender.Send(msg);
        }
    }
}
