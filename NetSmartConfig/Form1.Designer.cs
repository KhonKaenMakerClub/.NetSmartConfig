namespace NetSmartConfig
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
            this.btConfig = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ckIsHidden = new System.Windows.Forms.CheckBox();
            this.txtSSID = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // btConfig
            // 
            this.btConfig.Location = new System.Drawing.Point(216, 69);
            this.btConfig.Name = "btConfig";
            this.btConfig.Size = new System.Drawing.Size(75, 23);
            this.btConfig.TabIndex = 0;
            this.btConfig.Text = "Config";
            this.btConfig.UseVisualStyleBackColor = true;
            this.btConfig.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "SSID : ";
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(61, 43);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(230, 20);
            this.txtPass.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Pass : ";
            // 
            // ckIsHidden
            // 
            this.ckIsHidden.AutoSize = true;
            this.ckIsHidden.Location = new System.Drawing.Point(61, 73);
            this.ckIsHidden.Name = "ckIsHidden";
            this.ckIsHidden.Size = new System.Drawing.Size(88, 17);
            this.ckIsHidden.TabIndex = 5;
            this.ckIsHidden.Text = "SSID Hidden";
            this.ckIsHidden.UseVisualStyleBackColor = true;
            // 
            // txtSSID
            // 
            this.txtSSID.Enabled = false;
            this.txtSSID.Location = new System.Drawing.Point(62, 17);
            this.txtSSID.Name = "txtSSID";
            this.txtSSID.Size = new System.Drawing.Size(229, 20);
            this.txtSSID.TabIndex = 6;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(12, 96);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(286, 13);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "https://www.facebook.com/groups/KhonKaenMakerClub/";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 118);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.txtSSID);
            this.Controls.Add(this.ckIsHidden);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btConfig);
            this.Name = "Form1";
            this.Text = "ESP8266 SmartConfig";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btConfig;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox ckIsHidden;
        private System.Windows.Forms.TextBox txtSSID;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

