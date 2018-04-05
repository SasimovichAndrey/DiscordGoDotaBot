using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordPartyBot.Model
{
    public class Party
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public List<string> Users { get; set; } = new List<string>();
    }
}
