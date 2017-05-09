using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whale.Api.Models
{
    public class Slogan : ISlogan
    {
        public String SubmitterName { get; set; }
        public int Votes { get; set; }
        public String Text { get; set; }
    }

    public interface ISlogan
    {
        String SubmitterName { get; set; }
        int Votes { get; set; }
        String Text { get; set; }
    }
}
