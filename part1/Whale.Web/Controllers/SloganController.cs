using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Whale.Web.Models;

namespace Whale.Web.Controllers
{
    public class SloganController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody] Slogan slogan)
        {
            try
            {
                //var byteArray = Utils.ObjectToSerializedByteArray(slogan);

                //var baseUri = new Uri(String.Format("http://{0}", SloganApiHostname));
                //var uri = new Uri(baseUri, "api/slogan");

                //WebRequest request = WebRequest.Create(uri);
                //request.Method = "POST";
                //request.ContentLength = byteArray.Length;
                //request.ContentType = "application/json";
                //Stream dataStream = request.GetRequestStream();
                //dataStream.Write(byteArray, 0, byteArray.Length);
                //dataStream.Close();
                //WebResponse response = request.GetResponse();
                //return new HttpResponseMessage(((HttpWebResponse)response).StatusCode);
                var baseUri = new Uri(String.Format("http://{0}", SloganApiHostname));
                var uri = new Uri(baseUri, "api/slogan");

                var http = (HttpWebRequest)WebRequest.Create(uri);
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.Method = "POST";

                string parsedContent = JsonConvert.SerializeObject(slogan);
                ASCIIEncoding encoding = new ASCIIEncoding();
                Byte[] bytes = encoding.GetBytes(parsedContent);

                Stream newStream = http.GetRequestStream();
                newStream.Write(bytes, 0, bytes.Length);
                newStream.Close();

                var response = http.GetResponse();

                var stream = response.GetResponseStream();
                var sr = new StreamReader(stream);
                var content = sr.ReadToEnd();
                return new HttpResponseMessage(((HttpWebResponse)response).StatusCode);
            }
            catch (Exception exception)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
            
        }

        private String SloganApiHostname
        {
            get
            {
                return Environment.GetEnvironmentVariable("SLOGANAPI_HOSTNAME", EnvironmentVariableTarget.Machine) ?? "sloganapi";
            }
        }
    }
}
