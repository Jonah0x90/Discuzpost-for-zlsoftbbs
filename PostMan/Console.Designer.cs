namespace AutoPost
{
    partial class Console
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Console));
            this.Btn_Post = new System.Windows.Forms.Button();
            this.Dup_Sec = new System.Windows.Forms.DomainUpDown();
            this.Lab_Sec = new System.Windows.Forms.Label();
            this.Gbox_Params = new System.Windows.Forms.GroupBox();
            this.Ckb_Smart = new System.Windows.Forms.CheckBox();
            this.Cbb_Zone = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Ckb_Txt = new System.Windows.Forms.CheckBox();
            this.Txt_Content = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Cbb_Number = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Gbox_Input = new System.Windows.Forms.GroupBox();
            this.Txt_Print = new System.Windows.Forms.TextBox();
            this.Timer_time = new System.Windows.Forms.Timer(this.components);
            this.tTips = new System.Windows.Forms.ToolTip(this.components);
            this.Ckb_Ban = new System.Windows.Forms.CheckBox();
            this.Gbox_Params.SuspendLayout();
            this.Gbox_Input.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Post
            // 
            this.Btn_Post.Location = new System.Drawing.Point(278, 86);
            this.Btn_Post.Name = "Btn_Post";
            this.Btn_Post.Size = new System.Drawing.Size(75, 30);
            this.Btn_Post.TabIndex = 0;
            this.Btn_Post.Text = "button1";
            this.Btn_Post.UseVisualStyleBackColor = true;
            this.Btn_Post.Click += new System.EventHandler(this.Btn_Post_Click);
            // 
            // Dup_Sec
            // 
            this.Dup_Sec.Items.Add("30");
            this.Dup_Sec.Items.Add("60");
            this.Dup_Sec.Items.Add("90");
            this.Dup_Sec.Items.Add("120");
            this.Dup_Sec.Items.Add("360");
            this.Dup_Sec.Location = new System.Drawing.Point(69, 59);
            this.Dup_Sec.Name = "Dup_Sec";
            this.Dup_Sec.Size = new System.Drawing.Size(60, 21);
            this.Dup_Sec.TabIndex = 1;
            this.Dup_Sec.Text = "domainUpDown1";
            this.Dup_Sec.SelectedItemChanged += new System.EventHandler(this.Dup_Sec_SelectedItemChanged);
            // 
            // Lab_Sec
            // 
            this.Lab_Sec.AutoSize = true;
            this.Lab_Sec.Location = new System.Drawing.Point(135, 63);
            this.Lab_Sec.Name = "Lab_Sec";
            this.Lab_Sec.Size = new System.Drawing.Size(23, 12);
            this.Lab_Sec.TabIndex = 2;
            this.Lab_Sec.Text = "Lab";
            // 
            // Gbox_Params
            // 
            this.Gbox_Params.Controls.Add(this.Ckb_Ban);
            this.Gbox_Params.Controls.Add(this.Ckb_Smart);
            this.Gbox_Params.Controls.Add(this.Cbb_Zone);
            this.Gbox_Params.Controls.Add(this.label4);
            this.Gbox_Params.Controls.Add(this.Ckb_Txt);
            this.Gbox_Params.Controls.Add(this.Txt_Content);
            this.Gbox_Params.Controls.Add(this.label3);
            this.Gbox_Params.Controls.Add(this.Cbb_Number);
            this.Gbox_Params.Controls.Add(this.Btn_Post);
            this.Gbox_Params.Controls.Add(this.label2);
            this.Gbox_Params.Controls.Add(this.label1);
            this.Gbox_Params.Controls.Add(this.Lab_Sec);
            this.Gbox_Params.Controls.Add(this.Dup_Sec);
            this.Gbox_Params.Location = new System.Drawing.Point(3, 9);
            this.Gbox_Params.Name = "Gbox_Params";
            this.Gbox_Params.Size = new System.Drawing.Size(402, 126);
            this.Gbox_Params.TabIndex = 3;
            this.Gbox_Params.TabStop = false;
            this.Gbox_Params.Text = "回帖参数";
            // 
            // Ckb_Smart
            // 
            this.Ckb_Smart.AutoSize = true;
            this.Ckb_Smart.ForeColor = System.Drawing.Color.Red;
            this.Ckb_Smart.Location = new System.Drawing.Point(315, 25);
            this.Ckb_Smart.Name = "Ckb_Smart";
            this.Ckb_Smart.Size = new System.Drawing.Size(78, 16);
            this.Ckb_Smart.TabIndex = 11;
            this.Ckb_Smart.Text = "checkBox1";
            this.Ckb_Smart.UseVisualStyleBackColor = true;
            this.Ckb_Smart.CheckedChanged += new System.EventHandler(this.Ckb_Smart_CheckedChanged);
            // 
            // Cbb_Zone
            // 
            this.Cbb_Zone.FormattingEnabled = true;
            this.Cbb_Zone.Items.AddRange(new object[] {
            "热力板块",
            "活跃板块",
            "冷淡板块"});
            this.Cbb_Zone.Location = new System.Drawing.Point(69, 96);
            this.Cbb_Zone.Name = "Cbb_Zone";
            this.Cbb_Zone.Size = new System.Drawing.Size(81, 20);
            this.Cbb_Zone.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "回帖区域";
            // 
            // Ckb_Txt
            // 
            this.Ckb_Txt.AutoSize = true;
            this.Ckb_Txt.Location = new System.Drawing.Point(166, 25);
            this.Ckb_Txt.Name = "Ckb_Txt";
            this.Ckb_Txt.Size = new System.Drawing.Size(78, 16);
            this.Ckb_Txt.TabIndex = 8;
            this.Ckb_Txt.Text = "checkBox1";
            this.Ckb_Txt.UseVisualStyleBackColor = true;
            this.Ckb_Txt.CheckedChanged += new System.EventHandler(this.Ckb_Txt_CheckedChanged);
            // 
            // Txt_Content
            // 
            this.Txt_Content.Location = new System.Drawing.Point(219, 59);
            this.Txt_Content.Name = "Txt_Content";
            this.Txt_Content.Size = new System.Drawing.Size(177, 21);
            this.Txt_Content.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(164, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "回帖内容";
            // 
            // Cbb_Number
            // 
            this.Cbb_Number.FormattingEnabled = true;
            this.Cbb_Number.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.Cbb_Number.Location = new System.Drawing.Point(69, 23);
            this.Cbb_Number.Name = "Cbb_Number";
            this.Cbb_Number.Size = new System.Drawing.Size(60, 20);
            this.Cbb_Number.TabIndex = 5;
            this.Cbb_Number.SelectedIndexChanged += new System.EventHandler(this.Cbb_Number_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "回帖数量";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "回帖间隔";
            // 
            // Gbox_Input
            // 
            this.Gbox_Input.Controls.Add(this.Txt_Print);
            this.Gbox_Input.Location = new System.Drawing.Point(3, 145);
            this.Gbox_Input.Name = "Gbox_Input";
            this.Gbox_Input.Size = new System.Drawing.Size(402, 145);
            this.Gbox_Input.TabIndex = 4;
            this.Gbox_Input.TabStop = false;
            this.Gbox_Input.Text = "输出信息";
            // 
            // Txt_Print
            // 
            this.Txt_Print.Location = new System.Drawing.Point(6, 21);
            this.Txt_Print.Multiline = true;
            this.Txt_Print.Name = "Txt_Print";
            this.Txt_Print.Size = new System.Drawing.Size(390, 118);
            this.Txt_Print.TabIndex = 0;
            // 
            // Timer_time
            // 
            this.Timer_time.Interval = 1000;
            this.Timer_time.Tick += new System.EventHandler(this.Timer_time_Tick);
            // 
            // Ckb_Ban
            // 
            this.Ckb_Ban.AutoSize = true;
            this.Ckb_Ban.Location = new System.Drawing.Point(166, 99);
            this.Ckb_Ban.Name = "Ckb_Ban";
            this.Ckb_Ban.Size = new System.Drawing.Size(78, 16);
            this.Ckb_Ban.TabIndex = 12;
            this.Ckb_Ban.Text = "checkBox1";
            this.Ckb_Ban.UseVisualStyleBackColor = true;
            this.Ckb_Ban.CheckedChanged += new System.EventHandler(this.Ckb_Ban_CheckedChanged);
            // 
            // Console
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 291);
            this.Controls.Add(this.Gbox_Input);
            this.Controls.Add(this.Gbox_Params);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Console";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Console";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Console_FormClosing);
            this.Load += new System.EventHandler(this.Console_Load);
            this.Gbox_Params.ResumeLayout(false);
            this.Gbox_Params.PerformLayout();
            this.Gbox_Input.ResumeLayout(false);
            this.Gbox_Input.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Btn_Post;
        private System.Windows.Forms.DomainUpDown Dup_Sec;
        private System.Windows.Forms.Label Lab_Sec;
        private System.Windows.Forms.GroupBox Gbox_Params;
        private System.Windows.Forms.ComboBox Cbb_Number;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox Ckb_Txt;
        private System.Windows.Forms.TextBox Txt_Content;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox Gbox_Input;
        private System.Windows.Forms.TextBox Txt_Print;
        private System.Windows.Forms.ComboBox Cbb_Zone;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer Timer_time;
        private System.Windows.Forms.CheckBox Ckb_Smart;
        private System.Windows.Forms.ToolTip tTips;
        private System.Windows.Forms.CheckBox Ckb_Ban;
    }
}

