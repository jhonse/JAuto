using JAuto.function;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace JAuto.lib.kuaishou
{
    /// <summary>
    /// 定时器类
    /// </summary>
    class JTimer
    {
        System.Timers.Timer t = null;
        System.Timers.Timer install_t = null;
        System.Timers.Timer show_t = null;

        private Form form = null;
        private ListView lv_simulator = null;
        private ListView lv_ads = null;
        private ListView lv_users = null;
        private ListBox lb_logs = null;
        private TextBox tb_simulator_number = null;

        private JWindows jwindows = null;
        private JService jservice = null;
        private JAppium jappium = new JAppium();
        private JIP jip = null;
        private int index = 0;

        private Boolean install_status = true;

        private int android_service_weight_times = 0;

        private int andriod_comment_counts = 0;
        private int android_comment_home_counts = 0;
        private Boolean android_driver_connect = false;
        private int android_index = -1;
        private int android_home_loaddata = 0;
        private int android_home_viewplay = 0;
        private int android_home_login_check = 0;
        private int android_home_login_click = 0;
        private int android_play_like = 0;
        private Boolean android_play_like_status = false;
        private int android_play_follow = 0;
        private Boolean android_play_follow_status = false;
        private int android_play_showInput = 0;
        private Boolean android_play_showInput_status = false;
        private int android_play_clickInput = 0;
        private Boolean android_play_clickInput_status = false;
        private int android_play_input = 0;
        private Boolean android_play_input_status = false;
        private int android_play_input_index = 0;
        private int android_play_submit = 0;
        private Boolean android_play_submit_status = false;
        private int android_play_return = 0;
        private int android_login_index = 0;
        private int android_login_change = 0;
        private int android_login_input = 0;
        private int android_login_input_index = 0;
        private int android_login_submit = 0;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="s_form"></param>
        /// <param name="lv_simulator_s"></param>
        /// <param name="lv_ads_s"></param>
        /// <param name="lv_users_s"></param>
        /// <param name="lb_logs_s"></param>
        /// <param name="tb"></param>
        /// <param name="number"></param>
        public JTimer(Form s_form, ListView lv_simulator_s, ListView lv_ads_s, ListView lv_users_s, ListBox lb_logs_s, TextBox tb, WebBrowser route, int number = 1000)
        {
            form = s_form;
            lv_simulator = lv_simulator_s;
            lv_ads = lv_ads_s;
            lv_users = lv_users_s;
            lb_logs = lb_logs_s;
            tb_simulator_number = tb;
            jwindows = new JWindows();
            jservice = new JService();
            jip = new JIP(s_form,route);
            initTimer(number);
        }

        /// <summary>
        /// 初始化定时器
        /// </summary>
        /// <param name="number"></param>
        private void initTimer(int number=1000)
        {
            t = new System.Timers.Timer(number);
            t.AutoReset = true;
            t.Enabled = false;
            t.Elapsed += t_Elapsed;

            install_t = new System.Timers.Timer(number*5);
            install_t.AutoReset = true;
            install_t.Enabled = false;
            install_t.Elapsed += install_t_Elapsed;

            show_t = new System.Timers.Timer(number/100);
            show_t.AutoReset = true;
            show_t.Enabled = true;
            show_t.Elapsed += show_t_Elapsed;
        }

        /// <summary>
        /// 检测是否显示模拟器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void show_t_Elapsed(object sender, ElapsedEventArgs e)
        {
            jwindows.showSimulator();
            //jservice.startService();
            //jip.start();
            //this.updateLogsListItemsData(jip.msg);
        }

        /// <summary>
        /// 定时器安装执行方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void install_t_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (jwindows != null)
                {
                    int number = int.Parse(tb_simulator_number.Text);
                    if (number > 0)
                    {
                        if (install_status)
                        {
                            if (jwindows.checkSimulatorRunFinishing())
                            {
                                jwindows.closeSimulatorAll();
                            }
                            else
                            {
                                int simulator_new_index = 1;
                                if (lv_simulator.Items.Count > 0)
                                {
                                    string simulator_name = lv_simulator.Items[lv_simulator.Items.Count - 1].SubItems[0].Text;
                                    if(!simulator_name.Equals("")){
                                        string[] simulator_name_arr = simulator_name.Split('_');
                                        if (simulator_name_arr.Length == 2)
                                        {
                                            simulator_new_index = int.Parse(simulator_name_arr[1]) + 1;
                                        }
                                        else
                                        {
                                            simulator_new_index = lv_simulator.Items.Count + 1;
                                        }
                                    }
                                    else
                                    {
                                        simulator_new_index = lv_simulator.Items.Count + 1;
                                    }
                                }
                                jwindows.createSimulator(simulator_new_index);
                                install_status = false;
                            }
                        }
                        else
                        {
                            if (jwindows.checkSimulatorRunFinished()) {
                                updateSimulatorListItemsData();
                                jwindows.closeSimulatorAll();
                                tb_simulator_number.Text = (number - 1).ToString();
                                this.updateLogsListItemsData("【模拟器操作】创建模拟器" + number.ToString() + "成功!");
                                install_status = true;
                            }
                            else
                            {
                                this.updateLogsListItemsData("【模拟器操作】模拟器" + number.ToString() + "正在创建中...!");
                                install_status = false;
                            }
                        }
                    }
                    else
                    {
                        this.updateLogsListItemsData("【模拟器操作】创建模拟器失败!");
                        this.createSimulator(false);
                    }
                }
                else
                {
                    this.updateLogsListItemsData("【模拟器操作】创建模拟器失败!");
                    this.createSimulator(false);
                }
            }
            catch (Exception es)
            {
                this.updateLogsListItemsData("【模拟器操作】创建模拟器失败!" + es.Message);
                this.createSimulator(false);
            }
            
        }

        /// <summary>
        /// 定时器任务执行方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void t_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (lv_simulator != null && lv_simulator.Items.Count > 0) {
                    if (index >= lv_simulator.Items.Count)
                    {
                        updateSimulatorListItemsData();
                        index = 0;
                    }
                    else {
                        string simulator_name = lv_simulator.Items[index].SubItems[0].Text;
                        int simulator_task_type = int.Parse(lv_simulator.Items[index].SubItems[2].Text);
                        int simulator_exe_times = int.Parse(lv_simulator.Items[index].SubItems[3].Text);
                        if (!simulator_name.Equals("") && simulator_task_type == 0)
                        {
                            //开启模拟器
                            if (lv_simulator.Items.Count > 1)
                            {
                                if (!android_driver_connect)
                                {
                                    osAndroidClose();
                                }
                                else
                                {
                                    if (index == 0)
                                    {
                                        jwindows.closeSimulator(lv_simulator.Items[lv_simulator.Items.Count - 1].SubItems[0].Text);
                                        lv_simulator.Items[lv_simulator.Items.Count - 1].SubItems[1].Text = "未开启";
                                        lv_simulator.Items[lv_simulator.Items.Count - 1].SubItems[2].Text = "0";
                                        lv_simulator.Items[lv_simulator.Items.Count - 1].SubItems[3].Text = "0";
                                        lv_simulator.Items[lv_simulator.Items.Count - 1].BackColor = Color.White;
                                        lv_simulator.Items[lv_simulator.Items.Count - 1].ForeColor = Color.Black;
                                    }
                                    else
                                    {
                                        jwindows.closeSimulator(lv_simulator.Items[index - 1].SubItems[0].Text);
                                        lv_simulator.Items[index - 1].SubItems[1].Text = "未开启";
                                        lv_simulator.Items[index - 1].SubItems[2].Text = "0";
                                        lv_simulator.Items[index - 1].SubItems[3].Text = "0";
                                        lv_simulator.Items[index - 1].BackColor = Color.White;
                                        lv_simulator.Items[index - 1].ForeColor = Color.Black;
                                    }
                                    if (jwindows.checkSimulatorRunFinished())
                                    {
                                        jwindows.closeSimulatorAll();
                                    }
                                    else
                                    {
                                        if (jip.success)
                                        {
                                            this.updateLogsListItemsData("【android操作】开启模拟器: " + simulator_name);
                                            android_driver_connect = false;
                                            jwindows.openSimulator(simulator_name);
                                            lv_simulator.Items[index].SubItems[1].Text = "开启";
                                            lv_simulator.Items[index].SubItems[2].Text = "1";
                                            lv_simulator.Items[index].BackColor = Color.Blue;
                                            lv_simulator.Items[index].ForeColor = Color.White;
                                            jip.success = false;
                                        }
                                        else
                                        {
                                            jip.start();
                                            this.updateLogsListItemsData(jip.msg);
                                        }
                                    }
                                }
                            }
                        }
                        else {
                            //执行任务
                            lv_simulator.Items[index].SubItems[3].Text = (simulator_exe_times + 1).ToString();
                            osAndroid();
                        }
                    }
                }
                else
                {
                    updateSimulatorListItemsData();
                }
            }
            catch (Exception es)
            {
                this.updateLogsListItemsData("【android操作】异常信息: " + es.Message);
                closeTimer(false);
            }
        }

        /// <summary>
        /// 定时器任务 - 操作Android
        /// </summary>
        private void osAndroid() {
            if (android_index == -1)
            {
                //模拟器加载
                osAndroidLoad();
            }
            else if (android_index == 0)
            {
                //连接Appium
                osAndroidLinkAppium();
            }
            else if (android_index == 1) {
                //首页 - 下拉加载数据
                osAndroidHomeLoadData();
            }
            else if (android_index == 2) 
            { 
                //首页 - 检查登录 
                osAndroidHomeCheckLogin();
            }
            else if (android_index == 3) 
            {
                //首页 - 点击登录 
                osAndroidHomeClickLogin();
            }
            else if (android_index == 4)
            {
                //首页 - 查看内页
                osAndroidHomeViewPlay();
            }
            else if (android_index == 5)
            {
                //内页
                osAndroidPlay();
            }
            else if (android_index == 6)
            {
                //登录
                osAndroidLogin();
            }
            else if (android_index == 7)
            {
                //返回
                osAndroidReturnHome();
            }
            else if (android_index == 8)
            {
                //切换模拟器
                osAndroidNext();
            }
        }

        /// <summary>
        /// 定时器任务 - 模拟器加载 <-1>
        /// </summary>
        private void osAndroidLoad() {
            if (jwindows.checkSimulatorRunFinished())
            {
                android_index = 0;
                this.updateLogsListItemsData("【Android操作】模拟器加载成功!连接Appium!");
            }
            else
            {
                if (jwindows.checkSimulatorRunFinishing())
                {
                    this.updateLogsListItemsData("【Android操作】模拟器正在加载中...");
                }
                else
                {
                    lv_simulator.Items[index].SubItems[2].Text = "0";
                }
            }
        }

        /// <summary>
        /// 定时器任务 - 连接Appium <0>
        /// </summary>
        private void osAndroidLinkAppium()
        {
            if (jappium.isChanges)
            {
                if (!jappium.openStatus)
                {
                    jappium.initDriver();
                    this.updateLogsListItemsData("【Android操作】 连接失败!" + jappium.Msg);
                    if (android_service_weight_times >= 10)
                    {
                        //jservice.killService();
                        android_service_weight_times = 0;
                    }
                    android_service_weight_times++;
                }
                else
                {
                    android_index = 1;
                    this.updateLogsListItemsData("【Android操作】android以及app已准备好!");
                }
            }
            else
            {
                this.updateLogsListItemsData("【Android操作】 连接中...");
            }
        }

        /// <summary>
        /// 定时器任务 - 首页 - 下拉加载数据 <1>
        /// </summary>
        private void osAndroidHomeLoadData() {
            //判断是否需要下拉加载
            if (andriod_comment_counts >= int.Parse(iniOS.ReadIniData("模拟器设置", "comment_counts", "0", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini")))
            {
                //超过总评论数
                android_index = 8;
                this.updateLogsListItemsData("【Android操作】超过总评论次数!");
                return;
            }
            else if (android_comment_home_counts < int.Parse(iniOS.ReadIniData("模拟器设置", "comment_home_counts", "0", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini")))
            {
                android_index = 2;
                this.updateLogsListItemsData("【Android操作】首页评论次数: " + android_comment_home_counts.ToString() + "少于首页评论次数" + iniOS.ReadIniData("模拟器设置", "comment_home_counts", "0", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini") + "!");
                return;
            }
            if (jappium.isChanges)
            {
                if (android_home_loaddata >= int.Parse(iniOS.ReadIniData("模拟器设置", "loaddata", "0", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini")))
                {
                    if (jappium != null)
                    {
                        if (jappium.HomeLoadData())
                        {
                            android_index = 2;
                            android_home_loaddata = 0;
                            android_comment_home_counts = 0;
                            this.updateLogsListItemsData("【Android操作】下拉加载数据成功!");
                        }
                        else
                        {
                            android_index = 8;
                            android_home_loaddata = 0;
                            this.updateLogsListItemsData("【Android操作】下拉加载数据失败!" + jappium.Msg);
                        }
                    }
                    else
                    {
                        android_index = 8;
                        android_home_loaddata = 0;
                        this.updateLogsListItemsData("【Android操作】下拉加载数据失败!" + jappium.Msg);
                    }
                }
                else
                {
                    this.updateLogsListItemsData("【Android操作】首页等待下拉加载数据秒数: " + android_home_loaddata.ToString());
                    android_home_loaddata++;
                }
            }
            else
            {
                this.updateLogsListItemsData("【Android操作】首页下拉加载数据中... ");
            }
        }

        /// <summary>
        /// 定时器任务 - 首页 - 检测登录 <2>
        /// </summary>
        private void osAndroidHomeCheckLogin()
        {
            if (jappium.isChanges)
            {
                if (android_home_login_check >= int.Parse(iniOS.ReadIniData("模拟器设置", "checklogin", "0", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini")))
                {
                    if (jappium != null)
                    {
                        if (jappium.HomeCheckLogin())
                        {
                            android_index = 3;
                            android_home_login_check = 0;
                            this.updateLogsListItemsData("【Android操作】首页登录检测成功!");
                        }
                        else
                        {
                            android_index = 4;
                            android_home_login_check = 0;
                            this.updateLogsListItemsData("【Android操作】首页登录检测失败!" + jappium.Msg);
                        }
                    }
                    else
                    {
                        android_index = 1;
                        android_home_login_check = 0;
                        this.updateLogsListItemsData("【Android操作】首页登录检测失败!" + jappium.Msg);
                    }
                }
                else
                {
                    this.updateLogsListItemsData("【Android操作】首页等待登录检测秒数: " + android_home_login_check.ToString());
                    android_home_login_check++;
                }
            }
            else
            {
                this.updateLogsListItemsData("【Android操作】首页登录检测中... ");
            }
        }

        /// <summary>
        /// 定时器任务 - 首页 - 点击登录 <3>
        /// </summary>
        private void osAndroidHomeClickLogin()
        {
            if (jappium.isChanges)
            {
                if (android_home_login_click >= int.Parse(iniOS.ReadIniData("模拟器设置", "clicklogin", "0", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini")))
                {
                    if (jappium != null)
                    {
                        if (jappium.HomeLogin())
                        {
                            android_index = 6;
                            android_home_login_click = 0;
                            this.updateLogsListItemsData("【Android操作】首页登录点击成功!");
                        }
                        else
                        {
                            android_index = 1;
                            android_home_login_click = 0;
                            this.updateLogsListItemsData("【Android操作】首页登录点击失败!" + jappium.Msg);
                        }
                    }
                    else
                    {
                        android_index = 1;
                        android_home_login_click = 0;
                        this.updateLogsListItemsData("【Android操作】首页登录点击失败!" + jappium.Msg);
                    }
                }
                else
                {
                    this.updateLogsListItemsData("【Android操作】首页等待登录点击秒数: " + android_home_login_click.ToString());
                    android_home_login_click++;
                }
            }
            else
            {
                this.updateLogsListItemsData("【Android操作】首页登录点击中... ");
            }
        }

        /// <summary>
        /// 定时器任务 - 首页 - 查看内页 <4>
        /// </summary>
        private void osAndroidHomeViewPlay()
        {
            if (jappium.isChanges)
            {
                if (android_home_viewplay >= int.Parse(iniOS.ReadIniData("模拟器设置", "viewplay", "0", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini")))
                {
                    if (jappium != null)
                    {
                        if (jappium.HomeClickPhotePlay(android_comment_home_counts))
                        {
                            android_index = 5;
                            android_home_viewplay = 0;
                            android_comment_home_counts++;
                            this.updateLogsListItemsData("【Android操作】首页查看内页" + (android_comment_home_counts + 1).ToString() + "成功!");
                        }
                        else
                        {
                            android_index = 1;
                            android_home_viewplay = 0;
                            this.updateLogsListItemsData("【Android操作】首页查看内页失败!" + jappium.Msg);
                        }
                    }
                    else
                    {
                        android_index = 1;
                        android_home_viewplay = 0;
                        this.updateLogsListItemsData("【Android操作】首页查看内页失败!" + jappium.Msg);
                    }
                }
                else
                {
                    this.updateLogsListItemsData("【Android操作】首页停留秒数: " + android_home_viewplay.ToString());
                    android_home_viewplay++;
                }
            }
            else
            {
                this.updateLogsListItemsData("【Android操作】首页查看内页中... ");
            }
        }

        /// <summary>
        /// 定时器任务 - 内页 <5>
        /// </summary>
        private void osAndroidPlay()
        {
            string playCommentType = iniOS.ReadIniData("模拟器设置", "play_type", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
            if (playCommentType.Equals("先喜欢再关注再评论"))
            {
                osAndroidPlayLikeFollowComment();
            }
            else if (playCommentType.Equals("先关注再喜欢再评论"))
            {
                osAndroidPlayFollowLikeComment();
            }
            else if (playCommentType.Equals("先评论再喜欢再关注"))
            {
                osAndroidPlayCommentLikeFollow();
            }
            else if (playCommentType.Equals("先评论再关注再喜欢"))
            {
                osAndroidPlayCommentFollowLike();
            }
            else if (playCommentType.Equals("先喜欢再评论再关注"))
            {
                osAndroidPlayLikeCommentFollow();
            }
            else if (playCommentType.Equals("先关注再评论再喜欢"))
            {
                osAndroidPlayFollowCommentLike();
            }
            else if (playCommentType.Equals("先喜欢再评论"))
            {
                osAndroidPlayLikeComment();
            }
            else if (playCommentType.Equals("先关注再评论"))
            {
                osAndroidPlayFollowComment();
            }
            else if (playCommentType.Equals("评论") || playCommentType.Equals(""))
            {
                osAndroidPlayComment();
            }
        }

        /// <summary>
        /// 定时器任务 - 内页 - 先喜欢再关注再评论
        /// </summary>
        private void osAndroidPlayLikeFollowComment()
        {
            if (!android_play_like_status)
            {
                osAndroidPlayOsLike();
                return;
            }
            if (!android_play_follow_status)
            {
                osAndroidPlayOsFollow();
                return;
            }
            if (!android_play_showInput_status)
            {
                osAndroidPlayOsShowComment();
                return;
            }
            if (!android_play_clickInput_status)
            {
                osAndroidPlayOsClickComment();
                return;
            }
            if (!android_play_input_status)
            {
                osAndroidPlayOsComment();
                return;
            }
            if (!android_play_submit_status)
            {
                osAndroidPlayOsSubmit();
                return;
            }
            osAndroidReturnHome();
        }

        /// <summary>
        /// 定时器任务 - 内页 - 先关注再喜欢再评论
        /// </summary>
        private void osAndroidPlayFollowLikeComment()
        {
            if (!android_play_follow_status)
            {
                osAndroidPlayOsFollow();
                return;
            }
            if (!android_play_like_status)
            {
                osAndroidPlayOsLike();
                return;
            }
            if (!android_play_showInput_status)
            {
                osAndroidPlayOsShowComment();
                return;
            }
            if (!android_play_clickInput_status)
            {
                osAndroidPlayOsClickComment();
                return;
            }
            if (!android_play_input_status)
            {
                osAndroidPlayOsComment();
                return;
            }
            if (!android_play_submit_status)
            {
                osAndroidPlayOsSubmit();
                return;
            }
            osAndroidReturnHome();
        }

        /// <summary>
        /// 定时器任务 - 内页 - 先评论再喜欢再关注
        /// </summary>
        private void osAndroidPlayCommentLikeFollow()
        {
            if (!android_play_showInput_status)
            {
                osAndroidPlayOsShowComment();
                return;
            }
            if (!android_play_clickInput_status)
            {
                osAndroidPlayOsClickComment();
                return;
            }
            if (!android_play_input_status)
            {
                osAndroidPlayOsComment();
                return;
            }
            if (!android_play_submit_status)
            {
                osAndroidPlayOsSubmit();
                return;
            }
            if (!android_play_like_status)
            {
                osAndroidPlayOsLike();
                return;
            }
            if (!android_play_follow_status)
            {
                osAndroidPlayOsFollow();
                return;
            }
            osAndroidReturnHome();
        }

        /// <summary>
        /// 定时器任务 - 内页 - 先评论再关注再喜欢
        /// </summary>
        private void osAndroidPlayCommentFollowLike()
        {
            if (!android_play_showInput_status)
            {
                osAndroidPlayOsShowComment();
                return;
            }
            if (!android_play_clickInput_status)
            {
                osAndroidPlayOsClickComment();
                return;
            }
            if (!android_play_input_status)
            {
                osAndroidPlayOsComment();
                return;
            }
            if (!android_play_submit_status)
            {
                osAndroidPlayOsSubmit();
                return;
            }
            if (!android_play_follow_status)
            {
                osAndroidPlayOsFollow();
                return;
            }
            if (!android_play_like_status)
            {
                osAndroidPlayOsLike();
                return;
            }
            osAndroidReturnHome();
        }

        /// <summary>
        /// 定时器任务 - 内页 - 先喜欢再评论再关注
        /// </summary>
        private void osAndroidPlayLikeCommentFollow()
        {
            if (!android_play_like_status)
            {
                osAndroidPlayOsLike();
                return;
            }
            if (!android_play_showInput_status)
            {
                osAndroidPlayOsShowComment();
                return;
            }
            if (!android_play_clickInput_status)
            {
                osAndroidPlayOsClickComment();
                return;
            }
            if (!android_play_input_status)
            {
                osAndroidPlayOsComment();
                return;
            }
            if (!android_play_submit_status)
            {
                osAndroidPlayOsSubmit();
                return;
            }
            if (!android_play_follow_status)
            {
                osAndroidPlayOsFollow();
                return;
            }
            osAndroidReturnHome();
        }

        /// <summary>
        /// 定时器任务 - 内页 - 先关注再评论再喜欢
        /// </summary>
        private void osAndroidPlayFollowCommentLike()
        {
            if (!android_play_follow_status)
            {
                osAndroidPlayOsFollow();
                return;
            }
            if (!android_play_showInput_status)
            {
                osAndroidPlayOsShowComment();
                return;
            }
            if (!android_play_clickInput_status)
            {
                osAndroidPlayOsClickComment();
                return;
            }
            if (!android_play_input_status)
            {
                osAndroidPlayOsComment();
                return;
            }
            if (!android_play_submit_status)
            {
                osAndroidPlayOsSubmit();
                return;
            }
            if (!android_play_like_status)
            {
                osAndroidPlayOsLike();
                return;
            }
            osAndroidReturnHome();
        }

        /// <summary>
        /// 定时器任务 - 内页 - 先喜欢再评论
        /// </summary>
        private void osAndroidPlayLikeComment()
        {
            if (!android_play_like_status)
            {
                osAndroidPlayOsLike();
                return;
            }
            if (!android_play_showInput_status)
            {
                osAndroidPlayOsShowComment();
                return;
            }
            if (!android_play_clickInput_status)
            {
                osAndroidPlayOsClickComment();
                return;
            }
            if (!android_play_input_status)
            {
                osAndroidPlayOsComment();
                return;
            }
            if (!android_play_submit_status)
            {
                osAndroidPlayOsSubmit();
                return;
            }
            osAndroidReturnHome();
        }

        /// <summary>
        /// 定时器任务 - 内页 - 先关注再评论
        /// </summary>
        private void osAndroidPlayFollowComment()
        {
            if (!android_play_follow_status)
            {
                osAndroidPlayOsFollow();
                return;
            }
            if (!android_play_showInput_status)
            {
                osAndroidPlayOsShowComment();
                return;
            }
            if (!android_play_clickInput_status)
            {
                osAndroidPlayOsClickComment();
                return;
            }
            if (!android_play_input_status)
            {
                osAndroidPlayOsComment();
                return;
            }
            if (!android_play_submit_status)
            {
                osAndroidPlayOsSubmit();
                return;
            }
            osAndroidReturnHome();
        }

        /// <summary>
        /// 定时器任务 - 内页 - 评论
        /// </summary>
        private void osAndroidPlayComment()
        {
            if (!android_play_showInput_status)
            {
                osAndroidPlayOsShowComment();
                return;
            }
            if (!android_play_clickInput_status)
            {
                osAndroidPlayOsClickComment();
                return;
            }
            if (!android_play_input_status)
            {
                osAndroidPlayOsComment();
                return;
            }
            if (!android_play_submit_status)
            {
                osAndroidPlayOsSubmit();
                return;
            }
            osAndroidReturnHome();
        }

        /// <summary>
        /// 定时器任务 - 内页 - 操作 - 喜欢
        /// </summary>
        /// <returns></returns>
        private void osAndroidPlayOsLike()
        {
            if (jappium.isChanges)
            {
                if (android_play_like >= int.Parse(iniOS.ReadIniData("模拟器设置", "play_like", "0", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini")))
                {
                    if (jappium != null)
                    {
                        if (jappium.PlayClickLike())
                        {
                            android_index = 5;
                            android_play_like = 0;
                            android_play_like_status = true;
                            this.updateLogsListItemsData("【Android操作】内页喜欢成功!");
                        }
                        else
                        {
                            android_index = 7;
                            android_play_like = 0;
                            android_play_like_status = false;
                            this.updateLogsListItemsData("【Android操作】内页喜欢失败!" + jappium.Msg);
                        }
                    }
                    else
                    {
                        android_index = 7;
                        android_play_like = 0;
                        android_play_like_status = false;
                        this.updateLogsListItemsData("【Android操作】内页喜欢失败!" + jappium.Msg);
                    }
                }
                else
                {
                    android_play_like_status = false;
                    this.updateLogsListItemsData("【Android操作】内页喜欢等待秒数: " + android_play_like.ToString());
                    android_play_like++;
                }
            }
            else
            {
                android_play_like_status = false;
                this.updateLogsListItemsData("【Android操作】内页喜欢中... ");
            }
        }

        /// <summary>
        /// 定时器任务 - 内页 - 操作 - 关注
        /// </summary>
        /// <returns></returns>
        private void osAndroidPlayOsFollow()
        {
            if (jappium.isChanges)
            {
                if (android_play_follow >= int.Parse(iniOS.ReadIniData("模拟器设置", "play_follow", "0", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini")))
                {
                    if (jappium != null)
                    {
                        if (jappium.PlayClickFollow())
                        {
                            android_index = 5;
                            android_play_follow = 0;
                            android_play_follow_status = true;
                            this.updateLogsListItemsData("【Android操作】内页关注成功!");
                        }
                        else
                        {
                            android_index = 7;
                            android_play_follow = 0;
                            android_play_follow_status = false;
                            this.updateLogsListItemsData("【Android操作】内页关注失败!" + jappium.Msg);
                        }
                    }
                    else
                    {
                        android_index = 7;
                        android_play_follow = 0;
                        android_play_follow_status = false;
                        this.updateLogsListItemsData("【Android操作】内页关注失败!" + jappium.Msg);
                    }
                }
                else
                {
                    android_play_follow_status = false;
                    this.updateLogsListItemsData("【Android操作】内页关注等待秒数: " + android_play_follow.ToString());
                    android_play_follow++;
                }
            }
            else
            {
                android_play_follow_status = false;
                this.updateLogsListItemsData("【Android操作】内页关注中... ");
            }
        }

        /// <summary>
        /// 定时器任务 - 内页 - 操作 - 显示评论框
        /// </summary>
        private void osAndroidPlayOsShowComment()
        {
            if (jappium.isChanges)
            {
                if (android_play_showInput >= int.Parse(iniOS.ReadIniData("模拟器设置", "play_showInput", "0", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini")))
                {
                    if (jappium != null)
                    {
                        if (jappium.PlayShowCommentInput())
                        {
                            android_index = 5;
                            android_play_showInput = 0;
                            android_play_showInput_status = true;
                            this.updateLogsListItemsData("【Android操作】显示评论框成功!");
                        }
                        else
                        {
                            android_index = 7;
                            android_play_showInput = 0;
                            android_play_showInput_status = false;
                            this.updateLogsListItemsData("【Android操作】显示评论框失败!" + jappium.Msg);
                        }
                    }
                    else
                    {
                        android_index = 7;
                        android_play_showInput = 0;
                        android_play_showInput_status = false;
                        this.updateLogsListItemsData("【Android操作】显示评论框失败!" + jappium.Msg);
                    }
                }
                else
                {
                    this.updateLogsListItemsData("【Android操作】显示评论框等待秒数: " + android_play_showInput.ToString());
                    android_play_showInput++;
                    android_play_showInput_status = false;
                }
            }
            else
            {
                this.updateLogsListItemsData("【Android操作】显示评论框中... ");
                android_play_showInput_status = false;
            }
        }

        /// <summary>
        /// 定时器任务 - 内页 - 操作 - 点击评论框
        /// </summary>
        private void osAndroidPlayOsClickComment()
        {
            if (jappium.isChanges)
            {
                if (android_play_clickInput >= int.Parse(iniOS.ReadIniData("模拟器设置", "play_clickInput", "0", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini")))
                {
                    if (jappium != null)
                    {
                        if (jappium.PlayClickCommentInput())
                        {
                            android_index = 5;
                            android_play_clickInput = 0;
                            android_play_clickInput_status = true;
                            this.updateLogsListItemsData("【Android操作】点击评论框成功!");
                        }
                        else
                        {
                            android_index = 7;
                            android_play_clickInput = 0;
                            android_play_clickInput_status = false;
                            this.updateLogsListItemsData("【Android操作】点击评论框失败!" + jappium.Msg);
                        }
                    }
                    else
                    {
                        android_index = 7;
                        android_play_clickInput = 0;
                        android_play_clickInput_status = false;
                        this.updateLogsListItemsData("【Android操作】点击评论框失败!" + jappium.Msg);
                    }
                }
                else
                {
                    this.updateLogsListItemsData("【Android操作】点击评论框等待秒数: " + android_play_clickInput.ToString());
                    android_play_clickInput++;
                    android_play_clickInput_status = false;
                }
            }
            else
            {
                this.updateLogsListItemsData("【Android操作】点击评论框中... ");
                android_play_clickInput_status = false;
            }
        }

        /// <summary>
        /// 定时器任务 - 内页 - 操作 - 评论
        /// </summary>
        private void osAndroidPlayOsComment()
        {
            if (jappium.isChanges)
            {
                if (android_play_input >= int.Parse(iniOS.ReadIniData("模拟器设置", "play_comment_input", "0", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini")))
                {
                    if (jappium != null)
                    {
                        string ads_sort = iniOS.ReadIniData("广告设置", "sort", "正常调用", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
                        string ad = "";
                        if (ads_sort.Equals("正常调用"))
                        {
                            if (lv_ads.Items.Count > 0)
                            {
                                if (android_play_input_index >= lv_ads.Items.Count)
                                {
                                    android_play_input_index = 0;
                                }
                                ad = lv_ads.Items[android_play_input_index].SubItems[0].Text;
                                lv_ads.Items[android_play_input_index].SubItems[1].Text = (int.Parse(lv_ads.Items[android_play_input_index].SubItems[1].Text) + 1).ToString();
                                android_play_input_index++;
                            }
                            else
                            {
                                this.updateLogsListItemsData("【Android操作】没有广告信息!");
                                return;
                            }
                        }
                        else if (ads_sort.Equals("随机调用"))
                        {
                            int ads_count = lv_ads.Items.Count;
                            if (ads_count > 0)
                            {
                                Random rd = new Random();
                                int ads_index = rd.Next(0, ads_count - 1);
                                ad = lv_ads.Items[ads_index].SubItems[0].Text;
                                lv_ads.Items[ads_index].SubItems[1].Text = (int.Parse(lv_ads.Items[ads_index].SubItems[1].Text) + 1).ToString();
                            }
                            else
                            {
                                android_play_input_status = false;
                                this.updateLogsListItemsData("【Android操作】没有广告信息!");
                                return;
                            }
                        }
                        if (jappium.PlayWriteCommentInput(ad))
                        {
                            android_index = 5;
                            android_play_input = 0;
                            android_play_input_status = true;
                            this.updateLogsListItemsData("【Android操作】输入评论信息成功!");
                        }
                        else
                        {
                            android_index = 7;
                            android_play_input = 0;
                            android_play_input_status = false;
                            this.updateLogsListItemsData("【Android操作】输入评论信息失败!" + jappium.Msg);
                        }
                    }
                    else
                    {
                        android_index = 7;
                        android_play_input = 0;
                        android_play_input_status = false;
                        this.updateLogsListItemsData("【Android操作】输入评论信息失败!" + jappium.Msg);
                    }
                }
                else
                {
                    this.updateLogsListItemsData("【Android操作】输入评论信息等待秒数: " + android_play_input.ToString());
                    android_play_input++;
                    android_play_input_status = false;
                }
            }
            else
            {
                android_play_input_status = false;
                this.updateLogsListItemsData("【Android操作】输入评论信息中... ");
            }
        }

        /// <summary>
        /// 定时器任务 - 内页 - 操作 - 提交
        /// </summary>
        private void osAndroidPlayOsSubmit()
        {
            if (jappium.isChanges)
            {
                if (android_play_submit >= int.Parse(iniOS.ReadIniData("模拟器设置", "play_comment_submit", "0", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini")))
                {
                    if (jappium != null)
                    {
                        if (jappium.PlayCommentSubmit())
                        {
                            android_index = 5;
                            android_play_submit_status = true;
                            android_play_submit = 0;
                            andriod_comment_counts++;
                            this.updateLogsListItemsData("【Android操作】评论提交成功!");
                        }
                        else
                        {
                            android_index = 7;
                            android_play_submit = 0;
                            android_play_submit_status = false;
                            this.updateLogsListItemsData("【Android操作】评论提交失败!" + jappium.Msg);
                        }
                    }
                    else
                    {
                        android_index = 7;
                        android_play_submit = 0;
                        android_play_submit_status = false;
                        this.updateLogsListItemsData("【Android操作】评论提交失败!" + jappium.Msg);
                    }
                }
                else
                {
                    this.updateLogsListItemsData("【Android操作】评论提交等待秒数: " + android_play_submit.ToString());
                    android_play_submit++;
                    android_play_submit_status = false;
                }
            }
            else
            {
                this.updateLogsListItemsData("【Android操作】评论提交中... ");
                android_play_submit_status = false;
            }
        }

        /// <summary>
        /// 定时任务 - 登录 <6>
        /// </summary>
        private void osAndroidLogin() {
            if (android_login_index == 0)
            {
                osAndroidLoginChange();
            }
            else if (android_login_index == 1)
            {
                osAndroidLoginInput();
            }
            else if (android_login_index == 2)
            {
                osAndroidLoginSubmit();
            }
        }

        /// <summary>
        /// 定时任务 - 登录 - 切换 <6> <0>
        /// </summary>
        private void osAndroidLoginChange()
        {
            if (jappium.isChanges)
            {
                if (android_login_change >= int.Parse(iniOS.ReadIniData("模拟器设置", "login_change", "0", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini")))
                {
                    if (jappium != null)
                    {
                        if (jappium.LoginOnClickLink())
                        {
                            android_index = 6;
                            android_login_index = 1;
                            android_login_change = 0;
                            this.updateLogsListItemsData("【Android操作】登录页切换成功!");
                        }
                        else
                        {
                            android_index = 7;
                            android_login_index = 0;
                            android_login_change = 0;
                            this.updateLogsListItemsData("【Android操作】登录页切换失败!" + jappium.Msg);
                        }
                    }
                    else
                    {
                        android_index = 7;
                        android_login_index = 0;
                        android_login_change = 0;
                        this.updateLogsListItemsData("【Android操作】登录页切换失败!" + jappium.Msg);
                    }
                }
                else
                {
                    this.updateLogsListItemsData("【Android操作】登录页切换等待秒数: " + android_login_change.ToString());
                    android_login_change++;
                }
            }
            else
            {
                this.updateLogsListItemsData("【Android操作】登录页切换中... ");
            }
        }

        /// <summary>
        /// 定时任务 - 登录 - 输入信息 <6> <1>
        /// </summary>
        private void osAndroidLoginInput() {
            if (jappium.isChanges)
            {
                if (android_login_input >= int.Parse(iniOS.ReadIniData("模拟器设置", "login_input", "0", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini")))
                {
                    if (jappium != null)
                    {
                        string users_sort = iniOS.ReadIniData("用户设置", "sort", "正常调用", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
                        string username = "";
                        string password = "";
                        if (users_sort.Equals("正常调用"))
                        {
                            if (lv_users.Items.Count > 0)
                            {
                                if (android_login_input_index >= lv_users.Items.Count)
                                {
                                    android_login_input_index = 0;
                                }
                                username = lv_users.Items[android_login_input_index].SubItems[0].Text;
                                password = lv_users.Items[android_login_input_index].SubItems[1].Text;
                                lv_users.Items[android_login_input_index].SubItems[2].Text = (int.Parse(lv_users.Items[android_login_input_index].SubItems[2].Text) + 1).ToString();
                                android_login_input_index++;
                            }
                            else
                            {
                                this.updateLogsListItemsData("【Android操作】没有账号信息!");
                                return;
                            }
                        }
                        else if (users_sort.Equals("随机调用"))
                        {
                            int users_count = lv_users.Items.Count;
                            if (users_count > 0)
                            {
                                Random rd = new Random();
                                int users_index = rd.Next(0, users_count - 1);
                                username = lv_users.Items[users_index].SubItems[0].Text;
                                password = lv_users.Items[users_index].SubItems[1].Text;
                                lv_users.Items[users_index].SubItems[2].Text = (int.Parse(lv_users.Items[users_index].SubItems[2].Text) + 1).ToString();
                            }
                            else
                            {
                                this.updateLogsListItemsData("【Android操作】没有账号信息!");
                                return;
                            }
                        }
                        if (jappium.LoginOnPutInput(username, password))
                        {
                            android_index = 6;
                            android_login_index = 2;
                            android_login_input = 0;
                            this.updateLogsListItemsData("【Android操作】登录页输入信息成功!");
                        }
                        else
                        {
                            android_index = 7;
                            android_login_index = 0;
                            android_login_input = 0;
                            this.updateLogsListItemsData("【Android操作】登录页输入信息失败!" + jappium.Msg);
                        }
                    }
                    else
                    {
                        android_index = 7;
                        android_login_index = 0;
                        android_login_input = 0;
                        this.updateLogsListItemsData("【Android操作】登录页输入信息失败!" + jappium.Msg);
                    }
                }
                else
                {
                    this.updateLogsListItemsData("【Android操作】登录页输入信息等待秒数: " + android_login_input.ToString());
                    android_login_input++;
                }
            }
            else
            {
                this.updateLogsListItemsData("【Android操作】登录页输入信息中... ");
            }
        }

        /// <summary>
        /// 定时任务 - 登录 - 提交 <6> <2>
        /// </summary>
        private void osAndroidLoginSubmit()
        {
            if (jappium.isChanges)
            {
                if (android_login_submit >= int.Parse(iniOS.ReadIniData("模拟器设置", "login_submit", "0", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini")))
                {
                    if (jappium != null)
                    {
                        if (jappium.LoginOnSubmit())
                        {
                            android_index = 1;
                            android_login_index = 0;
                            android_login_submit = 0;
                            this.updateLogsListItemsData("【Android操作】登录页提交成功!");
                        }
                        else
                        {
                            android_index = 7;
                            android_login_index = 0;
                            android_login_submit = 0;
                            this.updateLogsListItemsData("【Android操作】登录页提交失败!" + jappium.Msg);
                        }
                    }
                    else
                    {
                        android_index = 7;
                        android_login_index = 0;
                        android_login_submit = 0;
                        this.updateLogsListItemsData("【Android操作】登录页提交失败!" + jappium.Msg);
                    }
                }
                else
                {
                    this.updateLogsListItemsData("【Android操作】登录页切换提交秒数: " + android_login_submit.ToString());
                    android_login_submit++;
                }
            }
            else
            {
                this.updateLogsListItemsData("【Android操作】登录页提交中... ");
            }
        }

        /// <summary>
        /// 定时任务 - 返回首页 <7>
        /// </summary>
        private void osAndroidReturnHome()
        {
            if (jappium.isChanges)
            {
                if (android_play_return >= int.Parse(iniOS.ReadIniData("模拟器设置", "return", "0", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini")))
                {
                    if (jappium != null)
                    {
                        if (jappium.PlayReturn())
                        {
                            android_index = 1;
                            android_play_return = 0;
                            android_play_like = 0;
                            android_play_like_status = false;
                            android_play_follow = 0;
                            android_play_follow_status = false;
                            android_play_input = 0;
                            android_play_input_status = false;
                            android_play_showInput = 0;
                            android_play_showInput_status = false;
                            android_play_clickInput = 0;
                            android_play_clickInput_status = false;
                            android_play_submit = 0;
                            android_play_submit_status = false;
                            this.updateLogsListItemsData("【Android操作】返回首页成功!");
                        }
                        else
                        {
                            android_index = 8;
                            android_play_return = 0;
                            this.updateLogsListItemsData("【Android操作】返回首页失败!" + jappium.Msg);
                        }
                    }
                    else
                    {
                        android_index = 8;
                        android_play_return = 0;
                        this.updateLogsListItemsData("【Android操作】返回首页失败!" + jappium.Msg);
                    }
                }
                else
                {
                    this.updateLogsListItemsData("【Android操作】返回首页秒数: " + android_play_return.ToString());
                    android_play_return++;
                }
            }
            else
            {
                this.updateLogsListItemsData("【Android操作】返回首页中... ");
            }
        }

        /// <summary>
        /// 定时任务 - 切换下一个模拟器
        /// </summary>
        private void osAndroidNext()
        {
            andriod_comment_counts = 0;
            android_comment_home_counts = 0;
            android_index = -1;
            android_home_loaddata = 0;
            android_home_viewplay = 0;
            android_home_login_check = 0;
            android_home_login_click = 0;
            android_play_return = 0;
            android_play_like = 0;
            android_play_like_status = false;
            android_play_follow = 0;
            android_play_follow_status = false;
            android_play_input = 0;
            android_play_input_status = false;
            android_play_showInput = 0;
            android_play_showInput_status = false;
            android_play_clickInput = 0;
            android_play_clickInput_status = false;
            android_play_submit = 0;
            android_play_submit_status = false;
            index++;
        }

        /// <summary>
        /// 定时任务 - 关闭链接
        /// </summary>
        private void osAndroidClose()
        {
            if (jappium.isChanges)
            {
                if (jappium != null)
                {
                    if (jappium.removeDriver())
                    {
                        android_driver_connect = true;
                    }
                    else
                    {
                        android_driver_connect = false;
                    }
                }
                else
                {
                    android_driver_connect = false;
                }
            }
            else
            {
                android_driver_connect = false;
                this.updateLogsListItemsData("【Android操作】正在关闭连接... ");
            }
        }

        /// <summary>
        /// 更新模拟器列表items数据
        /// </summary>
        public void updateSimulatorListItemsData(){
            if (lv_simulator != null)
            {
                this.lv_simulator.Items.Clear();
                lv_simulator.BeginUpdate();
                string[] dirs = jwindows.getSimulatorAll();
                try
                {
                    List<string> list_dirs = dirs.ToList();
                    if (dirs != null)
                    {
                        for (int i = 0; i < list_dirs.Count; i++)
                        {
                            for (int j = i + 1; j < list_dirs.Count; j++)
                            {
                                string[] simulator_x_name_arr = Path.GetFileName(list_dirs[i]).Split('_');
                                string[] simulator_y_name_arr = Path.GetFileName(list_dirs[j]).Split('_');
                                if (simulator_x_name_arr.Length == 2 && simulator_y_name_arr.Length == 2)
                                {
                                    try
                                    {
                                        int x_vaule = int.Parse(simulator_x_name_arr[1]);
                                        int y_value = int.Parse(simulator_y_name_arr[1]);
                                        if (x_vaule > y_value)
                                        {
                                            string temp = "";
                                            temp = list_dirs[i];
                                            list_dirs[i] = list_dirs[j];
                                            list_dirs[j] = temp;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        jwindows.deleteSimulator(Path.GetFileName(dirs[j]));
                                        list_dirs.RemoveAt(j);
                                        break;
                                    }
                                }
                            }
                        }
                        dirs = list_dirs.ToArray();
                        foreach (string dir in dirs)
                        {
                            ListViewItem lvi = new ListViewItem();
                            lvi.Text = Path.GetFileName(dir);
                            string simulatorInstallPath = iniOS.ReadIniData(jwindows.config_set_name, "simulator_install_path", "", jwindows.config_path);
                            if (simulatorInstallPath.Contains("Nox"))
                            {
                                lvi.ImageIndex = 0;
                            }
                            else if (simulatorInstallPath.Contains("Microvirt"))
                            {
                                lvi.ImageIndex = 1;
                            }
                            lvi.SubItems.Add("未开启");
                            lvi.SubItems.Add("0");
                            lvi.SubItems.Add("0");
                            lvi.SubItems.Add("0");
                            this.lv_simulator.Items.Add(lvi);
                        }
                        this.updateLogsListItemsData("更新模拟器列表数据成功!");
                    }
                    else
                    {
                        this.updateLogsListItemsData("【模拟器列表】模拟器路径有问题！");
                    }
                    lv_simulator.EndUpdate();
                }
                catch (Exception)
                {

                }
            }
        }

        /// <summary>
        /// 更新模拟器列表columns数据
        /// </summary>
        public void updateSimulatorListColumnData()
        {
            if (lv_simulator != null) {
                lv_simulator.Columns.Clear();
                lv_simulator.Columns.Add("模拟器名称", 134, HorizontalAlignment.Left);
                lv_simulator.Columns.Add("模拟器状态", 134, HorizontalAlignment.Left);
                lv_simulator.Columns.Add("任务状态", 134, HorizontalAlignment.Left);
                lv_simulator.Columns.Add("模拟器执行时间", 134, HorizontalAlignment.Left);
            }
        }

        /// <summary>
        /// 更新广告列表items数据
        /// </summary>
        public void updateAdsListItemsData() {
            if (lv_ads != null) {
                lv_ads.Items.Clear();
                lv_ads.BeginUpdate(); 
                string ad_file_path = iniOS.ReadIniData("广告设置", "file_path", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
                if (!ad_file_path.Equals("")) {
                    try
                    {
                        string[] ads = File.ReadAllLines(@ad_file_path, Encoding.UTF8);
                        foreach (string ad in ads)
                        {
                            if(!ad.Equals("")){
                                ListViewItem lvi = new ListViewItem();
                                lvi.ImageIndex = 2;
                                lvi.Text = ad;
                                lvi.SubItems.Add("0");
                                this.lv_ads.Items.Add(lvi);
                            }
                        }
                        this.updateLogsListItemsData("更新广告列表数据成功!");
                    }
                    catch (Exception e)
                    {
                        System.Windows.Forms.MessageBox.Show(e.Message);
                    }
                }
                else
                {
                    this.updateLogsListItemsData("【广告列表】请选择用户文件!");
                }
                lv_ads.EndUpdate();
            }
        }

        /// <summary>
        /// 更新广告列表columns数据
        /// </summary>
        public void updateAdsListColumnData() {
            if (lv_ads != null)
            {
                lv_ads.Columns.Clear();
                lv_ads.Columns.Add("广告词内容", 400, HorizontalAlignment.Left);
                lv_ads.Columns.Add("频率", 155, HorizontalAlignment.Left);
            }
        }

        /// <summary>
        /// 更新用户列表items数据
        /// </summary>
        public void updateUsersListItemsData()
        {
            if (lv_users != null)
            {
                lv_users.Items.Clear();
                lv_users.BeginUpdate();
                string user_file_path = iniOS.ReadIniData("用户设置", "file_path", "", System.IO.Directory.GetCurrentDirectory() + "/jAuto.ini");
                if (!user_file_path.Equals(""))
                {
                    try
                    {
                        string[] ads = File.ReadAllLines(@user_file_path, Encoding.UTF8);
                        foreach (string ad in ads)
                        {
                            string[] user_arr = ad.Split('|');
                            if (user_arr.Length >= 2)
                            {
                                ListViewItem lvi = new ListViewItem();
                                lvi.ImageIndex = 3;
                                lvi.Text = user_arr[0];
                                lvi.SubItems.Add(user_arr[1]);
                                lvi.SubItems.Add("0");
                                this.lv_users.Items.Add(lvi);
                            }
                        }
                        this.updateLogsListItemsData("更新用户列表数据成功!");
                    }
                    catch (Exception e) {
                        System.Windows.Forms.MessageBox.Show(e.Message);
                    }
                }
                else
                {
                    this.updateLogsListItemsData("【用户列表】请选择用户文件!");
                }
                lv_users.EndUpdate();
            }
        }

        /// <summary>
        /// 更新用户列表columns数据
        /// </summary>
        public void updateUsersListColumnData()
        {
            if (lv_users != null)
            {
                lv_users.Columns.Clear();
                lv_users.Columns.Add("账号", 200, HorizontalAlignment.Left);
                lv_users.Columns.Add("密码", 200, HorizontalAlignment.Left);
                lv_users.Columns.Add("频率", 158, HorizontalAlignment.Left);
            }
        }

        /// <summary>
        /// 更新日志列表items数据
        /// </summary>
        private void updateLogsListItemsData(string msg)
        {
            if( lb_logs != null){
                lb_logs.BeginUpdate();
                if (lb_logs.Items.Count >= 38)
                {
                    try
                    {
                        lb_logs.Items.RemoveAt(0);
                        lb_logs.Items.RemoveAt(0);
                        lb_logs.Items.RemoveAt(0);
                    }
                    catch (Exception)
                    {

                    }
                }
                lb_logs.Items.Add("**************************************************************************************");
                lb_logs.Items.Add(DateTime.Now.ToString() + " -> " + msg);
                lb_logs.Items.Add("**************************************************************************************");
                lb_logs.EndUpdate();
            }
        }

        /// <summary>
        /// 创建模拟器
        /// </summary>
        public void createSimulator(Boolean change) {
            install_t.Enabled = change;
            if (change)
            {
                this.updateLogsListItemsData("【模拟器操作】开启创建模拟器定时器!");
            }
            else
            {
                this.updateLogsListItemsData("【模拟器操作】关闭创建模拟器定时器!");
            }
        }

        /// <summary>
        /// 关闭所有模拟器
        /// </summary>
        public void closeSimulatorAll() {
            ThreadStart threadStart = new ThreadStart(ThreadCloseSimulatorAll);
            Thread thread = new Thread(threadStart);
            thread.Start();
        }

        private void ThreadCloseSimulatorAll()
        {
            if (jwindows != null)
            {
                jwindows.closeSimulatorAll();
                this.updateLogsListItemsData("【模拟器列表】关闭所有模拟器成功!");
            }
        }

        /// <summary>
        /// 删除所有模拟器
        /// </summary>
        public void deleteSimulatorAll() {
            ThreadStart threadStart = new ThreadStart(ThreadDeleteSimulatorAll);
            Thread thread = new Thread(threadStart);
            thread.Start();
        }

        private void ThreadDeleteSimulatorAll()
        {
            if (jwindows != null)
            {
                jwindows.deleteSimulatorAll();
                updateSimulatorListItemsData();
                this.updateLogsListItemsData("【模拟器列表】删除所有模拟器成功!");
            }
        }

        /// <summary>
        /// 删除指定模拟器
        /// </summary>
        /// <param name="simulatorName"></param>
        public void deleteSimulator()
        {
            ThreadStart threadStart = new ThreadStart(ThreadDeleteSimulator);
            Thread thread = new Thread(threadStart);
            thread.Start();
        }

        private void ThreadDeleteSimulator()
        {
            if (jwindows != null)
            {
                try
                {
                    int count = lv_simulator.SelectedItems.Count;
                    for (int i = 0; i < count; i++)
                    {
                        int index = lv_simulator.SelectedItems[i].Index;
                        string simulatorName = lv_simulator.Items[index].SubItems[0].Text;
                        jwindows.deleteSimulator(simulatorName);
                        updateSimulatorListItemsData();
                        this.updateLogsListItemsData("【模拟器列表】删除" + simulatorName + "模拟器成功!");
                    }
                }
                catch (Exception e)
                {
                    this.updateLogsListItemsData("【模拟器列表】删除模拟器失败!"+e.Message);
                }
            }
        }

        /// <summary>
        /// 打开指定模拟器
        /// </summary>
        public void openSimulator() {
            ThreadStart threadStart = new ThreadStart(ThreadOpenSimulator);
            Thread thread = new Thread(threadStart);
            thread.Start();
        }

        private void ThreadOpenSimulator()
        {
            if (jwindows != null)
            {
                int count = lv_simulator.SelectedItems.Count;
                for (int i = 0; i < count; i++) {
                    int index = lv_simulator.SelectedItems[i].Index;
                    string simulatorName = lv_simulator.Items[index].SubItems[0].Text;
                    jwindows.openSimulator(simulatorName);
                    lv_simulator.Items[index].SubItems[1].Text = "开启";
                    lv_simulator.Items[index].BackColor = Color.Blue;
                    lv_simulator.Items[index].ForeColor = Color.White;
                    this.updateLogsListItemsData("【模拟器列表】打开" + simulatorName + "模拟器成功!");
                }
            }
        }

        /// <summary>
        /// 关闭指定模拟器
        /// </summary>
        public void closeSimulator() {
            ThreadStart threadStart = new ThreadStart(ThreadCloseSimulator);
            Thread thread = new Thread(threadStart);
            thread.Start();
        }

        private void ThreadCloseSimulator()
        {
            if (jwindows != null)
            {
                int count = lv_simulator.SelectedItems.Count;
                for (int i = 0; i < count; i++)
                {
                    int index = lv_simulator.SelectedItems[i].Index;
                    string simulatorName = lv_simulator.Items[index].SubItems[0].Text;
                    jwindows.closeSimulator(simulatorName);
                    lv_simulator.Items[index].SubItems[1].Text = "未开启";
                    lv_simulator.Items[index].BackColor = Color.White;
                    lv_simulator.Items[index].ForeColor = Color.Black;
                    this.updateLogsListItemsData("【模拟器列表】关闭" + simulatorName + "模拟器成功!");
                }
            }
        }

        /// <summary>
        /// 定时器开关
        /// </summary>
        /// <param name="change"></param>
        public void closeTimer(Boolean change=false) {
            if (t != null)
            {
                t.Enabled = change;
                if (change)
                {
                    this.updateLogsListItemsData("【模拟器列表】开启任务!");
                }
                else
                {
                    this.updateLogsListItemsData("【模拟器列表】停止任务!");
                }
            }
        }

        /// <summary>
        /// 回收资源
        /// </summary>
        public void closeSource()
        {
            t.Enabled = false;
            t.Dispose();
            t = null;

            show_t.Enabled = false;
            show_t.Dispose();
            show_t = null;

            install_t.Enabled = false;
            install_t.Dispose();
            install_t = null;
        }
    }

}
