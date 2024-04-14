using System.ComponentModel;
using System.Diagnostics;

namespace DiscordNotificationBot
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        private string discordToken = "";
        public string DiscordToken
        {
            get { return discordToken; }
            set
            {
                discordToken = value;
                SetStatus();
                OnPropertyChanged(nameof(DiscordToken));
            }
        }

        private ulong channelId = 0;
        public ulong ChannelId
        {
            get { return channelId; }
            set
            {
                channelId = value;
                SetChannelName();
                SetStatus();
                OnPropertyChanged(nameof(ChannelId));
            }
        }

        private string channelName = "---";
        public string ChannelName
        {
            get
            {
                return channelName;
            }
            set
            {
                channelName = value;
                OnPropertyChanged(nameof(ChannelName));
            }
        }

        private string messageToSend = "";
        public string MessageToSend
        {
            get { return messageToSend; }
            set
            {
                messageToSend = value;
                SetStatus();
                OnPropertyChanged(nameof(MessageToSend));
            }
        }

        private double intervalTime = 0;
        public double IntervalTime
        {
            get { return intervalTime; }
            set
            {
                intervalTime = value;
                SetStatus();
                OnPropertyChanged(nameof(IntervalTime));
            }
        }

        private bool canExecute = false;
        public bool CanExecute
        {
            get { return canExecute; }
            set
            {
                canExecute = value;
                OnPropertyChanged(nameof(CanExecute));
            }
        }

        private IntervalTimeSelectUnitEnum intervalTimeSelectUnit = 0;
        public IntervalTimeSelectUnitEnum IntervalTimeSelectUnit
        {
            get { return intervalTimeSelectUnit; }
            set
            {
                intervalTimeSelectUnit = value;
                OnPropertyChanged(nameof(IntervalTimeSelectUnit));
            }
        }

        // TODO
        private string statusField = "";
        public string StatusField
        {
            get { return statusField; }
            set
            {
                statusField = value;
                OnPropertyChanged(nameof(StatusField));
            }
        }

        private async void SetChannelName()
        {
            ChannelName = await new DiscordService().GetChannelNameAsync(DiscordToken, ChannelId) ?? "---";
        }

        private void SetStatus()
        {
            CanExecute = DiscordToken != "" && ChannelId > 0 && MessageToSend != "" && IntervalTime >= 0;
        }
    }
}