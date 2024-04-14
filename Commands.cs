using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace DiscordNotificationBot
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        [Alias("-h", "--help", "HELP", "-H", "--HELP")]
        public async Task Help()
        {
            await ReplyAsync("```" +
                               "ip: Get the IP address of the server\n" +
                               "server_time: Get the current time of the server\n" +
                             "```");
        }

        [Command("ip")]
        [Alias("ip_address", "global_ip", "global_ip_address", "IP", "IPAddress", "IP_Address", "GlobalIP", "GlobalIPAddress", "Global_IPAddress", "Global_IP_Address", "IPv4")]
        public async Task Ping()
        {
            await ReplyAsync(new GetIPAddress().GlobalIPv4Address());
        }

        [Command("local_ip")]
        [Alias("local_ip_address", "LocalIP", "LocalIPAddress", "Local_IP", "Local_IPAddress", "Local_IP_Address", "LocalIPv4", "LocalIPv4Address")]
        public async Task LocalIP(string num)
        {
            int x = int.TryParse(num, out x) ? x : 0;
            await ReplyAsync(new GetIPAddress().LocalIPv4Address(x));
        }

        [Command("server_time")]
        [Alias("time", "TIME", "ServerTime", "Server_Time")]
        public async Task ServerTime()
        {
            await ReplyAsync(DateTime.Now.ToString());
        }

        [Command("system_info")]
        public async Task SystemInfo()
        {
            await ReplyAsync("```" +
                "OSVersion: " + Environment.OSVersion + "\n" +
                "ProcessorCount: " + Environment.ProcessorCount + "\n" +
                "TickCount64: " + Environment.TickCount64 + "\n" +
                "```");
        }
    }
}
