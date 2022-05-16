/*Answering support questions Application
 * Name: main_program.cs
 * 
 * Written by: Tobi Akinnola and Chigozie Muonagolu
 * 
 * Purpose: To start running the applications server
 * 
 * Uasage: run this after before the client code
 * 
 * Description of parameters: none
 * 
 * Namespaces required: See below
*/
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SupportQuestionsApplication
{
    public class main_program
    {
        private static string fileName = "Responses.json";
        private static ChatResponsePackets packet;
        private static SupportApplication application;
        private static SocketListener socketListener;
        public static void Main()
        {
            //sets up a connection for the client to connect to
            socketListener = new SocketListener();
            Socket listener = socketListener.startListening();
            //Creates an object of the SupportApplication class
            application = new SupportApplication(listener);
            int i = 0;
            string userInput="";
            while (userInput.ToLower()!="quit")
            {
                userInput=application.GetUserQuestion();
                Console.WriteLine("Clinet: " + userInput);
                application.SendResponse();
            }
            application.Quit();
        }
    }
}
