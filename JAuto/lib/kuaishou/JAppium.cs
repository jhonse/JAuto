using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAuto.lib.kuaishou
{
    class JAppium
    {
        private AppiumDriver<AppiumWebElement> driver = null;
        public Boolean openStatus = false;
        public Boolean isChanges = true;
        public String Msg = "";

        /// <summary>
        /// 初始化方法
        /// </summary>
        public void initDriver() {
            isChanges = false;
            if (driver == null)
            {
                DesiredCapabilities capabilities = new DesiredCapabilities();

                capabilities.SetCapability("deviceName", "127.0.0.1:62025");
                capabilities.SetCapability("platformName", "Android");
                capabilities.SetCapability("unicodeKeyboard", true);
                capabilities.SetCapability("resetKeyboard", true);
                //capabilities.SetCapability("app", "D:/kuaishou.apk");
                capabilities.SetCapability("appPackage", "com.smile.gifmaker");
                capabilities.SetCapability("appActivity", "com.yxcorp.gifshow.HomeActivity");

                try
                {
                    driver = new AndroidDriver<AppiumWebElement>(
                               new Uri("http://127.0.0.1:4723/wd/hub"),
                                   capabilities);
                    openStatus = true;
                    isChanges = true;
                }
                catch (Exception e)
                {
                    Msg = e.Message;
                    openStatus = false;
                    isChanges = true;
                }
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public Boolean removeDriver() {
            isChanges = false;
            if (driver != null)
            {
                try
                {
                    driver.Quit();
                    driver = null;
                    isChanges = true;
                    openStatus = false;
                    return true;
                }
                catch (Exception e)
                {
                    Msg = e.Message;
                    isChanges = true;
                    return false;
                }
            }
            else
            {
                isChanges = true;
                return true;
            }
        }

        /// <summary>
        /// 首页 - 下拉加载数据
        /// </summary>
        /// <returns></returns>
        public Boolean HomeLoadData() {
            isChanges = false;
            if (driver != null)
            {
                try
                {
                    int x = driver.Manage().Window.Size.Width;
                    int y = driver.Manage().Window.Size.Height;
                    driver.Swipe(x / 2, y / 3 * 1, x / 2, y / 3 * 2, 500);
                    isChanges = true;
                    return true;
                }catch(Exception e){
                    Msg = e.Message;
                    isChanges = true;
                    return false;
                }
            }
            else
            {
                isChanges = true;
                return false;
            }
        }

        /// <summary>
        /// 首页 - 点击指定的play
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Boolean HomeClickPhotePlay(int index)
        {
            isChanges = false;
            if (driver != null)
            {
                try
                {
                    ReadOnlyCollection<AppiumWebElement> playerList = driver.FindElementsById("com.smile.gifmaker:id/player");
                    if (playerList.Count > 0 && playerList.Count > index) {
                        if (playerList[index] != null)
                        {
                            playerList[index].Click();
                            isChanges = true;
                            return true;
                        }
                        else
                        {
                            isChanges = true;
                            return false;
                        }
                    }
                    else
                    {
                        isChanges = true;
                        return false;
                    }
                }
                catch (Exception e) {
                    Msg = e.Message;
                    isChanges = true;
                    return false;
                }
            }
            else
            {
                isChanges = true;
                return false;
            }
        }

        /// <summary>
        /// 首页 - 判断是否需要登录
        /// </summary>
        /// <returns></returns>
        public Boolean HomeCheckLogin() {
            isChanges = false;
            if (driver != null)
            {
                try
                {
                    AppiumWebElement loginElement = driver.FindElementById("com.smile.gifmaker:id/tab_login_button");
                    if (loginElement != null)
                    {
                        isChanges = true;
                        return true;
                    }
                    else
                    {
                        isChanges = true;
                        return false;
                    }
                }
                catch (Exception e) {
                    Msg = e.ToString();
                    isChanges = true;
                    return false;
                }
            }
            else
            {
                isChanges = true;
                return false;
            }
        }

        /// <summary>
        /// 首页 - 点击登陆
        /// </summary>
        /// <returns></returns>
        public Boolean HomeLogin()
        {
            isChanges = false;
            if (driver != null)
            {
                try
                {
                    AppiumWebElement loginElement = driver.FindElementById("com.smile.gifmaker:id/tab_login_button");
                    if (loginElement != null)
                    {
                        loginElement.Click();
                        isChanges = true;
                        return true;
                    }
                    else
                    {
                        isChanges = true;
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Msg = e.Message;
                    isChanges = true;
                    return false;
                }
            }
            else
            {
                isChanges = true;
                return false;
            }
        }

        /// <summary>
        /// play - 点击关注
        /// </summary>
        /// <returns></returns>
        public Boolean PlayClickFollow()
        {
            isChanges = false;
            if (driver != null)
            {
                try
                {
                    AppiumWebElement followElement = driver.FindElementById("com.smile.gifmaker:id/follow_button");
                    if (followElement != null)
                    {
                        followElement.Click();
                        isChanges = true;
                        return true;
                    }
                    else
                    {
                        isChanges = true;
                        return false;
                    }
                }
                catch (Exception e) {
                    Msg = e.Message;
                    isChanges = true;
                    return false;
                }
            }
            else
            {
                isChanges = true;
                return false;
            }
        }

        /// <summary>
        /// play - 点击喜欢
        /// </summary>
        /// <returns></returns>
        public Boolean PlayClickLike()
        {
            isChanges = false;
            if (driver != null)
            {
                try
                {
                    AppiumWebElement likeElement = driver.FindElementById("com.smile.gifmaker:id/like_button");
                    if (likeElement != null)
                    {
                        likeElement.Click();
                        isChanges = true;
                        return true;
                    }
                    else
                    {
                        isChanges = true;
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Msg = e.Message;
                    isChanges = true;
                    return false;
                }
            }
            else
            {
                isChanges = true;
                return false;
            }
        }

        /// <summary>
        /// Play - 查看评论输入框
        /// </summary>
        /// <returns></returns>
        public Boolean PlayShowCommentInput()
        {
            isChanges = false;
            if (driver != null)
            {
                try
                {
                    int x = driver.Manage().Window.Size.Width;
                    int y = driver.Manage().Window.Size.Height;
                    driver.Swipe(x / 2, y / 3 * 2, x / 2, y / 3 * 1, 500);
                    isChanges = true;
                    return true;
                }
                catch (Exception e)
                {
                    Msg = e.Message;
                    isChanges = true;
                    return false;
                }
            }
            else
            {
                isChanges = true;
                return false;
            }
        }

        /// <summary>
        /// Play - 点击评论输入框
        /// </summary>
        /// <returns></returns>
        public Boolean PlayClickCommentInput()
        {
            isChanges = false;
            if (driver != null)
            {
                try
                {
                    AppiumWebElement editorElement = driver.FindElementById("com.smile.gifmaker:id/editor_holder_text");
                    if (editorElement != null)
                    {
                        editorElement.Click();
                        isChanges = true;
                        return true;
                    }
                    else
                    {
                        isChanges = true;
                        return false;
                    }
                }
                catch (Exception e) {
                    Msg = e.Message;
                    isChanges = true;
                    return false;
                }
            }
            else {
                return false;
            }
        }

        /// <summary>
        /// Play - 输入评论
        /// </summary>
        /// <returns></returns>
        public Boolean PlayWriteCommentInput(string ad)
        {
            isChanges = false;
            if (driver != null)
            {
                try
                {
                    AppiumWebElement editorElement = driver.FindElementById("com.smile.gifmaker:id/editor");
                    if (editorElement != null)
                    {
                        editorElement.SendKeys(ad);
                        isChanges = true;
                        return true;
                    }
                    else
                    {
                        isChanges = true;
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Msg = e.Message;
                    isChanges = true;
                    return false;
                }
            }
            else
            {
                isChanges = true;
                return false;
            }
        }

        /// <summary>
        /// Play - 提交评论
        /// </summary>
        /// <returns></returns>
        public Boolean PlayCommentSubmit()
        {
            isChanges = false;
            if (driver != null)
            {
                try
                {
                    AppiumWebElement finishElement = driver.FindElementById("com.smile.gifmaker:id/finish_button");
                    if (finishElement != null)
                    {
                        finishElement.Click();
                        isChanges = true;
                        return true;
                    }
                    else
                    {
                        isChanges = true;
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Msg = e.Message;
                    isChanges = true;
                    return false;
                }
            }
            else
            {
                isChanges = true;
                return false;
            }
        }

        /// <summary>
        /// Play - 返回
        /// </summary>
        /// <returns></returns>
        public Boolean PlayReturn()
        {
            isChanges = false;
            if (driver != null)
            {
                try
                {
                    AppiumWebElement returnElement = driver.FindElementById("com.smile.gifmaker:id/back_btn");
                    if (returnElement != null)
                    {
                        returnElement.Click();
                        isChanges = true;
                        return true;
                    }
                    else
                    {
                        isChanges = true;
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Msg = e.Message;
                    isChanges = true;
                    return false;
                }
            }
            else
            {
                isChanges = true;
                return false;
            }
        }

        /// <summary>
        /// 登录 - 切换到登录
        /// </summary>
        /// <returns></returns>
        public Boolean LoginOnClickLink()
        {
            isChanges = false;
            if (driver != null)
            {
                try
                {
                    AppiumWebElement loginButton = driver.FindElementById("com.smile.gifmaker:id/login_radio");
                    if (loginButton != null)
                    {
                        loginButton.Click();
                        isChanges = true;
                        return true;
                    }
                    else
                    {
                        isChanges = true;
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Msg = e.Message;
                    isChanges = true;
                    return false;
                }
            }
            else
            {
                isChanges = true;
                return false;
            }
        }

        /// <summary>
        /// 登录 - 输入账号和密码
        /// </summary>
        /// <returns></returns>
        public Boolean LoginOnPutInput(string user, string password)
        {
            isChanges = false;
            if (driver != null)
            {
                try
                {
                    AppiumWebElement loginNameInput = driver.FindElementById("com.smile.gifmaker:id/login_name_et");
                    if (loginNameInput != null)
                    {
                        loginNameInput.SendKeys(user);
                        AppiumWebElement loginPsdInput = driver.FindElementById("com.smile.gifmaker:id/login_psd_et");
                        if (loginPsdInput != null)
                        {
                            loginPsdInput.SendKeys(password);
                            isChanges = true;
                            return true;
                        }
                        else
                        {
                            isChanges = true;
                            return false;
                        }
                    }
                    else
                    {
                        isChanges = true;
                        return false;
                    }
                }
                catch (Exception e) {
                    Msg = e.Message;
                    isChanges = true;
                    return false;
                }
            }
            else
            {
                isChanges = true;
                return false;
            }
        }

        /// <summary>
        /// 登录 - 开始登录
        /// </summary>
        /// <returns></returns>
        public Boolean LoginOnSubmit()
        {
            isChanges = false;
            if (driver != null)
            {
                try
                {
                    AppiumWebElement loginNext = driver.FindElementByAccessibilityId("下一步");
                    if (loginNext != null)
                    {
                        loginNext.Click();
                        isChanges = true;
                        return true;
                    }
                    else
                    {
                        isChanges = true;
                        return false;
                    }
                }
                catch (Exception e) {
                    Msg = e.Message;
                    isChanges = true;
                    return false;
                }
            }
            else
            {
                isChanges = true;
                return false;
            }
        }

    }
}
