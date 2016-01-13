using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetSmartConfig
{
    public class EsptouchGenerator
    {

        private byte[][] mGcBytes2;
        private byte[][] mDcBytes2;

        /**
         * Constructor of EsptouchGenerator, it will cost some time(maybe a bit
         * much)
         * 
         * @param apSsid
         *            the Ap's ssid
         * @param apBssid
         *            the Ap's bssid
         * @param apPassword
         *            the Ap's password
         * @param inetAddress
         *            the phone's or pad's local ip address allocated by Ap
         * @param isSsidHidden
         *            whether the Ap's ssid is hidden
         */
        public EsptouchGenerator(string apSsid, string apBssid, string apPassword,
            IPAddress inetAddress, bool isSsidHiden)
        {
            // generate guide code
            GuideCode gc = new GuideCode();
            char[] gcU81 = gc.getU8s();
            mGcBytes2 = new byte[gcU81.Length][];

            for (int i = 0; i < mGcBytes2.Length; i++)
            {
                mGcBytes2[i] = ByteUtil.genSpecBytes(gcU81[i]);
            }

            // generate data code
            DatumCode dc = new DatumCode(apSsid, apBssid, apPassword, inetAddress,
                    isSsidHiden);
            char[] dcU81 = dc.getU8s();
            mDcBytes2 = new byte[dcU81.Length][];

            for (int i = 0; i < mDcBytes2.Length; i++)
            {
                mDcBytes2[i] = ByteUtil.genSpecBytes(dcU81[i]);
            }
        }
        public byte[][] getGCBytes2()
        {
            return mGcBytes2;
        }

        public byte[][] getDCBytes2()
        {
            return mDcBytes2;
        }

    }
}
