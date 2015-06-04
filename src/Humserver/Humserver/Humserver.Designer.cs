
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HumTrans;
namespace Humserver
{
    partial class Humserver
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartAt = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnQuery = new System.Windows.Forms.Button();
            this.textInfo = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.meterBattery = new Meters.Meter();
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.meterHeight = new Meters.Meter();
            this.meterPitch = new Meters.Meter();
            this.meterRoll = new Meters.Meter();
            this.meterYaw = new Meters.Meter();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartAt)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartAt
            // 
            chartArea3.Name = "ChartArea1";
            this.chartAt.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chartAt.Legends.Add(legend3);
            this.chartAt.Location = new System.Drawing.Point(-1, -2);
            this.chartAt.Name = "chartAt";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chartAt.Series.Add(series3);
            this.chartAt.Size = new System.Drawing.Size(1007, 238);
            this.chartAt.TabIndex = 0;
            this.chartAt.Text = "chartAt";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(164, 20);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(75, 23);
            this.btnQuery.TabIndex = 3;
            this.btnQuery.Text = "查询";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // textInfo
            // 
            this.textInfo.Location = new System.Drawing.Point(14, 563);
            this.textInfo.Multiline = true;
            this.textInfo.Name = "textInfo";
            this.textInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textInfo.Size = new System.Drawing.Size(982, 153);
            this.textInfo.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.meterYaw);
            this.panel1.Controls.Add(this.meterRoll);
            this.panel1.Controls.Add(this.meterPitch);
            this.panel1.Controls.Add(this.meterHeight);
            this.panel1.Controls.Add(this.meterBattery);
            this.panel1.Location = new System.Drawing.Point(1, 256);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1012, 240);
            this.panel1.TabIndex = 10;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // meterBattery
            // 
            this.meterBattery.BackColor = System.Drawing.Color.Transparent;
            this.meterBattery.CurrentValue = 9D;
            this.meterBattery.EndValue = 12.6D;
            this.meterBattery.FaceColor = System.Drawing.Color.Blue;
            this.meterBattery.Location = new System.Drawing.Point(29, 30);
            this.meterBattery.MeterArc = 240;
            this.meterBattery.MeterMargin = 18;
            this.meterBattery.MeterName = "Battery";
            this.meterBattery.MeterSize = 180;
            this.meterBattery.Name = "meterBattery";
            this.meterBattery.Size = new System.Drawing.Size(180, 180);
            this.meterBattery.StartValue = 9D;
            this.meterBattery.TabIndex = 0;
            // 
            // cbPort
            // 
            this.cbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPort.FormattingEnabled = true;
            this.cbPort.Location = new System.Drawing.Point(16, 20);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(125, 20);
            this.cbPort.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.cbPort);
            this.groupBox1.Location = new System.Drawing.Point(14, 502);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(982, 55);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(360, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "导出数据";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // meterHeight
            // 
            this.meterHeight.BackColor = System.Drawing.Color.Transparent;
            this.meterHeight.CurrentValue = 9D;
            this.meterHeight.EndValue = 100D;
            this.meterHeight.FaceColor = System.Drawing.Color.Blue;
            this.meterHeight.Location = new System.Drawing.Point(215, 30);
            this.meterHeight.MeterArc = 240;
            this.meterHeight.MeterMargin = 18;
            this.meterHeight.MeterName = "Height";
            this.meterHeight.MeterSize = 180;
            this.meterHeight.Name = "meterHeight";
            this.meterHeight.Size = new System.Drawing.Size(180, 180);
            this.meterHeight.StartValue = 0D;
            this.meterHeight.TabIndex = 1;
            // 
            // meterPitch
            // 
            this.meterPitch.BackColor = System.Drawing.Color.Transparent;
            this.meterPitch.CurrentValue = 9D;
            this.meterPitch.EndValue = 90D;
            this.meterPitch.FaceColor = System.Drawing.Color.Blue;
            this.meterPitch.Location = new System.Drawing.Point(439, 30);
            this.meterPitch.MeterArc = 240;
            this.meterPitch.MeterMargin = 18;
            this.meterPitch.MeterName = "Pitch";
            this.meterPitch.MeterSize = 180;
            this.meterPitch.Name = "meterPitch";
            this.meterPitch.Size = new System.Drawing.Size(180, 180);
            this.meterPitch.StartValue = -90D;
            this.meterPitch.TabIndex = 2;
            // 
            // meterRoll
            // 
            this.meterRoll.BackColor = System.Drawing.Color.Transparent;
            this.meterRoll.CurrentValue = 9D;
            this.meterRoll.EndValue = 90D;
            this.meterRoll.FaceColor = System.Drawing.Color.Blue;
            this.meterRoll.Location = new System.Drawing.Point(625, 30);
            this.meterRoll.MeterArc = 240;
            this.meterRoll.MeterMargin = 18;
            this.meterRoll.MeterName = "Roll";
            this.meterRoll.MeterSize = 180;
            this.meterRoll.Name = "meterRoll";
            this.meterRoll.Size = new System.Drawing.Size(180, 180);
            this.meterRoll.StartValue = -90D;
            this.meterRoll.TabIndex = 3;
            // 
            // meterYaw
            // 
            this.meterYaw.BackColor = System.Drawing.Color.Transparent;
            this.meterYaw.CurrentValue = 9D;
            this.meterYaw.EndValue = 360D;
            this.meterYaw.FaceColor = System.Drawing.Color.Blue;
            this.meterYaw.Location = new System.Drawing.Point(815, 30);
            this.meterYaw.MeterArc = 240;
            this.meterYaw.MeterMargin = 18;
            this.meterYaw.MeterName = "Yaw";
            this.meterYaw.MeterSize = 180;
            this.meterYaw.Name = "meterYaw";
            this.meterYaw.Size = new System.Drawing.Size(180, 180);
            this.meterYaw.StartValue = 0D;
            this.meterYaw.TabIndex = 4;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(249, 20);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(105, 23);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "清空图像";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // Humserver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 730);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textInfo);
            this.Controls.Add(this.chartAt);
            this.Name = "Humserver";
            this.Text = "Humserver";
            ((System.ComponentModel.ISupportInitialize)(this.chartAt)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartAt;
        private System.Windows.Forms.Button btnQuery;

        private TextBox textInfo;

        private Panel panel1;
        private ComboBox cbPort;
        private GroupBox groupBox1;
        private Button button1;
        private Meters.Meter meterBattery;
        private Meters.Meter meterYaw;
        private Meters.Meter meterRoll;
        private Meters.Meter meterPitch;
        private Meters.Meter meterHeight;
        private Button btnClear;

    }
}
