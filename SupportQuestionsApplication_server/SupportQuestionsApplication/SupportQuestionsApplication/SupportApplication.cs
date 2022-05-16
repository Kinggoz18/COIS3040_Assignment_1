/*Answering support questions Application
 * Name: SupportApplication.cs
 * 
 * Written by: Tobi Akinnola and Chigozie Muonagolu
 * 
 * Purpose: To store the application server functions
 * 
 * Uasage: call in main_program
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
    public class SupportApplication
    {
        private Dictionary<int, string> responses;
        private string[] keywords = { "hello", "hi", "most", "rings", "nba","first", "moon", "trent", "university", "located","location", "mean", "meaning", "life", "coffee", "shop", "flying",
            "fly","impossible", "sea", "museum", "party", "prime", "minister", "canada",  "dead" };
        private DesrializeJson data;
        private ChatResponsePackets packets;
        private int listLen;
        private int[] keys;
        //The new socket created after the connection
        Socket handler;

        //Data buffer for incoming data
        byte[] bytes;

        private string userQuestion;

        public SupportApplication(Socket handler)
        {
            this.handler = handler;
            //initializes the following and loads the responses into the list
            this.data = new DesrializeJson();
            this.packets=data.LoadChatResponses();

            this.responses = new Dictionary<int, string>();
            listLen = packets.Responses.Count;
            keys = new int[listLen];
            //Populates the dictionary
            populateDictionary();
            //Starts a connection 
            //sets the timeout to 200000 ms = 3.3 minutes
            handler.ReceiveTimeout = 200000;
            runConnection(handler);
        }
        //Disconnects the servers socket
        public void Quit()
        {
            handler.Close();
            Console.WriteLine("Application ended.");
        }

        //Sends a response to the client program
        public void SendResponse()
        {
            string reply = findResponse(FindKeyWord());
            try
            {
                byte[] msg = Encoding.ASCII.GetBytes(reply);
                handler.Send(msg);
            }
            catch(SocketException ex)
            {
                throw new Exception(ex.ToString());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);  
            }
        }
        //Gets the Users Input from the Client side
        public string GetUserQuestion()
        {
            bool moreData = true;
            bytes = new Byte[1024];
            userQuestion = "";
            try
            {
                while (moreData)
                {
                    int receivedBytes = handler.Receive(bytes);
                    userQuestion += Encoding.ASCII.GetString(bytes, 0, receivedBytes);
                    if (userQuestion.IndexOf('?') > -1 || (userQuestion == "Q"))
                    {
                        moreData = false;
                    }
                }
            }
            catch(SocketException ex)
            {
                if (ex.ErrorCode == 10060)
                {
                    Console.WriteLine("Application timeout! User took too long to reply after, {0} milliseconds", handler.ReceiveTimeout);
                    throw new Exception(ex.ToString());
                }
                else
                    throw new Exception(ex.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return userQuestion;
        }
        //Finds the apropriate response from the dictionary list
        private string findResponse(string key)
        {
           int newKey= hashKey(key);
            int defaultKey = hashKey("");
            if(keys.Contains(newKey))
            {
                return responses[newKey];
            }
            else
            {
                return responses[defaultKey];
            }
        }
        //Finds key words in users input
        private string FindKeyWord()
        {
            try
            {
                string[] splitText = userQuestion.Split(' ', '?');
                int keyIndex = 0, i=0;
                string Key ="";
                foreach (string s in splitText)
                {
                    if (keywords.Contains(s.ToLower())) 
                    {
                        Key += s[i];
                    }
                }
                return Key.ToLower();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        //Starts the connection - called once in constructor 
        private Socket runConnection(Socket listener)
        {
            //Starts the connecttion and the new socket handler is created
            try
            {
                Console.WriteLine("Creating a connection...");
                handler = listener.Accept();
            }
            catch(SocketException ex)
            {
                throw new Exception(ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return handler;
        }
        //Populate Dictionary - called once in constructor 
        private void populateDictionary()
        {
            string current;
            int i = 0;
            string[] words;
            string key;
            //Creates Keywords and uses them to store the values in the dictionary
            while (i < listLen)
            {
                current = packets.Responses[i];
                key = "";
                words = current.Split(' ', '.', '!');
                foreach (string s in words)
                {
                    //If the key word in the response if found add first word to the key at index i to build its key 
                    if(keywords.Contains(s.ToLower()))
                    {
                        key += s[0];
                    }
                }
                keys[i] = hashKey(key.ToLower());
                i += 1;
            }
            //Finally stores the Key and their values in the dictionary
            for (int a = 0; a < listLen; a++)
            {
                current = packets.Responses[a];
                responses.Add(keys[a], current);
            }
        }
        //Hashes the key for the dictionary
        private int hashKey(string key)
        {
            char[] chars = key.ToCharArray();
            int newKey = 0;
            foreach (char c in chars)
            {
                newKey += c.ToString().GetHashCode();
            }
            return newKey;
        }

    }
}
