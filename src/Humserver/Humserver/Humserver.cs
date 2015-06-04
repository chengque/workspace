using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HumTrans;
using System.Threading;
using System.Drawing.Drawing2D;
using System.IO.Ports;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
namespace Humserver
{
    public partial class Humserver : Form
    {
        private Thread thQuery;
        public delegate void OnRecMsgHandler(RecMsgArgs args);
        private long sysTime;
        public Humserver()
        {
            InitializeComponent();
            Transmit.Instance.OnRecMsg += new Transmit.RecMsgEventHandler(Instance_OnRecMsg);
            chartAt.Series.Clear();
            chartAt.Series.Add("Pitch");
            chartAt.Series["Pitch"].ChartType = SeriesChartType.Line;
            chartAt.Series["Pitch"].YAxisType = AxisType.Primary;

            chartAt.Series.Add("Roll");
            chartAt.Series["Roll"].ChartType = SeriesChartType.Line;
            chartAt.Series["Roll"].YAxisType = AxisType.Secondary;
            
            sysTime = 0;
            /*
            Thread th = new Thread(new ThreadStart(QueryThread));
            th.IsBackground = true;
            th.Start();*/
            try
            {
                string[] sn = SerialPort.GetPortNames();
                for (int i = 0; i < sn.Length; i++)
                {
                    cbPort.Items.Add(sn[i]);
                }
                cbPort.SelectedIndex = 0;
            }
            catch
            {

            }
        }

        void Instance_OnRecMsg(RecMsgArgs args)
        {
            OnRecMsgHandler orm = new OnRecMsgHandler(OnRecMsg);
            BeginInvoke(orm, args);

        }

        void OnRecMsg(RecMsgArgs args)
        {
            try
            {
                if(args.DataType==typeof(LL_Status))
                {
                    LL_Status stat = (LL_Status)args.Data;
                    double batVol=(stat.battery_voltage_1)/1000.0;
                    meterBattery.CurrentValue=batVol;
                    textInfo.AppendText("状态数据LL_Status\r\n");
                }

               if(args.DataType==typeof(IMU_CalcData))
               {
                    IMU_CalcData icd=(IMU_CalcData)args.Data;
                   double heigt=icd.height/1000.0;
                   double pitch=icd.angle_nick/1000.0;
                   double roll=icd.angle_roll / 1000.0;
                   double yaw=icd.angle_yaw / 1000.0;
                    meterHeight.CurrentValue=heigt;
                    meterPitch.CurrentValue=pitch;
                    meterRoll.CurrentValue = roll;
                    meterYaw.CurrentValue = yaw;
                    chartAt.Series["Pitch"].Points.AddXY(sysTime / 10.0, pitch);
                    chartAt.Series["Roll"].Points.AddXY(sysTime / 10.0,roll);
                    //chartAt.Series["Yaw"].Points.AddXY(sysTime / 10.0, icd.angvel_yaw/1000.0);
                    textInfo.AppendText("飞行数据LL_Status\r\n");
               }

               if(args.DataType==null)
               {
                    textInfo.AppendText(string.Format("{0:X}",args.RawData)+"\r\n");
               }
               //Application.DoEvents();
            }
            catch
            {
            }
        }

       


        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                Debug.WriteLine("串口名:"+cbPort.Text);
                
                if (this.btnQuery.Text=="查询"&&Transmit.Instance.OpenPort(cbPort.Text))
                {
                    
                    this.btnQuery.Text = "停止";
                    thQuery = new Thread(new ThreadStart(QueryThread));
                    thQuery.IsBackground = true;
                    thQuery.Start();
                }
                else if(this.btnQuery.Text == "停止")
                {
                    
                    thQuery.Abort();
                    Application.DoEvents();
                    if (Transmit.Instance.ClosePort())
                    {
                        this.btnQuery.Text = "查询";
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void QueryThread()
        {
            try{
                while (true)
                {
               
                        sysTime += 1;
                        Thread.Sleep(500);
                        Transmit.Instance.QueryLLStatus();
                        //Thread.Sleep(50);
                        Transmit.Instance.QueryIMUCalcData();

                }
             }
            catch (Exception e)
            {
                Debug.WriteLine("查寻线程:"+e.Message);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush u = new LinearGradientBrush(new Point(512, 0), new Point(512, 25), Color.FromArgb(240,240,240),Color.FromArgb(100,100,100));
            LinearGradientBrush d = new LinearGradientBrush(new Point(512, 240), new Point(512,215), Color.FromArgb(240, 240, 240), Color.FromArgb(100, 100, 100));
            e.Graphics.FillRectangle(u, new Rectangle(0, 0, 1024, 25));
            e.Graphics.FillRectangle(d, new Rectangle(0, 215, 1024, 25));
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(200, 200, 200, 200)), new Rectangle(0, 25, 1024, 190));
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chartAt.Series.Count; i++)
            {
                chartAt.Series[i].Points.Clear();
            }
            sysTime = 0;
        }
    }
}
