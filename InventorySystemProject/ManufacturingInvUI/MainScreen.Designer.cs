namespace ManufacturingInvUI
{
    partial class MainScreen
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
            this.labelParts = new System.Windows.Forms.Label();
            this.labelProducts = new System.Windows.Forms.Label();
            this.partListGrid = new System.Windows.Forms.DataGridView();
            this.productListGrid = new System.Windows.Forms.DataGridView();
            this.mainHeader = new System.Windows.Forms.Label();
            this.partSearchButton = new System.Windows.Forms.Button();
            this.productSearchButton = new System.Windows.Forms.Button();
            this.partAddButton = new System.Windows.Forms.Button();
            this.partModifyButton = new System.Windows.Forms.Button();
            this.partDeleteButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.partSearchTextBox = new System.Windows.Forms.TextBox();
            this.productSearchTextBox = new System.Windows.Forms.TextBox();
            this.productAddButton = new System.Windows.Forms.Button();
            this.productModifyButton = new System.Windows.Forms.Button();
            this.productDeleteButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.partListGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.productListGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // labelParts
            // 
            this.labelParts.AutoSize = true;
            this.labelParts.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelParts.Location = new System.Drawing.Point(12, 100);
            this.labelParts.Name = "labelParts";
            this.labelParts.Size = new System.Drawing.Size(86, 32);
            this.labelParts.TabIndex = 0;
            this.labelParts.Text = "Parts";
            // 
            // labelProducts
            // 
            this.labelProducts.AutoSize = true;
            this.labelProducts.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProducts.Location = new System.Drawing.Point(567, 100);
            this.labelProducts.Name = "labelProducts";
            this.labelProducts.Size = new System.Drawing.Size(135, 32);
            this.labelProducts.TabIndex = 1;
            this.labelProducts.Text = "Products";
            // 
            // partListGrid
            // 
            this.partListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.partListGrid.Location = new System.Drawing.Point(19, 142);
            this.partListGrid.Name = "partListGrid";
            this.partListGrid.ReadOnly = true;
            this.partListGrid.RowTemplate.Height = 24;
            this.partListGrid.Size = new System.Drawing.Size(472, 150);
            this.partListGrid.TabIndex = 3;
            this.partListGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.partListGrid_CellClick);
            this.partListGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.partListGrid_CellContentClick);
            // 
            // productListGrid
            // 
            this.productListGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.productListGrid.Location = new System.Drawing.Point(573, 142);
            this.productListGrid.Name = "productListGrid";
            this.productListGrid.ReadOnly = true;
            this.productListGrid.RowTemplate.Height = 24;
            this.productListGrid.Size = new System.Drawing.Size(472, 150);
            this.productListGrid.TabIndex = 4;
            this.productListGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.productListGrid_CellClick);
            this.productListGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.productListGrid_CellContentClick);
            // 
            // mainHeader
            // 
            this.mainHeader.AutoSize = true;
            this.mainHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainHeader.Location = new System.Drawing.Point(12, 9);
            this.mainHeader.Name = "mainHeader";
            this.mainHeader.Size = new System.Drawing.Size(495, 38);
            this.mainHeader.TabIndex = 5;
            this.mainHeader.Text = "Inventory Management System";
            // 
            // partSearchButton
            // 
            this.partSearchButton.Location = new System.Drawing.Point(208, 103);
            this.partSearchButton.Name = "partSearchButton";
            this.partSearchButton.Size = new System.Drawing.Size(86, 27);
            this.partSearchButton.TabIndex = 6;
            this.partSearchButton.Text = "Search";
            this.partSearchButton.UseVisualStyleBackColor = true;
            this.partSearchButton.Click += new System.EventHandler(this.partSearchButton_Click);
            // 
            // productSearchButton
            // 
            this.productSearchButton.Location = new System.Drawing.Point(764, 103);
            this.productSearchButton.Name = "productSearchButton";
            this.productSearchButton.Size = new System.Drawing.Size(84, 27);
            this.productSearchButton.TabIndex = 7;
            this.productSearchButton.Text = "Search";
            this.productSearchButton.UseVisualStyleBackColor = true;
            this.productSearchButton.Click += new System.EventHandler(this.productSearchButton_Click);
            // 
            // partAddButton
            // 
            this.partAddButton.Location = new System.Drawing.Point(227, 319);
            this.partAddButton.Name = "partAddButton";
            this.partAddButton.Size = new System.Drawing.Size(84, 30);
            this.partAddButton.TabIndex = 8;
            this.partAddButton.Text = "Add";
            this.partAddButton.UseVisualStyleBackColor = true;
            this.partAddButton.Click += new System.EventHandler(this.partAddButton_Click);
            // 
            // partModifyButton
            // 
            this.partModifyButton.Location = new System.Drawing.Point(317, 319);
            this.partModifyButton.Name = "partModifyButton";
            this.partModifyButton.Size = new System.Drawing.Size(84, 30);
            this.partModifyButton.TabIndex = 9;
            this.partModifyButton.Text = "Modify";
            this.partModifyButton.UseVisualStyleBackColor = true;
            this.partModifyButton.Click += new System.EventHandler(this.partModifyButton_Click);
            // 
            // partDeleteButton
            // 
            this.partDeleteButton.Location = new System.Drawing.Point(407, 319);
            this.partDeleteButton.Name = "partDeleteButton";
            this.partDeleteButton.Size = new System.Drawing.Size(84, 30);
            this.partDeleteButton.TabIndex = 10;
            this.partDeleteButton.Text = "Delete";
            this.partDeleteButton.UseVisualStyleBackColor = true;
            this.partDeleteButton.Click += new System.EventHandler(this.partDeleteButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(908, 455);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(137, 43);
            this.exitButton.TabIndex = 11;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // partSearchTextBox
            // 
            this.partSearchTextBox.Location = new System.Drawing.Point(300, 103);
            this.partSearchTextBox.Name = "partSearchTextBox";
            this.partSearchTextBox.Size = new System.Drawing.Size(191, 30);
            this.partSearchTextBox.TabIndex = 12;
            this.partSearchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.partSearchTextBox_KeyDown);
            // 
            // productSearchTextBox
            // 
            this.productSearchTextBox.Location = new System.Drawing.Point(854, 103);
            this.productSearchTextBox.Name = "productSearchTextBox";
            this.productSearchTextBox.Size = new System.Drawing.Size(191, 30);
            this.productSearchTextBox.TabIndex = 13;
            this.productSearchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.productSearchTextBox_KeyDown);
            // 
            // productAddButton
            // 
            this.productAddButton.Location = new System.Drawing.Point(781, 319);
            this.productAddButton.Name = "productAddButton";
            this.productAddButton.Size = new System.Drawing.Size(84, 30);
            this.productAddButton.TabIndex = 14;
            this.productAddButton.Text = "Add";
            this.productAddButton.UseVisualStyleBackColor = true;
            this.productAddButton.Click += new System.EventHandler(this.productAddButton_Click);
            // 
            // productModifyButton
            // 
            this.productModifyButton.Location = new System.Drawing.Point(871, 319);
            this.productModifyButton.Name = "productModifyButton";
            this.productModifyButton.Size = new System.Drawing.Size(84, 30);
            this.productModifyButton.TabIndex = 15;
            this.productModifyButton.Text = "Modify";
            this.productModifyButton.UseVisualStyleBackColor = true;
            this.productModifyButton.Click += new System.EventHandler(this.productModifyButton_Click);
            // 
            // productDeleteButton
            // 
            this.productDeleteButton.Location = new System.Drawing.Point(961, 319);
            this.productDeleteButton.Name = "productDeleteButton";
            this.productDeleteButton.Size = new System.Drawing.Size(84, 30);
            this.productDeleteButton.TabIndex = 16;
            this.productDeleteButton.Text = "Delete";
            this.productDeleteButton.UseVisualStyleBackColor = true;
            this.productDeleteButton.Click += new System.EventHandler(this.productDeleteButton_Click);
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 511);
            this.Controls.Add(this.productDeleteButton);
            this.Controls.Add(this.productModifyButton);
            this.Controls.Add(this.productAddButton);
            this.Controls.Add(this.productSearchTextBox);
            this.Controls.Add(this.partSearchTextBox);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.partDeleteButton);
            this.Controls.Add(this.partModifyButton);
            this.Controls.Add(this.partAddButton);
            this.Controls.Add(this.productSearchButton);
            this.Controls.Add(this.partSearchButton);
            this.Controls.Add(this.mainHeader);
            this.Controls.Add(this.productListGrid);
            this.Controls.Add(this.partListGrid);
            this.Controls.Add(this.labelProducts);
            this.Controls.Add(this.labelParts);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainScreen";
            this.Text = "Main Screen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainScreen_FormClosing);
            this.Load += new System.EventHandler(this.MainScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.partListGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.productListGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelParts;
        private System.Windows.Forms.Label labelProducts;
        private System.Windows.Forms.DataGridView partListGrid;
        private System.Windows.Forms.DataGridView productListGrid;
        private System.Windows.Forms.Label mainHeader;
        private System.Windows.Forms.Button partSearchButton;
        private System.Windows.Forms.Button productSearchButton;
        private System.Windows.Forms.Button partAddButton;
        private System.Windows.Forms.Button partModifyButton;
        private System.Windows.Forms.Button partDeleteButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.TextBox partSearchTextBox;
        private System.Windows.Forms.TextBox productSearchTextBox;
        private System.Windows.Forms.Button productAddButton;
        private System.Windows.Forms.Button productModifyButton;
        private System.Windows.Forms.Button productDeleteButton;
    }
}

