using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterCardTradingGame
{
    public class ResponseHelper
    {
        public static Response ok()
        {
            Response response = new Response();
            response.Send(Response.StatusCode.OK, response.status_Code_Value[Response.StatusCode.OK], response.content_Type_Value[Response.ContentType.HTML]);
            return response;
        }
        public static Response serverError()
        {
            Response response = new Response();
            response.Send(Response.StatusCode.Internal_Server_Error, response.status_Code_Value[Response.StatusCode.Internal_Server_Error], response.content_Type_Value[Response.ContentType.HTML]);
            return response;
        }
        public static Response notFound()
        {
            Response response = new Response();
            response.Send(Response.StatusCode.Not_Found, response.status_Code_Value[Response.StatusCode.Not_Found], response.content_Type_Value[Response.ContentType.HTML]);
            return response;
        }

        public static Response jsonInvalid(String msg)
        {
            Response response = new Response();
            response.Send(Response.StatusCode.Bad_Request, msg, response.content_Type_Value[Response.ContentType.HTML]);
            return response;
        }
        public static Response jsonInvalid()
        {
            Response response = new Response();
            response.Send(Response.StatusCode.Bad_Request, response.status_Code_Value[Response.StatusCode.Bad_Request], response.content_Type_Value[Response.ContentType.HTML]);
            return response;
        }

    }
}
