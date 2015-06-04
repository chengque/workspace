////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
/////--本文件为串口通信类，基于Microsoft串口基础类构建//////////////
/////--董伟 2009.12.15 创建 实现通信所需功能          //////////////
/////--当前版本号:V0.1                                //////////////
////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.IO;
using System.Diagnostics;
using System.Threading;
namespace Chengque.SerialSup
{
    /// <summary>
    /// Serial类封装了通信所需功能及其基本设定。
    /// </summary>
    public class Serial
    {
        #region class menber
        /// <summary>
        /// 报文当前接收缓冲区大小
        /// </summary>
        private int msgBufSize;
        public int MsgBufSize 
        {
            get { return msgBufSize; }
            set { msgBufSize = value; }
        }
        /// <summary>
        /// 报文接收缓冲区
        /// </summary>
        private byte[] msgBuf;
        public byte [] MsgBuf
        {
            get { return msgBuf; }
            set { msgBuf = value; }
        }

        public bool IsBufferLocked { get; set; }

        public bool IsCheckStartFlag { get; set; }

        public byte StartFlag { get; set; }
        /// <summary>
        /// 报文字节大小
        /// </summary>
        private int msgSize;
        /// <summary>
        /// 异常记录
        /// </summary>
        public SerialExceptionArgs LastException { get; set; }
        /// <summary>
        /// 串口通信端口实例
        /// </summary>
        private SerialPort serialPort;
        /// <summary>
        /// 报文获取事件，当一个完整报文获取后，该事件被触发
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="args">事件参数</param>
        public delegate void SerialRecMsgEventHandler(object sender, SerialRecMsgEventArgs args);
        /// <summary>
        /// 当通信异常时此事件被触发
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="args">事件参数</param>
        public delegate void SerialRecErrorHandler(object sender, SerialErrorReceivedEventArgs args);
        /// <summary>
        /// 串口通信异常处理
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="args">事件参数</param>
        public delegate void SerialExceptionHandler(object sender, SerialExceptionArgs args);

        public delegate void SendMessageEventHandler(string content);
       
        /// <summary>
        /// 通信异常处理事件句柄
        /// </summary>
        public event SerialRecErrorHandler OnRecError;
        /// <summary>
        /// 报文接受处理事件句柄
        /// </summary>
        public event SerialRecMsgEventHandler OnRecMsg;

        public event SerialExceptionHandler OnException;

        public event SendMessageEventHandler SendMessageEvent;

        //public Thread ThDataReceive;
        #endregion
        /// <summary>
        /// Serial 构造函数，主要用于初始化参数。
        /// </summary>
        public Serial()
        {
            try
            {
                IsBufferLocked = false;
                IsCheckStartFlag = true;
                StartFlag = (byte)'i';
                serialPort = new SerialPort();
                serialPort.DataReceived+=new SerialDataReceivedEventHandler(OnRecData);
                serialPort.ErrorReceived+=new SerialErrorReceivedEventHandler(OnSerialRecError);
                msgBuf = new byte[8092];
                LastException = new SerialExceptionArgs();
                serialPort.ReceivedBytesThreshold = 1;
                
            }
            catch(Exception e)
            {
                ReportException((Exception)e,0);
            }
        }
        /*
        public void DataReceive()
        {
              while(serialPort.BytesToRead)
              {
                try
                {
                    int recSize;
                    IsBufferLocked = true;
                    recSize = serialPort.BytesToRead;
                    if (recSize < 1)
                        break;

                    string serialPort.ReadLine();

                    for (int i = 0; i < recSize; i++)
                    {
                        msgBuf[msgBufSize++] = buf[i];
                    }

                    //Trace.WriteLine(msgBufSize.ToString());
                    while (((msgBufSize >= msgSize) && (msgSize != 0)) || ((msgBufSize > 0) && (msgSize == 0)))
                    {
                        SerialRecMsgEventArgs sme = new SerialRecMsgEventArgs();

                        if (msgSize == 0)
                        {
                            sme.Message = new byte[recSize];
                            for (int i = 0; i < recSize; i++)
                            {
                                sme.Message[i] = msgBuf[i];
                            }
                            msgBufSize = 0;
                        }
                        else
                        {
                            sme.Message = new byte[msgSize];

                            if (IsCheckStartFlag&&(msgBuf[0] != StartFlag))
                            {
                                for (int i = 0; i < msgBufSize; i++)
                                {
                                    if (msgBuf[i] == StartFlag)
                                    {
                                        for (int j = i; j < MsgBufSize; j++)
                                        {
                                            msgBuf[j - i] = msgBuf[j];
                                        }
                                        msgBufSize -= i;
                                        break;

                                    }
                                }
                            }

                            if (msgBufSize < msgSize)
                            {
                                break;
                            }
                            for (int i = 0; i < msgSize; i++)
                            {
                                sme.Message[i] = msgBuf[i];
                            }
                            msgBufSize = msgBufSize - msgSize;
                            for (int i = 0; i < (msgBufSize - msgSize); i++)
                            {
                                msgBuf[i] = msgBuf[i + msgSize];
                            }
                            sme.recBuf = msgBuf;
                        }
                        OnRecMsg((object)this, sme);
                        IsBufferLocked = false;
                    }
                }
                catch (Exception e)
                {
                    ReportException(e, 0);
                }
                finally
                {
                    IsBufferLocked = false;
                    serialPort.ReceivedBytesThreshold = 1;
                }
            }
        }
        */
        public void ReportException(Exception e,int index)
        {
            LastException.ExceptionDescription = e.Message;
            LastException.ExceptionWord = index;
            LastException.ExceptionBox = e;
            try
            {
                Trace.WriteLine(this.ToString()+e.Message);
                OnException((object)this, LastException);
            }
            catch
            { 
            
            }
        }
        /// <summary>
        /// SetBaudRate:对通信端口的波特率进行设定。若出现异常可通过LastException查看异常描述。
        /// </summary>
        /// <param name="baudrate">待设定波特率</param>
        /// <returns>设定后的波特率，若出现异常则返回0。</returns>
        public int SetBaudRate(long baudrate)
        {
            try
            {
                serialPort.BaudRate = (int)baudrate;
                return serialPort.BaudRate;
            }
            catch (ArgumentOutOfRangeException e)
            {
                ReportException((Exception)e,1);
            }
            catch (IOException e)
            {
                 ReportException((Exception)e,2);
            }
            catch (Exception e)
            {
                 ReportException((Exception)e,0);
            }
            return 0;
        }

        /// <summary>
        /// 通信端口设定，若出现异常可查看LastException
        /// </summary>
        /// <param name="port">待设定端口</param>
        /// <returns>设定成功则返回true，否则返回false。</returns>
        public bool SetPort(string port)
        {
            try
            {
                serialPort.PortName = port;
                return true;
            }
            catch (ArgumentOutOfRangeException e)
            {
                 ReportException((Exception)e,1);
            }
            catch (ArgumentNullException e)
            {
                ReportException((Exception)e,2);
            }
            catch (InvalidOperationException e)
            {
                 ReportException((Exception)e,3);
            }
            catch (Exception e)
            {
                 ReportException((Exception)e,0);
            }
            return false;
        }

        /// <summary>
        /// 取得系统所有可用串口
        /// </summary>
        /// <returns>返回串口名字的字符串集,若发生异常则返回null。</returns>
        public static string[] GetAllPorts()
        {
            try
            {
                return SerialPort.GetPortNames(); 
            }
            catch
            {
            }
                /*
            catch (Win32Exception e)
            {
                 //ReportException((Exception)e,1);
            }
            catch (Exception e)
            {
                 //ReportException((Exception)e,0);
            }
                 * */
            return null;
        }
        /// <summary>
        /// 串口停止位设定
        /// </summary>
        /// <param name="stopbit">停止位,StopBits枚举型</param>
        /// <returns>返回设定好的停止位</returns>
        public StopBits SetStopBit(StopBits stopbit)
        {
            try
            {
                serialPort.StopBits = stopbit;
                return serialPort.StopBits;
            }
            catch (ArgumentOutOfRangeException e)
            {
                ReportException((Exception)e,1);
            }
            catch (IOException e)
            {
                ReportException((Exception)e,2);
            }
            catch (Exception e)
            {
                ReportException((Exception)e,0);
            }
            return 0;
        }

        /// <summary>
        /// 设置流控Dtr
        /// </summary>
        /// <param name="dtr">设置DTR使能与否</param>
        /// <returns>设置后的串口DTR</returns>
        public bool SetDTR(bool dtr)
        {
            try
            {
                serialPort.DtrEnable = dtr;
                return dtr;
            }
            catch (IOException e)
            {
                ReportException((Exception)e, 1);
            }
            catch (Exception e)
            {
                ReportException(e, 0);
            }
            return false;
        }

        /// <summary>
        /// 设置RTS
        /// </summary>
        /// <param name="rts">设置或取消RTS</param>
        /// <returns>设置后的串口RTS</returns>
        public bool SetRTS(bool rts)
        {
            try
            {
                serialPort.RtsEnable = rts;
                return rts;
            }
            catch (IOException e)
            {
                ReportException((Exception)e, 1);
            }
            catch (Exception e)
            {
                ReportException(e, 0);
            }
            return false;
        }
        /// <summary>
        /// 奇偶校验设定
        /// </summary>
        /// <param name="parity">待设定奇偶校验</param>
        /// <returns>成功则为真，否则为假</returns>
        public bool SetParity(Parity parity)
        {
            try
            {
                serialPort.Parity = parity;
                return true;
            }
            catch (Exception e)
            {
                ReportException(e, 0);
            }
            return false;
        }
        /// <summary>
        /// 设置写超时
        /// </summary>
        /// <param name="time">待设置超时时间</param>
        /// <returns>设定后的超时时间，若有异常则返回0。</returns>
        public int SetWriteTimeout(int time)
        {
            try
            {
                serialPort.WriteTimeout = time;
                return serialPort.WriteTimeout;
            }
            catch (ArgumentOutOfRangeException e)
            {
                ReportException((Exception)e,1);
            }
            catch (IOException e)
            {
                ReportException((Exception)e,2);
            }
            catch (Exception e)
            {
                ReportException((Exception)e,0);
            }
            return 0;
        }
        /// <summary>
        /// 设置写超时
        /// </summary>
        /// <param name="time">待设置超时时间</param>
        /// <returns>设定后的超时时间，若有异常则返回0。</returns>
        public int SetReadTimeout(int time)
        {
            try
            {
                serialPort.ReadTimeout = time;
                return serialPort.ReadTimeout;
            }
            catch (ArgumentOutOfRangeException e)
            {
                ReportException((Exception)e,1);
            }
            catch (IOException e)
            {
                ReportException((Exception)e,2);
            }
            catch (Exception e)
            {
                ReportException((Exception)e,3);
            }
            return 0;
        }

        /// <summary>
        /// 取得当前串口通信类实例
        /// </summary>
        /// <returns>当前串口通信类实例</returns>
        public SerialPort GetSerialPort()
        {
            try
            {
                return serialPort;
            }
            catch (Exception e)
            {
                ReportException(e, 0);
            }
            return null;
        }

        /// <summary>
        /// 设置报文字节大小
        /// </summary>
        /// <param name="size">报文每包字节大小</param>
        public void SetMsgSize(int size)
        {
            msgSize = size;
            //msgBuf=new byte[msgSize];
        }

        /// <summary>
        /// 串口接收数据处理事件
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="args">事件参数</param>
        public void OnRecData(object sender, SerialDataReceivedEventArgs args)
        {

            serialPort.ReceivedBytesThreshold = 8092;

            try
            {
                int recSize;
                recSize = serialPort.BytesToRead;
                SerialRecMsgEventArgs sme = new SerialRecMsgEventArgs();
                sme.MessageBuf = new byte[recSize];
                serialPort.Read(sme.MessageBuf, 0, recSize);
                OnRecMsg((object)this, sme);

            }
            catch (Exception e)
            {
                ReportException(e, 0);
            }
            finally
            {
                IsBufferLocked = false;
                serialPort.ReceivedBytesThreshold = 1;
            }
        }

        public static string BytesToString(byte[] data)
        {
            string res = string.Empty;
            for (int i = 0; i < data.Length; i++)
            {
                res += data[i];
            }
            return res;
        }

        public static byte[] StringToBytes(string data)
        {
            byte[] res = new byte[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                res[i] = (byte)data[i];
            }
            return res;
        }
        /// <summary>
        /// 串口接收数据异常事件
        /// </summary>
        /// <param name="sender">事件发送者</param>
        /// <param name="args">事件参数</param>
        public void OnSerialRecError(object sender, SerialErrorReceivedEventArgs args)
        {
            try
            {
                OnRecError(sender, args);
            }
            catch
            { 
            
            }
        }

        /// <summary>
        /// 串口发送报文
        /// </summary>
        /// <param name="msg">待发送发文</param>
        /// <returns>若发送成功则返回真，否则为假</returns>
        public bool SendMessage(byte[] msg)
        {
            try
            {
                serialPort.Write(msg, 0, msg.Length);
                OnSendMessage(msg);
                return true;
            }
            catch (Exception e)
            {
                ReportException(e, 0);
                Trace.WriteLine("SerialSend"+e.Message);
            }
            return false;
        }

        private void OnSendMessage(byte[] msg)
        {
            try
            {
                string str = "["+this.serialPort.PortName+"]发送：";
                for (int i = 0; i < msg.Length; i++)
                { 
                    str=str+" "+msg[i].ToString("X2");
                }
                SendMessageEvent(str);
            }
            catch
            { 
            
            }
        }

        /// <summary>
        /// 串口发送报文
        /// </summary>
        /// <param name="msg">待发送发文</param>
        /// <returns>若发送成功则返回真，否则为假</returns>
        public bool SendMessage(string msg)
        {
            try
            {
               SendMessage(Encoding.ASCII.GetBytes(msg));
                return true;
            }
            catch (Exception e)
            {
                ReportException(e, 0);
            }
            return false;
        }

        /// <summary>
        /// 打开预指定端口。
        /// </summary>
        /// <returns>成功则返回真，否则为假</returns>
        public bool OpenPort()
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    return serialPort.IsOpen;
                }
                serialPort.Open();
                //ThDataReceive.Start();

                return serialPort.IsOpen;
            }
            catch (Exception e)
            {
                ReportException(e, 0);
            }
            return false;
        }
        /// <summary>
        /// 打开指定端口
        /// </summary>
        /// <param name="port">端口名</param>
        /// <returns>成功则返回真，否则为假</returns>
        public bool OpenPort(string port)
        {
            
            try
            {
                serialPort.PortName = port;
                OpenPort();
                return serialPort.IsOpen;
            }
            catch (Exception e)
            {
                ReportException(e, 0);
            }
            return false;
        }

        /// <summary>
        /// 打开指定端口
        /// </summary>
        /// <param name="port">端口名</param>
        /// <param name="baud">波特率</param>
        /// <returns>成功则返回真，否则为假</returns>
        public bool OpenPort(string port,int baud)
        {

            try
            {
                serialPort.PortName = port;
                serialPort.BaudRate = baud;
                return OpenPort();
            }
            catch (Exception e)
            {
                ReportException(e, 0);
            }
            return false;
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns>若成功关闭则为真，否则为假。</returns>
        public bool ClosePort()
        {
            try {
                
                serialPort.Close();
                return true;
                //return !serialPort.IsOpen;
            }
            catch (Exception e)
            {
                ReportException(e, 0);
            }
            return false;

        }

        /// <summary>
        /// 返回端口是否打开
        /// </summary>
        /// <returns>若打开则为真，否则为假</returns>
        public bool IsPortOpen()
        {
            return serialPort.IsOpen;
        }
        
    }



    /// <summary>
    ///  串口接收数据事件参数
    /// </summary>

    public class SerialRecMsgEventArgs:EventArgs
    {
        public byte[] MessageBuf { get; set; }
        public byte[] recBuf { get; set; }
        public string Message { get; set; }
    }
    /// <summary>
    /// 通信异常管理类
    /// </summary>
    public class SerialExceptionArgs:EventArgs
    {

        /// <summary>
        /// 异常描述
        /// </summary>
        public string ExceptionDescription{get;set;}
        /// <summary>
        /// 异常标识
        /// </summary>
        public int ExceptionWord { get; set; }
        /// <summary>
        /// 异常装箱
        /// </summary>
        public Exception ExceptionBox { get; set; }
    }
}
