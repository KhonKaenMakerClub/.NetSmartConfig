using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSmartConfig
{
    public class CRC8
    {

        private short init;

        private static short[] crcTable = new short[256];

        private short value;

        private static short CRC_POLYNOM = 0x8c;

        private static short CRC_INITIAL = 0x00;

        public CRC8()
        {
            for (int dividend = 0; dividend < 256; dividend++)
            {
                int remainder = dividend;// << 8;
                for (int bit = 0; bit < 8; ++bit)
                    if ((remainder & 0x01) != 0)
                        remainder = (int)((uint)remainder >> 1) ^ CRC_POLYNOM;
                    else
                        remainder = (int)((uint)remainder >> 1);
                crcTable[dividend] = (short)remainder;
            }
            this.value = this.init = CRC_INITIAL;
        }
        public void update(byte[] buffer, int offset, int len)
        {
            for (int i = 0; i < len; i++)
            {
                int data = buffer[offset + i] ^ value;
                value = (short)(crcTable[data & 0xff] ^ (value << 8));
            }
        }

        /**
         * Updates the current checksum with the specified array of bytes.
         * Equivalent to calling <code>update(buffer, 0, buffer.length)</code>.
         * 
         * @param buffer
         *            the byte array to update the checksum with
         */
        public void update(byte[] buffer)
        {
            update(buffer, 0, buffer.Length);
        }

        public void update(int b)
        {
            update(new byte[] { (byte)b }, 0, 1);
        }

        public long getValue()
        {
            return value & 0xff;
        }

        public void reset()
        {
            value = init;
        }

    }
}
