﻿
namespace Arduino_CPU_and_GPU_temperarure_display
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.ColorButton = new System.Windows.Forms.Button();
            this.Brightness = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.radioTemperature = new System.Windows.Forms.RadioButton();
            this.radioSingle = new System.Windows.Forms.RadioButton();
            this.radioWhite = new System.Windows.Forms.RadioButton();
            this.ledCheck = new System.Windows.Forms.CheckBox();
            this.radioUA = new System.Windows.Forms.RadioButton();
            this.radioAmbilight = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.Brightness)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(13, 37);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "100",
            "250",
            "500",
            "750",
            "1000"});
            this.comboBox2.Location = new System.Drawing.Point(12, 83);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Set port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Interval";
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Location = new System.Drawing.Point(152, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Location = new System.Drawing.Point(233, 37);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Disconnect";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(214, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Disconnected";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Temperature";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(148, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Status:";
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // ColorButton
            // 
            this.ColorButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ColorButton.Location = new System.Drawing.Point(13, 129);
            this.ColorButton.Name = "ColorButton";
            this.ColorButton.Size = new System.Drawing.Size(75, 23);
            this.ColorButton.TabIndex = 8;
            this.ColorButton.Text = "Select color";
            this.ColorButton.UseVisualStyleBackColor = true;
            this.ColorButton.Click += new System.EventHandler(this.Color_Click);
            // 
            // Brightness
            // 
            this.Brightness.Cursor = System.Windows.Forms.Cursors.Default;
            this.Brightness.Location = new System.Drawing.Point(106, 129);
            this.Brightness.Maximum = 255;
            this.Brightness.Name = "Brightness";
            this.Brightness.Size = new System.Drawing.Size(121, 45);
            this.Brightness.TabIndex = 11;
            this.Brightness.Value = 150;
            this.Brightness.Scroll += new System.EventHandler(this.Brightness_Scroll);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(137, 158);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "Brightness";
            // 
            // radioTemperature
            // 
            this.radioTemperature.AutoSize = true;
            this.radioTemperature.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioTemperature.Location = new System.Drawing.Point(259, 106);
            this.radioTemperature.Name = "radioTemperature";
            this.radioTemperature.Size = new System.Drawing.Size(85, 17);
            this.radioTemperature.TabIndex = 13;
            this.radioTemperature.TabStop = true;
            this.radioTemperature.Text = "Temperature";
            this.radioTemperature.UseVisualStyleBackColor = true;
            this.radioTemperature.CheckedChanged += new System.EventHandler(this.radioTemperature_CheckedChanged);
            // 
            // radioSingle
            // 
            this.radioSingle.AutoSize = true;
            this.radioSingle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioSingle.Location = new System.Drawing.Point(259, 129);
            this.radioSingle.Name = "radioSingle";
            this.radioSingle.Size = new System.Drawing.Size(81, 17);
            this.radioSingle.TabIndex = 14;
            this.radioSingle.TabStop = true;
            this.radioSingle.Text = "Single Color";
            this.radioSingle.UseVisualStyleBackColor = true;
            // 
            // radioWhite
            // 
            this.radioWhite.AutoSize = true;
            this.radioWhite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioWhite.Location = new System.Drawing.Point(259, 152);
            this.radioWhite.Name = "radioWhite";
            this.radioWhite.Size = new System.Drawing.Size(53, 17);
            this.radioWhite.TabIndex = 15;
            this.radioWhite.TabStop = true;
            this.radioWhite.Text = "White";
            this.radioWhite.UseVisualStyleBackColor = true;
            this.radioWhite.CheckedChanged += new System.EventHandler(this.radioWhite_CheckedChanged);
            // 
            // ledCheck
            // 
            this.ledCheck.Appearance = System.Windows.Forms.Appearance.Button;
            this.ledCheck.AutoSize = true;
            this.ledCheck.BackColor = System.Drawing.Color.Lime;
            this.ledCheck.Checked = true;
            this.ledCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ledCheck.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ledCheck.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ledCheck.Location = new System.Drawing.Point(12, 180);
            this.ledCheck.Name = "ledCheck";
            this.ledCheck.Size = new System.Drawing.Size(55, 23);
            this.ledCheck.TabIndex = 16;
            this.ledCheck.Text = "LED On";
            this.ledCheck.UseVisualStyleBackColor = false;
            this.ledCheck.CheckedChanged += new System.EventHandler(this.ledCheck_CheckedChanged);
            // 
            // radioUA
            // 
            this.radioUA.AutoSize = true;
            this.radioUA.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioUA.Location = new System.Drawing.Point(259, 175);
            this.radioUA.Name = "radioUA";
            this.radioUA.Size = new System.Drawing.Size(63, 17);
            this.radioUA.TabIndex = 17;
            this.radioUA.TabStop = true;
            this.radioUA.Text = "Patriotic";
            this.radioUA.UseVisualStyleBackColor = true;
            this.radioUA.CheckedChanged += new System.EventHandler(this.radioUA_CheckedChanged);
            // 
            // radioAmbilight
            // 
            this.radioAmbilight.AutoSize = true;
            this.radioAmbilight.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radioAmbilight.Location = new System.Drawing.Point(259, 198);
            this.radioAmbilight.Name = "radioAmbilight";
            this.radioAmbilight.Size = new System.Drawing.Size(67, 17);
            this.radioAmbilight.TabIndex = 18;
            this.radioAmbilight.TabStop = true;
            this.radioAmbilight.Text = "Ambilight";
            this.radioAmbilight.UseVisualStyleBackColor = true;
            this.radioAmbilight.CheckedChanged += new System.EventHandler(this.radioAmbilight_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(391, 228);
            this.Controls.Add(this.radioAmbilight);
            this.Controls.Add(this.radioUA);
            this.Controls.Add(this.ledCheck);
            this.Controls.Add(this.radioWhite);
            this.Controls.Add(this.radioSingle);
            this.Controls.Add(this.radioTemperature);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Brightness);
            this.Controls.Add(this.ColorButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Temperature CPU/GPU";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.Brightness)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button ColorButton;
        private System.Windows.Forms.TrackBar Brightness;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton radioTemperature;
        private System.Windows.Forms.RadioButton radioSingle;
        private System.Windows.Forms.RadioButton radioWhite;
        private System.Windows.Forms.CheckBox ledCheck;
        private System.Windows.Forms.RadioButton radioUA;
        private System.Windows.Forms.RadioButton radioAmbilight;
    }
}

