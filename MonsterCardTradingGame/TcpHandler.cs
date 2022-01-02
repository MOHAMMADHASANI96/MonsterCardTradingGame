using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame
{
    public class TcpHandler
    {
        private TcpClient socket;
        public TcpHandler(TcpClient socket)
        {
            this.socket = socket;
        }
        // request and response
        public void process()
        {
            TcpClient client = socket;
            try
            {
                Console.WriteLine("-------opend-------");
                // take request -> seprate information 
                Request request = new Request(new StreamReader(client.GetStream()));

                Response response;
                try
                {
                    response = Program.endpointController.getResponse(request);

                }
                catch (Exception exception)
                {
                    Console.WriteLine("Error:" + exception.Message);
                    response = ResponseHelper.serverError();
                }
                response.Send(new StreamWriter(client.GetStream()));
                Console.WriteLine("-------closed-------");
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
            }

        }
    }
}
