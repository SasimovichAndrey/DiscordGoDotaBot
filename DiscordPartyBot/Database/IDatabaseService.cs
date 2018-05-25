using DiscordPartyBot.Model;
using System.Collections.Generic;

namespace DiscordPartyBot.Database
{
    public interface IDatabaseService
    {
        int CreateNewParty(string date, string creator);
        bool AddUserToParty(int partyId, string attendie);
        Party GetPartyById(int partyId);
        IEnumerable<Party> GetAllParties();
    }
}
