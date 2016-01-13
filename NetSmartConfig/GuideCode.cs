using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSmartConfig
{
    public class GuideCode
    {

        public static int GUIDE_CODE_LEN = 4;


        public byte[] getBytes()
        {
            throw new Exception("DataCode don't support getBytes()");
        }


        public string tostring()
        {
            StringBuilder sb = new StringBuilder();
            char[] dataU8s = getU8s();
            for (int i = 0; i < GUIDE_CODE_LEN; i++)
            {
                string hexstring = ByteUtil.convertU8ToHexstring(dataU8s[i]);
                sb.Append("0x");
                if (hexstring.Length == 1)
                {
                    sb.Append("0");
                }
                sb.Append(hexstring).Append(" ");
            }
            return sb.ToString();
        }


        public char[] getU8s()
        {
            char[] guidesU8s = new char[GUIDE_CODE_LEN];
            guidesU8s[0] = (char)515;
            guidesU8s[1] = (char)514;
            guidesU8s[2] = (char)513;
            guidesU8s[3] = (char)512;
            return guidesU8s;
        }
    }
}
