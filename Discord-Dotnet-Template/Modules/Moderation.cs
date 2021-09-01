using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordDotnetTemplate
{
    public class ModerationModule : ModuleBase<SocketCommandContext>
    {
        [Command("Kick")]
        [Summary("Kick's a specific user")]
        [RequireBotPermission(GuildPermission.KickMembers)]
        [RequireUserPermission(GuildPermission.KickMembers)]
        public async Task KickAsync(SocketGuildUser user, [Remainder] string reason)
        {
            EmbedBuilder embed = new EmbedBuilder
            {
                Title = $"Kicked {user.Username}#{user.DiscriminatorValue}",
                Description = $"Reason: {reason}"
            };
            await user.KickAsync(reason);
            await ReplyAsync("", false, embed.Build());
        }
    }
}
