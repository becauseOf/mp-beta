using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace StudyOfWpfApplication1
{
    class LyricShow
    {
        public LyricShow(Canvas lyricCanvas, StackPanel commonLyricStackPanel, Canvas canvasFocusLyric, TextBlock tBFocusLyricBack, Canvas canvasFocusLyricForeMove, TextBlock tBFocusLyricFore)
        {
            LyricShow.lyricCanvas = lyricCanvas;
            LyricShow.commonLyricStackPanel = commonLyricStackPanel;
            LyricShow.canvasFocusLyric = canvasFocusLyric;
            LyricShow.tBFocusLyricBack = tBFocusLyricBack;
            LyricShow.canvasFocusLyricForeMove = canvasFocusLyricForeMove;
            LyricShow.tBFocusLyricFore = tBFocusLyricFore;
            fontSizeChangeSmallA.Completed += fontSizeChangeSmallA_Completed;
            fontSizeChangeBigA.Completed += fontSizeChangeBigA_Completed;
        }
        /// <summary>
        /// 是否暂停了歌词秀(默认false，没有暂停歌词秀)
        /// </summary>
        private static bool isPauseLyricShow = false;
        /// <summary>
        /// 是否暂停了歌词秀(默认false，没有暂停歌词秀)
        /// </summary>
        public static bool IsPauseLyricShow
        {
            get { return LyricShow.isPauseLyricShow; }
            set { LyricShow.isPauseLyricShow = value; }
        }
        /// <summary>
        /// 当前(高亮)歌词"走过(刷过)"的距离占当前歌词字体总距离的百分比(初始0)
        /// </summary>
        public static double currentLyricWalkedPersent = 0;
        /// <summary>
        /// 当前这句歌词的索引值(初始-1，0是第一句)
        /// </summary>
        private static int currentLyricIndex = -1;
        /// <summary>
        /// 当前这句歌词的索引值(初始-1，0是第一句)
        /// </summary>
        public static int CurrentLyricIndex
        {
            get
            {
                if (LyricShow.currentLyricIndex <= -1)
                {
                    LyricShow.currentLyricIndex = -1;
                }
                if (LyricShow.currentLyricIndex >= TimeAndLyricDictionary.Count)
                {
                    try
                    {
                        //此值已超出歌词有效索引的范围,歌词index=0，表示第一句.故有效范围为[0,Count-1]
                        LyricShow.currentLyricIndex = TimeAndLyricDictionary.Count;
                    }
                    catch
                    {
                        LyricShow.currentLyricIndex = -1;
                    }
                }
                return LyricShow.currentLyricIndex;
            }
            set { LyricShow.currentLyricIndex = value; }
        }
        /// <summary>
        /// 上次歌词的索引值(初始-1，0是第一句)
        /// </summary>
        private static int lastLyricIndex = -1;
        /// <summary>
        /// 上次歌词的索引值(初始-1，0是第一句)
        /// </summary>
        public static int LastLyricIndex
        {
            get
            {
                if (LyricShow.lastLyricIndex <= -1)
                {
                    LyricShow.lastLyricIndex = -1;
                }
                if (LyricShow.lastLyricIndex >= TimeAndLyricDictionary.Count)
                {
                    try
                    {
                        LyricShow.lastLyricIndex = TimeAndLyricDictionary.Count;
                    }
                    catch
                    {
                        LyricShow.lastLyricIndex = -1;
                    }
                }
                return LyricShow.lastLyricIndex;
            }
            set { LyricShow.lastLyricIndex = value; }
        }
        /// <summary>
        /// 当前歌词和下一句歌词的时间间隔长度(初始0)
        /// </summary>
        public static double currentAndNextLyricIntervalTime = 0;
        /// <summary>
        /// 高亮歌词从左往右刷过的距离长度(初始0)
        /// </summary>
        public static double focusLyricBrushedDistance = 0;
        /// <summary>
        /// 高亮歌词文本里的字体总的宽度(初始0)
        /// </summary>
        public static double focusLyricTextFontTotalWidth = 0;
        /// <summary>
        /// 高亮歌词TextBlock文本居中对齐后所留的左边空白宽度(初始0)
        /// </summary>
        public static double focusLyricTextBlockLeftSpaceWidth = 0;
        /// <summary>
        /// 桌面歌词从左往右刷过的距离长度(初始0)
        /// </summary>
        public static double deskLyricBrushedDistance = 0;
        /// <summary>
        /// 桌面歌词文本里的字体总的宽度(初始0)
        /// </summary>
        public static double deskLyricTextFontTotalWidth = 0;
        /// <summary>
        /// 桌面歌词TextBlock文本居中对齐后所留的左边空白宽度(初始0)
        /// </summary>
        public static double deskLyricTextBlockLeftSpaceWidth = 0;
        /// <summary>
        /// 字体大小渐变到指定小的动画完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fontSizeChangeSmallA_Completed(object sender, EventArgs e)
        {
            try
            {
                TextBlock LastTB = commonLyricStackPanel.Children[LyricShow.LastLyricIndex] as TextBlock;
                LastTB.Foreground = new SolidColorBrush(Color.FromArgb(cA, cR, cG, cB));
                LastTB.FontFamily = cFontFamily;
            }
            catch { }
        }
        /// <summary>
        /// 字体大小渐变到指定大的动画完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fontSizeChangeBigA_Completed(object sender, EventArgs e)
        {
            try
            {
                tBFocusLyricBack.Visibility = Visibility.Visible;
                TextBlock CurrentTBBack = commonLyricStackPanel.Children[LyricShow.CurrentLyricIndex] as TextBlock;
                CurrentTBBack.Visibility = Visibility.Hidden;
                //#region 刷新情况不是在跳播放的前提下
                currentLyricWalkedPersent = 0;
                //高亮歌词
                focusLyricTextFontTotalWidth = MeasureTextWidth(tBFocusLyricBack.Text, tBFocusLyricBack.FontSize, tBFocusLyricBack.FontFamily.ToString());
                focusLyricTextBlockLeftSpaceWidth = (tBFocusLyricBack.ActualWidth - focusLyricTextFontTotalWidth) / 2;
                canvasFocusLyricForeMove.Width = Math.Abs(focusLyricTextBlockLeftSpaceWidth);//初始高亮歌词移动画布走完左边空白距离
                canvasFocusLyricForeMove.Visibility = Visibility.Visible;
                //桌面歌词
                if (isOpenDeskLyric == true)
                {
                    deskLyricTextFontTotalWidth = MeasureTextWidth(tBDeskLyricBack.Text, tBDeskLyricBack.FontSize, tBDeskLyricBack.FontFamily.ToString());
                    deskLyricTextBlockLeftSpaceWidth = (tBDeskLyricBack.ActualWidth - deskLyricTextFontTotalWidth) / 2;
                    canvasDeskLyricForeMove.Width = Math.Abs(deskLyricTextBlockLeftSpaceWidth);//初始桌面歌词移动画布走完左边空白距离
                    canvasDeskLyricForeMove.Visibility = Visibility.Visible;
                }
                //计算下句歌词和当前歌词的时间间隔
                int nextLyricIndex = CurrentLyricIndex + 1;
                if (nextLyricIndex < TimeAndLyricDictionary.Count && CurrentLyricIndex > -1)
                {
                    try
                    {
                        currentAndNextLyricIntervalTime = TimeAndLyricDictionary.Keys.ElementAt(nextLyricIndex) - TimeAndLyricDictionary.Keys.ElementAt(CurrentLyricIndex);
                    }
                    catch
                    { }
                }
                else
                {
                    //最后一句
                    currentAndNextLyricIntervalTime = 3;
                }
                if (isPauseLyricShow == false)
                {
                    //启动高亮歌词的从左刷到右的动画
                    beginHighLightLyricAnimation(currentAndNextLyricIntervalTime);
                    //启动桌面歌词的从左刷到右的动画
                    if (isOpenDeskLyric == true)
                    {
                        beginDeskLyricAnimation(currentAndNextLyricIntervalTime);
                    }
                }
            }
            catch { }
        }
        /// <summary>
        /// 歌词时间(键)和对应歌词(值)(歌词第0句是起始句)
        /// </summary>
        private static SortedDictionary<Double, string> timeAndLyricDictionary = new SortedDictionary<double, string>();
        /// <summary>
        /// 歌词时间(键)和对应歌词(值)(歌词第0句是起始句)
        /// </summary>
        public static SortedDictionary<Double, string> TimeAndLyricDictionary
        {
            get
            {
                if (LyricShow.timeAndLyricDictionary == null)
                {
                    LyricShow.timeAndLyricDictionary = new SortedDictionary<double, string>();
                }
                return LyricShow.timeAndLyricDictionary;
            }
            set
            { LyricShow.timeAndLyricDictionary = value; }
        }
        /// <summary>
        /// 歌词总画布,是这里所有可视元素的根节点元素
        /// </summary>
        public static Canvas lyricCanvas;
        /// <summary>
        /// 普通歌词面板
        /// </summary>
        public static StackPanel commonLyricStackPanel;
        /// <summary>
        /// 高亮歌词根画布
        /// </summary>
        public static Canvas canvasFocusLyric;
        /// <summary>
        /// 高亮歌词前景移动画布，从而体现从左往右刷的效果
        /// </summary>
        public static Canvas canvasFocusLyricForeMove;
        /// <summary>
        /// 高亮歌词前景文本TextBlock
        /// </summary>
        public static TextBlock tBFocusLyricFore;
        /// <summary>
        /// 高亮歌词背景文本TextBlock
        /// </summary>
        public static TextBlock tBFocusLyricBack;
        /// <summary>
        /// 是否打开了桌面歌词(默认没有打开false)
        /// </summary>
        private static bool isOpenDeskLyric = false;
        /// <summary>
        /// 是否打开了桌面歌词(默认没有打开false)
        /// </summary>
        public static bool IsOpenDeskLyric
        {
            get { return LyricShow.isOpenDeskLyric; }
            set { LyricShow.isOpenDeskLyric = value; }
        }
        /// <summary>
        /// 桌面歌词的背景文本TextBlock
        /// </summary>
        public static TextBlock tBDeskLyricBack;
        /// <summary>
        /// 桌面歌词的前景文本TextBlock
        /// </summary>
        public static TextBlock tBDeskLyricFore;
        /// <summary>
        /// 桌面歌词前景移动画布，从而体现从左往右刷的效果
        /// </summary>
        public static Canvas canvasDeskLyricForeMove;
        /// <summary>
        /// 歌词向上偏移量(初始是歌词画布的中间位置为偏移量)
        /// </summary>
        private static double topOffset;
        /// <summary>
        /// 歌词向上偏移量(初始是歌词画布的中间位置为偏移量)
        /// </summary>
        public static double TopOffset
        {
            get { return LyricShow.topOffset; }
            set { LyricShow.topOffset = value; }
        }
        /// <summary>
        /// 歌词向左偏移量
        /// </summary>
        private static double leftOffset;
        /// <summary>
        /// 歌词向左偏移量
        /// </summary>
        public static double LeftOffset
        {
            get { return LyricShow.leftOffset; }
            set { LyricShow.leftOffset = value; }
        }
        /// <summary>
        /// 歌词文本TextBlock的高度(默认36)
        /// </summary>
        private static double lyricTextBlockHeight = 36;

        /// <summary>
        /// 动画渐变到指定字体的大(也指高亮歌词的大小,默认22.范围[22,30])
        /// </summary>
        private static double fontBigA = 22;
        /// <summary>
        /// 动画渐变到指定字体的大(也指高亮歌词的大小,默认22.范围[22,30])
        /// </summary>
        public static double FontBigA
        {
            get
            {
                if (LyricShow.fontBigA <= 22)
                {
                    LyricShow.fontBigA = 22;
                }
                if (LyricShow.fontBigA >= 30)
                {
                    LyricShow.fontBigA = 30;
                }
                return LyricShow.fontBigA;
            }
            set { LyricShow.fontBigA = value; }
        }
        /// <summary>
        /// 高亮字体大小渐变到指定大的动画
        /// </summary>
        private static DoubleAnimation fontSizeChangeBigA = new DoubleAnimation();
        /// <summary>
        /// 高亮字体大小渐变到指定大的动画
        /// </summary>
        public static DoubleAnimation FontSizeChangeBigA
        {
            get
            {
                if (LyricShow.fontSizeChangeBigA == null)
                { LyricShow.fontSizeChangeBigA = new DoubleAnimation(); }
                return LyricShow.fontSizeChangeBigA;
            }
            set
            {
                LyricShow.fontSizeChangeBigA = value;
            }
        }
        /// <summary>
        /// 动画渐变到指定字体的小(也指非高亮的普通歌词的大小，默认14.范围[6,22])
        /// </summary>
        private static double fontSmallA = 14;
        /// <summary>
        /// 动画渐变到指定字体的小(也指非高亮的普通歌词的大小,默认14.范围[6,22])
        /// </summary>
        public static double FontSmallA
        {
            get
            {
                if (LyricShow.fontSmallA <= 6)
                {
                    LyricShow.fontSmallA = 6;
                }
                if (LyricShow.fontSmallA >= 22)
                {
                    LyricShow.fontSmallA = 22;
                }
                return LyricShow.fontSmallA;
            }
            set { LyricShow.fontSmallA = value; }
        }
        /// <summary>
        /// 字体大小渐变到指定小的动画
        /// </summary>
        private static DoubleAnimation fontSizeChangeSmallA = new DoubleAnimation();
        /// <summary>
        /// 字体大小渐变到指定小的动画
        /// </summary>
        public static DoubleAnimation FontSizeChangeSmallA
        {
            get
            {
                if (LyricShow.fontSizeChangeSmallA == null)
                { LyricShow.fontSizeChangeSmallA = new DoubleAnimation(); }
                return LyricShow.fontSizeChangeSmallA;
            }
            set { LyricShow.fontSizeChangeSmallA = value; }
        }
        /// <summary>
        /// 高亮歌词TextBlock字体动画:字体渐变到指定大的动画函数
        /// </summary>
        public static void fontSizeToBig(TextBlock tb)
        {
            fontSizeChangeBigA.From = fontSmallA;
            fontSizeChangeBigA.To = fontBigA;
            fontSizeChangeBigA.Duration = new Duration(TimeSpan.Parse("0:0:0.5"));
            tb.BeginAnimation(TextBlock.FontSizeProperty, fontSizeChangeBigA);
        }
        /// <summary>
        /// 歌词TextBlock字体动画:字体渐变到指定小的动画函数
        /// </summary>
        public static void fontSizeToSmall(TextBlock tb)
        {
            fontSizeChangeSmallA.From = fontBigA;
            fontSizeChangeSmallA.To = fontSmallA;
            fontSizeChangeSmallA.Duration = new Duration(TimeSpan.Parse("0:0:0.5"));
            tb.BeginAnimation(TextBlock.FontSizeProperty, fontSizeChangeSmallA);
        }
        /// <summary>
        /// 非高亮歌词的字体
        /// </summary>
        private static FontFamily cFontFamily = new FontFamily("Microsoft YaHei");

        /// <summary>
        /// 非高亮歌词的字体不透明度
        /// </summary>
        private static byte cA = 255;

        /// <summary>
        /// 非高亮歌词的字体Red
        /// </summary>
        private static byte cR = 255;

        /// <summary>
        /// 非高亮歌词的字体Green
        /// </summary>
        private static byte cG = 255;

        /// <summary>
        /// 非高亮歌词的字体Blue
        /// </summary>
        private static byte cB = 255;

        /// <summary>
        /// 高亮歌词的字体
        /// </summary>
        private static FontFamily hFontFamily = new FontFamily("Microsoft YaHei");

        /// <summary>
        /// 高亮歌词的字体不透明度
        /// </summary>
        private static byte hA = 255;

        /// <summary>
        /// 高亮歌词的字体Red
        /// </summary>
        private static byte hR = 255;

        /// <summary>
        /// 高亮歌词的字体Green
        /// </summary>
        private static byte hG = 255;

        /// <summary>
        /// 高亮歌词的字体Blue
        /// </summary>
        private static byte hB = 10;

        /// <summary>
        /// 桌面歌词字体的大小(默认40)
        /// </summary>
        private static double deskLyricFontSize = 40;
        /// <summary>
        /// 桌面歌词字体的大小(默认40)
        /// <summary>
        /// 桌面歌词的字体(微软雅黑是默认值)
        /// </summary>
        private static FontFamily deskLyricFontFamily = new FontFamily("Microsoft YaHei");

        /// <summary>
        /// 打开桌面歌词(请确保显示桌面歌词的窗体已经呈现完毕再调用此方法.Window.show()之后调用)
        /// </summary>
        public static void openDeskLyric(TextBlock tbDeskLyricFore, TextBlock tbDeskLyricBack, Canvas canvasDeskLyricMove)
        {
            isOpenDeskLyric = true;
            tBDeskLyricFore = tbDeskLyricFore;
            tBDeskLyricBack = tbDeskLyricBack;
            canvasDeskLyricForeMove = canvasDeskLyricMove;
            if (!string.IsNullOrWhiteSpace(tBFocusLyricBack.Text))
            {
                tBDeskLyricBack.Text = tBFocusLyricBack.Text;
            }
            tBDeskLyricBack.FontFamily = deskLyricFontFamily;
            tBDeskLyricBack.FontSize = deskLyricFontSize;
            //计算刷过的距离
            try
            {
                deskLyricTextFontTotalWidth = MeasureTextWidth(tbDeskLyricBack.Text, tbDeskLyricBack.FontSize, tbDeskLyricBack.FontFamily.ToString());
                deskLyricTextBlockLeftSpaceWidth = (tbDeskLyricBack.ActualWidth - deskLyricTextFontTotalWidth) / 2;
                double WalkPersent = (canvasFocusLyricForeMove.ActualWidth - focusLyricTextBlockLeftSpaceWidth) / focusLyricTextFontTotalWidth;
                WalkPersent = double.Parse(WalkPersent.ToString("F3"));
                double distance = deskLyricTextBlockLeftSpaceWidth + deskLyricTextFontTotalWidth * WalkPersent;
                canvasDeskLyricForeMove.Width = Math.Abs(distance);
                deskLyricBrushAni.From = distance;
                deskLyricBrushAni.To = deskLyricTextBlockLeftSpaceWidth + deskLyricTextFontTotalWidth;
                double interval = currentAndNextLyricIntervalTime - currentAndNextLyricIntervalTime * WalkPersent;//当前歌词的剩余时间
                interval = double.Parse(interval.ToString("F2"));
                deskLyricBrushAni.Duration = new Duration(TimeSpan.Parse(string.Format("0:0:{0}", interval)));
                if (isPauseLyricShow == false)
                {
                    canvasDeskLyricForeMove.BeginAnimation(Canvas.WidthProperty, deskLyricBrushAni);
                }
            }
            catch
            {
                tBDeskLyricBack.Text = "听音乐，用灵悦播放器！";
                canvasDeskLyricForeMove.Width = tBDeskLyricBack.ActualWidth;
            }
        }
        /// <summary>
        /// 关闭桌面歌词
        /// </summary>
        public static void closeDeskLyric()
        {
            isOpenDeskLyric = false;
        }
        /// <summary>
        /// 歌词面板逐渐滚动的动画
        /// </summary>
        private static DoubleAnimation lyricPanelScrollAni = new DoubleAnimation();
        /// <summary>
        /// 歌词面板逐渐滚动的动画
        /// </summary>
        public static DoubleAnimation LyricPanelScrollAni
        {
            get
            {
                if (LyricShow.lyricPanelScrollAni == null)
                {
                    LyricShow.lyricPanelScrollAni = new DoubleAnimation();
                }
                return LyricShow.lyricPanelScrollAni;
            }
            set { LyricShow.lyricPanelScrollAni = value; }
        }
        /// <summary>
        /// 歌词滚动。滚动到指定歌词索引位置
        /// </summary>
        /// <param name="indexSite">第几句歌词索引位置(歌词第0句是起始句)</param>
        public static void scrollLyric(int indexSite)
        {
            Canvas.SetTop(canvasFocusLyric, lyricCanvas.ActualHeight / 2 - lyricTextBlockHeight);
            topOffset = lyricCanvas.ActualHeight / 2 - indexSite * lyricTextBlockHeight - lyricTextBlockHeight;
            lyricPanelScrollAni.To = topOffset;
            lyricPanelScrollAni.Duration = new Duration(TimeSpan.Parse("0:0:0.5"));
            commonLyricStackPanel.BeginAnimation(Canvas.TopProperty, lyricPanelScrollAni);
        }
        /// <summary>
        /// 高亮歌词的从左刷到右的画刷动画
        /// </summary>
        private static DoubleAnimation focusLyricBrushAni = new DoubleAnimation();
        /// <summary>
        /// 高亮歌词的从左刷到右的画刷动画
        /// </summary>
        public static DoubleAnimation FocusLyricBrushAni
        {
            get
            {
                if (LyricShow.focusLyricBrushAni == null)
                {
                    LyricShow.focusLyricBrushAni = new DoubleAnimation();
                }
                return LyricShow.focusLyricBrushAni;
            }
            set { LyricShow.focusLyricBrushAni = value; }
        }
        /// <summary>
        /// 开始动画:高亮歌词从左刷到右的动画
        /// </summary>
        /// <param name="animationTimeLength">动画时长</param>
        public static void beginHighLightLyricAnimation(double animationTimeLength)
        {
            //0.6秒是,字体由小变大的完成时间.CPU耗时0.1
            //0.60= 0.5 + 0.1
            animationTimeLength = Math.Abs(animationTimeLength - 0.60);
            string intervalstr = string.Format("0:0:{0}", animationTimeLength.ToString("F2"));
            focusLyricBrushAni.From = focusLyricTextBlockLeftSpaceWidth;
            focusLyricBrushAni.To = focusLyricTextBlockLeftSpaceWidth + focusLyricTextFontTotalWidth;
            focusLyricBrushAni.Duration = new Duration(TimeSpan.Parse(intervalstr));
            canvasFocusLyricForeMove.BeginAnimation(Canvas.WidthProperty, focusLyricBrushAni);
        }
        /// <summary>
        /// 桌面歌词的从左刷到右的画刷动画
        /// </summary>
        private static DoubleAnimation deskLyricBrushAni = new DoubleAnimation();
        /// <summary>
        /// 桌面歌词的从左刷到右的画刷动画
        /// </summary>
        public static DoubleAnimation DeskLyricBrushAni
        {
            get
            {
                if (LyricShow.deskLyricBrushAni == null)
                {
                    LyricShow.deskLyricBrushAni = new DoubleAnimation();
                }
                return LyricShow.deskLyricBrushAni;
            }
            set { LyricShow.deskLyricBrushAni = value; }
        }
        /// <summary>
        /// 开始动画:桌面歌词从左刷到右的动画
        /// </summary>
        /// <param name="animationTimeLength">动画时长</param>
        public static void beginDeskLyricAnimation(double animationTimeLength)
        {
            //0.5秒是,字体由小变大的完成时间.CPU耗时0.1
            //0.60= 0.50 + 0.10
            animationTimeLength = Math.Abs(animationTimeLength - 0.60);
            string intervalstr = string.Format("0:0:{0}", animationTimeLength.ToString("F2"));
            deskLyricBrushAni.From = deskLyricTextBlockLeftSpaceWidth;
            deskLyricBrushAni.To = deskLyricTextBlockLeftSpaceWidth + deskLyricTextFontTotalWidth;
            deskLyricBrushAni.Duration = new Duration(TimeSpan.Parse(intervalstr));
            canvasDeskLyricForeMove.BeginAnimation(Canvas.WidthProperty, deskLyricBrushAni);
        }
        /// <summary>
        /// 测量文本字体所占的宽度（可以计算出每句歌词在对应TextBlock里居中对齐后所占的实际宽度）
        /// </summary>
        /// <param name="text"></param>
        /// <param name="fontSize"></param>
        /// <param name="fontFamily"></param>
        /// <returns></returns>
        private static double MeasureTextWidth(string text, double fontSize, string fontFamily)
        {
            FormattedText formattedText = new FormattedText(
            text,
            System.Globalization.CultureInfo.InvariantCulture,
            FlowDirection.LeftToRight,
            new Typeface(fontFamily.ToString()),
            fontSize,
            Brushes.Black
            );
            return formattedText.WidthIncludingTrailingWhitespace;
        }
        /// <summary>
        /// 暂停或者继续歌词秀.参数false代表不暂停,true表示暂停
        /// </summary>
        /// <param name="isPause">是否暂停，false代表不暂停,true表示暂停</param>
        public static void pauseOrContinueLyricShow(bool isPause)
        {
            if (isPause == true)
            {
                #region 暂停
                IsPauseLyricShow = true;
                try
                {
                    //计算暂停前所刷过的距离百分比
                    currentLyricWalkedPersent = (canvasFocusLyricForeMove.ActualWidth - focusLyricTextBlockLeftSpaceWidth) / focusLyricTextFontTotalWidth;
                    currentLyricWalkedPersent = double.Parse(currentLyricWalkedPersent.ToString("F3"));
                    focusLyricBrushAni.From = canvasFocusLyricForeMove.ActualWidth;
                    focusLyricBrushAni.To = canvasFocusLyricForeMove.ActualWidth;
                    canvasFocusLyricForeMove.BeginAnimation(Canvas.WidthProperty, focusLyricBrushAni);
                    if (isOpenDeskLyric == true)
                    {
                        deskLyricBrushAni.From = canvasDeskLyricForeMove.ActualWidth;
                        deskLyricBrushAni.To = canvasDeskLyricForeMove.ActualWidth;
                        canvasDeskLyricForeMove.BeginAnimation(Canvas.WidthProperty, deskLyricBrushAni);
                    }
                }
                catch { }
                #endregion
            }
            else
            {
                #region 继续
                IsPauseLyricShow = false;
                try
                {
                    //计算当前歌词还要刷的剩余时间
                    string remaintime = string.Format("0:0:{0}", (currentAndNextLyricIntervalTime - currentAndNextLyricIntervalTime * currentLyricWalkedPersent).ToString("F2"));
                    focusLyricBrushAni.From = canvasFocusLyricForeMove.ActualWidth;
                    focusLyricBrushAni.To = focusLyricTextBlockLeftSpaceWidth + focusLyricTextFontTotalWidth;
                    focusLyricBrushAni.Duration = new Duration(TimeSpan.Parse(remaintime));
                    canvasFocusLyricForeMove.BeginAnimation(Canvas.WidthProperty, focusLyricBrushAni);
                    if (isOpenDeskLyric == true)
                    {
                        deskLyricBrushAni.From = canvasDeskLyricForeMove.ActualWidth;
                        deskLyricBrushAni.To = deskLyricTextBlockLeftSpaceWidth + deskLyricTextFontTotalWidth;
                        deskLyricBrushAni.Duration = new Duration(TimeSpan.Parse(remaintime));
                        canvasDeskLyricForeMove.BeginAnimation(Canvas.WidthProperty, deskLyricBrushAni);
                    }
                }
                catch { }
                #endregion
            }
        }
        /// <summary>
        /// 停止歌词秀(回到最初状态)
        /// </summary>
        public static void stopLyricShow()
        {
            clearLyricShowAllText();
            focusLyricBrushAni.From = canvasFocusLyricForeMove.ActualWidth;
            focusLyricBrushAni.To = canvasFocusLyricForeMove.ActualWidth;
            canvasFocusLyricForeMove.BeginAnimation(Canvas.WidthProperty, focusLyricBrushAni);
            scrollLyric(-1);
            stopDeskLyricShow();
        }
        /// <summary>
        /// 停止桌面歌词秀(回到最初状态)
        /// </summary>
        public static void stopDeskLyricShow()
        {
            if (isOpenDeskLyric == true)
            {
                deskLyricBrushAni.From = tBDeskLyricBack.ActualWidth;
                deskLyricBrushAni.To = tBDeskLyricBack.ActualWidth;
                canvasDeskLyricForeMove.BeginAnimation(Canvas.WidthProperty, deskLyricBrushAni);
            }
        }
        /// <summary>
        /// 清除歌词秀里的所有文本(非桌面歌词)
        /// </summary>
        public static void clearLyricShowAllText()
        {
            commonLyricStackPanel.Children.Clear();
            tBFocusLyricBack.Text = null;
        }
        /// <summary>
        /// 清除歌词秀里的所有文本(桌面歌词)
        /// </summary>
        public static void clearDeskLyricShowAllText()
        {
            if (isOpenDeskLyric == true)
            {
                tBDeskLyricBack.Text = null;
            }
        }
        /// <summary>
        /// 回到初始歌词界面(initializeLyricUI)初始完成的状态
        /// </summary>
        public static void backInitial()
        {
            tBFocusLyricBack.Text = null;
            focusLyricBrushAni.From = canvasFocusLyricForeMove.ActualWidth;
            focusLyricBrushAni.To = canvasFocusLyricForeMove.ActualWidth;
            canvasFocusLyricForeMove.BeginAnimation(Canvas.WidthProperty, focusLyricBrushAni);
            scrollLyric(-1);
            if (isOpenDeskLyric == true)
            {
                deskLyricBrushAni.From = tBDeskLyricBack.ActualWidth;
                deskLyricBrushAni.To = tBDeskLyricBack.ActualWidth;
                canvasDeskLyricForeMove.BeginAnimation(Canvas.WidthProperty, deskLyricBrushAni);
            }
        }
        /// <summary>
        /// 初始化歌词界面(非桌面歌词),每次更换一首歌要显示它的歌词秀时，此方法是必须要第一调用的
        /// </summary>
        public static void initializeLyricUI(SortedDictionary<Double, string> TimeAndLyricDictionary)
        {
            stopLyricShow();//停止上次的歌词秀,接下去再重新开始初始化
            LyricShow.TimeAndLyricDictionary.Clear();
            foreach (double key in TimeAndLyricDictionary.Keys)
            {
                LyricShow.TimeAndLyricDictionary.Add(key, TimeAndLyricDictionary[key]);
            }
            //RefreshType = 1;            
            focusLyricTextBlockLeftSpaceWidth = 0;
            focusLyricTextFontTotalWidth = 0;
            deskLyricTextBlockLeftSpaceWidth = 0;
            deskLyricTextFontTotalWidth = 0;
            currentAndNextLyricIntervalTime = 0;//时间间隔
            currentLyricWalkedPersent = 0;//当前歌词刷过的距离百分比
            CurrentLyricIndex = -1;//当前这句歌词的索引置为原始
            LastLyricIndex = -1;//上次这句歌词的索引置为原始                      
            //添加歌词文本到歌词面板里
            foreach (string txt in TimeAndLyricDictionary.Values)
            {
                TextBlock tb = new TextBlock();
                tb.TextAlignment = TextAlignment.Center;
                tb.FontSize = fontSmallA;
                tb.FontFamily = cFontFamily;
                tb.Foreground = new SolidColorBrush(Color.FromArgb(cA, cR, cG, cB));
                tb.Background = null;
                tb.Height = lyricTextBlockHeight;
                tb.Text = txt;
                commonLyricStackPanel.Children.Add(tb);
            }
            //初始化高亮歌词的样式(高亮歌词的'背景色'是和普通歌词一样的,只是'前景'画刷色不一样)
            tBFocusLyricBack.FontFamily = hFontFamily;
            tBFocusLyricBack.FontSize = fontBigA;
            tBFocusLyricBack.Foreground = new SolidColorBrush(Color.FromArgb(hA, cR, cG, cB));
            tBFocusLyricFore.Foreground = new SolidColorBrush(Color.FromArgb(hA, hR, hG, hB));
        }
        /// <summary>
        /// 根据歌词第几句的索引值刷新歌词秀。
        /// </summary>
        /// <param name="lyricIndex">歌词的索引值</param>
        public static void refreshLyricShow(int lyricIndex)
        {
            try
            {
                if (lyricIndex >= 0 && lyricIndex < TimeAndLyricDictionary.Count)
                {
                    #region 当前歌词索引值是正常范围内的即([0 ,歌词总条数Count-1],歌词index=0是第一句)
                    try
                    {
                        LyricShow.CurrentLyricIndex = lyricIndex;
                        if (LyricShow.LastLyricIndex >= 0)
                        {
                            TextBlock LastTB = commonLyricStackPanel.Children[LyricShow.LastLyricIndex] as TextBlock;
                            LastTB.Visibility = Visibility.Visible;
                            fontSizeToSmall(LastTB);//把上一句的歌词字体由大渐变到小
                        }
                        TextBlock CurrentTB = commonLyricStackPanel.Children[LyricShow.CurrentLyricIndex] as TextBlock;
                        tBFocusLyricBack.Visibility = Visibility.Hidden;
                        canvasFocusLyricForeMove.Visibility = Visibility.Hidden;
                        CurrentTB.Foreground = new SolidColorBrush(Color.FromArgb(hA, cR, cG, cB));
                        CurrentTB.FontFamily = hFontFamily;
                        tBFocusLyricBack.Text = CurrentTB.Text;
                        if (isOpenDeskLyric == true)
                        {
                            canvasDeskLyricForeMove.Visibility = Visibility.Hidden;
                            if (string.IsNullOrWhiteSpace(CurrentTB.Text))
                            {
                                tBDeskLyricBack.Text = "......";
                            }
                            else
                            {
                                tBDeskLyricBack.Text = CurrentTB.Text;
                            }
                        }
                        LyricShow.LastLyricIndex = LyricShow.CurrentLyricIndex;
                        scrollLyric(LyricShow.CurrentLyricIndex);//滚动歌词                                               
                        fontSizeToBig(CurrentTB);//高亮歌词(当前歌词)字体由小到大渐变                        
                    }
                    catch { }
                    #endregion
                }
                else
                {
                    #region 不在范围内，小于等于-1就视为回到最初原始状态.大于等于Count不处理
                    if (lyricIndex <= -1)
                    {
                        try
                        {
                            if (LyricShow.LastLyricIndex >= 0)
                            {
                                TextBlock LastTB = commonLyricStackPanel.Children[LyricShow.LastLyricIndex] as TextBlock;
                                LastTB.Visibility = Visibility.Visible;
                                fontSizeToSmall(LastTB);//把上一句的歌词字体由大渐变到小
                            }
                        }
                        catch
                        { }
                        finally
                        {
                            tBFocusLyricBack.Text = "";
                            canvasFocusLyricForeMove.Visibility = Visibility.Hidden;
                            if (isOpenDeskLyric == true)
                            {
                                canvasDeskLyricForeMove.Width = tBDeskLyricBack.ActualWidth;
                                canvasDeskLyricForeMove.Visibility = Visibility.Visible;
                                tBDeskLyricBack.Text = "听音乐，用灵悦播放器!";
                            }
                            scrollLyric(lyricIndex);
                            LyricShow.LastLyricIndex = -1;
                            LyricShow.CurrentLyricIndex = -1;
                        }
                    }
                    #endregion
                }
            }
            catch { }
        }
        /// <summary>
        /// 在不跳播的情况下,根据播放的当前进度刷新歌词秀.当你需要歌词随播放进度刷新变化时,请使用这个方法.
        /// </summary>
        /// <param name="currentProgress"></param>
        public static void refreshLyricShow(double currentProgress)
        {
            if (IsPauseLyricShow == false)
            {
                //当前播放进度的索引值
                int currentProgressIndex = getLyricIndexByPlayProgress(currentProgress);
                if (LyricShow.CurrentLyricIndex != currentProgressIndex)
                {
                    //RefreshType = 1;
                    refreshLyricShow(currentProgressIndex);
                }
            }
        }
        /// <summary>
        /// 根据当前播放进度获得当前播放进度的这句歌词的索引值
        /// </summary>
        /// <param name="currentProgress">当前播放进度</param>
        /// <returns>返回当前播放进度的歌词索引值</returns>
        public static int getLyricIndexByPlayProgress(double currentProgress)
        {
            int index = -1;
            bool IsFind = false;
            foreach (double lyricTime in TimeAndLyricDictionary.Keys)
            {
                index++;
                if (lyricTime > currentProgress)
                {
                    IsFind = true;
                    break;
                }
                else
                {
                    continue;
                }
            }
            if (IsFind == false)
            {
                index = TimeAndLyricDictionary.Count - 1;
            }
            else
            {
                index = index - 1;
            }
            return index;
        }
    }
}