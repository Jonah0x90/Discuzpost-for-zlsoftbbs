using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using System.IO.Compression;
using System.Xml;
using System.Collections;
using AvensLab.Service;

namespace AutoPost.PublicFunction
{
    public class Function
    {

        private static Function _instance = new Function();

        public static Function Instance
        {
            get
            {
                return Function._instance;
            }
        }

        private Function()
        {

        }

        public CookieContainer Cookie = new CookieContainer();
        public string user { get; set; }

        public const string file_name = "tmp.png";
        public string verifycode { get; set; }

        public bool smartverify { get; set; }

        public bool smartreport { get; set; }

        /// <summary>
        /// 请求HTML源码
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="encoding">编码格式(gbk/gb2312/utf-8)</param>
        /// <returns></returns>
        public string GetHtml(string url, string encoding)
        {
            Uri uri = new Uri(url);
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(uri);
            myReq.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.81 Safari/537.36";
            myReq.Accept = "*/*";
            myReq.KeepAlive = true;
            myReq.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
            HttpWebResponse result = (HttpWebResponse)myReq.GetResponse();
            Stream receviceStream = result.GetResponseStream();
            StreamReader readerOfStream = new StreamReader(receviceStream, System.Text.Encoding.GetEncoding(encoding));
            string strHTML = readerOfStream.ReadToEnd();
            readerOfStream.Close();
            receviceStream.Close();
            result.Close();
            return strHTML;
        }

        /// <summary>
        /// 请求html源码带cookie
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="encoding">编码格式</param>
        /// <param name="cookie">COOKIE</param>
        /// <returns></returns>
        public string GetHtml(string url, string encoding, CookieContainer cookie)
        {
            Uri uri = new Uri(url);
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(uri);
            myReq.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.81 Safari/537.36";
            myReq.Accept = "*/*";
            myReq.KeepAlive = true;
            myReq.CookieContainer = cookie;
            myReq.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
            HttpWebResponse result = (HttpWebResponse)myReq.GetResponse();
            Stream receviceStream = result.GetResponseStream();
            StreamReader readerOfStream = new StreamReader(receviceStream, System.Text.Encoding.GetEncoding(encoding));
            string strHTML = readerOfStream.ReadToEnd();
            readerOfStream.Close();
            receviceStream.Close();
            result.Close();
            return strHTML;
        }

        /// <summary>
        /// 请求html源码带referer&cookie
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="referer">Referer</param>
        /// <param name="encoding">编码</param>
        /// <param name="cookie">COOKIE</param>
        /// <returns></returns>
        public string GetHtml(string url, string referer,string encoding, CookieContainer cookie)
        {
            Uri uri = new Uri(url);
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(uri);
            myReq.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.81 Safari/537.36";
            myReq.Accept = "*/*";
            myReq.KeepAlive = true;
            myReq.CookieContainer = cookie;
            myReq.Referer = referer;
            myReq.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
            HttpWebResponse result = (HttpWebResponse)myReq.GetResponse();
            Stream receviceStream = result.GetResponseStream();
            StreamReader readerOfStream = new StreamReader(receviceStream, System.Text.Encoding.GetEncoding(encoding));
            string strHTML = readerOfStream.ReadToEnd();
            readerOfStream.Close();
            receviceStream.Close();
            result.Close();
            return strHTML;
        }

        /// <summary>
        /// 请求验证码图片二进制流
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="referer">Referer</param>
        /// <param name="encoding">编码</param>
        /// <param name="cookie">COOKIE</param>
        /// <returns></returns>
        public IList<byte> GetVerifyPic(string url, string referer, string encoding, CookieContainer cookie,ref CookieContainer cookies)
        {
            Uri uri = new Uri(url);
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(uri);
            myReq.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.81 Safari/537.36";
            myReq.Accept = "*/*";
            myReq.KeepAlive = true;
            myReq.CookieContainer = cookie;
            myReq.Referer = referer;
            myReq.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
            //更新COOKIE
            HttpWebResponse result = (HttpWebResponse)myReq.GetResponse();
            cookies.Add(result.Cookies);
            Stream receviceStream = result.GetResponseStream();
            List<byte> bytes = new List<byte>();
            while (true)
            {
                int bt = receviceStream.ReadByte();
                if (bt == -1)
                    break;
                bytes.Add((byte)bt);
            }
            receviceStream.Close();
            receviceStream.Dispose();
            result.Close();
            return bytes;
        }

        /// <summary>
        /// 请求Hash验证码
        /// </summary>
        /// <param name="html">HTML源代码</param>
        /// <returns></returns>
        public string GetFormhash(string html)
        {
            Match match_FormHash = new Regex("<input type=\"hidden\" name=\"formhash\" value=\"(?<key>.*?)\" />", RegexOptions.None).Match(html);
            return match_FormHash.Groups["key"].Value.ToString();
        }

        /// <summary>
        /// 请求Loginhash验证码
        /// </summary>
        /// <param name="html">HTML源代码</param>
        /// <returns></returns>
        public string GetHtmlhash(string html)
        {
            Match match_UserValidateUrl = new Regex(";\" action=\"(?<key>.*?)\">", RegexOptions.None).Match(html);
            return match_UserValidateUrl.Groups["key"].Value.ToString();
        }

        /// <summary>
        /// 请求Tid
        /// </summary>
        /// <param name="html">HTML</param>
        /// <returns></returns>
        public string GetTid(string str)
        {
            //<a[^>]+href=['"](.*?)['"][^>]+class="xst"[^>]*>
            //<a href="forum.php?mod=viewthread&amp;tid=132061&amp;extra=" target="_blank" class="xst" >
            //Match match_FormHash = new Regex("<a href=\"(?<key>.*?)\" target=\"_blank\" class=\"xst\" />", RegexOptions.IgnoreCase).Match(str);
            Match match_FormHash = new Regex("<a[^>]+href=['\"](?<key>.*?)['\"][^>]+class=\"xst\"[^>]*>", RegexOptions.IgnoreCase).Match(str);
            return match_FormHash.Groups["key"].Value.ToString();
        }

        /// <summary>
        /// 请求Fid
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public string GetFid(string str)
        {
            Match match = new Regex("<input type=\"hidden\" name=\"srhfid\" value=\"(?<key>.*?)\" />", RegexOptions.None).Match(str);
            return match.Groups["key"].Value.ToString();
        }

        /// <summary>
        /// 获取用户名
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public string GetUser(string html)
        {
            //onMouseOver="showMenu({'ctrlid':this.id,'ctrlclass':'a'})">谭健</a>
            Match match = new Regex("<a[^>]+id=\"umnav\"[^>]*>(?<key>.*?)</a>", RegexOptions.None).Match(html);
            return match.Groups["key"].Value.ToString();
        }

        public string GetUid(string html)
        {
            Match match = new Regex("<a[^>]+href=['\"]home.php['?\"]mod=space['&\"]amp;uid=(?<key>.*?)['\"][^>]+title=\"访问我的空间\"[^>]*>", RegexOptions.None).Match(html);
            return match.Groups["key"].Value.ToString();
        }

        /// <summary>
        /// Post提交
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="postdata">数据</param>
        /// <returns></returns>
        public string GetHttpWebRequest(string url, string postdata, string referer, ref CookieContainer cookie)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = new CookieContainer(); //Cookie
            //以下是发送的http头，随便加，其中referer挺重要的，有些网站会根据这个来反盗链  
            request.Referer = referer;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.Headers["Accept-Language"] = "zh-CN,zh;q=0.";
            request.Headers["Accept-Charset"] = "GBK,utf-8;q=0.7,*;q=0.3";
            request.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.202 Safari/535.1";
            request.KeepAlive = true;
            //上面的http头看情况而定，但是下面俩必须加  
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";

            Encoding encoding = Encoding.Default;//根据网站的编码自定义  
            byte[] postData = encoding.GetBytes(postdata);//postDataStr即为发送的数据，格式还是和上次说的一样  
            request.ContentLength = postData.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(postData, 0, postData.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            cookie.Add(response.Cookies);
            Stream responseStream = response.GetResponseStream();
            //如果http头中接受gzip的话，这里就要判断是否为有压缩，有的话，直接解压缩即可  
            if (response.Headers["Content-Encoding"] != null && response.Headers["Content-Encoding"].ToLower().Contains("gzip"))
            {
                responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
            }

            StreamReader streamReader = new StreamReader(responseStream, encoding);
            string retString = streamReader.ReadToEnd();

            streamReader.Close();
            responseStream.Close();

            return retString;
        }

        /// <summary>
        /// 回帖
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="postdata">数据</param>
        /// <param name="referer">返回页</param>
        /// <param name="Cookie">COOKIE</param>
        /// <returns></returns>
        public string GetHttpWebRequest(string url, string postdata, string referer, CookieContainer Cookie)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.CookieContainer = Cookie; //发送附加Cookie
            //以下是发送的http头，随便加，其中referer挺重要的，有些网站会根据这个来反盗链  
            request.Referer = referer;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.Headers["Accept-Language"] = "zh-CN,zh;q=0.";
            request.Headers["Accept-Charset"] = "GBK,utf-8;q=0.7,*;q=0.3";
            request.UserAgent = "Mozilla/5.0 (Windows NT 5.1) AppleWebKit/535.1 (KHTML, like Gecko) Chrome/14.0.835.202 Safari/535.1";
            request.KeepAlive = true;
            //上面的http头看情况而定，但是下面俩必须加  
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";

            Encoding encoding = Encoding.Default;//根据网站的编码自定义  
            byte[] postData = encoding.GetBytes(postdata);//postDataStr即为发送的数据，格式还是和上次说的一样  
            request.ContentLength = postData.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(postData, 0, postData.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            //如果http头中接受gzip的话，这里就要判断是否为有压缩，有的话，直接解压缩即可  
            if (response.Headers["Content-Encoding"] != null && response.Headers["Content-Encoding"].ToLower().Contains("gzip"))
            {
                responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
            }

            StreamReader streamReader = new StreamReader(responseStream, encoding);
            string retString = streamReader.ReadToEnd();

            streamReader.Close();
            responseStream.Close();

            return retString;
        }

        /// <summary>
        /// URL转码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string UrlReCode(string url)
        {
            return System.Web.HttpUtility.UrlEncode(url);
        }

        /// <summary>
        /// GB2312转码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GB2312ReCode(string str)
        {
            return HttpUtility.UrlEncode(str, System.Text.Encoding.GetEncoding("GB2312"));
        }

        /// MD5加密
        /// </summary>
        /// <param name="toCryString">被加密字符串</param>
        /// <returns>加密后的字符串</returns>
        public string MD5(string input)
        {
            string result;
            if (string.IsNullOrEmpty(input))
            {
                result = string.Empty;
            }
            else
            {
                System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
                System.Security.Cryptography.MD5 mD = System.Security.Cryptography.MD5.Create();
                byte[] array = mD.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
                for (int i = 0; i < array.Length; i++)
                {
                    stringBuilder.Append(array[i].ToString("x2"));
                }
                result = stringBuilder.ToString();
            }
            return result;
        }

        /// <summary>  
        /// 获取当前时间戳  
        /// </summary>  
        /// <param name="flag">为真时获取10位时间戳,为假时获取13位时间戳.</param>  
        /// <returns></returns>  
        public string GetTimeStamp(bool flag = true)
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            string ret = string.Empty;
            if (flag)
                ret = Convert.ToInt64(ts.TotalSeconds).ToString();
            else
                ret = Convert.ToInt64(ts.TotalMilliseconds).ToString();

            return ret;
        }

        /// <summary>
        /// 随机回帖内容
        /// </summary>
        /// <returns></returns>
        public string ReportMsg()
        {
            string[] array = new string[] 
            { 
                "小手一抖，经验到手。手持酱油，低头猛走。飘过~~挽过~！！！！", 
                "看了楼主的帖子，我面色凝重，关掉电脑，起身打开冰箱，拿出一瓶农妇山泉，拿在手里走到窗前，凝视着远方，外面灰蒙蒙的天下着小雨，看着窗外，我的心情更忧郁了，我再也无法抑制内心的情感，打开窗子，对着窗外大声呐喊 ：你写的是神马玩意儿？", 
                "路过……（短短两个字，就说明我来过，也许不带有其他感情，但后面由六个点书写的省略号，说明了我还是有内涵的……）", 
                "汉语:关我什么事，我来打酱油。 英语:It's none of my business .I come to buy some sauce. 德语:Ich bezogen, was ich kam zu einer Soja-So?e. 法语:Je lis ce qui, j'en suis arriv une sauce de soja. 荷兰语:Ik gerelateerd wat, kwam ik tot een sojasaus.俄语:Я,касающихся того, что я пришелк соевым соусом. 西班牙语:Relacionados con lo que yo, me vino a un salsa de soja. 意大利语:I relativi cosa, sono venuto a una salsa di soia. 日语:私関连したどのような、私がして醤油. 希腊语:I σχετικ? σ τι ? ρθα σε μια σ? λτσα σ? γιασ. 火星语：関莪什庅倳，莪唻咑酱怞", 
                "实乃人间惨剧，竟无语凝噎。", 
                "灌，是一种美德。", 
                "看了一遍原文，不懂；又看了一遍原文，还是不懂。再看了一遍原文，实在不懂。最后看了一遍回帖，懂了我为什么不懂……于是我懂了，人有时候要学会放弃。", 
                "天书奇谭，建议列为中文专业博士学位答辩翻译必考题目。", 
                "虎躯一震，三分走人。", 
                "做为一名新人，不敢在论坛大声说话，也不敢得罪人，只能默默地顶完贴然后转身就走人。动作要快，姿势要帅，深藏功与名。" ,
                "这篇帖子构思新颖，题材独具匠心，段落清晰，情节诡异，跌宕起伏，主线分明，引人入胜，平淡中显示出不凡的文学功底，可谓是字字珠玑，句句经典，是我辈应当学习之典范。",
                "阁下淫的一手好诗， 犹如开天辟地之剑芒 好剑好剑 文笔之锋利 犹如茫茫人海之中那一戳大非主流子。 我辈楷模",
                "俺灌的不是水，是寂寞啊！",
                "以前不懂，看贴总是不回 ，一直没提升等级和增加经验，现在我明白了，反正回贴可以升级 ，也可以赚经验，而升级又需要经验，我就把这句话复制下来，遇贴就灌水，捞经验就闪",
                "我顶了，我没有灌水【你信么】，你以为我是谁？",
                "老夫聊发少年狂，治肾亏，不含糖。锦帽貂裘，千骑用康王。为报倾城随太守，三百年，九芝堂。酒酣胸胆尚开张，西瓜霜，喜之郎。持节云中，三金葡萄糖。会挽雕弓如满月，西北望 ，阿迪王。 十年生死两茫茫，恒源祥，羊羊羊。千里孤坟，洗衣用奇强。纵使相逢应不识，补维C，施尔康。夜来幽梦忽还乡，学外语，新东方。相顾无言，洗洗更健康。料得年年断肠处，找工作，富士康",
                "瞎了我的24K高强度硫酸铜硬化畴壁共振防涡流损耗和共振损耗电子脉冲带放光二极管及光敏三极管之晶圆脉冲散射之光斑照射粒子带发光半导体及光电感应器之光源硬化及反电磁波加硫酸亚铁硬化以及硝酸铜硬化氪金防暴狗眼",
                "好帖啊，难得一见的好贴！楼主的帖子简直是惊天地，泣鬼神， 图文并茂、嬉笑怒骂、指点论坛、激扬文字、带给我们的仅仅是视觉上的感受吗？",
                "苍天之下，厚土之上，竟有如此奇人异士、文人墨客！讥讽于谈笑间，笑骂于无形中，层次之高，境界之深，非我等所能匹及，偶像啊！！！仿高人此文，照作一篇，以表仰慕之情。。。",
                "这篇帖子构思新颖，题材独具匠心，段落清晰，情节诡异，跌宕起伏，主线分明，引人入胜，平淡中显示出不凡的文学功底，可谓是字字珠玑，句句经典，是我辈应当学习之典范。",
                "回一个帖需要理由吗",
                "看帖要厚道——怎么说也得顶一下吧",
                "一天不灌　就好象人在地上没有影子",
                "此帖虽说不上山明水秀，可是也别有一番风味",
                "我顶你，顶着你，就象老鼠顶大米",
                "顶就一个字!我不只顶一次!! ",
                "你问我顶的有多深.我顶的有多真...",
                "尊敬的楼主，您订购的十五字已到账，请注意查收",
                "我们的目标：向钱看，向厚赚。",
                "药 ！！药！！药！！切克闹！！抗母昂北鼻够！！洞次大赐洞洞大赐洞次大赐洞洞大次…！！万、吐、碎、佛。你是我心中最美的云彩！！动次大次！！让我用心把你留下来！！动次大次！！留下来… 动次大次！！！药！！药！！黑为狗…切克闹～切克闹",
                "看了楼主的帖子,不由得精神为之一振,自觉七经八脉为之一畅,七窍倒也开了六巧半,自古英雄出少年,楼主年纪轻轻,就有经天纬地之才,定国安邦之智,古人云,卧龙凤雏得一而安天下,而今,天佑我大中华,沧海桑田5000年,中华神州平地一声雷,飞沙走石,大舞迷天,朦胧中,只见顶天立地一金甲天神立于天地间,这人英雄手持双斧,二目如电,一斧下去,混沌初开,二斧下去,女娲造人,三斧下去,小生倾倒.得此大英雄,实耐之幸也,民之福也,怎不叫人喜极而泣.......古人有少年楼主说为证,少年之楼主如红日初升，其道大光；河出伏流，一泻汪洋；潜龙腾渊，鳞爪飞扬；乳虎啸谷，百兽震惶；鹰隼试翼，风尘吸张；奇花初胎，皇皇；干将发硎，有作其芒；天戴其苍，地履其黄；纵有千古，横有八荒；小生对楼主之仰慕如滔滔江水连绵不绝,海枯石烂,天崩地裂,永不变心. ",
                "作为江湖上有头有脸的人物，我很低调，但顶帖我很慎重。名气是大家给的，地位是兄弟拼的，要对大家负责任！楼主的帖的确不错！我代表江湖上所有的兄弟姐妹给你顶上去！你接下来收到的所有回帖都是我安排他们顶你的。我为人就两个字“低调”！你知道就行了！",
                "楼主聊发少年狂，治肾亏，不含糖。锦帽貂裘，千骑用康王。为报倾城随太守，三百年，九芝堂。 酒酣胸胆尚开张，西瓜霜，喜之郎。持节云中，三金葡萄糖。",
                "我龙袍加身，我富可敌国。我权倾一方，我挥金如土。我博学多才，我智勇双全。我英俊潇洒，我风流倜傥，我万众瞩目。我醒了，我要去搬砖了！",
                "元芳，此事你怎么看？",
                "天苍苍，野茫茫，挖掘机技术哪家强?可汗问所欲，木兰不用尚书郎，愿驰千里足，挖掘机技术哪家强?",
                "城会玩，乡话多，理都懂，然并卵，腿玩年，裤脱看，醉不行，图森破，睡起嗨，秀分快，直膝箭、虽不明，但觉厉，语死早，真日狗，上交国"
            };
            Random r = new Random();
            return array[r.Next(0, array.Length - 1)];
        }


        public void GetParams(string type, ref List<string> fid, ref List<string> tid)
        {
            //最新回复
            //http://www.zlsoft.com/bbs/forum.php?mod=guide&view=new 
            //最新热门
            //http://www.zlsoft.com/bbs/forum.php?mod=guide&view=hot
            //最新发表
            //http://www.zlsoft.com/bbs/forum.php?mod=guide&view=newthread
     
            var url = "http://www.zlsoft.com/bbs/forum.php?mod=guide";
            switch (type)
            {
                case "new":
                    url += "&view=new";
                    break;
                case "hot":
                    url += "&view=hot";
                    break;
                case "newthread":
                    url += "&view=newthread";
                    break;
                default:
                    url += "&view=new";
                    break;
            }
            var html = this.GetHtml(url, "gbk", this.Cookie);
            var result = this.GetUrlList(html);
            foreach (var item in result)
            {
                var tidtemp = this.GetTid(item);
                var fidtemp = this.GetHtml("http://www.zlsoft.com/bbs/" + this.GetTid(item).Replace("amp;", ""), "gbk", this.Cookie);
                tid.Add(tidtemp.Split(';').First(s => s.Contains("tid=")).Replace("tid=", "").Replace("&amp", ""));
                fid.Add(this.GetFid(fidtemp));
            }
            //var tidlist = this.GetTidUrl(tidhtml);
            //var fidhtml = this.GetHtml("http://www.zlsoft.com/bbs/" + tidlist.Replace("amp;", ""), "gbk", this.Cookie);
            //var fidlist = this.GetFid(fidhtml);
            ////fid = fidlist.Split(';').First(s => s.Contains("fid=")).Replace("fid=", "");
            //fid = fidlist;
            //tid = tidlist.Split(';').First(s => s.Contains("tid=")).Replace("tid=", "").Replace("&amp", "");
        }

        /// <summary>
        /// 获取UrlList
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public List<string> GetUrlList(string html)
        {
            List<string> list = new List<string>();
            Regex reg = new Regex("<a[^>]+href=['\"](.*?)['\"][^>]+class=\"xst\"[^>]*>", RegexOptions.IgnoreCase);
            MatchCollection matchs = reg.Matches(html);
            foreach (Match item in matchs)
            {
                if (item.Success)
                {
                    list.Add(item.Value.ToString());
                }
            }
            return list;
        }

        /// <summary>
        /// 获取二次验证hash
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public string GetSeccodeHash(string html)
        {
            if (html.Contains("seccode"))
            {
                //\"seccode_(?'hash'.*?)\"
                Match match = new Regex("\"seccode_(?'hash'.*?)\"", RegexOptions.None).Match(html);
                return match.Groups["hash"].Value.ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取LoginHash,二次验证时抓取
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public string GetLoginHash(string html)
        {
            Match match = new Regex(";loginhash=(?'hash'.*?)\"", RegexOptions.None).Match(html);
            return match.Groups["hash"].Value.ToString();
        }

        /// <summary>
        /// 获取验证码验证条件Update
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public string GetUpdate(string html)
        {
            Match match = new Regex("update=(?'update'.*?)&", RegexOptions.None).Match(html);
            return match.Groups["update"].Value.ToString();
        }

        /// <summary>
        /// 获取验证码地址
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        public IList<byte> GetVerifySrc(string hash,ref string smartverify)
        {
            //\"misc.php(?'params'.*?)\"
            //http://www.zlsoft.com/bbs/misc.php?mod=seccode&action=update&idhash=S0o0Fs40&inajax=1&ajaxtarget=seccode_S0o0Fs40
            //Referer是关键 否则提示 Access Denied
            var url = "http://www.zlsoft.com/bbs/misc.php?mod=seccode&action=update&idhash=" + hash + "&inajax=1&ajaxtarget=seccode_" + hash + "";
            var referer = "http://www.zlsoft.com/bbs/forum.php?mod=guide";
            var html = this.GetHtml(url, referer, "gbk", this.Cookie);
            var update = this.GetUpdate(html);
            var verifyurl = "http://www.zlsoft.com/bbs/misc.php?mod=seccode&update=" + update + "&idhash="+ hash;
            var fileurl = AppDomain.CurrentDomain.BaseDirectory + "tmp.png";
            var preprocess = "http://www.unknown.com"; //预处理
            smartverify = this.OrcKing(fileurl, preprocess);
            return this.GetVerifyPic(verifyurl, referer, "gbk", this.Cookie,ref this.Cookie);
            //Match match = new Regex("\"misc.php(?'params'.*?)\"", RegexOptions.None).Match(result);
            //var temp = "misc.php" + match.Groups["params"].Value.ToString();

        }

        /// <summary>
        /// 获取二次验证auth
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public string GetAuth(string html)
        {
            Match match = new Regex("<input type=\"hidden\" name=\"auth\" value=\"(?<key>.*?)\" />", RegexOptions.None).Match(html);
            return match.Groups["key"].Value;
        }

        /// <summary>
        /// 获取二次验证返回XML地址
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public string GetUrl(string html)
        {
            Match match = new Regex("\'member.php(?'src'.*?)\'", RegexOptions.None).Match(html);
            return  "http://www.zlsoft.com/bbs/member.php" + match.Groups["src"].Value.ToString();
        }

        /// <summary>
        /// 智能抓取回帖内容
        /// </summary>
        /// <returns></returns>
        public string SmartReport(string html)
        {
            //border="0"></a></div>(?'text'.*?)</td>
            var list = new List<string>();
            Regex reg = new Regex("border=\"0\"></a></div>(?'text'.*?)</td>", RegexOptions.IgnoreCase);
            MatchCollection matchs = reg.Matches(html);
            foreach (Match item in matchs)
            {
                if (item.Success)
                {
                    var str = NoHTML(item.Groups["text"].Value.ToString());
                    list.Add(str);
                }
            }
            return list[new Random().Next(list.Count)];
        }

        /// <summary>
        /// APIKEY
        /// </summary>
        public const string ApiKey = "5db08c2b14ba7b1473FS1f81G/KdZ6kt3Q7EWO1mgg5lo+52DQJvDSFjvCjvQP9gLONg";

        /// <summary>
        /// OrcKing识别验证码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string OrcKing(string fileurl,string url)
        {
            // 默认请求提交url为 http://lab.ocrking.com/ok.html
            // 后台不定时增加或减少apiUrl，当您使用的apiUrl无法正常请求时
            // 请通过 http://api.ocrking.com/server.html 获取
            // 使用其它请求apiUrl可以这样初始化
            // var ocrKing = new OcrKing(ApiKey, "http://www.ocrking.com/")
            var ocrKing = new OcrKing(ApiKey)
            {
                Language = AvensLab.Service.Models.Language.Eng,
                Service = AvensLab.Service.Models.Service.OcrKingForCaptcha,
                Charset = AvensLab.Service.Models.Charset.DigitLowerUpper,
                FilePath = fileurl,
                Type = url
            };

            // 网络文件识别时FileUrl传图片url  此时type可以省略
            // 服务端根据url进行匹配
            ocrKing.DoService();

            // 识别请求状态及结果
            // 检查是不是请求成功
            if (ocrKing.ProcessStatus)
            {
                // 解析结果
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(ocrKing.OcrResult);

                // 识别结果
                return doc.SelectSingleNode("//Results/ResultList/Item/Result").InnerText;

            }
            else
            {
                return "识别失败";
            }
        }


        public CookieCollection getHeaderSetCookie(string str)
        {
            CookieCollection cc = new CookieCollection();
            string s = "SID=ARRGy4M1QVBtTU-ymi8bL6X8mVkctYbSbyDgdH8inu48rh_7FFxHE6MKYwqBFAJqlplUxq7hnBK5eqoh3E54jqk=;Domain=.google.com;Path=/,LSID=AaMBTixN1MqutGovVSOejyb8mVkctYbSbyDgdH8inu48rh_7FFxHE6MKYwqBFAJqlhCe_QqxLg00W5OZejb_UeQ=;Domain=www.google.com;Path=/accounts";
            Regex re = new Regex("([^;,]+)=([^;,]+);Domain=([^;,]+);Path=([^;,]+)", RegexOptions.IgnoreCase);
            foreach (Match m in re.Matches(s))
            {
                //name, value, path, domain
                Cookie c = new Cookie(m.Groups[1].Value, m.Groups[2].Value, m.Groups[3].Value, m.Groups[3].Value);
                cc.Add(c);
            }
            return cc;
        }
        


        /// <summary>
        /// 遍历COOKIE返回键值对
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public IList<Cookie> ergodicCookie(CookieContainer cookie)
        {
            var listcookie = new List<Cookie>();
            if (cookie.Count > 0)
            {
                Hashtable table = (Hashtable)cookie.GetType().InvokeMember("m_domainTable",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField |
                System.Reflection.BindingFlags.Instance, null, cookie, new object[] { });
                foreach (object pathList in table.Values)
                {
                    SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField
                        | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                    foreach (CookieCollection colCookies in lstCookieCol.Values)
                    {
                        foreach (Cookie c in colCookies)
                        {
                            listcookie.Add(c);
                        }
                    }

                }
                return listcookie;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 更新COOKIE
        /// </summary>
        /// <param name="toUpdate"></param>
        /// <param name="cookies"></param>
        public CookieContainer addCookieToContainer(string cookie, CookieContainer cc, string domian)
        {
            string[] tempCookies = cookie.Split(';');
            string tempCookie = null;
            int Equallength = 0;//  =的位置 
            string cookieKey = null;
            string cookieValue = null; 
            for (int i = 0; i < tempCookies.Length; i++)
            {
                if (!string.IsNullOrEmpty(tempCookies[i]))
                {
                    tempCookie = tempCookies[i];

                    Equallength = tempCookie.IndexOf("=");

                    if (Equallength != -1)       //有可能cookie 无=，就直接一个cookiename；比如:a=3;ck;abc=; 
                    {

                        cookieKey = tempCookie.Substring(0, Equallength).Trim();
                        //cookie=

                        if (Equallength == tempCookie.Length - 1)    //这种是等号后面无值，如：abc=; 
                        {
                            cookieValue = "";
                        }
                        else
                        {
                            cookieValue = tempCookie.Substring(Equallength + 1, tempCookie.Length - Equallength - 1).Trim();
                        }
                    }

                    else
                    {
                        cookieKey = tempCookie.Trim();
                        cookieValue = "";
                    }

                    cc.Add(new Cookie(cookieKey, cookieValue, "", "zlsoft.cn"));

                }

            }

            return cc;
        }

        public CookieContainer addCookieToContainer(string cookie, CookieContainer cc)
        {
            return this.addCookieToContainer(cookie, cc, "zlsoft.cn");
        }

        /// <summary>
        /// 屏蔽某些贱人
        /// </summary>
        /// <param name="fid"></param>
        /// <returns></returns>
        public bool BanList(string fid)
        {
            string[] array = new string[]
            {
                "31", // HIS人生    - 翁代贇
                "52", // 奇图怪论   - 翁代贇
                "79", // 健身俱乐部 - 翁代贇
                "92"  // 车友世界   - 赵直枉
            };
            if (array.Contains(fid))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        ////   <summary>   
        ///   去除HTML标记   
        ///   </summary>   
        ///   <param   name="NoHTML">包括HTML的源码   </param>   
        ///   <returns>已经去除后的文字</returns>   
        public static string NoHTML(string Htmlstring)
        {
            //删除脚本   
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML   
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);

            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            //Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

            return Htmlstring.Trim();
        }
    }
}
