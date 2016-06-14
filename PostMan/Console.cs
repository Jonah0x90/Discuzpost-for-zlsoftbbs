using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Web;
using System.Threading;
using System.Runtime.Remoting;
using AutoPost.PublicFunction;
using AutoPost.Properties;

namespace AutoPost
{
    public partial class Console : Form
    {
        public Console()
        {
            InitializeComponent();
        }

        public static List<string> fid = new List<string>();
        public static List<string> tid = new List<string>();
        public static bool end = false;
        public static int number { get; set; }
        public static string context { get; set; }
        public static int mode { get; set; }
        public static string msg { get; set; }
        public static bool hits { get; set; }
        public static string smarthtml { get; set; }
        public static bool banlist { get; set; }
        public static Thread t = new Thread(new ThreadStart(StartReport));
        static AutoResetEvent autoEvent = new AutoResetEvent(false);  

        private void Console_Load(object sender, EventArgs e)
        {
            //Function.Instance.ergodicCookie(Function.Instance.Cookie);
            Lab_Sec.Text = "/秒";
            Btn_Post.Text = "执行";
            Cbb_Number.DropDownStyle = ComboBoxStyle.DropDownList;
            Cbb_Number.SelectedIndex = 0;
            Dup_Sec.ReadOnly = true;
            Dup_Sec.SelectedIndex = 0;
            Cbb_Zone.DropDownStyle = ComboBoxStyle.DropDownList;
            Cbb_Zone.SelectedIndex = 1;
            Ckb_Txt.Text = "使用常用灌水随机库";
            Ckb_Smart.Text = "智能回帖";
            Ckb_Ban.Text = "敏感规避";
            tTips.SetToolTip(Ckb_Smart, "智能回帖模式，你懂的。");
            tTips.SetToolTip(Ckb_Txt, "从回帖库中随机抽取回帖内容");
            tTips.SetToolTip(Ckb_Ban, "智能屏蔽某些敏感板块的贱人");
            //var uid = "";
            //var user = GetUserName(ref uid);
            //this.Text = "Console - " + "[UID:" + uid + "/" + user + "]";
            this.Text = string.Format("Console - [{0}]",Function.Instance.user); 
        }

        private void Btn_Post_Click(object sender, EventArgs e)
        {
            if (Ckb_Txt.Checked == false)
            {
                if (Ckb_Smart.Checked == false)
                {
                    if (string.IsNullOrEmpty(Txt_Content.Text.Trim()))
                    {
                        MessageBox.Show("回帖内容不得为空", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (Txt_Content.Text.Trim().Length < 8)
                    {
                        MessageBox.Show("回帖内容必须大于8个字符", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            Gbox_Params.Enabled = false;
            context = Txt_Content.Text.Trim();
            Timer_time.Interval = int.Parse(Dup_Sec.SelectedItem.ToString()) * 1000;
            Btn_Post.Text = "执行中...";
            Print("-------------------------");
            Print("少女祈祷中...");
            //初始化载入第一页50条fid&tid
            Function.Instance.GetParams(GetZone(), ref fid, ref tid);
            Print("-------------------------");
            Print("祈祷完毕");
            number = int.Parse(Cbb_Number.SelectedItem.ToString());
            t.Start();
            Print("-------------------------");
            Print("开始执行...");
            Timer_time.Start();
        }

        private static string Report(string fid, string tid)
        {
            //http://www.zlsoft.com/bbs/forum.php?mod=viewthread&tid=132187
            //message=%CA%B2%C3%B4%C7%E9%BF%F6%7E%7E&posttime=1444976844&formhash=e4f6837f&usesig=1&subject=++
            var url = "http://www.zlsoft.com/bbs/forum.php?mod=viewthread&tid=" + tid + "&extra=";
            smarthtml = Function.Instance.GetHtml(url, "gbk", Function.Instance.Cookie);
            //检查验证码
            var verify = Function.Instance.GetSeccodeHash(smarthtml);
            if (verify == null)
            {
                var formhash = Function.Instance.GetFormhash(smarthtml);
                var message = MsgSwitch(mode);
                var posttime = Function.Instance.GetTimeStamp(true);
                var referer = url;
                var posturl = "http://www.zlsoft.com/bbs/forum.php?mod=post&action=reply&fid=" + fid + "&tid=" + tid + "&extra=&replysubmit=yes&infloat=yes&handlekey=fastpost&inajax=1";
                var postdata = "message=" + message + "&posttime=" + posttime + "&formhash=" + formhash + "&usesig=1&subject=";
                var result = Function.Instance.GetHttpWebRequest(posturl, postdata, referer, Function.Instance.Cookie);
                if (result.Contains("succeedhandle_fastpost"))
                {
                    return "回帖：" + tid + " 成功";
                }
                else
                {
                    hits = false;
                    return "回帖：" + tid + " 失败";
                }
            }
            else
            {
                //获取验证码Src
                var seccodeverify = string.Empty;
                var smartverify = string.Empty;
                var src = Function.Instance.GetVerifySrc(verify, ref smartverify);
                if (Function.Instance.smartverify == true)
                {
                    seccodeverify = smartverify;
                }
                else
                {
                    Verify ify = new Verify();
                    ify.VerifySrc = src;
                    ify.SmartVerify = smartverify;
                    ify.ShowDialog();
                    seccodeverify = Function.Instance.verifycode;
                }
                //seccodehash
                var seccodehash = verify;
                var seccodemodid = "forum::viewthread";
                var formhash = Function.Instance.GetFormhash(smarthtml);
                var message = MsgSwitch(mode);
                var posttime = Function.Instance.GetTimeStamp(true);
                var referer = url;
                var posturl = "http://www.zlsoft.com/bbs/forum.php?mod=post&action=reply&fid=" + fid + "&tid=" + tid + "&extra=&replysubmit=yes&infloat=yes&handlekey=fastpost&inajax=1";
                var postdata = "message=" + message + "&seccodehash=" + seccodehash + "&seccodemodid" + seccodemodid + "&seccodeverify=" + seccodeverify + "&posttime=" + posttime + "&formhash=" + formhash + "&usesig=1&subject=";
                var result = Function.Instance.GetHttpWebRequest(posturl, postdata, referer, Function.Instance.Cookie);
                if (result.Contains("succeedhandle_fastpost"))
                {
                    if (Function.Instance.smartverify == true)
                    {
                        return "回帖：" + tid + " 成功(*智能识别验证码命中)";
                    }
                    else
                    {
                        return "回帖：" + tid + " 成功";
                    }
                }
                else
                {
                    hits = false;
                    if (Function.Instance.smartverify == true)
                    {
                        return "回帖：" + tid + " 失败(*智能识别验证码未命中)";
                    }
                    else
                    {
                        return "回帖：" + tid + " 失败";
                    }
                }
            }
        }

        public static string MsgSwitch(int mode)
        {
            switch (mode)
            { 
                case 0:
                    var ramlist = Function.Instance.GB2312ReCode(Function.Instance.ReportMsg()).Replace("!", "%21");
                    return ramlist.Replace("！", "%21");
                case 1:
                    var cont = Function.Instance.GB2312ReCode(context).Replace("!", "%21");
                    return cont.Replace("！", "%21");
                case 2:
                    var smartrdm = Function.Instance.GB2312ReCode(Function.Instance.SmartReport(smarthtml));
                    return smartrdm.Replace("！", "%21");
                default:
                    return null;
            }
        }

        private void Console_FormClosing(object sender, FormClosingEventArgs e)
        {
            t.Abort();
            autoEvent.Set();
            Application.Exit();
        }

        private void Print(string msg)
        {
            this.Txt_Print.AppendText("->" + msg + "\r\n");
        }

        private static void StartReport()
        {
            for (int i = 0; i < number; i++)
            {
                if (banlist == true)
                {
                    if (Function.Instance.BanList(fid[i]))
                    {
                        number += 1;
                        Messages("敏感规避成功");
                        continue;
                    }
                }
                var result = Report(fid[i], tid[i]);
                Messages(result);
                if (hits == false)
                {
                    number += 1;
                    Messages("正在尝试重试...");
                }
                hits = true;
                //t.Suspend();
                autoEvent.WaitOne();
            }
            end = true;
            t.Abort();

        }

        private void Ckb_Txt_CheckedChanged(object sender, EventArgs e)
        {
            if (Ckb_Txt.Checked == true)
            {
                Txt_Content.Enabled = false;
                mode = 0;
                Print("启用常用灌水随机库");
                Ckb_Smart.Enabled = false;
            }
            else
            {
                Txt_Content.Enabled = true;
                mode = 1;
                Print("启用自定义回帖");
                Ckb_Smart.Enabled = true;
            }
        }

        private void Cbb_Number_SelectedIndexChanged(object sender, EventArgs e)
        {
            Print("回帖数量:" + Cbb_Number.SelectedItem.ToString());
        }

        private void Dup_Sec_SelectedItemChanged(object sender, EventArgs e)
        {
            Print("回帖间隔:" + Dup_Sec.SelectedItem.ToString() + "/秒");
        }

        private static void Messages(string str)
        {
            msg = str;
        }

        private string GetZone()
        {
            var id = Cbb_Zone.SelectedIndex;
            switch (id)
            {
                case 0:
                    return "hot";
                case 1:
                    return "new";
                case 2:
                    return "newthread";
                default:
                    return "new";
            }

        }

        private void Timer_time_Tick(object sender, EventArgs e)
        {
            if (t.ThreadState == ThreadState.WaitSleepJoin)
            {
                //t.Resume();
                autoEvent.Set();
                Print(msg);
            }
            if (t.ThreadState == ThreadState.Stopped)
            {
                if (end)
                {
                    Gbox_Params.Enabled = true;
                    Btn_Post.Text = "执行完毕";
                    Btn_Post.Enabled = false;
                    Print("-------------------------");
                    Print("执行完毕...");
                    Timer_time.Stop();
                }
            }
        }

        private string GetUserName(ref string uid)
        {
            var url = "http://www.zlsoft.com/bbs/forum.php";
            var html = Function.Instance.GetHtml(url, "gbk", Function.Instance.Cookie);
            uid = Function.Instance.GetUid(html);
            return Function.Instance.GetUser(html);
        }

        private void Ckb_Smart_CheckedChanged(object sender, EventArgs e)
        {
            if (Ckb_Txt.Checked == false)
            {
                if (Ckb_Smart.Checked)
                {
                    mode = 2;
                    Ckb_Txt.Enabled = false;
                    Txt_Content.Enabled = false;
                    Print("启用智能回帖");
                }
                else
                {
                    mode = 1;
                    Ckb_Txt.Enabled = true;
                    Txt_Content.Enabled = true;
                    Print("启用自定义回帖");
                }
            }            
        }

        private void Ckb_Ban_CheckedChanged(object sender, EventArgs e)
        {
            if (Ckb_Ban.Checked == true)
            {
                banlist = true;
            }
            else
            {
                banlist = false;
            }
        }
    }
}
