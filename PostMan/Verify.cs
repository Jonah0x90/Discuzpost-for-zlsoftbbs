using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using AutoPost.PublicFunction;

namespace AutoPost
{
    public partial class Verify : Form
    {
        public Verify()
        {
            InitializeComponent();
        }

        public string VerifyValue { get { return Txt_Verify.Text; } set { Txt_Verify.Text = value; } }

        public IList<byte> VerifySrc { get; set; }

        public string SmartVerify { get; set; }
        
        private void Verify_Load(object sender, EventArgs e)
        {
            if (File.Exists(Function.file_name))
            {
                if (PicBox_Verify.Image != null)
                {
                    PicBox_Verify.Image.Dispose();
                    PicBox_Verify.Image = null;
                }
                File.Delete(Function.file_name);
                Thread.Sleep(50);
            }
            File.WriteAllBytes(Function.file_name, VerifySrc.ToArray());
            Thread.Sleep(50);
            PicBox_Verify.Image = Image.FromFile(Function.file_name);
            PicBox_Verify.Height = PicBox_Verify.Image.Height;
            PicBox_Verify.Width = PicBox_Verify.Image.Width;

            //if (SmartVerify != null || string.IsNullOrEmpty(SmartVerify))
            //{
            //    if (SmartVerify != "识别失败")
            //    {
            //        for (int i = 0; i < SmartVerify.Length; i++)
            //        {
            //            Txt_Verify.Text += SmartVerify.Substring(i, i++);
            //            Thread.Sleep(1000);
            //        }
            //    }
            //    else
            //    {
            //        Txt_Verify.Text = null;
            //    }
            //}
           
        }

        private void Txt_Verify_TextChanged(object sender, EventArgs e)
        {
            if (Txt_Verify.Text.Length == 4)
            {
                Function.Instance.verifycode = Txt_Verify.Text;
                PicBox_Verify.Image.Dispose();
                PicBox_Verify.Image = null;
                File.Delete(Function.file_name);
                this.Close();
            }
        }
    }
}
