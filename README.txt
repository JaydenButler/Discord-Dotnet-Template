AMPER DEVELOPMENT - BASE DISCORD BOT - README

Thank you for using our base project!
We will shortly have a video out regarding using our base project to set up a Discord bot, including how to maneuver the Discord Developers portal and more.
In the meantime, we have included a detailed step-by-step introduction getting the credentials set up for your Discord bot. 
Keep in mind, this bot is not intended to be run itself. Instead, copy/paste the code inside the namespace, and paste it into your Program and Commands files.
If you HAVEN'T set that up, follow these instructions. If you HAVE set that up, and all you need to do is copy/paste, well go ahead.

Step 1: Create your project in Visual Studio. 
Step 2: Right click your Solution, which has a C# in a box for an icon, using version Visual Studio 2020.
Step 3: Hover over Add, and click Class.
Step 4: Name the new Class 'Commands.cs'.
Step 5: You  should now have 2 files in your Solution Explorer, Program.cs and Commands.cs. Copy everything inside the namespace (not including 'namespace BaseDiscordBot') from Program.cs in the BaseDiscordBot project to your Program.cs. Do the same for Commands.cs.
Step 6: It's now time to get your Discord Bot set up. Go to https://www.discordapp.com/developers/, and sign into your Discord account.
Step 7: Click on New Application, and give your app a name. This is not the name of your bot in the future.
Step 8: Your application should now be made. Click on the Bot tab on the side of the screen, then click Add Bot, and follow the prompt.
Step 9: Your app now has a Bot instance for Discord to use. Reveal the bot's token, and paste that in the botToken variable, inside the RunBotAsync function of the Program.cs file.
Step 10: Your bot will run, but there's no servers to connect to. Go back to the Discord Developers portal, and click the oauth2 tab.
Step 11: There's a BUNCH you can do with different oauth's, but for now, we're going to be basic. In the Scopes box, find the 'bot' option. That's the only one we *need*, but feel free to add more if you feel that you need them. This allows you access to certain different parts of the Discord API.
Step 12: Copy the link provided at the bottom, and paste it into a new tab.
Step 13: Assuming you've created a Discord server of your own for the bot to connect to, choose your server from the drop down list, and invite the bot to your server.
Step 14: Voila! After that, if you run the bot from Visual Studio using the green 'Play' arrow button at the top of the program, your bot *should* connect to Discord (may take a little longer than usual bit the first time).

Hopefully those instructions are fairly simple! I know it's wordy, but most of it is useful to understand why this works as opposed to simply how, so please read the instructions thoroughly.

Any further questions, don't hesitate to contact us through our Twitter or Discord, we'll do our best to help.
Twitter: http://www.twitter.com/AmperDev
Discord: http://www.discord.io/AmperDev

Best of luck!
- AmperDev (Unbound & High)