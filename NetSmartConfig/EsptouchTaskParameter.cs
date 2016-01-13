using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetSmartConfig
{
    public class EsptouchTaskParameter
    {

        private long mIntervalGuideCodeMillisecond;
        private long mIntervalDataCodeMillisecond;
        private long mTimeoutGuideCodeMillisecond;
        private long mTimeoutDataCodeMillisecond;
        private int mTotalRepeatTime;
        private int mEsptouchResultOneLen;
        private int mEsptouchResultMacLen;
        private int mEsptouchResultIpLen;
        private int mEsptouchResultTotalLen;
        private int mPortListening;
        private int mTargetPort;
        private int mWaitUdpReceivingMilliseond;
        private int mWaitUdpSendingMillisecond;
        private int mThresholdSucBroadcastCount;
        private int mExpectTaskResultCount;
        private static int _datagramCount = 0;

        public EsptouchTaskParameter()
        {
            mIntervalGuideCodeMillisecond = 10;
            mIntervalDataCodeMillisecond = 10;
            mTimeoutGuideCodeMillisecond = 2000;
            mTimeoutDataCodeMillisecond = 4000;
            mTotalRepeatTime = 1;
            mEsptouchResultOneLen = 1;
            mEsptouchResultMacLen = 6;
            mEsptouchResultIpLen = 4;
            mEsptouchResultTotalLen = 1 + 6 + 4;
            mPortListening = 18266;
            mTargetPort = 7001;
            mWaitUdpReceivingMilliseond = 15000;
            mWaitUdpSendingMillisecond = 45000;
            mThresholdSucBroadcastCount = 1;
            mExpectTaskResultCount = 1;
        }

        // the range of the result should be 1-100
        private static int __getNextDatagramCount()
        {
            return 1 + (_datagramCount++) % 100;
        }

        public long getIntervalGuideCodeMillisecond()
        {
            return mIntervalGuideCodeMillisecond;
        }

        public long getIntervalDataCodeMillisecond()
        {
            return mIntervalDataCodeMillisecond;
        }

        public long getTimeoutGuideCodeMillisecond()
        {
            return mTimeoutGuideCodeMillisecond;
        }

        public long getTimeoutDataCodeMillisecond()
        {
            return mTimeoutDataCodeMillisecond;
        }

        public long getTimeoutTotalCodeMillisecond()
        {
            return mTimeoutGuideCodeMillisecond + mTimeoutDataCodeMillisecond;
        }

        public int getTotalRepeatTime()
        {
            return mTotalRepeatTime;
        }

        public int getEsptouchResultOneLen()
        {
            return mEsptouchResultOneLen;
        }

        public int getEsptouchResultMacLen()
        {
            return mEsptouchResultMacLen;
        }

        public int getEsptouchResultIpLen()
        {
            return mEsptouchResultIpLen;
        }

        public int getEsptouchResultTotalLen()
        {
            return mEsptouchResultTotalLen;
        }

        public int getPortListening()
        {
            return mPortListening;
        }

        // target hostname is : 234.1.1.1, 234.2.2.2, 234.3.3.3 to 234.100.100.100
        public string getTargetHostname()
        {
            int count = __getNextDatagramCount();
            return "234." + count + "." + count + "." + count;
        }

        public int getTargetPort()
        {
            return mTargetPort;
        }

        public int getWaitUdpReceivingMillisecond()
        {
            return mWaitUdpReceivingMilliseond;
        }

        public int getWaitUdpSendingMillisecond()
        {
            return mWaitUdpSendingMillisecond;
        }

        public int getWaitUdpTotalMillisecond()
        {
            return mWaitUdpReceivingMilliseond + mWaitUdpSendingMillisecond;
        }

        public int getThresholdSucBroadcastCount()
        {
            return mThresholdSucBroadcastCount;
        }

        public void setWaitUdpTotalMillisecond(int waitUdpTotalMillisecond)
        {
            if (waitUdpTotalMillisecond < mWaitUdpReceivingMilliseond
                    + getTimeoutTotalCodeMillisecond())
            {
                // if it happen, even one turn about sending udp broadcast can't be
                // completed
                throw new Exception(
                        "waitUdpTotalMillisecod is invalid, "
                                + "it is less than mWaitUdpReceivingMilliseond + getTimeoutTotalCodeMillisecond()");
            }
            mWaitUdpSendingMillisecond = waitUdpTotalMillisecond
                    - mWaitUdpReceivingMilliseond;
        }

        public int getExpectTaskResultCount()
        {
            return this.mExpectTaskResultCount;
        }

        public void setExpectTaskResultCount(int expectTaskResultCount)
        {
            this.mExpectTaskResultCount = expectTaskResultCount;
        }

    }
}
