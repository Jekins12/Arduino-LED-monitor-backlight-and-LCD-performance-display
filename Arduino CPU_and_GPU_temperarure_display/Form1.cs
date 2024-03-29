﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;
using OpenHardwareMonitor.Hardware;
using System.Windows;


namespace Arduino_CPU_and_GPU_temperarure_display
{
    
    public partial class Form1 : Form
    {
        Bitmap bitmap = new Bitmap(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
        ColorDialog colorDialog = new ColorDialog();
        SerialPort port = new SerialPort();
        Computer c = new Computer()
        {
            GPUEnabled = true,
            CPUEnabled = true
        };

        int value1, value2, value3, value4, tick;
        int R = 255, G = 255, B = 255;
        

        bool hide_on_open = false;           // Hide window on start
        int NUM_LEDS = 30;                  // define number of led's  (I have 1 strip (15 led) on top of the monitor and 1 (15 -> bottom))

        int resolution_X = 4;               // default 4 (1-7);
        int resolution_Y = 4;               // default 
        
        
        


        public Form1()
        {
            InitializeComponent();
            Init();
            c.Open();
            button1_Click(null,null);      // Autoconnect
            
        }


        private void Init()
        {
            colorDialog.SolidColorOnly = true;
            radioWhite.Checked = true;
            comboBox2.SelectedIndex = 4;

            try
            {
                notifyIcon1.Visible = true;
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
                port.BaudRate = 500000;             // 500000 baud rate only for Ambilight mode due to heavy data packets (9600 will be good without that mode)
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
            
            if (hide_on_open)
            {
                Hide();
                hide_on_open = false;
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
                    timer2.Interval = 50;
                    timer2.Enabled = true;

                    label3.Text = "Connected";
                    port.Write("c");
                    ledCheck.Enabled = true;
                    button2.Enabled = true;
                    ledCheck.BackColor = Color.Lime;
                    ledCheck.Text = "LED On";
                    Brightness.Enabled = true;
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
                Show();
                ShowInTaskbar = true;
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
                setColor(255,255,255);
            }

            else if (radioSingle.Checked == true)
            {
                setColor(R, G, B);
            }

            else if (radioUA.Checked == true)
            {
                setMode();
            }

            else if (radioAmbilight.Checked == true)
            {
                radioAmbilight_CheckedChanged(null, null);
                setAmbient();
            }

        }

        private void Color_Click(object sender, EventArgs e)
        {
            colorDialog.ShowDialog();
            Color color = colorDialog.Color;

            R = color.R;
            G = color.G;
            B = color.B;
        }

        

        private void radioTemperature_CheckedChanged(object sender, EventArgs e)
        {
            if (radioTemperature.Checked == true)
            {
                ColorButton.Enabled = false;
            }
            else
                ColorButton.Enabled = true;
        }

        private void radioWhite_CheckedChanged(object sender, EventArgs e)
        {
            if (radioWhite.Checked == true)
            {
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



        private void ledCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (ledCheck.Checked == false)
            {
                ledCheck.BackColor = Color.Red;
                ledCheck.Text = "LED Off";
                setBrightness(0);
                Brightness.Enabled = false;
                timer2.Stop();
            }

            else if(ledCheck.Checked == true)
            {
                ledCheck.BackColor = Color.Lime;
                ledCheck.Text = "LED On";
                setBrightness(170);
                Brightness.Enabled = true;
                timer2.Start();
            }
        }

        private void radioUA_CheckedChanged(object sender, EventArgs e)
        {
            if (radioUA.Checked == true)
            {
                ColorButton.Enabled = false;
            }

            else
                ColorButton.Enabled = true;
        }


       

        private void LED()                      // Getting CPU temperature and sending it's value to Arduino where it's converted to color
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

        private void setMode()              // "Patriotic" mode XD --> defined in Arduino code
        {
            try
            {
                port.Write("m");
            }

            catch (Exception ex)
            {
                timer1.Stop();
                timer2.Stop();
                MessageBox.Show(ex.Message);
                label3.Text = "Arduino's not responding...";
            }
        }

        private void radioAmbilight_CheckedChanged(object sender, EventArgs e)
        {
            if (radioAmbilight.Checked == true)
            {
                timer1.Stop();                          // Stop sending data to LCD ( because of the led glitching )   need to fix (probablly add start and end pointers to data packet)
                ColorButton.Enabled = false;
                ambiMode.Visible = true;
            }

            else
            {
                timer1.Start();
                ColorButton.Enabled = true;
                ambiMode.Visible = false;
            }
                
        }

        private void ambiNormal_CheckedChanged(object sender, EventArgs e)
        {

        }

        public int[] getFrame(int frame_num)                // Calculating average color for 1 frame (1 frame == 1 led) 
        {
            int shift = 0;

            if (ambiNormal.Checked == true)
                shift = 0;

            if(ambiWide.Checked == true)
                shift = 140;
        

            int[] frame = new int[3];

            if (frame_num == 1)                                             // Taking scrennshot every first "frame"
            {
                Graphics graphics = Graphics.FromImage(bitmap as Image); // Create a new graphics objects that can capture the screen
                try
                {
                    graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size); // Screenshot moment → screen content to graphics object
                }
                catch (Exception) { }

            }


            int sumR = 0;
            int sumG = 0;
            int sumB = 0;

            if (frame_num <= NUM_LEDS / 2)
            {
                for (int x = bitmap.Width / (NUM_LEDS / 2) * frame_num - bitmap.Width / (NUM_LEDS / 2); x < bitmap.Width / (NUM_LEDS / 2) * frame_num; x += (int)Math.Pow(resolution_X, 2))
                {

                    for (int y = 0 + shift; y < bitmap.Height / 6 + shift; y += 18)
                    {

                        byte rr = bitmap.GetPixel(x, y).R;
                        byte gg = bitmap.GetPixel(x, y).G;
                        byte bb = bitmap.GetPixel(x, y).B;

                        sumR += rr;
                        sumG += gg;
                        sumB += bb;
                    }
                }
            }

            if (frame_num > NUM_LEDS / 2)
            {
                frame_num -= NUM_LEDS / 2;

                for (int x = bitmap.Width / (NUM_LEDS / 2) * frame_num - bitmap.Width / (NUM_LEDS / 2); x < bitmap.Width / (NUM_LEDS / 2) * frame_num; x += (int)Math.Pow(resolution_X, 2))
                {
                    for (int y = bitmap.Height / 6 * 5 - shift; y < bitmap.Height - shift; y += 18)
                    {
                        byte rr = bitmap.GetPixel(x, y).R;
                        byte gg = bitmap.GetPixel(x, y).G;
                        byte bb = bitmap.GetPixel(x, y).B;

                        sumR += rr;
                        sumG += gg;
                        sumB += bb;
                    }
                }
            }

            frame[0] = sumR / ((bitmap.Width / (NUM_LEDS / 2)) / 16 * ((bitmap.Height / 6)) / 18);
            frame[1] = sumG / ((bitmap.Width / (NUM_LEDS / 2)) / 16 * ((bitmap.Height / 6)) / 18);
            frame[2] = sumB / ((bitmap.Width / (NUM_LEDS / 2)) / 16 * ((bitmap.Height / 6)) / 18);

            return frame;

        }


        public int[] GetScreen()
        {
            int[] screen = new int[3 * NUM_LEDS];
            int[] screen_temp = new int[3];

            for (int i = 1; i <= NUM_LEDS; i++)
            {

                screen_temp = getFrame(i);
                screen[i * 3 - 3] = screen_temp[0];
                screen[i * 3 - 2] = screen_temp[1];
                screen[i * 3 - 1] = screen_temp[2];
            }


            return screen;
        }


        public void setAmbient()                // Ambilight mode
        {
            
            int[] screen = GetScreen();

            try
            {
                port.Write(string.Join(" ", screen) + "A");
            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }




        private void setColor(int R, int G, int B)               // Color fill
        {

            try
            {
                port.Write(R + " " + G + " " + B + " C");
            }

            catch (Exception ex)
            {
                timer1.Stop();
                timer2.Stop();
                MessageBox.Show(ex.Message);
                label3.Text = "Arduino's not responding...";
            }
        }

        private void setBrightness(int brightness)          // Set brightness for led strip
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



        private void Status()               // Sending data for LCD
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
