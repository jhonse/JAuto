using JAuto.function;
using JAuto.lib.kuaishou;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JAuto
{
    public partial class Main : Form
    {

        public Main()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        #region 快手设置

        /// <summary>
        /// 设置 - 选择多开执行文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_kuaishou_mul_exe_open_file_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dirDialog = new FolderBrowserDialog();
            dirDialog.Description = "请选择文件夹";
            if (dirDialog.ShowDialog() == DialogResult.OK)
            {
                String fileName = dirDialog.SelectedPath;
                tb_kuaishou_mul_ext_path.Text = fileName;
                iniOS.WriteIniData("快手设置", "simulator_install_path", fileName, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            }
        }

        /// <summary>
        /// 设置 - 选择快手apk文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_kuaishou_apk_open_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Title = "选择快手app文件";
            ofd.Filter = "app文件(.apk)|*.apk";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                String fileName = ofd.FileName;
                tb_kuaishou_apk_path.Text = fileName;
                iniOS.WriteIniData("快手设置", "apk_file_path", fileName, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            }
        }

        /// <summary>
        /// tab页面切换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabKuaishou_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_kuaishou_mul_ext_path.Text = iniOS.ReadIniData("快手设置", "simulator_install_path", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_kuaishou_apk_path.Text = iniOS.ReadIniData("快手设置", "apk_file_path", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_setting_simulator_ID.Text = iniOS.ReadIniData("快手设置", "simulator_setting_ID", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_setting_simulator_title.Text = iniOS.ReadIniData("快手设置", "simulator_setting_title", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_setting_simulator_screen.Text = iniOS.ReadIniData("快手设置", "simulator_setting_screen", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_setting_simulator_performance.Text = iniOS.ReadIniData("快手设置", "simulator_setting_performance", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_setting_simulator_cpu.Text = iniOS.ReadIniData("快手设置", "simulator_setting_cpu", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_setting_simulator_memory.Text = iniOS.ReadIniData("快手设置", "simulator_setting_memory", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_setting_simulator_root.Text = iniOS.ReadIniData("快手设置", "simulator_setting_root", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_setting_simulator_virtualKey.Text = iniOS.ReadIniData("快手设置", "simulator_setting_virtualKey", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");

            tb_kuaishou_ad_file_path.Text = iniOS.ReadIniData("广告设置", "file_path", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            cb_kuaishou_ads_sort.Text = iniOS.ReadIniData("广告设置", "sort", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");

            tb_kuaishou_users_file_path.Text = iniOS.ReadIniData("用户设置", "file_path", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            cb_kuaishou_users_sort.Text = iniOS.ReadIniData("用户设置", "sort", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
        }

        /// <summary>
        /// 模拟器列表 - 开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnKuaishouStart_Click(object sender, EventArgs e)
        {
            btnKuaishouStart.Enabled = false;
            btnKuaishouStop.Enabled = true;
            btnKuaishouUpdate.Enabled = false;
            btnKuaishouCreate.Enabled = false;
            btnKuaishouDeleteAll.Enabled = false;
            btnKuaishouQuit.Enabled = false;
            btnKuaishouSave.Enabled = false;
            btnKuaishouSetSave.Enabled = false;
            btnKuaishouStopCreate.Enabled = false;
            JOs.init(this, lv_kuaishou_simulator, lv_kuaishou_ads, lv_kuaishou_users, lb_kuaishou_logs, tb_simulator_number, webRoute,1000);
            JOs.startTimer();
        }

        /// <summary>
        /// 模拟器列表 - 结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnKuaishouStop_Click(object sender, EventArgs e)
        {
            btnKuaishouStart.Enabled = true;
            btnKuaishouStop.Enabled = false;
            btnKuaishouUpdate.Enabled = true;
            btnKuaishouCreate.Enabled = true;
            btnKuaishouDeleteAll.Enabled = true;
            btnKuaishouQuit.Enabled = true;
            btnKuaishouSave.Enabled = true;
            btnKuaishouSetSave.Enabled = true;
            btnKuaishouStopCreate.Enabled = true;
            JOs.init(this, lv_kuaishou_simulator, lv_kuaishou_ads, lv_kuaishou_users, lb_kuaishou_logs, tb_simulator_number, webRoute,1000);
            JOs.closeTimer();
        }

        /// <summary>
        /// 模拟器列表 - 更新列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnKuaishouUpdate_Click(object sender, EventArgs e)
        {
            JOs.init(this, lv_kuaishou_simulator, lv_kuaishou_ads, lv_kuaishou_users, lb_kuaishou_logs, tb_simulator_number, webRoute, 1000);
            JOs.updateSimulator();
        }

        /// <summary>
        /// 模拟器列表 - 全部删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnKuaishouDeleteAll_Click(object sender, EventArgs e)
        {
            JOs.init(this, lv_kuaishou_simulator, lv_kuaishou_ads, lv_kuaishou_users, lb_kuaishou_logs, tb_simulator_number, webRoute, 1000);
            JOs.deleteAllSimulator();
        }

        /// <summary>
        /// 模拟器列表 - 全部退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnKuaishouQuit_Click(object sender, EventArgs e)
        {
            JOs.init(this, lv_kuaishou_simulator, lv_kuaishou_ads, lv_kuaishou_users, lb_kuaishou_logs, tb_simulator_number, webRoute, 1000);
            JOs.closeAllSimulator();
        }

        /// <summary>
        /// 模拟器列表 - 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnKuaishouSave_Click(object sender, EventArgs e)
        {
            iniOS.WriteIniData("快手设置", "simulator_setting_ID", tb_setting_simulator_ID.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            iniOS.WriteIniData("快手设置", "simulator_setting_title", tb_setting_simulator_title.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            iniOS.WriteIniData("快手设置", "simulator_setting_screen", tb_setting_simulator_screen.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            iniOS.WriteIniData("快手设置", "simulator_setting_performance", tb_setting_simulator_performance.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            iniOS.WriteIniData("快手设置", "simulator_setting_cpu", tb_setting_simulator_cpu.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            iniOS.WriteIniData("快手设置", "simulator_setting_memory", tb_setting_simulator_memory.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            iniOS.WriteIniData("快手设置", "simulator_setting_root", tb_setting_simulator_root.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            iniOS.WriteIniData("快手设置", "simulator_setting_virtualKey", tb_setting_simulator_virtualKey.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            MessageBox.Show("保存成功!");
        }

        /// <summary>
        /// 模拟器列表 - 一键创建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnKuaishouCreate_Click(object sender, EventArgs e)
        {
            int number = int.Parse(tb_simulator_number.Text);
            if (number <= 0)
            {
                MessageBox.Show("数目不能少于1");
            }
            else {
                JOs.init(this, lv_kuaishou_simulator, lv_kuaishou_ads, lv_kuaishou_users, lb_kuaishou_logs, tb_simulator_number, webRoute,1000);
                JOs.createSimulator();
            }
        }

        /// <summary>
        /// 模拟器列表 - 暂停一键创建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnKuaishouStopCreate_Click(object sender, EventArgs e)
        {
            JOs.init(this, lv_kuaishou_simulator, lv_kuaishou_ads, lv_kuaishou_users, lb_kuaishou_logs, tb_simulator_number, webRoute,1000);
            JOs.stopCreateSimulator();
        }

        /// <summary>
        /// 模拟器列表 - 当前行打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_kuaishou_open_Click(object sender, EventArgs e)
        {
            JOs.init(this, lv_kuaishou_simulator, lv_kuaishou_ads, lv_kuaishou_users, lb_kuaishou_logs, tb_simulator_number, webRoute, 1000);
            JOs.openSimulator();
        }

        /// <summary>
        /// 模拟器列表 - 当前行关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_kuaishou_quit_Click(object sender, EventArgs e)
        {
            JOs.init(this, lv_kuaishou_simulator, lv_kuaishou_ads, lv_kuaishou_users, lb_kuaishou_logs, tb_simulator_number, webRoute, 1000);
            JOs.closeSimulator();
        }

        /// <summary>
        /// 模拟器列表 - 当前行删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmi_kuaishou_delete_Click(object sender, EventArgs e)
        {
            JOs.init(this, lv_kuaishou_simulator, lv_kuaishou_ads, lv_kuaishou_users, lb_kuaishou_logs, tb_simulator_number, webRoute, 1000);
            JOs.deleteSimulator();
        }

        /// <summary>
        /// 模拟器列表 - 保存基本信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnKuaishouSetSave_Click(object sender, EventArgs e)
        {
            iniOS.WriteIniData("模拟器设置", "loaddata", tb_kuaishou_simulator_set_loaddata.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            iniOS.WriteIniData("模拟器设置", "viewplay", tb_kuaishou_simulator_set_viewplay.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            iniOS.WriteIniData("模拟器设置", "checklogin", tb_kuaishou_simulator_set_checklogin.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            iniOS.WriteIniData("模拟器设置", "clicklogin", tb_kuaishou_simulator_set_clicklogin.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            iniOS.WriteIniData("模拟器设置", "play_follow", tb_kuaishou_simulator_set_play_follow.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            iniOS.WriteIniData("模拟器设置", "play_like", tb_kuaishou_simulator_set_play_like.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            iniOS.WriteIniData("模拟器设置", "play_showInput", tb_kuaishou_simulator_set_play_showInput.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            iniOS.WriteIniData("模拟器设置", "play_clickInput", tb_kuaishou_simulator_set_play_clickInput.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            iniOS.WriteIniData("模拟器设置", "play_comment_input", tb_kuaishou_simulator_set_play_comment_input.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            iniOS.WriteIniData("模拟器设置", "play_comment_submit", tb_kuaishou_simulator_set_play_comment_submit.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            iniOS.WriteIniData("模拟器设置", "return", tb_kuaishou_simulator_set_return.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            iniOS.WriteIniData("模拟器设置", "login_change", tb_kuaishou_simulator_set_login_change.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            iniOS.WriteIniData("模拟器设置", "login_input", tb_kuaishou_simulator_set_login_input.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            iniOS.WriteIniData("模拟器设置", "login_submit", tb_kuaishou_simulator_set_login_submit.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            iniOS.WriteIniData("模拟器设置", "play_type", tb_kuaishou_simulator_set_play_type.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            iniOS.WriteIniData("模拟器设置", "comment_counts", tb_kuaishou_simulator_set_comment_counts.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            iniOS.WriteIniData("模拟器设置", "comment_home_counts", tb_kuaishou_simulator_set_comment_home_counts.Text, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            MessageBox.Show("保存成功!");
        }

        /// <summary>
        /// 模拟器列表 - 显示模拟器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbShowSimulator_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowSimulator.Checked)
            {
                iniOS.WriteIniData("模拟器设置", "showSimulator", "1", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            }
            else
            {
                iniOS.WriteIniData("模拟器设置", "showSimulator", "0", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            }
        }

        /// <summary>
        /// 广告列表 - 选择文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_kuaishou_ads_chose_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Title = "选择广告文件";
            ofd.Filter = "广告文件(.txt)|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK) {
                String fileName = ofd.FileName;
                tb_kuaishou_ad_file_path.Text = fileName;
                iniOS.WriteIniData("广告设置", "file_path", fileName, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            }
        }

        /// <summary>
        /// 广告列表 - 选择排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_kuaishou_ads_sort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sort = cb_kuaishou_ads_sort.Text;
            iniOS.WriteIniData("广告设置", "sort", sort, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
        }

        /// <summary>
        /// 广告列表 - 更新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_kuaishou_ads_update_Click(object sender, EventArgs e)
        {
            JOs.init(this, lv_kuaishou_simulator, lv_kuaishou_ads, lv_kuaishou_users, lb_kuaishou_logs, tb_simulator_number, webRoute, 1000);
            JOs.updateAds();
        }

        /// <summary>
        /// 用户列表 - 选择文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_kuaishou_users_chose_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Title = "选择用户文件";
            ofd.Filter = "用户文件(.txt)|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                String fileName = ofd.FileName;
                tb_kuaishou_users_file_path.Text = fileName;
                iniOS.WriteIniData("用户设置", "file_path", fileName, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            }
        }

        /// <summary>
        /// 用户列表 - 选择排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_kuaishou_users_sort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sort = cb_kuaishou_users_sort.Text;
            iniOS.WriteIniData("用户设置", "sort", sort, System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
        }

        /// <summary>
        /// 用户列表 - 更新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_kuaishou_users_update_Click(object sender, EventArgs e)
        {
            JOs.init(this, lv_kuaishou_simulator, lv_kuaishou_ads, lv_kuaishou_users, lb_kuaishou_logs, tb_simulator_number, webRoute, 1000);
            JOs.updateUsers();
        }

        #endregion

        /// <summary>
        /// 窗体初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Load(object sender, EventArgs e)
        {
            //初始化模拟器列表数据
            tb_kuaishou_simulator_set_loaddata.Text = iniOS.ReadIniData("模拟器设置", "loaddata", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_kuaishou_simulator_set_viewplay.Text = iniOS.ReadIniData("模拟器设置", "viewplay", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_kuaishou_simulator_set_checklogin.Text = iniOS.ReadIniData("模拟器设置", "checklogin", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_kuaishou_simulator_set_clicklogin.Text = iniOS.ReadIniData("模拟器设置", "clicklogin", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_kuaishou_simulator_set_play_follow.Text = iniOS.ReadIniData("模拟器设置", "play_follow", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_kuaishou_simulator_set_play_like.Text = iniOS.ReadIniData("模拟器设置", "play_like", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_kuaishou_simulator_set_play_showInput.Text = iniOS.ReadIniData("模拟器设置", "play_showInput", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_kuaishou_simulator_set_play_clickInput.Text = iniOS.ReadIniData("模拟器设置", "play_clickInput", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_kuaishou_simulator_set_play_comment_input.Text = iniOS.ReadIniData("模拟器设置", "play_comment_input", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_kuaishou_simulator_set_play_comment_submit.Text = iniOS.ReadIniData("模拟器设置", "play_comment_submit", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_kuaishou_simulator_set_return.Text = iniOS.ReadIniData("模拟器设置", "return", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_kuaishou_simulator_set_login_change.Text = iniOS.ReadIniData("模拟器设置", "login_change", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_kuaishou_simulator_set_login_input.Text = iniOS.ReadIniData("模拟器设置", "login_input", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_kuaishou_simulator_set_login_submit.Text = iniOS.ReadIniData("模拟器设置", "login_submit", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_kuaishou_simulator_set_play_type.Text = iniOS.ReadIniData("模拟器设置", "play_type", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_kuaishou_simulator_set_comment_counts.Text = iniOS.ReadIniData("模拟器设置", "comment_counts", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            tb_kuaishou_simulator_set_comment_home_counts.Text = iniOS.ReadIniData("模拟器设置", "comment_home_counts", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            if (iniOS.ReadIniData("模拟器设置", "showSimulator", "0", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini") == "0")
            {
                cbShowSimulator.Checked = false;
            }
            else
            {
                cbShowSimulator.Checked = true;
            }
            //初始化整体
            JOs.init(this, lv_kuaishou_simulator, lv_kuaishou_ads, lv_kuaishou_users, lb_kuaishou_logs, tb_simulator_number, webRoute, 1000);
            JOs.updateSimulator();
            JOs.updateAds();
            JOs.updateUsers();
        }

        /// <summary>
        /// 窗体退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            JOs.init(this, lv_kuaishou_simulator, lv_kuaishou_ads, lv_kuaishou_users, lb_kuaishou_logs, tb_simulator_number, webRoute, 1000);
            JOs.closeSource();
        }
    }
}
