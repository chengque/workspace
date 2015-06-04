using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chengque.Convertor;
using Chengque.SerialSup;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;

namespace HumTrans
{
    public class Transmit
    {
        public delegate void RecMsgEventHandler(RecMsgArgs args);

        public event RecMsgEventHandler OnRecMsg;
        public bool mutex;
        private Serial xbee;
        private static Transmit _instance = new Transmit();

        private byte[] lastPack;
        private byte[] newPack;

        public static Transmit Instance
        {
            get
            {
                return _instance;
            }
        }

        private Transmit()
        {

        }

        public bool OpenPort(string port)
        {

            try
            {
                bool res = false;
                xbee = new Serial();
                res = xbee.OpenPort(port, 57600);
                //xbee.GetSerialPort().Handshake = Handshake.RequestToSend;

                xbee.SetParity(Parity.None);
                xbee.SetStopBit(StopBits.One);
                xbee.OnRecMsg += new Serial.SerialRecMsgEventHandler(xbee_OnRecMsg);
                lastPack = null;
                newPack = null;
                mutex = true;
                return res;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            return false;
        }

        public bool ClosePort()
        {
            return xbee.ClosePort();
        }


        void xbee_OnRecMsg(object sender, SerialRecMsgEventArgs args)
        {
            while (!mutex)
            {
                Thread.Sleep(1);
            }
            mutex = false;
            try
            {

                byte[] pack = CombineBytes(lastPack, args.MessageBuf);

                RecMsgArgs targs = new RecMsgArgs();
                Debug.WriteLine("上次：" + BytesToHexString(lastPack));
                Debug.WriteLine("新接收：" + BytesToHexString(args.MessageBuf));
                Debug.WriteLine("组合包：" + BytesToHexString(pack));
                lastPack = pack;
                while (pack != null)
                {
                    pack = GetPackage(lastPack, out lastPack);
                    try
                    {
                        if (lastPack != null)
                        {
                            Debug.WriteLine("剩余包：" + BytesToHexString(lastPack));
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine("空引用--" + e.Message);
                    }

                    Debug.WriteLine("完整包:" + BytesToHexString(pack));
                    if (pack == null)
                    {
                        return;
                    }
                    Debug.WriteLine("数据总长度：" + pack.Length.ToString());
                    byte p = (byte)pack[5];

                    UInt16 l = (UInt16)(((UInt16)pack[4]) * 256 + (UInt16)pack[3]);

                    Debug.WriteLine("数据有效字段长度：" + l.ToString());

                    byte[] data = GetBytes(pack, 6, l);


                    switch (p)
                    {
                        case (byte)PackDes.LL_Status:
                            targs.RawData = args.Message;
                            targs.DataType = typeof(LL_Status);
                            targs.Data = StructBytesConvertor.BytesToStruts(data, typeof(LL_Status));
                            SetRecEvent(targs);
                            break;
                        case (byte)PackDes.IMU_CalcData:
                            targs.RawData = args.Message;

                            targs.DataType = typeof(IMU_CalcData);
                            byte[] tmp = new byte[4];
                            for (int k = 0; k < 4; k++)
                            {
                                tmp[k] = data[k + 76];
                            }
                            /*
                            byte tmp = data[76];
                            data[76] = data[78];
                            data[78] = tmp;
                            tmp = data[77];
                            data[77] = data[79];
                            data[79] = tmp;*/
                            targs.Data = StructBytesConvertor.BytesToStruts(data, typeof(IMU_CalcData));
                            SetRecEvent(targs);
                            break;

                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("接收线程异常:" + e.Message);
            }
            finally
            {
                mutex = true;
            }
        }

        private byte[] GetBytes(byte[] data, int start, int length)
        {
            try
            {
                byte[] res = new byte[length];
                for (int i = start; i < start + length; i++)
                {
                    res[i - start] = data[i];
                }
                return res;
            }
            catch
            {

            }
            return null;
        }

        private byte[] CombineBytes(byte[] b1, byte[] b2)
        {
            try
            {
                int lb1 = 0, lb2 = 0;
                if (b1 != null)
                {
                    lb1 = b1.Length;
                }

                if (b2 != null)
                {
                    lb2 = b2.Length;
                }

                byte[] res = new byte[lb1 + lb2];
                if (lb1 > 0)
                {
                    for (int i = 0; i < lb1; i++)
                    {
                        res[i] = b1[i];
                    }
                }
                if (lb2 > 0)
                {
                    for (int i = 0; i < lb2; i++)
                    {
                        res[i + lb1] = b2[i];
                    }
                }
                return res;
            }
            catch (Exception e)
            {
                Debug.WriteLine("组合包错误：" + e.Message);
            }
            return null;
        }

        private byte[] GetPackage(byte[] data, out byte[] left)
        {
            byte[] bl = null;
            byte[] res = null;
            try
            {
                Debug.WriteLine("查找包头...包大小为:" + data.Length.ToString());
                int start = 0, end = data.Length - 1;
                for (int i = 0; i < data.Length - 5; i++)
                {
                    start = i;
                    if (data[i] == (byte)'>')
                    {
                        if (data[i + 1] == (byte)'*' && data[i + 2] == '>')
                        {
                            break;
                        }
                    }
                }
                end = start;
                if (start < data.Length - 6)
                {
                    Debug.WriteLine("包头为:" + start.ToString());
                    for (int i = start; i < data.Length - 2; i++)
                    {

                        if (data[i] == (byte)'<')
                        {
                            if (data[i + 1] == (byte)'#' && data[i + 2] == (byte)'<')
                            {
                                end = i + 2;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    end = 0;
                    Debug.WriteLine("未查找到包头.");
                }
                Debug.WriteLine("包尾为:" + end.ToString());
                int c = end - start;
                if (c > 0)
                {
                    res = new byte[end - start + 1];
                    for (int i = start; i <= end; i++)
                    {
                        res[i - start] = data[i];
                    }
                }
                int ls = 0;
                if (end < 2)
                {
                    ls = 0;
                }
                else
                {
                    ls = end + 1;
                }



                int lc = data.Length - ls;
                Debug.WriteLine("剩余包大小:" + lc.ToString());
                if (lc > 0)
                {
                    bl = new byte[lc];
                    Debug.WriteLine("left数组大小:" + bl.Length.ToString());
                    if (ls < data.Length)
                    {
                        for (int i = ls; i < data.Length; i++)
                        {
                            bl[i - ls] = data[i];
                            // Debug.WriteLine("复制" + i.ToString());
                        }
                    }
                }
                Debug.WriteLine("剩余包:" + BytesToHexString(bl));
            }
            catch (Exception e)
            {
                Debug.WriteLine("包查找错误:" + e.Message);
            }
            left = bl;
            Debug.WriteLine("包分析完毕.");
            return res;
        }

        private string BytesToHexString(byte[] b)
        {
            try
            {
                string result = string.Empty;
                for (int i = 0; i < b.Length; i++)//逐字节变为16进制字符，以%隔开
                {
                    result += " " + Convert.ToString(b[i], 16);
                }
                return result;
            }
            catch
            {

            }
            return "数据错误";
        }

        public void SetRecEvent(RecMsgArgs msg)
        {
            try
            {
                OnRecMsg(msg);
            }
            catch
            {
            }
        }

        public void SendMessage(string msg)
        {
            try
            {
                xbee.SendMessage(msg);
            }
            catch
            {

            }
        }

        public void QueryLLStatus()
        {
            string msg = ">*>p" + (char)0x01 + (char)0x00;
            SendMessage(msg);

        }

        public void QueryIMUCalcData()
        {
            string msg = ">*>p" + (char)0x04 + (char)0x00;
            SendMessage(msg);

        }
    }

    public class RecMsgArgs : EventArgs
    {
        public string RawData
        { get; set; }
        public Type DataType
        { get; set; }
        public object Data
        { get; set; }
    }

    public enum PollType
    {
        LL_Status = 0x0001,
        IMU_RawData = 0x0002,
        IMU_CalcData = 0x0004,
        RC_Data = 0x0008
    }

    public enum PackDes
    {
        LL_Status = 0x02,
        IMU_CalcData = 0x03
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public struct LL_Status
    {
        //电池电量
        public Int16 battery_voltage_1;
        public Int16 battery_voltage_2;

        public Int16 status;

        public Int16 cpu_load;

        public byte compass_enabled;
        public byte chksum_error;
        public byte flying;
        public byte motors_on;

        public Int16 flightMode;

        //马达启动时间
        public Int16 up_time;

    }
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public struct IMU_CalcData
    {
        //角度,24位
        public Int32 angle_nick;
        public Int32 angle_roll;
        public Int32 angle_yaw;


        public Int32 angvel_nick;
        public Int32 angvel_roll;
        public Int32 angvel_yaw;


        public Int16 acc_x_calib;
        public Int16 acc_y_calib;
        public Int16 acc_z_calib;


        public Int16 acc_x;
        public Int16 acc_y;
        public Int16 acc_z;

        public Int32 acc_angle_nick;
        public Int32 acc_angle_roll;
        public Int32 acc_absolute_value;

        public Int32 Hx;
        public Int32 Hy;
        public Int32 Hz;

        public Int32 Mag_heading;

        public Int32 speed_x;
        public Int32 speed_y;
        public Int32 speed_z;

        public Int32 height;

        public Int32 dheight;

        //diff. height measured by the pressure sensor [mm/s]
        public Int32 dheight_reference;
        //height measured by the pressure sensor [mm]
        public Int32 height_reference;
    }
}
