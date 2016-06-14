namespace AutoPost
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.Btn_Login = new System.Windows.Forms.Button();
            this.Txt_User = new System.Windows.Forms.TextBox();
            this.Txt_Pwd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_save = new System.Windows.Forms.CheckBox();
            this.Cb_SmartVerify = new System.Windows.Forms.CheckBox();
            this.tTips = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // Btn_Login
            // 
            this.Btn_Login.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Btn_Login.Location = new System.Drawing.Point(105, 199);
            this.Btn_Login.Name = "Btn_Login";
            this.Btn_Login.Size = new System.Drawing.Size(75, 23);
            this.Btn_Login.TabIndex = 0;
            this.Btn_Login.Text = "button1";
            this.Btn_Login.UseVisualStyleBackColor = true;
            this.Btn_Login.Click += new System.EventHandler(this.Btn_Login_Click);
            // 
            // Txt_User
            // 
            this.Txt_User.Location = new System.Drawing.Point(116, 75);
            this.Txt_User.Name = "Txt_User";
            this.Txt_User.Size = new System.Drawing.Size(100, 21);
            this.Txt_User.TabIndex = 1;
            // 
            // Txt_Pwd
            // 
            this.Txt_Pwd.Location = new System.Drawing.Point(116, 112);
            this.Txt_Pwd.Name = "Txt_Pwd";
            this.Txt_Pwd.PasswordChar = '*';
            this.Txt_Pwd.Size = new System.Drawing.Size(100, 21);
            this.Txt_Pwd.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(69, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(69, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "label2";
            // 
            // cb_save
            // 
            this.cb_save.AutoSize = true;
            this.cb_save.Location = new System.Drawing.Point(116, 144);
            this.cb_save.Name = "cb_save";
            this.cb_save.Size = new System.Drawing.Size(96, 16);
            this.cb_save.TabIndex = 5;
            this.cb_save.Text = "保存帐号密码";
            this.cb_save.UseVisualStyleBackColor = true;
            this.cb_save.CheckedChanged += new System.EventHandler(this.cb_save_CheckedChanged);
            // 
            // Cb_SmartVerify
            // 
            this.Cb_SmartVerify.AutoSize = true;
            this.Cb_SmartVerify.Location = new System.Drawing.Point(116, 167);
            this.Cb_SmartVerify.Name = "Cb_SmartVerify";
            this.Cb_SmartVerify.Size = new System.Drawing.Size(84, 16);
            this.Cb_SmartVerify.TabIndex = 6;
            this.Cb_SmartVerify.Text = "验证码识别";
            this.Cb_SmartVerify.UseVisualStyleBackColor = true;
            this.Cb_SmartVerify.CheckedChanged += new System.EventHandler(this.Cb_SmartVerify_CheckedChanged);
            // 
            // Login
            // 
            this.AcceptButton = this.Btn_Login;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.Cb_SmartVerify);
            this.Controls.Add(this.cb_save);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Txt_Pwd);
            this.Controls.Add(this.Txt_User);
            this.Controls.Add(this.Btn_Login);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Login_FormClosing);
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_Login;
        private System.Windows.Forms.TextBox Txt_User;
        private System.Windows.Forms.TextBox Txt_Pwd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cb_save;
        private System.Windows.Forms.CheckBox Cb_SmartVerify;
        private System.Windows.Forms.ToolTip tTips;
    }
}