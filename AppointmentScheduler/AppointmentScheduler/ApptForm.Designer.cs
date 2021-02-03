namespace AppointmentScheduler
{
    partial class AppointmentForm
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
            this.ApptIDLabel = new System.Windows.Forms.Label();
            this.AppIDTextBox = new System.Windows.Forms.TextBox();
            this.CustomerNameLabel = new System.Windows.Forms.Label();
            this.HeaderLabel = new System.Windows.Forms.Label();
            this.AppointmentsDGV = new System.Windows.Forms.DataGridView();
            this.TypeTextbox = new System.Windows.Forms.TextBox();
            this.TypeLabel = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.customerButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AppointmentsDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ApptIDLabel
            // 
            this.ApptIDLabel.AutoSize = true;
            this.ApptIDLabel.Location = new System.Drawing.Point(70, 74);
            this.ApptIDLabel.Name = "ApptIDLabel";
            this.ApptIDLabel.Size = new System.Drawing.Size(104, 17);
            this.ApptIDLabel.TabIndex = 0;
            this.ApptIDLabel.Text = "Appointment ID";
            this.ApptIDLabel.Visible = false;
            // 
            // AppIDTextBox
            // 
            this.AppIDTextBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.AppIDTextBox.Location = new System.Drawing.Point(180, 71);
            this.AppIDTextBox.Name = "AppIDTextBox";
            this.AppIDTextBox.ReadOnly = true;
            this.AppIDTextBox.Size = new System.Drawing.Size(212, 22);
            this.AppIDTextBox.TabIndex = 1;
            this.AppIDTextBox.Visible = false;
            // 
            // CustomerNameLabel
            // 
            this.CustomerNameLabel.AutoSize = true;
            this.CustomerNameLabel.Location = new System.Drawing.Point(65, 102);
            this.CustomerNameLabel.Name = "CustomerNameLabel";
            this.CustomerNameLabel.Size = new System.Drawing.Size(109, 17);
            this.CustomerNameLabel.TabIndex = 2;
            this.CustomerNameLabel.Text = "Customer Name";
            // 
            // HeaderLabel
            // 
            this.HeaderLabel.AutoSize = true;
            this.HeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HeaderLabel.Location = new System.Drawing.Point(12, 9);
            this.HeaderLabel.Name = "HeaderLabel";
            this.HeaderLabel.Size = new System.Drawing.Size(190, 32);
            this.HeaderLabel.TabIndex = 4;
            this.HeaderLabel.Text = "Appointments";
            // 
            // AppointmentsDGV
            // 
            this.AppointmentsDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.AppointmentsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AppointmentsDGV.Location = new System.Drawing.Point(398, 71);
            this.AppointmentsDGV.Name = "AppointmentsDGV";
            this.AppointmentsDGV.RowTemplate.Height = 24;
            this.AppointmentsDGV.Size = new System.Drawing.Size(688, 350);
            this.AppointmentsDGV.TabIndex = 11;
            this.AppointmentsDGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AppointmentsDGV_CellClick);
            this.AppointmentsDGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AppointmentsDGV_CellContentClick);
            this.AppointmentsDGV.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AppointmentsDGV_CellContentDoubleClick);
            this.AppointmentsDGV.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AppointmentsDGV_CellDoubleClick);
            this.AppointmentsDGV.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.AppointmentsDGV_CellMouseEnter);
            this.AppointmentsDGV.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.AppointmentsDGV_CellMouseLeave);
            this.AppointmentsDGV.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.AppointmentsDGV_ColumnHeaderMouseClick);
            this.AppointmentsDGV.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.AppointmentsDGV_DataBindingComplete);
            this.AppointmentsDGV.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AppointmentsDGV_KeyDown);
            // 
            // TypeTextbox
            // 
            this.TypeTextbox.Location = new System.Drawing.Point(180, 127);
            this.TypeTextbox.Name = "TypeTextbox";
            this.TypeTextbox.Size = new System.Drawing.Size(212, 22);
            this.TypeTextbox.TabIndex = 17;
            // 
            // TypeLabel
            // 
            this.TypeLabel.AutoSize = true;
            this.TypeLabel.Location = new System.Drawing.Point(134, 130);
            this.TypeLabel.Name = "TypeLabel";
            this.TypeLabel.Size = new System.Drawing.Size(40, 17);
            this.TypeLabel.TabIndex = 16;
            this.TypeLabel.Text = "Type";
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(137, 211);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(121, 56);
            this.SaveButton.TabIndex = 24;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(264, 211);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(121, 56);
            this.DeleteButton.TabIndex = 25;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(264, 335);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(121, 56);
            this.ExitButton.TabIndex = 28;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(180, 97);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(212, 24);
            this.comboBox1.TabIndex = 31;
            this.comboBox1.Text = "Select Customer";
            this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
            // 
            // customerButton
            // 
            this.customerButton.Location = new System.Drawing.Point(137, 335);
            this.customerButton.Name = "customerButton";
            this.customerButton.Size = new System.Drawing.Size(121, 56);
            this.customerButton.TabIndex = 32;
            this.customerButton.Text = "Customer";
            this.customerButton.UseVisualStyleBackColor = true;
            this.customerButton.Click += new System.EventHandler(this.customerButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(200, 273);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(121, 56);
            this.resetButton.TabIndex = 33;
            this.resetButton.Text = "Clear Input";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AppointmentScheduler.Properties.Resources.unsplash_photo_1575425186775_b8de9a427e67;
            this.pictureBox1.Location = new System.Drawing.Point(398, 71);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(278, 350);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 35;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(157, 155);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(235, 22);
            this.dateTimePicker1.TabIndex = 36;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(157, 183);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(235, 22);
            this.dateTimePicker2.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(113, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 17);
            this.label1.TabIndex = 38;
            this.label1.Text = "Start";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(118, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 17);
            this.label2.TabIndex = 39;
            this.label2.Text = "End";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(874, 41);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(212, 24);
            this.comboBox2.TabIndex = 41;
            this.comboBox2.Text = "Select Appointment";
            this.comboBox2.SelectionChangeCommitted += new System.EventHandler(this.comboBox2_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(745, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 17);
            this.label3.TabIndex = 40;
            this.label3.Text = "Appointment Type";
            // 
            // AppointmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1101, 440);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.AppointmentsDGV);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.customerButton);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.TypeTextbox);
            this.Controls.Add(this.TypeLabel);
            this.Controls.Add(this.HeaderLabel);
            this.Controls.Add(this.CustomerNameLabel);
            this.Controls.Add(this.AppIDTextBox);
            this.Controls.Add(this.ApptIDLabel);
            this.Name = "AppointmentForm";
            this.Text = "Appointment Management";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AppointmentForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.AppointmentsDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ApptIDLabel;
        private System.Windows.Forms.TextBox AppIDTextBox;
        private System.Windows.Forms.Label CustomerNameLabel;
        private System.Windows.Forms.Label HeaderLabel;
        private System.Windows.Forms.DataGridView AppointmentsDGV;
        private System.Windows.Forms.TextBox TypeTextbox;
        private System.Windows.Forms.Label TypeLabel;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button customerButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label3;
    }
}