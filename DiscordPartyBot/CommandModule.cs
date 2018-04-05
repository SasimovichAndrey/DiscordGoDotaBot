using Discord.Commands;
using System.Threading.Tasks;

namespace DiscordPartyBot.Modules
{
    public class CommandModule : ModuleBase<SocketCommandContext>
    {
        [Command("godota")]
        public async Task GoParty(string when = null)
        {
            await Context.Channel.SendMessageAsync($"User {Context.User.Username} wants to play some dota!");
        }
    }
}
