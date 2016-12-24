using JAuto.function;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JAuto.lib.kuaishou
{
    /// <summary>
    /// 窗口处理类
    /// </summary>
    class JWindows
    {
        public String config_path = System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini";
        public String config_set_name = "快手设置";
        public String config_action_name = "快手动作";

        private Process proc = null;

        /// <summary>
        /// 获得所有模拟器
        /// </summary>
        /// <returns></returns>
        public string[] getSimulatorAll() {
            string simulatorInstallPath = iniOS.ReadIniData(config_set_name, "simulator_install_path", "", config_path);
            if (!simulatorInstallPath.Equals("")) {
                try
                {
                    if (simulatorInstallPath.Contains("Nox"))
                    {
                        return Directory.GetDirectories(simulatorInstallPath + "/bin/BignoxVMS");
                    }
                    else if (simulatorInstallPath.Contains("Microvirt"))
                    {
                        return Directory.GetDirectories(simulatorInstallPath + "/MEmu/MemuHyperv VMs");
                    }
                }
                catch (Exception e) {
                    System.Windows.Forms.MessageBox.Show(e.Message);
                    return null;
                }
                
            }
            return null;
        }

        /// <summary>
        /// 打开指定模拟器
        /// </summary>
        /// <param name="simulatorName">模拟器ID</param>
        public void openSimulator(string simulatorName = "")
        {
            if (!simulatorName.Equals(""))
            {
                JCmd jcmd = new JCmd();
                String MultExePath = iniOS.ReadIniData(config_set_name, "simulator_install_path", "", config_path) + "/bin/Nox.exe";
                if (!MultExePath.Equals("")) {
                    string cmd = MultExePath + " " + "-clone:" + simulatorName;
                    jcmd.RunCmd(cmd);
                }
            }
        }

        /// <summary>
        /// 关闭指定模拟器
        /// </summary>
        /// <param name="simulatorName">模拟器ID</param>
        public void closeSimulator(string simulatorName = "") {
            if (!simulatorName.Equals(""))
            {
                JCmd jcmd = new JCmd();
                String MultExePath = iniOS.ReadIniData(config_set_name, "simulator_install_path", "", config_path) + "/bin/Nox.exe";
                if (!MultExePath.Equals(""))
                {
                    string cmd = MultExePath + " " + "-clone:" + simulatorName + " -quit";
                    jcmd.RunCmd(cmd);
                    //KillSimulatorProcesses(); 
                }
            }
        }

        /// <summary>
        /// 关闭所有模拟器
        /// </summary>
        public void closeSimulatorAll() {
            JCmd jcmd = new JCmd();
            String MultExePath = iniOS.ReadIniData(config_set_name, "simulator_install_path", "", config_path) + "/bin/Nox.exe";
            if (!MultExePath.Equals(""))
            {
                string[] simulatorAll = getSimulatorAll();
                foreach (string simulator in simulatorAll)
                {
                    string cmd = MultExePath + " -clone:" + Path.GetFileName(simulator) + " -quit";
                    jcmd.RunCmd(cmd);
                    KillSimulatorProcesses();
                }
            }
        }

        /// <summary>
        /// 关闭模拟器进程
        /// </summary>
        private void KillSimulatorProcesses() {
            Process[] allproc = Process.GetProcesses();
            foreach (Process proc in allproc)
            {
                if (proc.ProcessName.Contains("Nox"))
                {
                    //Console.WriteLine(proc.ProcessName);
                    try
                    {
                        proc.Kill();
                        proc.CloseMainWindow();
                        proc.Close();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        /// <summary>
        /// 创建模拟器
        /// </summary>
        /// <param name="count"></param>
        public void createSimulator(int count = 0) {
            string ID = iniOS.ReadIniData(config_set_name, "simulator_setting_ID", "", config_path);
            string apk_path = iniOS.ReadIniData(config_set_name, "apk_file_path", "", config_path);
            string title = iniOS.ReadIniData(config_set_name, "simulator_setting_title", "", config_path);
            string screen = iniOS.ReadIniData(config_set_name, "simulator_setting_screen", "", config_path);
            string performance = iniOS.ReadIniData(config_set_name, "simulator_setting_performance", "", config_path);
            string cpu = iniOS.ReadIniData(config_set_name, "simulator_setting_cpu", "", config_path);
            string memory = iniOS.ReadIniData(config_set_name, "simulator_setting_memory", "", config_path);
            string root = iniOS.ReadIniData(config_set_name, "simulator_setting_root", "", config_path);
            string virtualKey = iniOS.ReadIniData(config_set_name, "simulator_setting_virtualKey", "", config_path);
            JCmd jcmd = new JCmd();
            string s_cmd = "";
            if (!ID.Equals(""))
            {
                if (count == 0)
                {
                    s_cmd += " -clone:" + ID;
                }
                else
                {
                    s_cmd += " -clone:" + ID + "_" + count;
                }
            }
            if (!title.Equals(""))
            {
                s_cmd += " -title:" + title + "_" + count;
            }
            if (!screen.Equals(""))
            {
                s_cmd += " -screen:" + screen;
            }
            if (!performance.Equals(""))
            {
                s_cmd += " -performance:" + performance;
            }
            if (!cpu.Equals(""))
            {
                s_cmd += " -cpu:" + cpu;
            }
            if (!memory.Equals(""))
            {
                s_cmd += " -memory:" + memory;
            }
            if (!root.Equals(""))
            {
                s_cmd += " -root:" + root;
            }
            if (!virtualKey.Equals(""))
            {
                s_cmd += " -virtualKey:" + virtualKey;
            }
            if (!apk_path.Equals(""))
            {
                s_cmd += " -apk:" + apk_path;
            }
            String MultExePath = iniOS.ReadIniData(config_set_name, "simulator_install_path", "", config_path) + "/bin/Nox.exe";
            if (!MultExePath.Equals(""))
            {
                string cmd = MultExePath + s_cmd;
                jcmd.RunCmd(cmd);
            }
        }

        /// <summary>
        /// 删除所有模拟器
        /// </summary>
        public void deleteSimulatorAll() {
            string simulatorInstallPath = iniOS.ReadIniData(config_set_name, "simulator_install_path", "", config_path);
            if (!simulatorInstallPath.Equals(""))
            {
                simulatorInstallPath = simulatorInstallPath + "/bin/BignoxVMS";
                DirectoryInfo dir = new DirectoryInfo(simulatorInstallPath);
                if (dir.Exists) {
                    DirectoryInfo[] childs = dir.GetDirectories();
                    foreach (DirectoryInfo child in childs)
                    {
                        try
                        {
                            child.Delete(true);
                        }catch(Exception e){
                            e.ToString();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 删除指定模拟器
        /// </summary>
        /// <param name="simulatorName"></param>
        public void deleteSimulator(string simulatorName = "") { 
            string simulatorInstallPath = iniOS.ReadIniData(config_set_name, "simulator_install_path", "", config_path);
            if (!simulatorInstallPath.Equals(""))
            {
                simulatorInstallPath = simulatorInstallPath + "/bin/BignoxVMS/" + simulatorName;
                DirectoryInfo dir = new DirectoryInfo(simulatorInstallPath);
                if (dir.Exists) {
                    try
                    {
                        dir.Delete(true);
                    }
                    catch(Exception e)
                    {
                        System.Windows.Forms.MessageBox.Show(e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 检查模拟器是否加载完
        /// </summary>
        /// <returns></returns>
        public Boolean checkSimulatorRunFinished() {
            Boolean ret = false;
            winApi.EnumDesktopWindows(IntPtr.Zero,
            new winApi.EnumDesktopWindowsDelegate(delegate(IntPtr hWnd, uint lparam)
            {
                StringBuilder className = new StringBuilder(100);
                winApi.GetClassName(hWnd, className, 100);
                int length = winApi.GetWindowTextLength(hWnd);
                StringBuilder titleName = new StringBuilder(length + 1);
                winApi.GetWindowText(hWnd, titleName, titleName.Capacity);
                if (className.ToString().Equals("Qt5QWindowToolSaveBits") && titleName.ToString().Equals("Nox"))
                {
                    winApi.RECT rect = new winApi.RECT();
                    winApi.GetWindowRect(hWnd, ref rect);
                    int width = rect.Right - rect.Left; //窗口的宽度
                    //int height = rect.Bottom - rect.Top; //窗口的高度
                    if (width == 36)
                    {
                        ret = true;
                    }
                }
                return true;
            }),
            IntPtr.Zero);
            return ret;
            /*IntPtr hWnd = winApi.FindWindow("Qt5QWindowToolSaveBits", "Nox");
            if (!hWnd.Equals(IntPtr.Zero))
            {
                winApi.RECT rect = new winApi.RECT();
                winApi.GetWindowRect(hWnd, ref rect);
                int width = rect.Right - rect.Left; //窗口的宽度
                //int height = rect.Bottom - rect.Top; //窗口的高度
                if (width == 36)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }*/
        }

        /// <summary>
        /// 检查模拟器是否正在加载
        /// </summary>
        /// <returns></returns>
        public Boolean checkSimulatorRunFinishing()
        {
            IntPtr hWnd = winApi.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Qt5QWindowIcon", null);
            if (hWnd.Equals(IntPtr.Zero))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 显示模拟器
        /// </summary>
        public void showSimulator() {
            IntPtr hWnd = winApi.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Qt5QWindowIcon", null);
            if (iniOS.ReadIniData("模拟器设置", "showSimulator", "0", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini") == "0")
            {
                winApi.ShowWindow(hWnd, winApi.SW_HIDE);
                winApi.EnumDesktopWindows(IntPtr.Zero,
                    new winApi.EnumDesktopWindowsDelegate(delegate(IntPtr hWnd_son, uint lparam)
                    {
                        StringBuilder className = new StringBuilder(100);
                        winApi.GetClassName(hWnd_son, className, 100);
                        int length = winApi.GetWindowTextLength(hWnd_son);
                        StringBuilder titleName = new StringBuilder(length + 1);
                        winApi.GetWindowText(hWnd_son, titleName, titleName.Capacity);
                        if (className.ToString().Equals("Qt5QWindowToolSaveBits") && titleName.ToString().Equals("Nox"))
                        {
                            winApi.ShowWindow(hWnd_son, winApi.SW_HIDE);
                        }
                        return true;
                    }),
                    IntPtr.Zero);
            }
            else
            {
                winApi.ShowWindow(hWnd, winApi.SW_SHOW);
                winApi.EnumDesktopWindows(IntPtr.Zero,
               new winApi.EnumDesktopWindowsDelegate(delegate(IntPtr hWnd_son, uint lparam)
               {
                   StringBuilder className = new StringBuilder(100);
                   winApi.GetClassName(hWnd_son, className, 100);
                   int length = winApi.GetWindowTextLength(hWnd_son);
                   StringBuilder titleName = new StringBuilder(length + 1);
                   winApi.GetWindowText(hWnd_son, titleName, titleName.Capacity);
                   if (className.ToString().Equals("Qt5QWindowIcon") && titleName.ToString().Equals("Nox"))
                   {
                       winApi.ShowWindow(hWnd_son, winApi.SW_HIDE);
                   }
                   if (className.ToString().Equals("Qt5QWindowToolSaveBits") && titleName.ToString().Equals("Nox"))
                   {
                       winApi.ShowWindow(hWnd_son, winApi.SW_SHOW);
                   }
                   return true;
               }),
               IntPtr.Zero);
            }
        }

        /// <summary>
        /// 打开多开模拟器
        /// </summary>
        private void openMultExe()
        {
            String MultExePath = iniOS.ReadIniData(config_set_name, "mul_ext_path", "", config_path);
            if (MultExePath.Equals(IntPtr.Zero))
            {
                iniOS.WriteIniData(config_action_name, "action_status", "0", config_path);
            }
            else
            {
                try
                {
                    proc = Process.Start(MultExePath);
                    if (proc != null)
                    {
                        //监视进程退出
                        proc.EnableRaisingEvents = true;
                        //指定退出事件方法
                        proc.Exited += new EventHandler(proc_Exited);
                        iniOS.WriteIniData(config_action_name, "action_status", "1", config_path);
                    }
                    else
                    {
                        iniOS.WriteIniData(config_action_name, "action_status", "0", config_path);
                    }
                }
                catch (ArgumentException ex)
                {
                    ex.ToString();
                    iniOS.WriteIniData(config_action_name, "action_status", "0", config_path);
                }
            }
        }

        /// <summary>
        /// 多开模拟器关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void proc_Exited(object sender, EventArgs e)
        {
            iniOS.WriteIniData(config_action_name, "action_status", "0", config_path);
        }

        /// <summary>
        /// 关闭多开模拟器
        /// </summary>
        private void closeMultExe() {
            if (proc != null) {
                proc.Kill();
            }
        }
    }
}
