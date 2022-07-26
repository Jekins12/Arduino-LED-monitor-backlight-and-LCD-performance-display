using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using OpenHardwareMonitor.Hardware;


namespace Arduino_CPU_and_GPU_temperarure_display
{
    
    public partial class Form1 : Form
    {
        
        ColorDialog colorDialog = new ColorDialog();
        SerialPort port = new SerialPort();
        Computer c = new Computer()
        {
            GPUEnabled = true,
            CPUEnabled = true
        };

        int value1, value2, value3, value4, tick, hue;

        bool flag = true;

        public Form1()
        {
            InitializeComponent();
            Init();
            c.Open();
            button1_Click(null,null);
            
        }


        private void Init()
        {
            colorDialog.SolidColorOnly = true;
            radioWhite.Checked = true;
            comboBox2.SelectedIndex = 4;
            try
            {
                notifyIcon1.Visible = false;
                port.Parity = Parity.None;
                port.StopBits = StopBits.One;
                port.DataBits = 8;
                port.Handshake = Handshake.None;
                port.RtsEnable = true;
                string[] ports = SerialPort.GetPortNames();
                foreach (string port in ports)
                {
                    comboBox1.Items.Add(port);
                }
                port.BaudRate = 9600;
                comboBox1.SelectedIndex = 1;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            Status();
            tick++;
            
            if (flag)
            {
                notifyIcon1.Visible = true;
                Hide();
            }    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!port.IsOpen)
                {
                    port.PortName = comboBox1.Text;
                    port.Open();
                    timer1.Interval = Convert.ToInt32(comboBox2.Text);
                    timer1.Enabled = true;
                    timer2.Interval = 30;
                    timer2.Enabled = true;
                    label3.Text = "Connected";
                    port.Write("c");
                    ledCheck.Enabled = true;
                    button2.Enabled = true;
                    ledCheck.BackColor = Color.Lime;
                    ledCheck.Text = "LED On";
                    Brightness.Enabled = true;
                    ColorButton.Enabled = true;
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                port.Write("DIS*");
                port.Close();
                ledCheck.Enabled = false;
                button2.Enabled = false;
                ledCheck.BackColor = Color.Red;
                ledCheck.Text = "LED Off";
                Brightness.Enabled = false;
                ColorButton.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            label3.Text = "Disconnected";
            timer1.Enabled = false;
            timer2.Enabled = false;
        }

       

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            flag = false;
            Show();
            ShowInTaskbar = true;
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            

            if (radioTemperature.Checked == true)
            {
                LED();
            }
            
            else if (radioWhite.Checked == true)
            {
                setColor(0, true);
            }

            else if (radioSingle.Checked == true)
            {
                setColor(hue, false);
            }

            else if (radioUA.Checked == true)
            {
                setMode("UA");
            }

        }

        private void Color_Click(object sender, EventArgs e)
        {
            colorDialog.ShowDialog();
            Color color = colorDialog.Color;
            hue = (int)color.GetHue();
        }

        

        private void radioTemperature_CheckedChanged(object sender, EventArgs e)
        {
            if (radioTemperature.Checked == true)
            {
                timer2.Interval = 30;
                ColorButton.Enabled = false;
            }
            else
                ColorButton.Enabled = true;
        }

        private void radioWhite_CheckedChanged(object sender, EventArgs e)
        {
            if (radioWhite.Checked == true)
            {
                timer2.Interval = 100;
                ColorButton.Enabled = false;
            }
                

            else
                ColorButton.Enabled = true;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Visible = true;   
                Hide();
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (port.IsOpen == true)
            {
                port.Write("DIS*");
                port.Close();
            }
            else
                port.Close();
            
        }

        private void Brightness_Scroll(object sender, EventArgs e)
        {
            setBrightness(Brightness.Value);
        }

        private void radioSingle_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSingle.Checked == true)
            {
                timer2.Interval = 100;
                
            }
        }

        private void ledCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (ledCheck.Checked == false)
            {
                ledCheck.BackColor = Color.Red;
                ledCheck.Text = "LED Off";
                setBrightness(0);
                Brightness.Enabled = false;
            }

            else if(ledCheck.Checked == true)
            {
                ledCheck.BackColor = Color.Lime;
                ledCheck.Text = "LED On";
                setBrightness(170);
                Brightness.Enabled = true;
            }
        }

        private void radioUA_CheckedChanged(object sender, EventArgs e)
        {
            if (radioUA.Checked == true)
            {
                timer2.Interval = 100;
                ColorButton.Enabled = false;
            }


            else
                ColorButton.Enabled = true;
        }

        private void LED()
        {
            int temp=0;
            
            foreach (var hardwadre in c.Hardware)
            {
                if (hardwadre.HardwareType == HardwareType.GpuNvidia)
                {
                    hardwadre.Update();
                    foreach (var sensor in hardwadre.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                            temp = (int)sensor.Value.GetValueOrDefault();

                    }
                }
            }
            try
            {
                port.Write(temp + "T");
            }

            catch (Exception ex)
            {
                timer1.Stop();
                timer2.Stop();
                MessageBox.Show(ex.Message);
                label3.Text = "Arduino's not responding...";
            }

        }

        private void setMode(string mode)
        {
            try
            {
                port.Write(mode + "m");
            }

            catch (Exception ex)
            {
                timer1.Stop();
                timer2.Stop();
                MessageBox.Show(ex.Message);
                label3.Text = "Arduino's not responding...";
            }
        }

        private void setColor(int hue, bool white)
        {
            if (!white)
            {
                try
                {
                    port.Write(hue + "C");
                }

                catch (Exception ex)
                {
                    timer1.Stop();
                    timer2.Stop();
                    MessageBox.Show(ex.Message);
                    label3.Text = "Arduino's not responding...";
                }
            }
            else
            {
                try
                {
                    port.Write("W");
                }

                catch (Exception ex)
                {
                    timer1.Stop();
                    timer2.Stop();
                    MessageBox.Show(ex.Message);
                    label3.Text = "Arduino's not responding...";
                }
            }
            
        }

        private void setBrightness(int brightness)
        {
            try
            {
                port.Write(brightness + "B");
            }

            catch (Exception ex)
            {
                timer1.Stop();
                timer2.Stop();
                MessageBox.Show(ex.Message);
                label3.Text = "Arduino's not responding...";
            }
        }



        private void Status()
        {
            if (tick < 20)
            {
                foreach (var hardwadre in c.Hardware)
                {
                    if (hardwadre.HardwareType == HardwareType.GpuNvidia)
                    {
                        hardwadre.Update();
                        foreach (var sensor in hardwadre.Sensors)
                        {
                            if (sensor.SensorType == SensorType.Temperature)
                                value1 = (int)sensor.Value.GetValueOrDefault();

                            if (sensor.SensorType == SensorType.Load)
                                value3 = (int)sensor.Value.GetValueOrDefault();
                        }


                    }
                    if (hardwadre.HardwareType == HardwareType.CPU)
                    {
                        hardwadre.Update();
                        foreach (var sensor in hardwadre.Sensors)
                        {
                            if (sensor.SensorType == SensorType.Temperature)
                                value2 = (int)sensor.Value.GetValueOrDefault();

                            if (sensor.SensorType == SensorType.Load)
                                value4 = (int)sensor.Value.GetValueOrDefault();

                        }

                    }

                    try
                    {
                        port.Write(value1 + "*" + value2 + "#" + value3 + "$" + value4 + "%");
                    }

                    catch (Exception ex)
                    {
                        timer1.Stop();
                        MessageBox.Show(ex.Message);
                        label3.Text = "Arduino's not responding...";
                    }

                }
            }
            
           
            if (tick >= 20)
            {
                if (tick == 25) tick = 0;
                    //string input = port.ReadExisting();
                    //string keyword = "1";
                try
                {
                    port.Write(DateTime.Now.ToString("HH:mm:ss") + "t" + DateTime.Now.ToString("dd-MM-yyyy") + "d");
                }

                catch (Exception ex)
                {
                    timer1.Stop();
                    MessageBox.Show(ex.Message);
                    label3.Text = "Arduino's not responding...";
                }
            }
            timer2.Start();
        }
    }
}
