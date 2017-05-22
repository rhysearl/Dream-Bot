using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Imgur;
using System.Net.Http.Headers;

// Using

namespace QT_Discord
{
    class Mybot
    {
        DiscordClient discord;
        CommandService commands;
        Random rand; //Random Generator

        string[] Crew; // Crew Array
        string[] randomText; // Text Array
        string[] Quote; // quote

        public Mybot()
        {
            rand = new Random(); // Declaring Random

            Crew = new string[] // Crew Array
            {
                "Crew/bram-fish.jpg",
                "Crew/oguz-ship.jpg"
            };

            randomText = new string[]
            {
                "QT"
            };
            {
                Quote = new string[]
                {
                    "The most difficult thing is the decision to act, the rest is merely tenacity. The fears are paper tigers. You can do anything you decide to do. You can act to change and control your life; and the procedure, the process is its own reward. - Amelia Earhart"

                };
            }
            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            discord.UsingCommands(x =>
                {
                    x.PrefixChar = '+'; // Command Prefix
                    x.AllowMentionPrefix = true; // Allow Mention
                });

            commands = discord.GetService<CommandService>();

            RegisterCrewCommand(); //Register Crew
            RegisterPurgeCommand(); // Register Purge
            RegisterQuoteCommand(); // Register Quote
            RegisterImgurCommand(); // Register Imgur Command
            RegisterHelpCommand(); // Register Help Commands


            commands.CreateCommand("qt")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("My names.. Q..te, Dream made me in his laboratory!");
                });

            discord.ExecuteAndWait(async () =>
            {

                await discord.Connect("MzE1OTE1OTkyMDIwNTQ5NjM1.DAN4xg.HEyrlOU7owDjiohBi7_l8NXRmd8", TokenType.Bot); //Await for bot to connect - Token provided.
            });
        }
        private void RegisterCrewCommand() // Crew
        {
            commands.CreateCommand("crew")
                .Do(async (e) =>
                {
                    int randomCrewIndex = rand.Next(Crew.Length);
                    string CrewToPost = Crew[randomCrewIndex];
                    await e.Channel.SendFile(CrewToPost);
                });
        }
        private void RegisterrandomTextCommand() //Text
        {
            commands.CreateCommand("random")
                .Do(async (e) =>
                {
                    int randomTextIndex = rand.Next(randomText.Length);
                    string randomTextToPost = randomText[randomTextIndex];
                    await e.Channel.SendMessage(randomTextToPost);
                });
        }
        private void RegisterPurgeCommand()
        {
            commands.CreateCommand("purge")
                .Parameter("purgeAmount")
                .Do(async (e) =>
                {
                    var messagesToDelete = await
                    e.Channel.DownloadMessages(Convert.ToInt32(e.GetArg("purgeAmount")));

                    await e.Channel.DeleteMessages(messagesToDelete);
                    await e.Channel.SendMessage(e.GetArg("purgeAmount") + " Messages deleted - QT out!");
                });
        }
        private void RegisterQuoteCommand()
        {
            commands.CreateCommand("q")
                .Do(async (e) =>
                {

                    int QuoteIndex = rand.Next(Quote.Length);
                    string QuoteToPost = Quote[QuoteIndex];
                    await e.Channel.SendMessage(QuoteToPost);
                });
            }
        public void RegisterImgurCommand()
        {
            commands.CreateCommand("imgur")
           .Do(async (e) =>
           {
               int QuoteIndex = rand.Next(Quote.Length);
               string QuoteToPost = Quote[QuoteIndex];
               await e.Channel.SendMessage(QuoteToPost);
           });
        }
            public void RegisterHelpCommand()
        {
            commands.CreateCommand("help")
                .Do(async (e) =>
                 {;
                     Console.WriteLine("+info excuted");
                     await e.Channel.SendMessage("Hey.. thanks for noticing me, I'm still kinda new around here, but... I can still do some pretty neat stuff!" + "**\n\n`+crew - `**");
                 });
        }
        private void Log(object sender, LogMessageEventArgs e)
        {

            Console.WriteLine(e.Message);
        }
    }
}
