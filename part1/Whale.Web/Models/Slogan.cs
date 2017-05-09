using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Whale.Web.Models
{
    [Serializable]
    public class Slogan
    {
        public String SubmitterName { get; set; }
        public int Votes { get; set; }
        public String Text { get; set;  }
    }
}