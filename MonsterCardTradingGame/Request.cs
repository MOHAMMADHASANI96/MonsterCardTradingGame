using System;
using System.Collections;
using System.IO;

namespace MonsterCardTradingGame
{
    public class Request
    {
        public enum Methode { GET,POST,PUT,DELETE}
        public Methode methode { get; private set; }
        public String path { get; private set; }
        public String httpVersion { get; private set; }
        public Hashtable httpHeaders = new Hashtable();
        public String payload { get; private set; }

        public Methode getMethode()
        {
            return this.methode;
        }
        public Request(StreamReader input)
        {
            // streamReader = header + methode + body + ... 
            try
            {
                StreamReader streamReader = input;
                if(streamReader.EndOfStream)
                    throw new Exception("Request is empty");
                
                String line = streamReader.ReadLine();
                if(String.IsNullOrEmpty(line))
                    throw new Exception("Line is empty");
                // tokens = methode + path + httpVersion
                String[] tokens = line.Split(' ');
                if(tokens.Length !=3)
                    throw new Exception("Invalid http Request Line");

                if(!Enum.TryParse(typeof(Methode),tokens[0],out object? method))
                    throw new Exception("Invalid Methode");

                this.methode = (Methode)method;
                this.path = tokens[1];
                this.httpVersion = tokens[2];

                Console.WriteLine("Methode: " + this.methode );
                Console.WriteLine("Path: " + this.path);
                Console.WriteLine("Http Version: " + this.httpVersion);

                while (!streamReader.EndOfStream && (line = streamReader.ReadLine()!) != "")
                {
                    String[] reqHeaders = line.Split(": ", 2);
                    if(reqHeaders.Length == 2)
                    {
                        this.httpHeaders.Add(reqHeaders[0], reqHeaders[1]);
                    }
                }
                if(this.httpHeaders.ContainsKey("Content-Type") && this.httpHeaders.ContainsKey("Content-Length"))
                {
                    // compare Content_len with payload lenght
                    int content_len = Convert.ToInt32(this.httpHeaders["Content-Length"]);
                    char[] buf = new char[content_len];
                    int hasRead = input.Read(buf, 0, content_len);
                    if(hasRead !=content_len)
                        throw new Exception("Payload was not to expected Length");
                    this.payload = new String(buf);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
            }
        }
        public bool isValid()
        {
            if (this.methode != null && this.path != null && this.httpVersion != null)
                return true;
            return false;
        }
    }
}
