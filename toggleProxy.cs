// A Hello World! program in C#.
using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace ToggleProxy
{
    class InternetSettings 
    {

        [DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
        public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        public const int INTERNET_OPTION_REFRESH = 37;

        private static bool ToggleProxy(){

            RegistryKey registry = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);

            int isProxyOn = (int)registry.GetValue("ProxyEnable");
            
            switch (isProxyOn){

                case 0:
                    registry.SetValue("ProxyEnable", 1);
                    Console.WriteLine("Now it's on");
                    break;
                case 1:
                    registry.SetValue("ProxyEnable", 0);
                    Console.WriteLine("Now it's off");
                    break;
                default:
                    Console.WriteLine("Something went wrong");
                    return false;

            }

            registry.Close();

            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);    

            return true;        
        }

        static void Main() 
        {
            

            ToggleProxy();

        }
    }
}
