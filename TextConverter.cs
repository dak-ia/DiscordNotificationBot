using System.Text.RegularExpressions;

namespace DiscordNotificationBot
{
    public class TextConverter
    {
        GetIPAddress getIPAddress = new GetIPAddress();

        public string TextReplace(string text)
        {
            text = Regex.Replace(text, @"\$\{Global_IPv4\}", getIPAddress.GlobalIPv4Address());
            text = Regex.Replace(text, @"\$\{Local_IPv4_\d+\}", (Match text) =>
            {
                int x = int.TryParse(Regex.Match(text.Value, @"(?<=\$\{Local_IPv4_)\d+(?=\})").Value, out x) ? x : 0;
                return getIPAddress.LocalIPv4Address(x);
            });
            text = Regex.Replace(text, @"\$\{Server_Time\}", System.DateTime.Now.ToString());
            return text;
        }
    }
}
