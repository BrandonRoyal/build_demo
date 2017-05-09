using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace Whale.Web
{
    public static class Utils
    {
        public static byte[] ObjectToSerializedByteArray(object obj)
        {
            if (obj == null)
                return null;
            var str = JsonConvert.SerializeObject(obj);
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, str);
                return ms.ToArray();
            }
        }
    }
}