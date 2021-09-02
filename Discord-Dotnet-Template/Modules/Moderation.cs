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
        public async Task KickAsync(SocketGuildUser user, [Remainder] string reason = "None")
        {
            EmbedBuilder embed = new EmbedBuilder
            {
                Title = $"Kicked {user.Username}#{user.DiscriminatorValue}",
                Description = $"Reason: {reason}"
            };
            await user.KickAsync(reason);
            await ReplyAsync("", false, embed.Build());
        }

        [Command("Ban")]
        [Summary("Ban's a specific user")]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task BanAsync(SocketGuildUser user, [Remainder] string reason = "None")
        {
            EmbedBuilder embed = new EmbedBuilder
            {
                Title = $"Banned {user.Username}#{user.DiscriminatorValue}",
                Description = $"Reason: {reason}"
            };
            await user.BanAsync(14, reason);
            await ReplyAsync("", false, embed.Build());
        }

        [Command("Softban")]
        [Summary("Softban's a specific user")]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task SoftbanAsync(SocketGuildUser user, [Remainder] string reason = "None")
        {
            EmbedBuilder embed = new EmbedBuilder
            {
                Title = $"Softbanned {user.Username}#{user.DiscriminatorValue}",
                Description = $"Reason: {reason}"
            };
            await user.BanAsync(14, reason);
            await Context.Guild.RemoveBanAsync(user.Id);
            await ReplyAsync("", false, embed.Build());
        }

        [Command("Clear")]
        [RequireBotPermission(ChannelPermission.ManageMessages)]
        [RequireUserPermission(ChannelPermission.ManageMessages)]
        [Summary("Clears a specific amount of messages from a channel")]
        public async Task ClearAsync(int amount)
        {
            if (amount < 99)
            {
                var messages = await Context.Channel.GetMessagesAsync(amount + 1).FlattenAsync();
                await (Context.Channel as SocketTextChannel).DeleteMessagesAsync(messages);
                var msg = await ReplyAsync($"Deleted **{amount}** messages");
                await Task.Delay(2000);
                await msg.DeleteAsync();
            }
            else
            {
                await ReplyAsync("You cannot delete more than 100 messages at once");
            }
        }
    }
}
