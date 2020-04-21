namespace SubLottery
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
            this.SubsTable = new System.Windows.Forms.DataGridView();
            this.InsertButton = new System.Windows.Forms.Button();
            this.DrawButton = new System.Windows.Forms.Button();
            this.nameText = new System.Windows.Forms.TextBox();
            this.subCountText = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.WinnerLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.EnableAllButton = new System.Windows.Forms.Button();
            this.DisableAllButton = new System.Windows.Forms.Button();
            this.ActivityButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SubsTable)).BeginInit();
            this.SuspendLayout();
            // 
            // SubsTable
            // 
            this.SubsTable.AllowUserToAddRows = false;
            this.SubsTable.AllowUserToDeleteRows = false;
            this.SubsTable.AllowUserToResizeRows = false;
            this.SubsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SubsTable.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.SubsTable.Location = new System.Drawing.Point(12, 12);
            this.SubsTable.Name = "SubsTable";
            this.SubsTable.RowHeadersWidth = 51;
            this.SubsTable.RowTemplate.Height = 24;
            this.SubsTable.Size = new System.Drawing.Size(271, 417);
            this.SubsTable.TabIndex = 0;
            this.SubsTable.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.SubsTable_CellValueChanged);
            this.SubsTable.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.SubsTable_DataError);
            // 
            // InsertButton
            // 
            this.InsertButton.Location = new System.Drawing.Point(480, 65);
            this.InsertButton.Name = "InsertButton";
            this.InsertButton.Size = new System.Drawing.Size(158, 43);
            this.InsertButton.TabIndex = 1;
            this.InsertButton.Text = "Přidat";
            this.InsertButton.UseVisualStyleBackColor = true;
            this.InsertButton.Click += new System.EventHandler(this.InsertButton_Click);
            // 
            // DrawButton
            // 
            this.DrawButton.Location = new System.Drawing.Point(12, 435);
            this.DrawButton.Name = "DrawButton";
            this.DrawButton.Size = new System.Drawing.Size(271, 44);
            this.DrawButton.TabIndex = 2;
            this.DrawButton.Text = "Vylosovat";
            this.DrawButton.UseVisualStyleBackColor = true;
            this.DrawButton.Click += new System.EventHandler(this.DrawButton_Click);
            // 
            // nameText
            // 
            this.nameText.Location = new System.Drawing.Point(316, 37);
            this.nameText.Name = "nameText";
            this.nameText.Size = new System.Drawing.Size(201, 22);
            this.nameText.TabIndex = 3;
            // 
            // subCountText
            // 
            this.subCountText.Location = new System.Drawing.Point(538, 37);
            this.subCountText.Name = "subCountText";
            this.subCountText.Size = new System.Drawing.Size(100, 22);
            this.subCountText.TabIndex = 4;
            this.subCountText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SubCountText_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(313, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Jméno";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(535, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Počet subů";
            // 
            // WinnerLabel
            // 
            this.WinnerLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.WinnerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WinnerLabel.Location = new System.Drawing.Point(289, 136);
            this.WinnerLabel.Name = "WinnerLabel";
            this.WinnerLabel.Size = new System.Drawing.Size(374, 293);
            this.WinnerLabel.TabIndex = 8;
            this.WinnerLabel.Text = "3";
            this.WinnerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.WinnerLabel.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // EnableAllButton
            // 
            this.EnableAllButton.Location = new System.Drawing.Point(316, 435);
            this.EnableAllButton.Name = "EnableAllButton";
            this.EnableAllButton.Size = new System.Drawing.Size(158, 44);
            this.EnableAllButton.TabIndex = 9;
            this.EnableAllButton.Text = "Aktivovat všechny";
            this.EnableAllButton.UseVisualStyleBackColor = true;
            this.EnableAllButton.Click += new System.EventHandler(this.EnableAllButton_Click);
            // 
            // DisableAllButton
            // 
            this.DisableAllButton.Location = new System.Drawing.Point(480, 435);
            this.DisableAllButton.Name = "DisableAllButton";
            this.DisableAllButton.Size = new System.Drawing.Size(158, 44);
            this.DisableAllButton.TabIndex = 10;
            this.DisableAllButton.Text = "Deaktivovat všechny";
            this.DisableAllButton.UseVisualStyleBackColor = true;
            this.DisableAllButton.Click += new System.EventHandler(this.DisableAllButton_Click);
            // 
            // ActivityButton
            // 
            this.ActivityButton.Location = new System.Drawing.Point(316, 66);
            this.ActivityButton.Name = "ActivityButton";
            this.ActivityButton.Size = new System.Drawing.Size(158, 42);
            this.ActivityButton.TabIndex = 11;
            this.ActivityButton.Text = "Změnit aktivitu";
            this.ActivityButton.UseVisualStyleBackColor = true;
            this.ActivityButton.Click += new System.EventHandler(this.ActivityButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 491);
            this.Controls.Add(this.ActivityButton);
            this.Controls.Add(this.DisableAllButton);
            this.Controls.Add(this.EnableAllButton);
            this.Controls.Add(this.WinnerLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.subCountText);
            this.Controls.Add(this.nameText);
            this.Controls.Add(this.DrawButton);
            this.Controls.Add(this.InsertButton);
            this.Controls.Add(this.SubsTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Gterry";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SubsTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView SubsTable;
        private System.Windows.Forms.Button InsertButton;
        private System.Windows.Forms.Button DrawButton;
        private System.Windows.Forms.TextBox nameText;
        private System.Windows.Forms.TextBox subCountText;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label WinnerLabel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button EnableAllButton;
        private System.Windows.Forms.Button DisableAllButton;
        private System.Windows.Forms.Button ActivityButton;
    }
}

