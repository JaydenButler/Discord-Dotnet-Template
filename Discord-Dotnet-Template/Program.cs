﻿using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;
using dotenv.net;

// Rename this to whatever you'd like. Also rename in the Commands.cs file and any other files you are using this namespace in.
namespace DiscordDotnetTemplate
{
    class DiscordBot
    {
        // Basic Discord variables. Client, for the instance of the Discord bot client, and the commands and services that are required to run the bot and inject commands.
        public DiscordSocketClient Client;
        public CommandService Commands;
        public IServiceProvider Services;
        public DiscordSocketConfig Config;

        private static void Main(string[] args) => new DiscordBot().RunBotAsync().GetAwaiter().GetResult();

        // This is a singleton.
        // This is used to get a recurring instance of the Discord bot that is the same as long as the Bot is running.
        // This is so you can get the client of the Bot, and other variables saved in here.
        #region Bot Singleton
        private static DiscordBot _instance;
        private DiscordBot() { }

        public static DiscordBot Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DiscordBot();
                }

                return _instance;
            }
        }
        #endregion

        private async Task RunBotAsync()
        {
            //Load our DotEnv file and make var
            DotEnv.Load();
            var envFile = DotEnv.Read();

            // Creates the Client, Command Service and Discord Service.
            Config = new DiscordSocketConfig
            {
                AlwaysDownloadUsers = true,
                MessageCacheSize = 100
            };
            Client = new DiscordSocketClient(Config);
            Commands = new CommandService();
            Services = new ServiceCollection().AddSingleton(Client).AddSingleton(Commands).BuildServiceProvider();

            // This is your bot token. You get this from http://www.discordapp.com/developers/, if you need help, read the README in the repository.
            string botToken = envFile["BOT_TOKEN"];

            // Injects the commands into the bot as something to monitor.
            await RegisterCommandsAsync();

            // Log the Bot in, as a Bot, using the token retrieved earlier, then start the bot.
            await Client.LoginAsync(Discord.TokenType.Bot, botToken);
            await Client.StartAsync();

            System.Console.WriteLine("Bot is connected!");

            // This is so the bot never closes unless there is a bug or error.
            await Task.Delay(-1);
        }

        private async Task RegisterCommandsAsync()
        {
            // This is a Message Received event. The client has a multitude of different events that you can use to sign up and customize how certain things are handled.
            // Here, we register ourselves to handle commands differently to how Discord handles them without our help (which is by doing nothing).
            Client.MessageReceived += HandleCommandAsync;

            // We then add the Commands module through the Discord service, and then the bot will listen for certain commands in the Commands.cs file.
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), Services);
        }

        // In this function, we handle a SocketMessage. This is a Discord class for any message the Bot monitors.
        private async Task HandleCommandAsync(SocketMessage arg)
        {
            // Turn that SocketMessage into a SocketUserMessage, giving us access to the User who wrote the message as well as the message itself.
            var msg = arg as SocketUserMessage;
            // If the message is empty or is from a bot, do nothing.
            if (msg is null || msg.Author.IsBot) return;

            // Start at the beginning of the string.
            int argumentPosition = 0;

            // This checks if the message starts with !, in this case.
            // The ! can be anything you want, but this goes before the command. ie - !ping, !help, !trade.
            if (msg.HasStringPrefix("!", ref argumentPosition))
            {
                // Injects the Context variable for the message.
                var context = new SocketCommandContext(Client, msg);

                // Tries to find and execute a command with the remainder of the message.
                // If there's no command for the remainder of the message, the result will comeback with an Unknown Command message.
                var result = await Commands.ExecuteAsync(context, argumentPosition, Services);

                if (!result.IsSuccess)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
        }
    }
}