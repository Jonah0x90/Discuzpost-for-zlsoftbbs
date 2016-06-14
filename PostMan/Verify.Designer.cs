namespace AutoPost
{
    partial class Verify
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Verify));
            this.PicBox_Verify = new System.Windows.Forms.PictureBox();
            this.Txt_Verify = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.PicBox_Verify)).BeginInit();
            this.SuspendLayout();
            // 
            // PicBox_Verify
            // 
            resources.ApplyResources(this.PicBox_Verify, "PicBox_Verify");
            this.PicBox_Verify.Name = "PicBox_Verify";
            this.PicBox_Verify.TabStop = false;
            // 
            // Txt_Verify
            // 
            resources.ApplyResources(this.Txt_Verify, "Txt_Verify");
            this.Txt_Verify.Name = "Txt_Verify";
            this.Txt_Verify.TextChanged += new System.EventHandler(this.Txt_Verify_TextChanged);
            // 
            // Verify
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Txt_Verify);
            this.Controls.Add(this.PicBox_Verify);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Verify";
            this.Load += new System.EventHandler(this.Verify_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PicBox_Verify)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PicBox_Verify;
        private System.Windows.Forms.TextBox Txt_Verify;
    }
}