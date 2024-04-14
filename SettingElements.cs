using System.Xml.Serialization;

namespace DiscordNotificationBot
{
    public class SettingElements
    {
        [XmlElement("WindowPosition")]
        public string windowPosition = "";
        [XmlElement("DiscordToken")]
        public string discordToken = "";
        [XmlElement("ChannelId")]
        public ulong channelId = 0;
        [XmlElement("MessageToSend")]
        public string messageToSend = "";
        [XmlElement("IntervalTime")]
        public double intervalTime = 0;
        [XmlElement("IntervalTimeSelectUnit")]
        public IntervalTimeSelectUnitEnum IntervalTimeSelectUnit = 0;
    }
}