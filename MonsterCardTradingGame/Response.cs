using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame
{
    public class Response
    {
        public enum StatusCode { OK, Not_Found, Bad_Request, Forbidden, Internal_Server_Error }
        public enum ContentType { JSON, PLAIN, HTML }
        public Dictionary<StatusCode, String> status_Code_Value { get; set; }
        public Dictionary<ContentType, String> content_Type_Value { get; set; }
        //------
        public StatusCode statusCode { get; private set; }
        public String payload { get; private set; }
        public String content_type { get; private set; }
        //------
        public Response()
        {
            status_Code_Value = new Dictionary<StatusCode, string>()
            {
                { StatusCode.OK, "200 OK"},
                { StatusCode.Not_Found, "404 Not Found"},
                { StatusCode.Bad_Request, "400 Bad Request"},
                { StatusCode.Forbidden, "403 Forbidden"},
                { StatusCode.Internal_Server_Error, "500 Internal Server Error"}
            };

            content_Type_Value = new Dictionary<ContentType, string>()
            {
                { ContentType.JSON, "application/json" },
                { ContentType.PLAIN, "text/plain" },
                { ContentType.HTML, "text/html" }
            };
        }
        public bool isValid()
        {
            return (this.statusCode != null) && (this.payload == null || this.content_type != null);
        }
        public void Send(StatusCode statusCode, String payload, String contentType)
        {
            this.statusCode = statusCode;
            this.payload = payload;
            this.content_type = contentType;
        }
        public void Send(StreamWriter sw)
        {
            if(!isValid())
                throw new Exception("Response is not full");

            String statusCode = status_Code_Value[this.statusCode];
            StreamWriter streamWriter = sw;

            try
            {
                streamWriter.WriteLine($"HTTP/1.1 " + statusCode);
                streamWriter.WriteLine("Date: " + DateTime.Now);
                streamWriter.WriteLine("Content-Type: " + content_type);

                if (!String.IsNullOrEmpty(this.payload))
                {
                    streamWriter.WriteLine("Content-Length: " + this.payload.Length);
                    streamWriter.WriteLine();
                    streamWriter.WriteLine(this.payload);
                }
                else
                {
                    streamWriter.WriteLine("Content-Length: 0");
                    streamWriter.WriteLine();
                }
                streamWriter.Flush();
                streamWriter.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error:" + exception.Message);
            }

        }

    }
}
