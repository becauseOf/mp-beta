using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;

namespace StudyOfWpfApplication1
{
    //得到歌词和歌词的时间
    class GetLyricAndLyricTime
    {
        private string lyricFilePath;
        public string LyricFilePath
        {
            get { return lyricFilePath; }
            set { lyricFilePath = value; }
        }
        private string title = null;//标题，默认空
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        private string artist = null;//艺术家即歌手
        public string Artist
        {
            get { return artist; }
            set { artist = value; }
        }
        private string album = null;//专辑名
        public string Album
        {
            get { return album; }
            set { album = value; }
        }
        private bool lyricParseResult =true;//得到歌词解析的结果
        public bool LyricParseResult
        {
            get { return lyricParseResult; }
            set { lyricParseResult = value; }
        }       
        /// <summary>
        /// 存放歌词时间(键)和对应歌词(值),不包括歌词文件里的标题,艺术家,专辑名(你可以通过以上字段获取结果)
        /// </summary>
        private SortedDictionary<Double, string> lyricAndTimeDictionary = new SortedDictionary<Double, string>();
        /// <summary>
        /// 存放歌词时间(键)和对应歌词(值),不包括歌词文件里的标题,艺术家,专辑名(你可以通过以上字段获取结果)
        /// </summary>
        public SortedDictionary<Double, string> LyricAndTimeDictionary
        {
            get { return lyricAndTimeDictionary; }
            set { lyricAndTimeDictionary = value; }
        }

        #region 从指定本地路径读取歌词文件,从而得到歌词头信息,歌词内容和时间.返回解析歌词结果
        //如果输出的歌词乱码,那么请注意修改方法里的编码格式,我的是Encoding.Default   
        /// <summary>
        /// 您只需要使用这个方法输入歌词文件路径就可以得到解析好的歌词文件，存放在LyricAndTimeDictionary
        /// </summary>
        /// <param name="lyricPath">歌词路径</param>
        /// <returns></returns>
        public bool getLyricAndLyricTimeByLyricPath(string lyricPath)
        {
            lyricParseResult = true;
            this.LyricFilePath = lyricPath;
            //防止时间重复,lyricDictionary该字典只存放当前文件的歌词时间和歌词
            //所以每次重新获得歌词时间和歌词时要清空上一次保存的               
           lyricAndTimeDictionary.Clear();
           if (System.IO.File.Exists(lyricPath))
           {                
                try
                {
                    string[] lines = System.IO.File.ReadAllLines(lyricPath, Encoding.Default);
                    foreach (string line in lines)
                    {
                        //解析歌词文本的每行
                        parserLine(line);
                    }
                    if (lyricAndTimeDictionary.Count == 0)
                    {
                        lyricParseResult = false;//没有内容
                    }                  
                }
                catch
                {
                    lyricParseResult = false;//请确保你的歌词文件内容是正确的格式
                }
            }
           else
           {
               lyricParseResult = false;//请确保你的歌词文件路径是正确的             
           }
            return this.lyricParseResult;
        }
        #endregion
        /// <summary>
        /// 把从文本读出的每行进行解析每行
        /// </summary>
        /// <param name="str"></param>
        public void parserLine(string str)
        {
            #region 取得歌词头信息
            //取得歌曲名
            if (str.StartsWith("[ti:"))
            {
                try
                {
                    title = str.Substring(4, str.Length - 5);
                }
                catch
                {
                    title = "未知";
                }
            }
            //取得歌手名
            else if (str.StartsWith("[ar:"))
            {
                try
                {
                    Artist = str.Substring(4, str.Length - 5);
                }
                catch
                {
                    Artist = "未知";
                }
            }
            //取得专辑名
            else if (str.StartsWith("[al:"))
            {
                try
                {
                    Album = str.Substring(4, str.Length - 5);
                }
                catch
                {
                    Album = "未知";
                }
            }
            #endregion
            #region 通过正则表达式取得每句歌词内容信息
            else
            {
                //正则表达式
                string regStr = "\\[(\\d{2}:\\d{2}\\.\\d{2})\\]";//匹配[00:00.00]....
                Regex regex = new Regex(regStr);
                string regTimeStr = "\\d{2}:\\d{2}\\.\\d{2}";
                Regex regexTime = new Regex(regTimeStr);//匹配00:00.00
                //如果当前行匹配成功就执行以下操作
                if (regex.IsMatch(str) == true)
                {
                    //得到当前行匹配的所有内容并再次按正则表达式分割(分割的结果含时间和歌词)存放到数组里                  
                    string[] Content = regex.Split(str);
                    //时间数组.最大20个时间。您可以设置成更大。但是一般最多是3个时间对应一句歌词。本文件底下的"李克勤-红日"歌词文件有5个时间对应一句“哦”歌词的
                    string[] timesStr = new string[20];
                    Double currentTime;
                    string currentTxt = null;//歌词(每一行歌词信息是只有一句歌词和可以有多个时间)
                    //在内容数组里轮询查找符合正则表达式的时间。找出时间并存放在数组里和找出歌词
                    int i = 0;
                    string correctLyricTxt = null;//正确歌词,因为数组Content里歌词有可能是null
                    foreach (string content in Content)
                    {
                        //时间匹配正则表达式成功  
                        if (regexTime.IsMatch(content) == true)
                        {
                            timesStr[i] = content;//这是时间
                        }
                        else
                        {
                            currentTxt = content;//这是歌词  
                            if (!string.IsNullOrEmpty(content))
                            {
                                correctLyricTxt = content;
                            }
                        }
                        i++;
                    }
                    //存放歌词时间和对应的歌词到资源字典里
                    //存放前先把时间转换成double型的毫秒
                    foreach (string time in timesStr)
                    {
                        if (!string.IsNullOrEmpty(time))
                        {
                            try
                            {
                                currentTime = strToDouble(time);
                                if (!lyricAndTimeDictionary.ContainsKey(currentTime))
                                {
                                    lyricAndTimeDictionary.Add(currentTime, correctLyricTxt);
                                }
                            }
                            catch
                            {
                                //如果以上时间解析错误就不加入.说明该时间有误
                            }
                        }
                    }
                }

            }
            #endregion
        }       
        /// <summary>
        /// 将时间字符串转换成Double类型
        /// </summary>
        /// <param name="timeStr"></param>
        /// <returns></returns>
        private double strToDouble(string timeStr)
        {
            //输入timeStr时间字符串格式如  01:02.50                      
            string[] s = timeStr.Split(':');//分:秒
            string[] ss = s[1].Split('.');//秒.毫秒
            double min = 0, sec = 0;
            min = Convert.ToDouble(s[0]);
            sec = Convert.ToDouble(s[1]);
            return min * 60 + sec;
        }
    }
}

