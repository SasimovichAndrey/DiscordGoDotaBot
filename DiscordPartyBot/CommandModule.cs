using Discord.Commands;
using DiscordPartyBot.Database;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordPartyBot.Modules
{
    public class CommandModule : ModuleBase<SocketCommandContext>
    {
        private IDatabaseService _dbService = new InMemoryDatabaseService();

        public CommandModule()
        {
            _dbService = DependencyProvider.ServiceProvider.GetService(typeof(IDatabaseService)) as IDatabaseService;
        }

        [Command("godota")]
        public async Task GoParty(params string[] date)
        {
            if(date.Length != 0)
            {
                var creator = Context.User.Username;
                var joinedDate = string.Join(" ", date);
                var partyId = _dbService.CreateNewParty(joinedDate, creator);

                await Context.Channel.SendMessageAsync($"{Context.User.Username} собирает пати {joinedDate}! Напиши \"!go {partyId}\" шоб подключицца");
            }
            else
            {
                await Context.Channel.SendMessageAsync($"Напиши время когда собираться. ");
            }
        }

        [Command("go")]
        public async Task Go(int partyId)
        {
            var attendie = Context.User.Username;
            var party = _dbService.GetPartyById(partyId);
            if (party !=null && !party.Users.Any(u => u == attendie))
            {
                _dbService.AddUserToParty(partyId, attendie);
                await Context.Channel.SendMessageAsync($"{Context.User.Username} добавлен в пати");
            }
            else
            {
                await Context.Channel.SendMessageAsync($"Нельзя добавить {attendie} в пати с id {partyId}. {attendie} уже в пати либо пати с id {partyId} не существует. Напиши \"show :partyId:\" шоб посмотреть список участников пати");
            }
        }

        [Command("show")]
        public async Task Show(int partyId)
        {
            var party = _dbService.GetPartyById(partyId);
            if(party != null)
            {
                var strBuilder = new StringBuilder();
                strBuilder.AppendLine($"Дотка начинаецца: {party.Date}");
                strBuilder.AppendLine("Зарегистрированные бойцы:");
                foreach(var member in party.Users)
                {
                    strBuilder.AppendLine(member);
                }

                await Context.Channel.SendMessageAsync(strBuilder.ToString());
            }
            else
            {
                await Context.Channel.SendMessageAsync($"Пати с id {partyId} не существует");
            }
        }
    }
}
