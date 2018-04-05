using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Configuration;

namespace DiscordPartyBot
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            Func<LogMessage, Task> logHandler = (message) =>
            {
                Console.WriteLine(message);
                return Task.FromResult<object>(null);
            };

            string token = ConfigurationManager.AppSettings["botToken"];

            var bot = new PartyBot(token, logHandler);
            await bot.Start();

            Console.ReadLine();


            bot.Disconnect();
        }
    }
}
