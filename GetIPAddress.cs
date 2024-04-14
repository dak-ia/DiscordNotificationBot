using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;

namespace DiscordNotificationBot
{
    public class GetIPAddress
    {
        public string GlobalIPv4Address()
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://ipinfo.io/ip");
                var response = httpClient.Send(request);
                var responseBody = new StreamReader(response.Content.ReadAsStream()).ReadToEnd();
                return IPAddress.Parse(responseBody).ToString();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.ToString());
            }
            return "";
        }

        public string LocalIPv4Address(int x)
        {
            x = LocalIPv4AddressList().Length > x ? x : 0;
            return LocalIPv4AddressList()[x];
        }

        private string[] LocalIPv4AddressList()
        {
            IPAddress[] ipList = Dns.GetHostAddresses(Dns.GetHostName());
            List<string> ipv4List = [];
            foreach (var ip in ipList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipv4List.Add(ip.ToString());
                }
            }
            return ipv4List.ToArray();
        }
    }
}