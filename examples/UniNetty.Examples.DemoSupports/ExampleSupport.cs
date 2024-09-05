using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using UniNetty.Common.Internal.Logging;

namespace UniNetty.Examples.DemoSupports
{
    public class ExampleSupport
    {
        private static readonly IInternalLogger Logger = InternalLoggerFactory.GetInstance<ExampleSupport>();

        public static readonly ExampleSupport Shared = new ExampleSupport();

        public void OpenUrl(string url)
        {
            try
            {
                // OS에 따라 다른 명령 실행
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    var psi = new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true };
                    Process.Start(psi);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
            }
            catch (Exception ex)
            {
                Logger.Info($"Error opening web browser: {ex.Message}");
            }
        }

        public string GetPrivateIp()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            return string.Empty;
        }
    }
}