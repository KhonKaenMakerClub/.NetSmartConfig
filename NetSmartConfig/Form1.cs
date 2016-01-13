/*
Smart Config .NetFramework By Comdet Phaudphut
Distributed from https://github.com/EspressifApp/EsptouchForAndroid
Required .netframework 4.5 
Creative Common License
*/
using NativeWifi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetSmartConfig
{
    public partial class Form1 : Form
    {
        private static int ONE_DATA_LEN = 3;
        private IPEndPoint udp_ep;
        public UdpClient udp;     
        private EsptouchTaskParameter mParameter = new EsptouchTaskParameter();
        private EsptouchGenerator generator;
        private WlanClient wlanClient = new WlanClient();
        private List<EsptouchResult> mEsptouchResultList = new List<EsptouchResult>();
        bool mIsInterrupt = false;
        string ssid = "";
        string bssid = "";
        string pass = "";
        public Form1()
        {
            InitializeComponent();
            udp_ep = new IPEndPoint(IPAddress.Any, mParameter.getPortListening());
            udp = new UdpClient(udp_ep);            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            WlanClient.WlanInterface iface = wlanClient.Interfaces.First();            
            var bssids = iface.GetNetworkBssList();
            foreach(var wlist in bssids)
            {
                ssid = Encoding.ASCII.GetString(wlist.dot11Ssid.SSID, 0, (int)wlist.dot11Ssid.SSIDLength);
                if (ssid == iface.CurrentConnection.profileName)
                {
                    bssid = string.Join(":",wlist.dot11Bssid.Select(b => b.ToString("x2").ToString()));
                    break;
                }
            }
            txtSSID.Text = ssid;
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            pass = txtPass.Text;
            generator = new EsptouchGenerator(ssid, bssid, pass, EspNetUtil.getLocalInetAddress(), false);
            mIsInterrupt = false;
            this.Enabled = false;
            Config();
            bool isSucces = await WaitSuccess(mParameter.getEsptouchResultTotalLen());            
            if (isSucces)
            {
                MessageBox.Show("SmartConfig connect cuccess to : "+ ssid+", IP = "+mEsptouchResultList[0].InetAddress.ToString());
            }
            this.Enabled = true;
        }

        private void Config()
        {
            long startTime = DateTime.Now.Ticks/10000;
            long currentTime = startTime;
            long lastTime = currentTime - mParameter.getTimeoutTotalCodeMillisecond();

            byte[][] gcBytes2 = generator.getGCBytes2();
            byte[][] dcBytes2 = generator.getDCBytes2();

            int index = 0;
            Task t = new Task(() =>
            {
                while (!mIsInterrupt)
                {
                    if (currentTime - lastTime >= mParameter.getTimeoutTotalCodeMillisecond())
                    {
                        Console.WriteLine("Send gc");
                        while (!mIsInterrupt
                                && (DateTime.Now.Ticks / 10000) - currentTime < mParameter
                                        .getTimeoutGuideCodeMillisecond())
                        {
                            Send(gcBytes2, 0, gcBytes2.Length,
                                    mParameter.getTargetHostname(),
                                    mParameter.getTargetPort(),
                                    (int)mParameter.getIntervalGuideCodeMillisecond());
                            if ((DateTime.Now.Ticks / 10000) - startTime > mParameter.getWaitUdpSendingMillisecond())
                            {
                                break;
                            }
                        }
                        lastTime = currentTime;
                    }
                    else
                    {
                          Send(dcBytes2, index, ONE_DATA_LEN,
                                 mParameter.getTargetHostname(),
                                 mParameter.getTargetPort(),
                                 (int)mParameter.getIntervalDataCodeMillisecond());
                        index = (index + ONE_DATA_LEN) % dcBytes2.Length;
                    }
                    currentTime = DateTime.Now.Ticks / 10000;
                    if (currentTime - startTime > mParameter.getWaitUdpSendingMillisecond())
                    {
                        break;
                    }
                }
            });
            t.Start();
        }
        private async Task<bool> WaitSuccess(int expectDataLen)
        {
            long startTimestamp = DateTime.Now.Ticks / 10000;
            byte[] apSsidAndPassword = ByteUtil.getBytesBystring(ssid + pass);
            byte expectOneByte = (byte)(apSsidAndPassword.Length + 9);
            Console.WriteLine("expectOneByte: " + (0 + expectOneByte));
            int receiveOneByte = -1;
            byte[] receiveBytes = null;
            Task<bool> t = new Task<bool>(() =>
            {
                while (mEsptouchResultList.Count < mParameter.getExpectTaskResultCount() && !mIsInterrupt)
                {
                    IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);                    
                    if (udp.Available > 0)
                    {
                        receiveBytes = udp.Receive(ref sender).Take(expectDataLen).ToArray();
                        if (receiveBytes != null)
                            receiveOneByte = receiveBytes[0];
                        else
                            receiveOneByte = -1;
                        if (receiveOneByte == expectOneByte)
                        {
                            Console.WriteLine("receive correct broadcast");
                            // change the socket's timeout
                            long consume = (DateTime.Now.Ticks / 10000) - startTimestamp;
                            int timeout = (int)(mParameter.getWaitUdpTotalMillisecond() - consume);
                            if (timeout < 0)
                            {
                                Console.WriteLine("esptouch timeout");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("mSocketServer's new timeout is " + timeout + " milliseconds");
                                udp.Client.SendTimeout = timeout; //<<<<<<<<<<<
                                Console.WriteLine("receive correct broadcast");

                                if (receiveBytes != null)
                                {
                                    string bssid = ByteUtil.parseBssid(
                                            receiveBytes,
                                            mParameter.getEsptouchResultOneLen(),
                                            mParameter.getEsptouchResultMacLen());
                                    IPAddress inetAddress = EspNetUtil
                                            .parseInetAddr(
                                                    receiveBytes,
                                                    mParameter.getEsptouchResultOneLen()
                                                    + mParameter.getEsptouchResultMacLen(),
                                                    mParameter.getEsptouchResultIpLen());
                                    mEsptouchResultList.Add(new EsptouchResult() { Suc = true, Bssid = bssid, InetAddress = inetAddress });
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("receive rubbish message, just ignore");
                        }
                    }
                }
                mIsInterrupt = true;
                Console.WriteLine("__listenAsyn() finish");
                return mEsptouchResultList.Count >= mParameter.getExpectTaskResultCount();
            });
            t.Start();
            return await t;
        }
        private void Send(byte[][] data,int index,int count,string host,int port,int interval)
        {
            if ((data == null) || (data.Length <= 0))
            {
                Console.WriteLine("sendData(): data == null or length <= 0");
                return;
            }
            for (int i = index; !mIsInterrupt && i < index+count; i++)
            {
                if (data[i].Length == 0)
                {
                    continue;
                }
                try
                {
                    udp.Send(data[i], data[i].Length, host, port);
                    System.Threading.Thread.Sleep(interval);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    mIsInterrupt = true;
                    break;
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/groups/KhonKaenMakerClub/");
        }
    }
    public class EsptouchResult
    {

        /**
         * check whether the esptouch task is executed suc
         * 
         * @return whether the esptouch task is executed suc
         */
        public bool Suc;

        /**
         * get the device's bssid
         * 
         * @return the device's bssid
         */
        public string Bssid;

        /**
         * check whether the esptouch task is cancelled by user
         * 
         * @return whether the esptouch task is cancelled by user
         */
        public bool Cancelled;

        /**
         * get the ip address of the device
         * 
         * @return the ip device of the device
         */
        public IPAddress InetAddress;
    }

}
