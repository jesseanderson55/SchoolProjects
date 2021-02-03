namespace AppointmentScheduler
{
    partial class Mainscreen
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
            this.CalenderDGV = new System.Windows.Forms.DataGridView();
            this.MonthlyRadio = new System.Windows.Forms.RadioButton();
            this.WeeklyRadio = new System.Windows.Forms.RadioButton();
            this.AppointmentButton = new System.Windows.Forms.Button();
            this.CustomerButton = new System.Windows.Forms.Button();
            this.LogoutButton = new System.Windows.Forms.Button();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.clearButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.CalenderDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // CalenderDGV
            // 
            this.CalenderDGV.AllowUserToResizeRows = false;
            this.CalenderDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.CalenderDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CalenderDGV.Location = new System.Drawing.Point(332, 106);
            this.CalenderDGV.Name = "CalenderDGV";
            this.CalenderDGV.ReadOnly = true;
            this.CalenderDGV.RowTemplate.Height = 24;
            this.CalenderDGV.Size = new System.Drawing.Size(538, 372);
            this.CalenderDGV.TabIndex = 0;
            this.CalenderDGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CalenderDGV_CellClick);
            this.CalenderDGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.CalenderDGV_CellContentClick);
            this.CalenderDGV.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.CalenderDGV_CellMouseEnter);
            this.CalenderDGV.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.CalenderDGV_CellMouseLeave);
            this.CalenderDGV.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.CalenderDGV_DataBindingComplete);
            // 
            // MonthlyRadio
            // 
            this.MonthlyRadio.AutoSize = true;
            this.MonthlyRadio.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.MonthlyRadio.Location = new System.Drawing.Point(332, 79);
            this.MonthlyRadio.Name = "MonthlyRadio";
            this.MonthlyRadio.Size = new System.Drawing.Size(139, 21);
            this.MonthlyRadio.TabIndex = 1;
            this.MonthlyRadio.TabStop = true;
            this.MonthlyRadio.Text = "Monthly Calender";
            this.MonthlyRadio.UseVisualStyleBackColor = true;
            this.MonthlyRadio.CheckedChanged += new System.EventHandler(this.MonthlyRadio_CheckedChanged);
            // 
            // WeeklyRadio
            // 
            this.WeeklyRadio.AutoSize = true;
            this.WeeklyRadio.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.WeeklyRadio.Location = new System.Drawing.Point(468, 79);
            this.WeeklyRadio.Name = "WeeklyRadio";
            this.WeeklyRadio.Size = new System.Drawing.Size(136, 21);
            this.WeeklyRadio.TabIndex = 2;
            this.WeeklyRadio.TabStop = true;
            this.WeeklyRadio.Text = "Weekly Calender";
            this.WeeklyRadio.UseVisualStyleBackColor = true;
            this.WeeklyRadio.CheckedChanged += new System.EventHandler(this.WeeklyRadio_CheckedChanged);
            // 
            // AppointmentButton
            // 
            this.AppointmentButton.Location = new System.Drawing.Point(24, 32);
            this.AppointmentButton.Name = "AppointmentButton";
            this.AppointmentButton.Size = new System.Drawing.Size(179, 41);
            this.AppointmentButton.TabIndex = 3;
            this.AppointmentButton.Text = "Appointments";
            this.AppointmentButton.UseVisualStyleBackColor = true;
            this.AppointmentButton.Click += new System.EventHandler(this.AppointmentButton_Click);
            // 
            // CustomerButton
            // 
            this.CustomerButton.Location = new System.Drawing.Point(209, 32);
            this.CustomerButton.Name = "CustomerButton";
            this.CustomerButton.Size = new System.Drawing.Size(179, 41);
            this.CustomerButton.TabIndex = 9;
            this.CustomerButton.Text = "Customers";
            this.CustomerButton.UseVisualStyleBackColor = true;
            this.CustomerButton.Click += new System.EventHandler(this.CustomerButton_Click);
            // 
            // LogoutButton
            // 
            this.LogoutButton.Location = new System.Drawing.Point(394, 32);
            this.LogoutButton.Name = "LogoutButton";
            this.LogoutButton.Size = new System.Drawing.Size(179, 41);
            this.LogoutButton.TabIndex = 10;
            this.LogoutButton.Text = "Logout";
            this.LogoutButton.UseVisualStyleBackColor = true;
            this.LogoutButton.Click += new System.EventHandler(this.LogoutButton_Click);
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(24, 106);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 11;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(648, 76);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.comboBox1.Size = new System.Drawing.Size(222, 24);
            this.comboBox1.Sorted = true;
            this.comboBox1.TabIndex = 12;
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1148, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 17);
            this.label1.TabIndex = 13;
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(67, 325);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(179, 41);
            this.clearButton.TabIndex = 14;
            this.clearButton.Text = "Reset Appointment View";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AppointmentScheduler.Properties.Resources.unsplash_photo_1575425186775_b8de9a427e67;
            this.pictureBox1.Location = new System.Drawing.Point(332, 106);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(278, 372);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // Mainscreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(882, 490);
            this.Controls.Add(this.CalenderDGV);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.LogoutButton);
            this.Controls.Add(this.CustomerButton);
            this.Controls.Add(this.AppointmentButton);
            this.Controls.Add(this.WeeklyRadio);
            this.Controls.Add(this.MonthlyRadio);
            this.Name = "Mainscreen";
            this.Text = "Mainscreen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Mainscreen_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.CalenderDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView CalenderDGV;
        private System.Windows.Forms.RadioButton MonthlyRadio;
        private System.Windows.Forms.RadioButton WeeklyRadio;
        private System.Windows.Forms.Button AppointmentButton;
        private System.Windows.Forms.Button CustomerButton;
        private System.Windows.Forms.Button LogoutButton;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}