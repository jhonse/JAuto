using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAuto.lib.kuaishou
{
    class JService
    {

        private Process service_proc = null;

        /// <summary>
        /// 开启服务
        /// </summary>
        public void startService()
        {
            if (service_proc == null)
            {
                try
                {
                    service_proc = new Process();
                    service_proc.StartInfo.CreateNoWindow = true;
                    service_proc.StartInfo.FileName = /*System.IO.Directory.GetCurrentDirectory()*/"D:" + "/Appium/node_modules/.bin/appium.cmd";
                    service_proc.StartInfo.UseShellExecute = false;
                    service_proc.StartInfo.RedirectStandardError = true;
                    service_proc.StartInfo.RedirectStandardInput = true;
                    service_proc.StartInfo.RedirectStandardOutput = true;
                    service_proc.EnableRaisingEvents = true;
                    service_proc.Exited += service_proc_Exited;
                    service_proc.Start();
                }
                catch (Exception)
                {

                }
            }
        }

        /// <summary>
        /// 退出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void service_proc_Exited(object sender, EventArgs e)
        {
            if (service_proc != null)
                service_proc = null;
        }

        /// <summary>
        /// 关闭服务
        /// </summary>
        public void killService()
        {
            if (service_proc != null)
            {
                try
                {
                    service_proc.Kill();
                    service_proc = null;
                }
                catch (Exception)
                {

                }
            }
        }
    }
}
