namespace ManufacturingInvUI
{
    partial class AddModifyProductScreen
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
            this.mainProductHeader = new System.Windows.Forms.Label();
            this.labelProductPrice = new System.Windows.Forms.Label();
            this.partsForProductLabel = new System.Windows.Forms.Label();
            this.candidatepartsLabel = new System.Windows.Forms.Label();
            this.labelProductMin = new System.Windows.Forms.Label();
            this.labelProductMax = new System.Windows.Forms.Label();
            this.labelProductInv = new System.Windows.Forms.Label();
            this.labelProductName = new System.Windows.Forms.Label();
            this.labelProductID = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.partSearchButton = new System.Windows.Forms.Button();
            this.productAddButton = new System.Windows.Forms.Button();
            this.productDeleteButton = new System.Windows.Forms.Button();
            this.productCancelButton = new System.Windows.Forms.Button();
            this.productSaveButton = new System.Windows.Forms.Button();
            this.partSearchTextBox = new System.Windows.Forms.TextBox();
            this.productIDTextBox = new System.Windows.Forms.TextBox();
            this.productNameTextBox = new System.Windows.Forms.TextBox();
            this.productInvTextBox = new System.Windows.Forms.TextBox();
            this.productPriceTextBox = new System.Windows.Forms.TextBox();
            this.productMaxTextBox = new System.Windows.Forms.TextBox();
            this.productMinTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // mainProductHeader
            // 
            this.mainProductHeader.AutoSize = true;
            this.mainProductHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainProductHeader.Location = new System.Drawing.Point(13, 13);
            this.mainProductHeader.Name = "mainProductHeader";
            this.mainProductHeader.Size = new System.Drawing.Size(182, 32);
            this.mainProductHeader.TabIndex = 0;
            this.mainProductHeader.Text = "Add Product";
            // 
            // labelProductPrice
            // 
            this.labelProductPrice.AutoSize = true;
            this.labelProductPrice.Location = new System.Drawing.Point(20, 207);
            this.labelProductPrice.Name = "labelProductPrice";
            this.labelProductPrice.Size = new System.Drawing.Size(40, 17);
            this.labelProductPrice.TabIndex = 1;
            this.labelProductPrice.Text = "Price";
            // 
            // partsForProductLabel
            // 
            this.partsForProductLabel.AutoSize = true;
            this.partsForProductLabel.Location = new System.Drawing.Point(485, 233);
            this.partsForProductLabel.Name = "partsForProductLabel";
            this.partsForProductLabel.Size = new System.Drawing.Size(221, 17);
            this.partsForProductLabel.TabIndex = 2;
            this.partsForProductLabel.Text = "Parts Associated with this Product";
            // 
            // candidatepartsLabel
            // 
            this.candidatepartsLabel.AutoSize = true;
            this.candidatepartsLabel.Location = new System.Drawing.Point(485, 43);
            this.candidatepartsLabel.Name = "candidatepartsLabel";
            this.candidatepartsLabel.Size = new System.Drawing.Size(126, 17);
            this.candidatepartsLabel.TabIndex = 3;
            this.candidatepartsLabel.Text = "All candidate Parts";
            // 
            // labelProductMin
            // 
            this.labelProductMin.AutoSize = true;
            this.labelProductMin.Location = new System.Drawing.Point(20, 267);
            this.labelProductMin.Name = "labelProductMin";
            this.labelProductMin.Size = new System.Drawing.Size(30, 17);
            this.labelProductMin.TabIndex = 4;
            this.labelProductMin.Text = "Min";
            // 
            // labelProductMax
            // 
            this.labelProductMax.AutoSize = true;
            this.labelProductMax.Location = new System.Drawing.Point(20, 237);
            this.labelProductMax.Name = "labelProductMax";
            this.labelProductMax.Size = new System.Drawing.Size(33, 17);
            this.labelProductMax.TabIndex = 5;
            this.labelProductMax.Text = "Max";
            // 
            // labelProductInv
            // 
            this.labelProductInv.AutoSize = true;
            this.labelProductInv.Location = new System.Drawing.Point(20, 179);
            this.labelProductInv.Name = "labelProductInv";
            this.labelProductInv.Size = new System.Drawing.Size(66, 17);
            this.labelProductInv.TabIndex = 6;
            this.labelProductInv.Text = "Inventory";
            // 
            // labelProductName
            // 
            this.labelProductName.AutoSize = true;
            this.labelProductName.Location = new System.Drawing.Point(20, 151);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(45, 17);
            this.labelProductName.TabIndex = 8;
            this.labelProductName.Text = "Name";
            // 
            // labelProductID
            // 
            this.labelProductID.AutoSize = true;
            this.labelProductID.Location = new System.Drawing.Point(20, 123);
            this.labelProductID.Name = "labelProductID";
            this.labelProductID.Size = new System.Drawing.Size(21, 17);
            this.labelProductID.TabIndex = 9;
            this.labelProductID.Text = "ID";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(488, 63);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(375, 118);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(488, 253);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(375, 118);
            this.dataGridView2.TabIndex = 11;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // partSearchButton
            // 
            this.partSearchButton.Location = new System.Drawing.Point(623, 22);
            this.partSearchButton.Name = "partSearchButton";
            this.partSearchButton.Size = new System.Drawing.Size(75, 23);
            this.partSearchButton.TabIndex = 12;
            this.partSearchButton.Text = "Search";
            this.partSearchButton.UseVisualStyleBackColor = true;
            this.partSearchButton.Click += new System.EventHandler(this.partSearchButton_Click);
            // 
            // productAddButton
            // 
            this.productAddButton.Location = new System.Drawing.Point(788, 187);
            this.productAddButton.Name = "productAddButton";
            this.productAddButton.Size = new System.Drawing.Size(75, 23);
            this.productAddButton.TabIndex = 13;
            this.productAddButton.Text = "Add";
            this.productAddButton.UseVisualStyleBackColor = true;
            this.productAddButton.Click += new System.EventHandler(this.productAddButton_Click);
            // 
            // productDeleteButton
            // 
            this.productDeleteButton.Location = new System.Drawing.Point(788, 377);
            this.productDeleteButton.Name = "productDeleteButton";
            this.productDeleteButton.Size = new System.Drawing.Size(75, 23);
            this.productDeleteButton.TabIndex = 14;
            this.productDeleteButton.Text = "Delete";
            this.productDeleteButton.UseVisualStyleBackColor = true;
            this.productDeleteButton.Click += new System.EventHandler(this.productDeleteButton_Click);
            // 
            // productCancelButton
            // 
            this.productCancelButton.Location = new System.Drawing.Point(788, 415);
            this.productCancelButton.Name = "productCancelButton";
            this.productCancelButton.Size = new System.Drawing.Size(75, 23);
            this.productCancelButton.TabIndex = 15;
            this.productCancelButton.Text = "Cancel";
            this.productCancelButton.UseVisualStyleBackColor = true;
            this.productCancelButton.Click += new System.EventHandler(this.productCancelButton_Click);
            // 
            // productSaveButton
            // 
            this.productSaveButton.Location = new System.Drawing.Point(707, 415);
            this.productSaveButton.Name = "productSaveButton";
            this.productSaveButton.Size = new System.Drawing.Size(75, 23);
            this.productSaveButton.TabIndex = 16;
            this.productSaveButton.Text = "Save";
            this.productSaveButton.UseVisualStyleBackColor = true;
            this.productSaveButton.Click += new System.EventHandler(this.productSaveButton_Click);
            // 
            // partSearchTextBox
            // 
            this.partSearchTextBox.Location = new System.Drawing.Point(707, 22);
            this.partSearchTextBox.Name = "partSearchTextBox";
            this.partSearchTextBox.Size = new System.Drawing.Size(156, 22);
            this.partSearchTextBox.TabIndex = 17;
            this.partSearchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.partSearchTextBox_KeyDown);
            // 
            // productIDTextBox
            // 
            this.productIDTextBox.Location = new System.Drawing.Point(92, 120);
            this.productIDTextBox.Name = "productIDTextBox";
            this.productIDTextBox.Size = new System.Drawing.Size(156, 22);
            this.productIDTextBox.TabIndex = 18;
            this.productIDTextBox.TextChanged += new System.EventHandler(this.productIDTextBox_TextChanged);
            this.productIDTextBox.MouseLeave += new System.EventHandler(this.productIDTextBox_MouseLeave);
            this.productIDTextBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.productIDTextBox_MouseMove);
            // 
            // productNameTextBox
            // 
            this.productNameTextBox.Location = new System.Drawing.Point(92, 148);
            this.productNameTextBox.Name = "productNameTextBox";
            this.productNameTextBox.Size = new System.Drawing.Size(156, 22);
            this.productNameTextBox.TabIndex = 19;
            this.productNameTextBox.TextChanged += new System.EventHandler(this.productNameTextBox_TextChanged);
            // 
            // productInvTextBox
            // 
            this.productInvTextBox.Location = new System.Drawing.Point(92, 176);
            this.productInvTextBox.Name = "productInvTextBox";
            this.productInvTextBox.Size = new System.Drawing.Size(156, 22);
            this.productInvTextBox.TabIndex = 20;
            this.productInvTextBox.TextChanged += new System.EventHandler(this.productInvTextBox_TextChanged);
            // 
            // productPriceTextBox
            // 
            this.productPriceTextBox.Location = new System.Drawing.Point(92, 204);
            this.productPriceTextBox.Name = "productPriceTextBox";
            this.productPriceTextBox.Size = new System.Drawing.Size(156, 22);
            this.productPriceTextBox.TabIndex = 21;
            this.productPriceTextBox.TextChanged += new System.EventHandler(this.productPriceTextBox_TextChanged);
            // 
            // productMaxTextBox
            // 
            this.productMaxTextBox.Location = new System.Drawing.Point(92, 232);
            this.productMaxTextBox.Name = "productMaxTextBox";
            this.productMaxTextBox.Size = new System.Drawing.Size(84, 22);
            this.productMaxTextBox.TabIndex = 22;
            this.productMaxTextBox.TextChanged += new System.EventHandler(this.productMaxTextBox_TextChanged);
            // 
            // productMinTextBox
            // 
            this.productMinTextBox.Location = new System.Drawing.Point(92, 262);
            this.productMinTextBox.Name = "productMinTextBox";
            this.productMinTextBox.Size = new System.Drawing.Size(84, 22);
            this.productMinTextBox.TabIndex = 23;
            this.productMinTextBox.TextChanged += new System.EventHandler(this.productMinTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(254, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 17);
            this.label1.TabIndex = 24;
            this.label1.Text = "To change ID, Recreate Product";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(254, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 17);
            this.label2.TabIndex = 25;
            this.label2.Text = "ID";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(254, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 17);
            this.label3.TabIndex = 26;
            this.label3.Text = "ID";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(254, 207);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 17);
            this.label4.TabIndex = 27;
            this.label4.Text = "ID";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(182, 235);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 17);
            this.label5.TabIndex = 28;
            this.label5.Text = "ID";
            this.label5.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(182, 265);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 17);
            this.label6.TabIndex = 29;
            this.label6.Text = "ID";
            this.label6.Visible = false;
            // 
            // AddModifyProductScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 450);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.productMinTextBox);
            this.Controls.Add(this.productMaxTextBox);
            this.Controls.Add(this.productPriceTextBox);
            this.Controls.Add(this.productInvTextBox);
            this.Controls.Add(this.productNameTextBox);
            this.Controls.Add(this.productIDTextBox);
            this.Controls.Add(this.partSearchTextBox);
            this.Controls.Add(this.productSaveButton);
            this.Controls.Add(this.productCancelButton);
            this.Controls.Add(this.productDeleteButton);
            this.Controls.Add(this.productAddButton);
            this.Controls.Add(this.partSearchButton);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.labelProductID);
            this.Controls.Add(this.labelProductName);
            this.Controls.Add(this.labelProductInv);
            this.Controls.Add(this.labelProductMax);
            this.Controls.Add(this.labelProductMin);
            this.Controls.Add(this.candidatepartsLabel);
            this.Controls.Add(this.partsForProductLabel);
            this.Controls.Add(this.labelProductPrice);
            this.Controls.Add(this.mainProductHeader);
            this.Name = "AddModifyProductScreen";
            this.Text = "Add Product";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddModifyProductScreen_FormClosing);
            this.Load += new System.EventHandler(this.AddModifyProductScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mainProductHeader;
        private System.Windows.Forms.Label labelProductPrice;
        private System.Windows.Forms.Label partsForProductLabel;
        private System.Windows.Forms.Label candidatepartsLabel;
        private System.Windows.Forms.Label labelProductMin;
        private System.Windows.Forms.Label labelProductMax;
        private System.Windows.Forms.Label labelProductInv;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelProductID;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button partSearchButton;
        private System.Windows.Forms.Button productAddButton;
        private System.Windows.Forms.Button productDeleteButton;
        private System.Windows.Forms.Button productCancelButton;
        private System.Windows.Forms.Button productSaveButton;
        private System.Windows.Forms.TextBox partSearchTextBox;
        private System.Windows.Forms.TextBox productIDTextBox;
        private System.Windows.Forms.TextBox productNameTextBox;
        private System.Windows.Forms.TextBox productInvTextBox;
        private System.Windows.Forms.TextBox productPriceTextBox;
        private System.Windows.Forms.TextBox productMaxTextBox;
        private System.Windows.Forms.TextBox productMinTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}