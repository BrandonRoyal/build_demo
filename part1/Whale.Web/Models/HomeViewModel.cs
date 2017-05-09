using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Whale.Web.Models
{
    public class HomeViewModel
    {
        public String Hostname
        {
            get
            {
                return Dns.GetHostName();
            }
        }

        public IDictionary<String, Slogan> Slogans
        {
            get
            {
                return GetSlogans();
            }
        }

        public Slogan Slogan { get; set; }

        private IDictionary<String, Slogan> GetSlogans()
        {
            try
            {
                var baseUri = new Uri(String.Format("http://{0}", SloganApiHostname));
                var uri = new Uri(baseUri, "api/slogan");

                WebRequest request = WebRequest.Create(uri);
                request.Method = "GET";
                request.ContentType = "application/json";
                WebResponse response = request.GetResponse();
                var dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                dataStream.Close();
                response.Close();
                return JsonConvert.DeserializeObject<IDictionary<String, Slogan>>(responseFromServer);
            }
            catch (Exception exception)
            {
                return new Dictionary<String, Slogan>();
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