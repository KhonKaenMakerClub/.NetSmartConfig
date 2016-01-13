using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSmartConfig
{
    public class DataCode
    {

        public static int DATA_CODE_LEN = 6;

        private static int INDEX_MAX = 127;

        private byte mSeqHeader;
        private byte mDataHigh;
        private byte mDataLow;
        // the crc here means the crc of the data and sequence header be transformed
        // it is calculated by index and data to be transformed
        private byte mCrcHigh;
        private byte mCrcLow;

        /**
         * Constructor of DataCode
         * @param u8 the character to be transformed
         * @param index the index of the char
         */
        public DataCode(char u8, int index)
        {
            if (index > INDEX_MAX)
            {
                throw new Exception("index > INDEX_MAX");
            }
            byte[] dataBytes = ByteUtil.splitUint8To2bytes(u8);
            mDataHigh = dataBytes[0];
            mDataLow = dataBytes[1];
            CRC8 crc8 = new CRC8();
            crc8.update(ByteUtil.convertUint8toByte(u8));
            crc8.update(index);
            byte[] crcBytes = ByteUtil.splitUint8To2bytes((char)crc8.getValue());
            mCrcHigh = crcBytes[0];
            mCrcLow = crcBytes[1];
            mSeqHeader = (byte)index;
        }

        public byte[] getBytes()
        {
            byte[] dataBytes = new byte[DATA_CODE_LEN];
            dataBytes[0] = 0x00;
            dataBytes[1] = ByteUtil.combine2bytesToOne(mCrcHigh, mDataHigh);
            dataBytes[2] = 0x01;
            dataBytes[3] = mSeqHeader;
            dataBytes[4] = 0x00;
            dataBytes[5] = ByteUtil.combine2bytesToOne(mCrcLow, mDataLow);
            return dataBytes;
        }


        public string tostring()
        {
            StringBuilder sb = new StringBuilder();
            byte[] dataBytes = getBytes();
            for (int i = 0; i < DATA_CODE_LEN; i++)
            {
                string hexstring = ByteUtil.convertByte2Hexstring(dataBytes[i]);
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
            throw new Exception("DataCode don't support getU8s()");
        }

    }
}
