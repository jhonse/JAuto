using JAuto.function;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JAuto.lib.kuaishou
{
    /// <summary>
    /// 操作类
    /// </summary>
    class JOs
    {
        private static JTimer jtimer = null;

        #region 模拟器操作

        /// <summary>
        /// 快手整体初始化
        /// </summary>
        /// <param name="form"></param>
        /// <param name="lv_simulator"></param>
        /// <param name="lv_ads"></param>
        /// <param name="lv_users"></param>
        /// <param name="lb_logs"></param>
        /// <param name="tb_num"></param>
        /// <param name="number"></param>
        public static void init(Form form, ListView lv_simulator, ListView lv_ads, ListView lv_users, ListBox lb_logs, TextBox tb_num, WebBrowser rounte, int number = 1000)
        {
            if(jtimer == null)
                jtimer = new JTimer(form, lv_simulator, lv_ads, lv_users, lb_logs, tb_num, rounte, number);
        }

        /// <summary>
        /// 更新模拟器列表
        /// </summary>
        public static void updateSimulator()
        {
            if (jtimer != null)
            {
                jtimer.updateSimulatorListColumnData();
                jtimer.updateSimulatorListItemsData();
            }
        }

        /// <summary>
        /// 更新广告列表
        /// </summary>
        public static void updateAds() {
            if (jtimer != null)
            {
                jtimer.updateAdsListColumnData();
                jtimer.updateAdsListItemsData();
            }
        }

        /// <summary>
        /// 更新用户列表
        /// </summary>
        public static void updateUsers()
        {
            if (jtimer != null)
            {
                jtimer.updateUsersListColumnData();
                jtimer.updateUsersListItemsData();
            }
        }

        /// <summary>
        /// 开启任务
        /// </summary>
        public static void startTimer() {
            if (jtimer != null)
                jtimer.closeTimer(true);
        }

        /// <summary>
        /// 关闭任务
        /// </summary>
        public static void closeTimer() {
            if (jtimer != null)
                jtimer.closeTimer(false);
        }

        /// <summary>
        /// 创建模拟器
        /// </summary>
        public static void createSimulator()
        {
            if (jtimer != null)
                jtimer.createSimulator(true);
        }

        /// <summary>
        /// 暂停创建模拟器
        /// </summary>
        public static void stopCreateSimulator() {
            if (jtimer != null)
                jtimer.createSimulator(false);
        }

        /// <summary>
        /// 关闭所有模拟器
        /// </summary>
        public static void closeAllSimulator()
        {
            if (jtimer != null)
                jtimer.closeSimulatorAll();
        }

        /// <summary>
        /// 删除所有模拟器
        /// </summary>
        public static void deleteAllSimulator()
        {
            if (jtimer != null)
                jtimer.deleteSimulatorAll();
        }

        /// <summary>
        /// 开启单个模拟器
        /// </summary>
        public static void openSimulator() {
            if (jtimer != null)
                jtimer.openSimulator();
        }

        /// <summary>
        /// 关闭单个模拟器
        /// </summary>
        public static void closeSimulator() {
            if (jtimer != null)
                jtimer.closeSimulator();
        }

        /// <summary>
        /// 删除单个模拟器
        /// </summary>
        public static void deleteSimulator() {
            if (jtimer != null)
                jtimer.deleteSimulator();
        }

        public static void closeSource()
        {
            if (jtimer != null)
            {
                jtimer.closeSource();
            }
        }

        #endregion

    }
}
