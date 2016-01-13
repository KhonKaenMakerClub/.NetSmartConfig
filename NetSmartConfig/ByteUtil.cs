using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSmartConfig
{
    public class ByteUtil
    {

        public static string ESPTOUCH_ENCODING_CHARSET = "UTF-8";

        /**
         * Put string to byte[]
         * 
         * @param destbytes
         *            the byte[] of dest
         * @param srcstring
         *            the string of src
         * @param destOffset
         *            the offset of byte[]
         * @param srcOffset
         *            the offset of string
         * @param count
         *            the count of dest, and the count of src as well
         */
        public static void putstring2bytes(byte[] destbytes, string srcstring,
                int destOffset, int srcOffset, int count)
        {
            for (int i = 0; i < count; i++)
            {
                destbytes[count + i] = (byte)srcstring[i];//.getBytes()[i];
            }
        }

        /**
         * Convert uint8 into char( we treat char as uint8)
         * 
         * @param uint8
         *            the unit8 to be converted
         * @return the byte of the unint8
         */
        public static byte convertUint8toByte(char uint8)
        {
            if (uint8 > byte.MaxValue - byte.MinValue)
            {
                throw new Exception("Out of Boundary");
            }
            return (byte)uint8;
        }

        /**
         * Convert char into uint8( we treat char as uint8 )
         * 
         * @param b
         *            the byte to be converted
         * @return the char(uint8)
         */
        public static char convertByte2Uint8(byte b)
        {
            // char will be promoted to int for char don't support & operator
            // & 0xff could make negatvie value to positive
            return (char)(b & 0xff);
        }

        /**
         * Convert byte[] into char[]( we treat char[] as uint8[])
         * 
         * @param bytes
         *            the byte[] to be converted
         * @return the char[](uint8[])
         */
        public static char[] convertBytes2Uint8s(byte[] bytes)
        {
            int len = bytes.Length;
            char[] uint8s = new char[len];
            for (int i = 0; i < len; i++)
            {
                uint8s[i] = convertByte2Uint8(bytes[i]);
            }
            return uint8s;
        }

        /**
         * Put byte[] into char[]( we treat char[] as uint8[])
         * 
         * @param dest
         *            the char[](uint8[]) array
         * @param src
         *            the byte[]
         * @param destOffset
         *            the offset of char[](uint8[])
         * @param srcOffset
         *            the offset of byte[]
         * @param count
         *            the count of dest, and the count of src as well
         */
        public static void putbytes2Uint8s(char[] destUint8s, byte[] srcBytes,
                int destOffset, int srcOffset, int count)
        {
            for (int i = 0; i < count; i++)
            {
                destUint8s[destOffset + i] = convertByte2Uint8(srcBytes[srcOffset
                        + i]);
            }
        }

        /**
         * Convert byte to Hex string
         * 
         * @param b
         *            the byte to be converted
         * @return the Hex string
         */
        public static string convertByte2Hexstring(byte b)
        {
            char u8 = convertByte2Uint8(b);
            return ((int)u8).ToString("X");//.toHexstring(u8);
        }

        /**
         * Convert char(uint8) to Hex string
         * 
         * @param u8
         *            the char(uint8) to be converted
         * @return the Hex string
         */
        public static string convertU8ToHexstring(char u8)
        {
            return ((int)u8).ToString("X");
        }

        /**
         * Split uint8 to 2 bytes of high byte and low byte. e.g. 20 = 0x14 should
         * be split to [0x01,0x04] 0x01 is high byte and 0x04 is low byte
         * 
         * @param uint8
         *            the char(uint8)
         * @return the high and low bytes be split, byte[0] is high and byte[1] is
         *         low
         */
        public static byte[] splitUint8To2bytes(char uint8)
        {
            if (uint8 < 0 || uint8 > 0xff)
            {
                throw new Exception("Out of Boundary");
            }
            string hexstring = ((int)uint8).ToString("X");
            byte low;
            byte high;
            if (hexstring.Length > 1)
            {
                high = Convert.ToByte(hexstring.Substring(0, 1), 16); //(byte)int.Parse(hexstring.Substring(0, 1), 16);
                low = Convert.ToByte(hexstring.Substring(1, 1), 16);  //(byte)int.ParseInt(hexstring.Substring(1, 2), 16);
            }
            else
            {
                high = 0;
                low = Convert.ToByte(hexstring.Substring(0, 1), 16); //(byte)Integer.parseInt(hexstring.substring(0, 1), 16);
            }
            byte[] result = new byte[] { high, low };
            return result;
        }

        /**
         * Combine 2 bytes (high byte and low byte) to one whole byte
         * 
         * @param high
         *            the high byte
         * @param low
         *            the low byte
         * @return the whole byte
         */
        public static byte combine2bytesToOne(byte high, byte low)
        {
            if (high < 0 || high > 0xf || low < 0 || low > 0xf)
            {
                throw new Exception("Out of Boundary");
            }
            return (byte)(high << 4 | low);
        }

        /**
         * Combine 2 bytes (high byte and low byte) to
         * 
         * @param high
         *            the high byte
         * @param low
         *            the low byte
         * @return the char(u8)
         */
        public static char combine2bytesToU16(byte high, byte low)
        {
            char highU8 = convertByte2Uint8(high);
            char lowU8 = convertByte2Uint8(low);
            return (char)(highU8 << 8 | lowU8);
        }

        /**
         * Generate the random byte to be sent
         * 
         * @return the random byte
         */
        private static byte randomByte()
        {
            return (byte)(127 - new Random().Next(256));
        }

        /**
         * Generate the random byte to be sent
         * 
         * @param len
         *            the len presented by u8
         * @return the byte[] to be sent
         */
        public static byte[] randomBytes(char len)
        {
            byte[] data = new byte[len];
            for (int i = 0; i < len; i++)
            {
                data[i] = randomByte();
            }
            return data;
        }

        public static byte[] genSpecBytes(char len)
        {
            byte[] data = new byte[len];
            for (int i = 0; i < len; i++)
            {
                data[i] = (byte)'1';
            }
            return data;
        }

        /**
         * Generate the random byte to be sent
         * 
         * @param len
         *            the len presented by byte
         * @return the byte[] to be sent
         */
        public static byte[] randomBytes(byte len)
        {
            char u8 = convertByte2Uint8(len);
            return randomBytes(u8);
        }

        /**
         * Generate the specific byte to be sent
         * @param len
         *            the len presented by byte
         * @return the byte[] 
         */
        public static byte[] genSpecBytes(byte len)
        {
            char u8 = convertByte2Uint8(len);
            return genSpecBytes(u8);
        }

        public static string parseBssid(byte[] bssidBytes, int offset, int count)
        {
            byte[] bytes = new byte[count];
            for (int i = 0; i < count; i++)
            {
                bytes[i] = bssidBytes[i + offset];
            }
            return parseBssid(bytes);
        }

        /**
         * parse "24,-2,52,-102,-93,-60" to "18,fe,34,9a,a3,c4"
         * parse the bssid from hex to string
         * @param bssidBytes the hex bytes bssid, e.g. {24,-2,52,-102,-93,-60}
         * @return the string of bssid, e.g. 18fe349aa3c4
         */
        public static string parseBssid(byte[] bssidBytes)
        {
            StringBuilder sb = new StringBuilder();
            int k;
            string hexK;
            string str;
            for (int i = 0; i < bssidBytes.Length; i++)
            {
                k = 0xff & bssidBytes[i];
                hexK = k.ToString("X");
                str = ((k < 16) ? ("0" + hexK) : (hexK));
                Console.WriteLine(str);
                sb.Append(str);
            }
            return sb.ToString();
        }

        /**
         * @param string the string to be used
         * @return the byte[] of string according to {@link #ESPTOUCH_ENCODING_CHARSET}
         */
        public static byte[] getBytesBystring(string str)
        {
            try
            {
                return System.Text.Encoding.UTF8.GetBytes(str);//str.getBytes(ESPTOUCH_ENCODING_CHARSET);
            }
            catch (Exception e)
            {
                throw new Exception("the charset is invalid");
            }
        }
        /*
        private static void test_splitUint8To2bytes()
        {
            // 20 = 0x14
            byte[] result = splitUint8To2bytes((char)20);
            if (result[0] == 1 && result[1] == 4)
            {
                Console.WriteLine("test_splitUint8To2bytes(): pass");
            }
            else
            {
                Console.WriteLine("test_splitUint8To2bytes(): fail");
            }
        }

        private static void test_combine2bytesToOne()
        {
            byte high = 0x01;
            byte low = 0x04;
            if (combine2bytesToOne(high, low) == 20)
            {
                Console.WriteLine("test_combine2bytesToOne(): pass");
            }
            else
            {
                Console.WriteLine("test_combine2bytesToOne(): fail");
            }
        }

        private static void test_convertChar2Uint8()
        {
            byte b1 = (byte)'a';
            // -128: 1000 0000 should be 128 in unsigned char
            // -1: 1111 1111 should be 255 in unsigned char
            byte b2 = 128;
            byte b3 = 255;
            if (convertByte2Uint8(b1) == 97 && convertByte2Uint8(b2) == 128
                    && convertByte2Uint8(b3) == 255)
            {
                Console.WriteLine("test_convertChar2Uint8(): pass");
            }
            else
            {
                Console.WriteLine("test_convertChar2Uint8(): fail");
            }
        }*/
        /*
        private static void test_convertUint8toByte()
        {
            char c1 = 'a';
            // 128: 1000 0000 should be -128 in byte
            // 255: 1111 1111 should be -1 in byte
            char c2 = (byte)128;
            char c3 = 255;
            if (convertUint8toByte(c1) == 97 && convertUint8toByte(c2) == -128
                    && convertUint8toByte(c3) == -1)
            {
                System.out.println("test_convertUint8toByte(): pass");
            }
            else
            {
                System.out.println("test_convertUint8toByte(): fail");
            }
        }

        private static void test_parseBssid()
        {
            byte b[] = { 15, -2, 52, -102, -93, -60 };
            if (parseBssid(b).equals("0ffe349aa3c4"))
            {
                System.out.println("test_parseBssid(): pass");
            }
            else
            {
                System.out.println("test_parseBssid(): fail");
            }
        }*/

       /* public static void main(string args[])
        {
            test_convertUint8toByte();
            test_convertChar2Uint8();
            test_splitUint8To2bytes();
            test_combine2bytesToOne();
            test_parseBssid();
        }*/
    }
}
