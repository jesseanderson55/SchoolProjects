namespace AppointmentScheduler
{
    partial class CustomerForm
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
            this.ExitButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.customerDGV = new System.Windows.Forms.DataGridView();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.UserIDLabel = new System.Windows.Forms.Label();
            this.HeaderLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.CustomerIDLabel = new System.Windows.Forms.Label();
            this.IDTextBox = new System.Windows.Forms.TextBox();
            this.ApptIDLabel = new System.Windows.Forms.Label();
            this.yesRadioButton = new System.Windows.Forms.RadioButton();
            this.noRadioButton = new System.Windows.Forms.RadioButton();
            this.address1TextBox = new System.Windows.Forms.TextBox();
            this.zipTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.countryTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.phoneTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cityTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.appointmentsButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.customerDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(399, 312);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(121, 56);
            this.ExitButton.TabIndex = 55;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(399, 126);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(121, 56);
            this.DeleteButton.TabIndex = 53;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(399, 64);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(121, 56);
            this.SaveButton.TabIndex = 52;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // customerDGV
            // 
            this.customerDGV.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.customerDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customerDGV.Location = new System.Drawing.Point(526, 64);
            this.customerDGV.Name = "customerDGV";
            this.customerDGV.ReadOnly = true;
            this.customerDGV.RowTemplate.Height = 24;
            this.customerDGV.Size = new System.Drawing.Size(639, 442);
            this.customerDGV.TabIndex = 39;
            this.customerDGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.customerDGV_CellClick);
            this.customerDGV.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.customerDGV_CellContentClick);
            this.customerDGV.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.customerDGV_CellMouseEnter);
            this.customerDGV.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.customerDGV_CellMouseLeave);
            this.customerDGV.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.customerDGV_DataBindingComplete);
            this.customerDGV.KeyDown += new System.Windows.Forms.KeyEventHandler(this.customerDGV_KeyDown);
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Location = new System.Drawing.Point(128, 266);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(46, 17);
            this.TitleLabel.TabIndex = 37;
            this.TitleLabel.Text = "Active";
            // 
            // UserIDLabel
            // 
            this.UserIDLabel.AutoSize = true;
            this.UserIDLabel.Location = new System.Drawing.Point(115, 151);
            this.UserIDLabel.Name = "UserIDLabel";
            this.UserIDLabel.Size = new System.Drawing.Size(60, 17);
            this.UserIDLabel.TabIndex = 35;
            this.UserIDLabel.Text = "Address";
            // 
            // HeaderLabel
            // 
            this.HeaderLabel.AutoSize = true;
            this.HeaderLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HeaderLabel.Location = new System.Drawing.Point(13, 2);
            this.HeaderLabel.Name = "HeaderLabel";
            this.HeaderLabel.Size = new System.Drawing.Size(151, 32);
            this.HeaderLabel.TabIndex = 33;
            this.HeaderLabel.Text = "Customers";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(181, 92);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(212, 22);
            this.nameTextBox.TabIndex = 32;
            // 
            // CustomerIDLabel
            // 
            this.CustomerIDLabel.AutoSize = true;
            this.CustomerIDLabel.Location = new System.Drawing.Point(130, 95);
            this.CustomerIDLabel.Name = "CustomerIDLabel";
            this.CustomerIDLabel.Size = new System.Drawing.Size(45, 17);
            this.CustomerIDLabel.TabIndex = 31;
            this.CustomerIDLabel.Text = "Name";
            // 
            // IDTextBox
            // 
            this.IDTextBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.IDTextBox.Location = new System.Drawing.Point(181, 64);
            this.IDTextBox.Name = "IDTextBox";
            this.IDTextBox.ReadOnly = true;
            this.IDTextBox.Size = new System.Drawing.Size(212, 22);
            this.IDTextBox.TabIndex = 30;
            this.IDTextBox.Visible = false;
            // 
            // ApptIDLabel
            // 
            this.ApptIDLabel.AutoSize = true;
            this.ApptIDLabel.Location = new System.Drawing.Point(90, 67);
            this.ApptIDLabel.Name = "ApptIDLabel";
            this.ApptIDLabel.Size = new System.Drawing.Size(85, 17);
            this.ApptIDLabel.TabIndex = 29;
            this.ApptIDLabel.Text = "Customer ID";
            this.ApptIDLabel.Visible = false;
            // 
            // yesRadioButton
            // 
            this.yesRadioButton.AutoSize = true;
            this.yesRadioButton.Checked = true;
            this.yesRadioButton.Location = new System.Drawing.Point(181, 264);
            this.yesRadioButton.Name = "yesRadioButton";
            this.yesRadioButton.Size = new System.Drawing.Size(53, 21);
            this.yesRadioButton.TabIndex = 56;
            this.yesRadioButton.TabStop = true;
            this.yesRadioButton.Text = "Yes";
            this.yesRadioButton.UseVisualStyleBackColor = true;
            // 
            // noRadioButton
            // 
            this.noRadioButton.AutoSize = true;
            this.noRadioButton.Location = new System.Drawing.Point(241, 264);
            this.noRadioButton.Name = "noRadioButton";
            this.noRadioButton.Size = new System.Drawing.Size(47, 21);
            this.noRadioButton.TabIndex = 57;
            this.noRadioButton.Text = "No";
            this.noRadioButton.UseVisualStyleBackColor = true;
            // 
            // address1TextBox
            // 
            this.address1TextBox.Location = new System.Drawing.Point(181, 148);
            this.address1TextBox.Name = "address1TextBox";
            this.address1TextBox.Size = new System.Drawing.Size(212, 22);
            this.address1TextBox.TabIndex = 58;
            // 
            // zipTextBox
            // 
            this.zipTextBox.Location = new System.Drawing.Point(180, 204);
            this.zipTextBox.Name = "zipTextBox";
            this.zipTextBox.Size = new System.Drawing.Size(212, 22);
            this.zipTextBox.TabIndex = 68;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 207);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 17);
            this.label2.TabIndex = 67;
            this.label2.Text = "Postal Code";
            // 
            // countryTextBox
            // 
            this.countryTextBox.Location = new System.Drawing.Point(180, 232);
            this.countryTextBox.Name = "countryTextBox";
            this.countryTextBox.Size = new System.Drawing.Size(212, 22);
            this.countryTextBox.TabIndex = 66;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(117, 235);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 17);
            this.label3.TabIndex = 65;
            this.label3.Text = "Country";
            // 
            // phoneTextBox
            // 
            this.phoneTextBox.Location = new System.Drawing.Point(181, 120);
            this.phoneTextBox.Name = "phoneTextBox";
            this.phoneTextBox.Size = new System.Drawing.Size(212, 22);
            this.phoneTextBox.TabIndex = 64;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(72, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 17);
            this.label4.TabIndex = 63;
            this.label4.Text = "Phone Number";
            // 
            // cityTextBox
            // 
            this.cityTextBox.Location = new System.Drawing.Point(181, 176);
            this.cityTextBox.Name = "cityTextBox";
            this.cityTextBox.Size = new System.Drawing.Size(212, 22);
            this.cityTextBox.TabIndex = 62;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(143, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 17);
            this.label5.TabIndex = 61;
            this.label5.Text = "City";
            // 
            // appointmentsButton
            // 
            this.appointmentsButton.Location = new System.Drawing.Point(399, 250);
            this.appointmentsButton.Name = "appointmentsButton";
            this.appointmentsButton.Size = new System.Drawing.Size(122, 56);
            this.appointmentsButton.TabIndex = 69;
            this.appointmentsButton.Text = "Appointments";
            this.appointmentsButton.UseVisualStyleBackColor = true;
            this.appointmentsButton.Click += new System.EventHandler(this.appointmentsButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(399, 188);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(121, 56);
            this.resetButton.TabIndex = 70;
            this.resetButton.Text = "Clear Input";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // CustomerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1183, 518);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.appointmentsButton);
            this.Controls.Add(this.zipTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.countryTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.phoneTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cityTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.address1TextBox);
            this.Controls.Add(this.noRadioButton);
            this.Controls.Add(this.yesRadioButton);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.customerDGV);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.UserIDLabel);
            this.Controls.Add(this.HeaderLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.CustomerIDLabel);
            this.Controls.Add(this.IDTextBox);
            this.Controls.Add(this.ApptIDLabel);
            this.Name = "CustomerForm";
            this.Text = "CustomerForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CustomerForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.customerDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.DataGridView customerDGV;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label UserIDLabel;
        private System.Windows.Forms.Label HeaderLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label CustomerIDLabel;
        private System.Windows.Forms.TextBox IDTextBox;
        private System.Windows.Forms.Label ApptIDLabel;
        private System.Windows.Forms.RadioButton yesRadioButton;
        private System.Windows.Forms.RadioButton noRadioButton;
        private System.Windows.Forms.TextBox address1TextBox;
        private System.Windows.Forms.TextBox zipTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox countryTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox phoneTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox cityTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button appointmentsButton;
        private System.Windows.Forms.Button resetButton;
    }
}