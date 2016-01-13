using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetSmartConfig
{
    public class EspNetUtil
    {

        /**
         * get the local ip address by Android System
         * 
         * @param context
         *            the context
         * @return the local ip addr allocated by Ap
         */
        public static IPAddress getLocalInetAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                return null;
            }

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            return host
                .AddressList
                .FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        }

        private static string __formatstring(int value)
        {
            string strValue = "";
            byte[] ary = __intToByteArray(value);
            for (int i = ary.Length - 1; i >= 0; i--)
            {
                strValue += (ary[i] & 0xFF);
                if (i > 0)
                {
                    strValue += ".";
                }
            }
            return strValue;
        }

        private static byte[] __intToByteArray(int value)
        {
            byte[] b = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                int offset = (b.Length - 1 - i) * 8;
                b[i] = (byte)(((uint)value >> offset) & 0xFF);
            }
            return b;
        }

        /**
         * parse InetAddress
         * 
         * @param inetAddrBytes
         * @return
         */
        public static IPAddress parseInetAddr(byte[] inetAddrBytes, int offset,
                int count)
        {
            IPAddress inetAddress = null;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.Append((inetAddrBytes[offset + i] & 0xff).ToString());
                if (i != count - 1)
                {
                    sb.Append('.');
                }
            }
            try
            {
                inetAddress = IPAddress.Parse(sb.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return inetAddress;
        }

        /**
         * parse bssid
         * 
         * @param bssid the bssid
         * @return byte converted from bssid
         */
        public static byte[] parseBssid2bytes(string bssid)
        {
            string[] bssidSplits = bssid.Split(new char[] { ':' });
            byte[] result = new byte[bssidSplits.Length];
            for (int i = 0; i < bssidSplits.Length; i++)
            {
                result[i] = Convert.ToByte(bssidSplits[i], 16);
            }
            return result;
        }
    }
}
