using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JAuto.lib.kuaishou
{
    class JIP
    {
        delegate HtmlElement getId(string ID);
        delegate HtmlElementCollection getCollection(string tag);
        delegate HtmlElementCollection getCollection2(HtmlElement obj,string tag);

        private Form form = null;
        private WebBrowser route = null;
        private bool complate = false;
        
        public int status = 0;
        public bool success = false;
        public string last_ip = "";
        public string current_ip = "";
        public string msg = "[切换IP]...";

        /// <summary>
        /// 构造方法
        /// </summary>
        public JIP(Form form_s, WebBrowser route_s)
        {
            form = form_s;
            route = route_s;
            route.ScriptErrorsSuppressed = true;
            route.DocumentCompleted += route_DocumentCompleted;
            last_ip = current_ip = GetIP();
        }

        private HtmlElement GetElementById(string ID)
        {
            if (!route.InvokeRequired)
            {
                return route.Document.GetElementById(ID);
            }
            else {
                getId gid = new getId(GetElementById);
                return form.Invoke(gid, new object[]{ID}) as HtmlElement;
            }
        }

        private HtmlElementCollection GetElementsByTagName(string tag) {
            if (!route.InvokeRequired)
            {
                return route.Document.GetElementsByTagName(tag);
            }
            else
            {
                getCollection gid = new getCollection(GetElementsByTagName);
                return form.Invoke(gid, new object[] { tag }) as HtmlElementCollection;
            }
        }

        private HtmlElementCollection GetElementsByTagName2(HtmlElement obj, string tag)
        {
            if (!route.InvokeRequired)
            {
                return obj.GetElementsByTagName(tag);
            }
            else
            {
                getCollection2 gid = new getCollection2(GetElementsByTagName2);
                return form.Invoke(gid, new object[] { obj,tag }) as HtmlElementCollection;
            }
        }

        /// <summary>
        /// 浏览器加载完毕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void route_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url != route.Document.Url) return;
            if (route.ReadyState == WebBrowserReadyState.Complete)
            {
                //页面加载完毕
                complate = true;
            }
        }

        public void start() {
            if (status == 0)
            {
                openPage();
            }
            else if (status == 1) {
                login();
            }
            else if (status == 2) {
                changePage();
            }
            else if (status == 3) {
                changeNet();
            }
            else if (status == 4) {
                checkIP();
            }
        }

        /// <summary>
        /// 打开页面 0
        /// </summary>
        private void openPage() {
            status = 1;
            complate = false;
            route.Navigate("http://192.168.1.1");
            msg = "[切换IP] 开启路由器页面...";
        }

        /// <summary>
        /// 登录 1
        /// </summary>
        private void login() {
            if (status > 1)
            {
                status = 0;
            }
            else {
                if (complate) {
                    try
                    {
                        HtmlElement lgPwd = GetElementById("lgPwd");
                        HtmlElement loginSub = GetElementById("loginSub");
                        if (lgPwd != null && loginSub != null)
                        {
                            status = 2;
                            complate = true;
                            lgPwd.SetAttribute("value", "zlh900412yzl");
                            loginSub.InvokeMember("click");
                            msg = "[切换IP] 成功登录路由器.";
                        }
                        else {
                            status = 0;
                            msg = "[切换IP] 失败登录路由器.";
                        }
                    }
                    catch(Exception e)
                    {
                        status = 0;
                        msg = "[切换IP] 失败登录路由器." + e.ToString();
                    }
                }
                else
                {
                    msg = "[切换IP] 等待登录路由器...";
                }
            }
        }

        /// <summary>
        /// 切换页面 2
        /// </summary>
        private void changePage() {
            if (status > 2) {
                status = 1;
            }
            else
            {
                if (complate)
                {
                    try
                    {
                        HtmlElement headFunc = GetElementById("headFunc");
                        if (headFunc != null)
                        {
                            HtmlElementCollection li = GetElementsByTagName2(headFunc,"li");
                            if (li.Count == 3)
                            {
                                status = 3;
                                complate = true;
                                li[1].InvokeMember("click");
                                msg = "[切换IP] 成功切换重启页面.";
                            }
                            else {
                                status = 1;
                                msg = "[切换IP] 失败切换重启页面.";
                            }
                        }
                        else {
                            status = 1;
                            msg = "[切换IP] 失败切换重启页面.";
                        }
                    }
                    catch (Exception e)
                    {
                        status = 1;
                        msg = "[切换IP] 失败切换重启页面."+e.ToString();
                    }
                }
                else
                {
                    msg = "[切换IP] 等待切换重启页面...";
                }
            }
        }

        /// <summary>
        /// 切换IP 3
        /// </summary>
        private void changeNet() {
            if (status > 3)
            {
                status = 2;
            }
            else {
                if (complate) {
                    try
                    {
                        HtmlElement disconnect = GetElementById("disconnect");
                        HtmlElement save = GetElementById("save");
                        if (disconnect != null && save != null)
                        {
                            status = 4;
                            complate = true;
                            disconnect.InvokeMember("click");
                            save.InvokeMember("click");
                            msg = "[切换IP] 成功切换IP操作.";
                        }
                        else {
                            status = 2;
                            msg = "[切换IP] 失败切换IP操作.";
                        }
                    }
                    catch(Exception e) {
                        status = 2;
                        msg = "[切换IP] 失败切换IP操作." + e.ToString();
                    }
                }
                else
                {
                    msg = "[切换IP] 等待切换IP操作...";
                }
            }
        }

        /// <summary>
        /// 检查IP
        /// </summary>
        private void checkIP() {
            if (status > 4)
            {
                status = 3;
            }
            else {
                current_ip = GetIP();
                if (current_ip != last_ip)
                {
                    success = true;
                    last_ip = current_ip;
                    msg = "[切换IP] 切换IP成功.上次IP: " + last_ip + "; 当前IP: " + current_ip;
                }
                else {
                    status = 3;
                    msg = "[切换IP] 切换IP失败...";
                }
            }
        }

        /// <summary>
        /// 获得IP
        /// </summary>
        /// <returns></returns>
        private string GetIP()
        {
            string tempip = "";
            try
            {
                WebRequest wr = WebRequest.Create("http://www.ip138.com/");
                Stream s = wr.GetResponse().GetResponseStream();
                StreamReader sr = new StreamReader(s, Encoding.Default);
                string all = sr.ReadToEnd(); //读取网站的数据

                int start = all.IndexOf("您的IP地址是：[") + 9;
                int end = all.IndexOf("]", start);
                tempip = all.Substring(start, end - start);
                sr.Close();
                s.Close();
            }
            catch
            {
            }
            return tempip;
        }
    }
}
