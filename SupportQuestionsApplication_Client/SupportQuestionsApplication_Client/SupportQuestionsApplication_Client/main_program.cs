/*Answering support questions Application
 * Name: main_program.cs
 * 
 * Written by: Tobi Akinnola and Chigozie Muonagolu
 * 
 * Purpose: To bring together the application and its functionalities
 * 
 * Uasage: run this after running the server code
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

namespace SupportQuestionsApplication_Client
{
    public class main_program
    {
        public static void Main(string[] args)
        {
            //Starts an instance of the application and creates connection
            SupportApplication_clinet application=  new SupportApplication_clinet();
            Console.WriteLine("==================================");
            Console.WriteLine("Welcome to our support application");
            Console.WriteLine("==================================");
            string text, response;
            //Main too
            int i = 0;
            while (true) 
            {
                Console.Write("Enter:", i);
                text = Console.ReadLine();
                application.sendQuestion(text);
                response = application.getResponse();
                text = text.ToString();
                if (text == "quit")
                {
                    application.endConnection();
                    break; 
                }
                ++i;
                Console.WriteLine("Reply: {0}", response);
            } 
        }
    }
}
