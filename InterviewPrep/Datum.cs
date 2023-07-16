using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrep
{
    public class Games
    {
        public string competition { get; set; }
        public int year { get; set; }
        public string round { get; set; }
        public string team1 { get; set; }
        public string team2 { get; set; }
        public string team1goals { get; set; }
        public string team2goals { get; set; }
    }

    public class Root
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<Games> data { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Winner
    {
        public string name { get; set; }
        public string country { get; set; }
        public int year { get; set; }
        public string winner { get; set; }
        public string runnerup { get; set; }
    }

    public class Root2
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<Winner> data { get; set; }
    }
}
