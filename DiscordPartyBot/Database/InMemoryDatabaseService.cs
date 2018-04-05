using DiscordPartyBot.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordPartyBot.Database
{
    public class InMemoryDatabaseService : IDatabaseService
    {
        private readonly List<Party> _parties = new List<Party>();
        private int lastId = 0;

        public bool AddUserToParty(int partyId, string attendie)
        {
            var party = _parties.SingleOrDefault(p => p.Id == partyId);
            if(party != default(Party))
            {
                party.Users.Add(attendie);
                return true;
            }
            else
            {
                return false;
            }
        }

        public int CreateNewParty(string date, string creator)
        {
            var newParty = new Party
            {
                Id = lastId++,
                Date = date
            };
            newParty.Users.Add(creator);

            _parties.Add(newParty);

            return newParty.Id;
        }

        public Party GetPartyById(int partyId)
        {
            return _parties.SingleOrDefault(p => p.Id == partyId);
        }
    }
}
