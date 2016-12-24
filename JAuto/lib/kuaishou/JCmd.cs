using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAuto.lib.kuaishou
{
    /// <summary>
    /// CMD类
    /// </summary>
    class JCmd
    {
        private Process proc = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        public JCmd()
        {
            proc = new Process();
        }

        /// <summary>
        ///  执行CMD语句
        /// </summary>
        /// <param name="cmd">要执行的CMD命令</param>
        public void RunCmd(string cmd)
        {
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = "cmd.exe";
            //是否使用操作系统shell启动
            proc.StartInfo.UseShellExecute = false;
            //接受来自调用程序的输入信息
            proc.StartInfo.RedirectStandardError = true;
            //由调用程序获取输出信息
            proc.StartInfo.RedirectStandardInput = true;
            //重定向标准错误输出
            proc.StartInfo.RedirectStandardOutput = true;
            //不显示程序窗口
            proc.StartInfo.CreateNoWindow = true;
            proc.Start();
            proc.StandardInput.WriteLine(cmd + " & exit");
            proc.StandardInput.AutoFlush = true;
            //string outStr = proc.StandardOutput.ReadToEnd();
            //等待程序执行完退出进程
            proc.WaitForExit();
            proc.Close();
        }

        /// <summary>
        /// 打开软件并执行命令
        /// </summary>
        /// <param name="programName">软件路径加名称（.exe文件）</param>
        /// <param name="cmd">要执行的命令</param>
        public void RunProgram(string programName, string cmd)
        {
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = programName;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            if (cmd.Length != 0)
            {
                proc.StandardInput.WriteLine(cmd);
            }
            proc.Close(); 
        }

    }
}
