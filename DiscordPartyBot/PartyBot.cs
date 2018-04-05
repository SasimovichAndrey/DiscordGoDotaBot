﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace DiscordPartyBot
{
    class PartyBot
    {
        private DiscordSocketClient _client;
        private CommandService _commandService;
        private string _token;

        public PartyBot(string token, Func<LogMessage, Task> logHandler)
        {
            _token = token;
            _client = new DiscordSocketClient();
            _commandService = new CommandService();
            _client.Log += logHandler;
            _client.MessageReceived += MessageReceived;
        }

        public async Task Initilize()
        {
            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        public async Task Start()
        {
            await Initilize();

            await _client.LoginAsync(TokenType.Bot, _token);
            await _client.StartAsync();
        }

        private async Task MessageReceived(SocketMessage message)
        {
            var userMessage = message as SocketUserMessage;
            int argPos = 0;
            if (userMessage != null && userMessage.HasCharPrefix('!', ref argPos))
            {
                var ctx = new SocketCommandContext(_client, userMessage);
                await _commandService.ExecuteAsync(ctx, argPos);
            }
        }

        internal void Disconnect()
        {
            _client.Dispose();
        }
    }
}