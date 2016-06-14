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
using AutoPost.PublicFunction;
using System.Text.RegularExpressions;
using AutoPost.Properties;
using System.Threading;


namespace AutoPost
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Btn_Login_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Txt_User.Text.Trim()) && string.IsNullOrEmpty(Txt_Pwd.Text.Trim()))
            {
                MessageBox.Show("用户名或密码不能为空","提示",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            var result = DoLogin();
            var state = this.ChecinState(result);

            if (state != "登录成功")
            {
                if (state == "请输入验证码")
                {
                    //验证码登录
                    var verify = VerifyLogin(result);
                    var info = this.ChecinState(verify);
                    if (info != "登录成功")
                    {
                        MessageBox.Show(info, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        Function.Instance.user = Txt_User.Text.Trim();
                        Console console = new Console();
                        this.Hide();
                        console.Show();
                    }
                }
                else
                {
                    MessageBox.Show(state, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                Function.Instance.user = Txt_User.Text.Trim();
                Console console = new Console();
                this.Hide();
                console.Show();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            label1.Text = "帐号:";
            label2.Text = "密码:";
            Btn_Login.Text = "登录";
            Txt_User.Text = Settings.Default.UserName;
            Txt_Pwd.Text = Settings.Default.PassWord;
            tTips.SetToolTip(cb_save, "保存帐号密码到本地");
            tTips.SetToolTip(Cb_SmartVerify, "智能识别验证码，80%+ hit rate! *非验证码发帖用户组请勿勾选");

        }

        private string DoLogin()
        { 
            var url = "http://www.zlsoft.com/bbs/member.php?mod=logging&action=login&loginsubmit=yes&infloat=yes&lssubmit=yes&inajax=1";
            var referer = "http://www.zlsoft.com/bbs/member.php?mod=logging&action=login";
            var username = Function.Instance.GB2312ReCode(Txt_User.Text.Trim()).Replace("!", "%21");
            var password = Function.Instance.UrlReCode(Txt_Pwd.Text.Trim()).Replace("!", "%21");
            var postdata = "username="+username+"&password="+password+"&quickforward=yes&handlekey=ls";
            var result = Function.Instance.GetHttpWebRequest(url, postdata, referer, ref Function.Instance.Cookie);
            return result;
        }

        private string VerifyLogin(string html)
        {
            var verify = string.Empty;
            var referer = "http://www.zlsoft.com/bbs/forum.php";
            var xmlurl = Function.Instance.GetUrl(html);
            var info = Function.Instance.GetHtml(xmlurl, "gbk", Function.Instance.Cookie);
            var auth = Function.Instance.GB2312ReCode(Function.Instance.GetAuth(info));
            var loginhash = Function.Instance.GetLoginHash(info);
            var idhash = Function.Instance.GetSeccodeHash(info);
            var formhash = Function.Instance.GetFormhash(info);
            var smartverify = string.Empty;
            var verifysrc = Function.Instance.GetVerifySrc(idhash, ref smartverify);
            if (Function.Instance.smartverify == true)
            {
                verify = smartverify;
            }
            else
            {
                Verify ify = new Verify();
                ify.VerifySrc = verifysrc;
                ify.SmartVerify = smartverify;
                ify.ShowDialog();
                verify = Function.Instance.verifycode;
            }
            var url = "http://www.zlsoft.com/bbs/member.php?mod=logging&action=login&loginsubmit=yes&handlekey=login&loginhash=" + loginhash + "&inajax=1";
            var username = Function.Instance.GB2312ReCode(Txt_User.Text.Trim()).Replace("!", "%21");
            var password = Function.Instance.UrlReCode(Txt_Pwd.Text.Trim()).Replace("!", "%21");
            var postdata = "formhash=" + formhash + "&referer=http%3A%2F%2Fwww.zlsoft.com%2Fbbs%2Fforum.php&auth=" + auth + "&seccodehash=" + idhash + "&seccodemodid=member%3A%3Alogging&seccodeverify=" + verify + "&loginsubmit=true";
            var result = Function.Instance.GetHttpWebRequest(url, postdata, referer, ref Function.Instance.Cookie);
            return result;
        }  

        private void test()
        {
            var url = "http://www.zlsoft.com/bbs/forum.php?mod=guide&view=new";
            var html = Function.Instance.GetHtml(url, "gbk");
            List<string> list = new List<string>();
            string temps = Regex.Match(html, "<a[^>]+href=['\"](.*?)['\"][^>]+class=\"xst\"[^>]*>",RegexOptions.Multiline).ToString();
            Regex reg = new Regex("<a[^>]+href=['\"](.*?)['\"][^>]+class=\"xst\"[^>]*>", RegexOptions.IgnoreCase);
            MatchCollection matchs = reg.Matches(html);
            foreach (Match item in matchs)
            {
                if (item.Success)
                {
                    list.Add(item.Value.ToString());
                }
            }
            list.Add(temps);
            foreach (Match m in Regex.Matches(html, "<a[^>]+href=['\"](<key>.*?)['\"][^>]+class=\"xst\"[^>]*>"))
            {
                var temp = (m.Groups["key"].Value + "\n").ToString();
                list.Add(temp);
            }
            
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void cb_save_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_save.Checked == true)
            {
                Settings.Default.UserName = Txt_User.Text.Trim();
                Settings.Default.PassWord = Txt_Pwd.Text.Trim();
                Settings.Default.Save();    
            }
            else
            {
                Settings.Default.UserName = null;
                Settings.Default.PassWord = null;
                Settings.Default.Reset();
            }
        }

        private void Cb_SmartVerify_CheckedChanged(object sender, EventArgs e)
        {
            if (Cb_SmartVerify.Checked == true)
            {
                Function.Instance.smartverify = true;
            }
            else
            {
                Function.Instance.smartverify = false;
            }
        }

        private string ChecinState(string result)
        {
            if (result.Contains("登录失败"))
            {
                return "密码错误";
            }
            if (result.Contains("请选择安全提问以及填写正确的答案"))
            {
                return "暂不支持带密保的帐号登录";
            }
            if (result.Contains("密码错误次数过多"))
            {
                return "密码错误次数过多，请 15 分钟后重新登录";
            }
            if (result.Contains("密码空或包含非法字符"))
            {
                return "网络错误，请稍后再试";
            }
            if (result.Contains("errorhandle_ls"))
            {
                return "请输入验证码";
            }
            if (result.Contains("您当前的访问请求当中含有非法字符，已经被系统拒绝"))
            {
                return "密码错误";
            }
            else
            {
                return "登录成功";
            }
        }

        public void temptest()
        {
            var url = "http://www.zlsoft.com/bbs/forum.php?mod=viewthread&tid=133364&extra=page%3D1";
            var html = Function.Instance.GetHtml(url,"gbk");
            var result = Function.Instance.SmartReport(html);
        }
    }
}
