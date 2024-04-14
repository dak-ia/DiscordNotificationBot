using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace DiscordNotificationBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SettingElements settingElements = new SettingElements();
        private MainViewModel mainViewModel = new MainViewModel();
        private TextConverter textConverter = new TextConverter();
        private DiscordService discordService = new DiscordService();
        private string? exeDirectory = System.AppContext.BaseDirectory;
        private string settingFileName = @"setting.xml";
        private string settingSplitSymbol = ",";
        private string windowPositionVar = "";
        private bool execStatus = false;

        public MainWindow()
        {
            DataContext = this;
            DataContext = mainViewModel;

            InitializeComponent();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Width = MinWidth;
            Height = MinHeight;
            mainWindow.Title = mainWindow.Title + " " + Assembly.GetExecutingAssembly()?.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion;
            Directory.SetCurrentDirectory(exeDirectory);
            if (File.Exists(settingFileName))
            {
                StreamReader streamReader = new StreamReader(settingFileName, new UTF8Encoding(false));
                XmlSerializer serializer = new XmlSerializer(typeof(SettingElements));
                try
                {
                    settingElements = (SettingElements)serializer.Deserialize(streamReader)!;
                    streamReader.Close();
                    windowPositionVar = settingElements.windowPosition != null && settingElements.windowPosition.ToString().Length != 0 ? settingElements.windowPosition.ToString() : "";
                    if (windowPositionVar != null && windowPositionVar.Length != 0)
                    {
                        double positionTop = Top;
                        double positionLeft = Left;
                        double positionWidth = MinWidth;
                        double positionHeight = MinHeight;
                        string[] positions = windowPositionVar.Split(settingSplitSymbol);
                        if (double.TryParse(positions[0], out positionTop) &&
                        double.TryParse(positions[1], out positionLeft) &&
                        double.TryParse(positions[2], out positionWidth) &&
                        double.TryParse(positions[3], out positionHeight) &&
                        positionTop < SystemParameters.VirtualScreenHeight &&
                        positionLeft < SystemParameters.VirtualScreenWidth)
                        {
                            WindowStartupLocation = WindowStartupLocation.Manual;
                            Top = positionTop;
                            Left = positionLeft;
                            Width = positionWidth;
                            Height = positionHeight;
                        }
                    }
                    mainViewModel.DiscordToken = settingElements.discordToken != null && settingElements.discordToken is string && settingElements.discordToken.Length != 0 ? settingElements.discordToken : "";
                    mainViewModel.ChannelId = ulong.TryParse(settingElements.channelId.ToString(), out _) ? settingElements.channelId : 0;
                    mainViewModel.MessageToSend = settingElements.messageToSend != null && settingElements.messageToSend is string && settingElements.messageToSend.Length != 0 ? settingElements.messageToSend : "";
                    mainViewModel.IntervalTime = double.TryParse(settingElements.intervalTime.ToString(), out _) ? settingElements.intervalTime : 0;
                    mainViewModel.IntervalTimeSelectUnit = Enum.IsDefined(typeof(IntervalTimeSelectUnitEnum), settingElements.IntervalTimeSelectUnit) ? settingElements.IntervalTimeSelectUnit : 0;
                }
                catch (Exception ex)
                {
                    Debug.Print(ex.ToString());
                    WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    Width = MinWidth;
                    Height = MinHeight;
                }
                finally
                {
                    streamReader.Close();
                }
            }
            discordToken.Password = mainViewModel.DiscordToken;
            MainAsyncTask();
        }

        private async void MainAsyncTask()
        {
            await discordService.CommandReceiveAsync(mainViewModel.DiscordToken);
        }

        private void WindowCloseing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            EditSettingValueToXML();
        }

        private void DiscordTokenChangedHandler(object sender, RoutedEventArgs e)
        {
            mainViewModel.DiscordToken = discordToken.Password.ToString();
        }

        private async void ExecButtonHandler(object sender, RoutedEventArgs e)
        {
            EditSettingValueToXML();
            if (mainViewModel.IntervalTime <= 0)
            {
                execStatus = false;
                execButton.Content = execStatus ? "stop" : "start";
                DisableInput();
                await discordService.SendMessageAsync(mainViewModel.DiscordToken, mainViewModel.ChannelId, textConverter.TextReplace(mainViewModel.MessageToSend));
                EnableInput();
            }
            else
            {
                execStatus = !execStatus;
                execButton.Content = execStatus ? "stop" : "start";
                if (execStatus)
                {
                    DisableInput();
                    double intervalTime = mainViewModel.IntervalTime *
                        (mainViewModel.IntervalTimeSelectUnit == IntervalTimeSelectUnitEnum.H ? 60 * 60 : mainViewModel.IntervalTimeSelectUnit == IntervalTimeSelectUnitEnum.M ? 60 : 1) *
                        1000;
                    while (execStatus)
                    {
                        await discordService.SendMessageAsync(mainViewModel.DiscordToken, mainViewModel.ChannelId, textConverter.TextReplace(mainViewModel.MessageToSend));
                        await Task.Delay((int)intervalTime);
                    }
                }
                EnableInput();
            }
        }

        private void EditSettingValueToXML()
        {
            settingElements.windowPosition = Top.ToString() + settingSplitSymbol + Left.ToString() + settingSplitSymbol + Width.ToString() + settingSplitSymbol + Height.ToString();
            settingElements.discordToken = mainViewModel.DiscordToken;
            settingElements.channelId = mainViewModel.ChannelId;
            settingElements.messageToSend = mainViewModel.MessageToSend;
            settingElements.intervalTime = mainViewModel.IntervalTime;
            settingElements.IntervalTimeSelectUnit = mainViewModel.IntervalTimeSelectUnit;
            StreamWriter streamWriter = new StreamWriter(settingFileName, false, new UTF8Encoding(false));
            XmlSerializer serializer = new XmlSerializer(typeof(SettingElements));
            try
            {
                serializer.Serialize(streamWriter, settingElements);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
            }
            finally
            {
                streamWriter.Close();
            }
        }

        private void EnableInput()
        {
            discordToken.IsEnabled = true;
            channelId.IsEnabled = true;
            messageToSend.IsEnabled = true;
            intervalTime.IsEnabled = true;
            intervalHour.IsEnabled = true;
            intervalMinute.IsEnabled = true;
            intervalSecond.IsEnabled = true;
        }

        private void DisableInput()
        {
            discordToken.IsEnabled = false;
            channelId.IsEnabled = false;
            messageToSend.IsEnabled = false;
            intervalTime.IsEnabled = false;
            intervalHour.IsEnabled = false;
            intervalMinute.IsEnabled = false;
            intervalSecond.IsEnabled = false;
        }
    }
}
