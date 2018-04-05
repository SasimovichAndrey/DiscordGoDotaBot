using DiscordPartyBot.Database;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordPartyBot
{
    class DependencyProvider
    {
        public static IServiceProvider ServiceProvider { get; set; } = new ServiceCollection()
                .AddSingleton<IDatabaseService>(new InMemoryDatabaseService())
                .BuildServiceProvider();
    }
}
