using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//using System.Windows.Shapes;
using System.Media;
using System.Windows.Threading;
using System.Collections;
using System.Xml;
using System.Globalization;
using System.ComponentModel;
using System.IO;
using Shell32;

namespace StudyOfWpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //设定播放状态
        private const int ORDER_PLAY = 0;
        private const int SINGLE_PLAY = 1;
        private const int RANDOM_PLAY = 2;
        private int state = ORDER_PLAY;//默认状态为顺序播放
        private const int ALL_PLAY = 3;
        private const int ALB_PLAY = 4;
        private const int MUS_PLAY = 5;
        private const int COL_MUS_PLAY = 6;
        private const int LIST_MUS_PLAY = 7;
        private int state_playing = ALL_PLAY;//默认播放全部音乐
        private bool isOpen = false;
        MediaPlayer mp;
        private bool isPlay;//判断音乐是否正在播放
        bool isPageChange = false;//判断页面是否切换
        double total;
        TimeSpan total1;
        private DispatcherTimer dTimer = new DispatcherTimer();
        private List<AllData> dataList;//所有数据
        private List<MusData> musDataList;//我的歌手
        private List<AlbData> albDataList;//我的专辑
        private List<AllData> dataList1;//我收藏的单曲
        private List<AllData> dataList2;//我创建的歌单的歌曲

        private List<LocalMus> localMusicList = new List<LocalMus>();//存储本地音乐的一些信息

        private ArrayList stateList = new ArrayList();
        //private ArrayList stateList;
        private ArrayList stateList_mus;
        private ArrayList stateList_alb;
        private ArrayList stateList_mus_col;
        private ArrayList stateList_mus_list;

        GetLyricAndLyricTime getLT;//歌词

        string path = AppDomain.CurrentDomain.BaseDirectory;
        string rootpath ;
        string rootpath1;

        LyricShow lyricShow;
        public MainWindow()
        {
            InitializeComponent();
            isPlay = false;
            mp = new MediaPlayer();
            setPlayStateList(stateList);//默认播放全部音乐

            dTimer.Tick += new EventHandler(dTimer_Tick);
            dTimer.Interval = new TimeSpan(0,0,1);

            

            rootpath = path.Substring(0, path.LastIndexOf("\\"));
            rootpath1 = rootpath.Substring(0, rootpath.LastIndexOf("\\"));
            rootpath1 = rootpath1.Substring(0, rootpath1.LastIndexOf("\\"));

            //初始化我创建的歌单
            InitSongList();
            //for (int j = 0; j < stateList.Count; j++) { stateList[j] = "false"; }
            //dTimer.Start();点击播放时才启动定时器
            Create_list();
            this.all_list.ItemsSource = dataList;

            lyricShow = new LyricShow(CanvasLyric, StackPanelCommonLyric, CanvasFocusLyric, TBFocusLyricBack, CanvasFocusLyricFore, TBFocusLyricFore);
            getLT = new GetLyricAndLyricTime(); 
        }

        private void PlayMusic(string music_path)
        {
            mp.Open(new Uri(music_path));
            //mp.Volume设置音量
            mp.MediaOpened += new EventHandler(mp_MediaOpened);
        }
        
        //获取歌曲时间
        private void mp_MediaOpened(object sender, EventArgs e)
        {
            total = mp.NaturalDuration.TimeSpan.TotalMilliseconds;
            total1 = mp.NaturalDuration.TimeSpan;
        }

        private void InitSongList()
        {
            XmlDocument xdd = new XmlDocument();
            xdd.Load(rootpath1+"/listshow.xml");
            XmlNode root = xdd.SelectSingleNode("definelist");
            XmlNodeList listNodes = root.ChildNodes;
            //遍历所有子节点
            //TreeViewItem tri = new TreeViewItem();
            foreach (XmlNode xn in listNodes)
            {
                TreeViewItem tri = new TreeViewItem();
                tri.Header = xn.InnerText;
                tree_song_list.Items.Add(tri);
            }
        }

        private void Create_list()
        {
            //stateList = new ArrayList();
            stateList.Clear();
            dataList = new List<AllData>();
            XmlDocument xdd = new XmlDocument();
            xdd.Load(rootpath1+"/mylist.xml");
            //XmlElement xe = xdd.DocumentElement;
            XmlNode root = xdd.SelectSingleNode("all_song");
            XmlNodeList listNodes = root.ChildNodes;
            //listNodes = xdd.SelectNodes("/bookstore/book/price");
            //遍历所有子节点
            int i = 0;
            foreach (XmlNode xn in listNodes)
            {
                XmlElement xe = (XmlElement)xn;
                //Console.WriteLine("节点的ID为： " + xe.GetAttribute("id"));
                XmlNodeList subList = xe.ChildNodes;
                foreach (XmlNode xmlNode in subList)
                {
                    dataList.Add(new AllData());
                    if ("name".Equals(xmlNode.Name))
                    {
                        dataList[i].Name = xmlNode.InnerText;
                    }
                    if ("musician".Equals(xmlNode.Name))
                    {
                        dataList[i].Mus = xmlNode.InnerText;
                    }
                    if ("album".Equals(xmlNode.Name))
                    {
                        dataList[i].Alb = xmlNode.InnerText;
                    }
                    if("time".Equals(xmlNode.Name))
                    {
                        dataList[i].Time = xmlNode.InnerText;
                    }
                }
                stateList.Add("false");
                i++;
            }

            //初始化从本地导入的音乐列表
            XmlDocument xdd1 = new XmlDocument();
            xdd1.Load(rootpath1+"/LocalList.xml");
            //XmlElement xe = xdd.DocumentElement;
            XmlNode root1 = xdd1.SelectSingleNode("locmus");
            XmlNodeList listNodes1 = root1.ChildNodes;
            int i1 = 0;
            foreach (XmlNode xn in listNodes1)
            {
                XmlElement xe = (XmlElement)xn;
                XmlNodeList subList = xe.ChildNodes;
                foreach (XmlNode xmlNode in subList)
                {
                    localMusicList.Add(new LocalMus());
                    if ("name".Equals(xmlNode.Name))
                    {
                        localMusicList[i1].Name = xmlNode.InnerText;
                    }
                    if ("path".Equals(xmlNode.Name))
                    {
                        localMusicList[i1].Path = xmlNode.InnerText;
                    }
                }
                i1++;
            }
            //xdd1.Save(rootpath1+"/LocalList.xml");

        }
        public void DragWindow(object sender, MouseButtonEventArgs args)
        {
            this.DragMove();
        }
        public void CloseWindow(object sender, RoutedEventArgs args)
        {
            this.Close();
        }
        public void MinWindow(object sender, RoutedEventArgs args)
        {
            this.WindowState = WindowState.Minimized;
        }
        
        //播放列表
        private void btn_open_playlist_Click(object sender, RoutedEventArgs e)
        {
            int num = box_playlist.Items.Count;
            for (int k = 0; k < num; k++)
            {
                box_playlist.Items.RemoveAt(0);
            }
            //box_playlist.ItemsSource
            switch (state_playing)
            {
                case ALL_PLAY:
                    for (int i = 0; i < dataList.Capacity / 4; i++)
                    {
                        box_playlist.Items.Add(dataList[i].Name);
                    }
                    break;
                case ALB_PLAY:
                    for (int i = 0; i < albDataList.Count; i++)
                    {
                        box_playlist.Items.Add(albDataList[i].Name);
                    }
                    break;
                case MUS_PLAY:
                    for (int i = 0; i < musDataList.Count; i++)
                    {
                        box_playlist.Items.Add(musDataList[i].Name);
                    }
                    break;
                case COL_MUS_PLAY:
                    for (int i = 0; i < dataList1.Count; i++)
                    {
                        box_playlist.Items.Add(dataList1[i].Name);
                    }
                    break;
                case LIST_MUS_PLAY:
                    for (int i = 0; i < dataList2.Count; i++)
                    {
                        box_playlist.Items.Add(dataList2[i].Name);
                    }
                    break;
            }
            if (!isOpen)
            {
                this.Pop_playlist.IsOpen = true;
                isOpen = true;
            }
            else
            {
                this.Pop_playlist.IsOpen = false;
                isOpen = false;
            }
        }
        
        private void dTimer_Tick(object sender, EventArgs e)
        {
            show_time.Content = mp.Position.ToString("mm\\:ss") + "/" + total1.ToString("mm\\:ss");
            jindu.Content = ((mp.Position.TotalMilliseconds) / total) * 100;
            if (((mp.Position.TotalMilliseconds) / total) * 100 == 100)
            {
                switch (state)
                {
                    case ORDER_PLAY:
                        do_next_Song();
                        break;
                    case SINGLE_PLAY:
                        do_single_Song();
                        break;
                    case RANDOM_PLAY:
                        do_random_Song();
                        break;
                }
            }

            //更新歌词界面
            LyricShow.refreshLyricShow(mp.Position.TotalSeconds);
        }

        //List<T> thePlayingList;
        //class cl { };
        //class lQst<T>{};
        //static class dl<T>{
        //    public static T a;
        //    private static void setPlayClass(T t){
        //        a = t;
        //    }
        //    public static T getPlayClass()
        //    {
        //        return a;
        //    }
        //};
        //private void setPlayClass(class q)
        //{

        //}
        //private class getPlayClass(class q)
        //{

        //}

        //private List setPlayList(List x){
        //    return dataList as List;
        //}
        ArrayList thePlayingStateList;

        private void setPlayStateList(ArrayList al)
        {
            thePlayingStateList = al;
        }

        //private ArrayList getPlayStateList()
        //{
        //    return thePlayingStateList;
        //}


        private bool do_play(string name)
        {
            //仍然存在和歌单同样的问题，每次添加本地音乐时，便把所有的状态都变成false了，
            //所以必须先添加本地音乐，然后才能选择播放！！！
            bool isLocal = false;
            for (int i = 0; i < localMusicList.Capacity / 2; i++)
            {
                if (localMusicList[i].Name == name)
                {
                    isLocal = true;
                    PlayMusic(localMusicList[i].Path);
                    info_mus.Content = name;
                    break;
                }
            }
            return isLocal;
        }

        //上一首顺序播放
        private void do_former_Song()
        {
            int se = 0;
            mp.Close();
            //死循环的另外一种表达方式
            //for (int i = thePlayingStateList.Count-1; i >= 0; i--)
            for (int i = 0; i < thePlayingStateList.Count; i++)
            {
                if (thePlayingStateList[i].ToString().Equals("true"))
                {
                    thePlayingStateList[i] = "false";
                    if (i ==0) { i = thePlayingStateList.Count; }
                    thePlayingStateList[i - 1] = "true";
                    se = i;
                    break;
                }
            }
            switch (state_playing)
            {
                case ALL_PLAY: 
                    if (!do_play(dataList[se - 1].Name))
                    {
                        PlayMusic(rootpath1 + "/mus/" + dataList[se - 1].Mus + " - " + dataList[se - 1].Name + ".mp3");
                        info_mus.Content = dataList[se - 1].Name + "-" + dataList[se - 1].Mus;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + dataList[se - 1].Name + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);//解析歌词->得到歌词时间和歌词
                    break;
                case ALB_PLAY:
                    if (!do_play(albDataList[se - 1].Name))
                    {
                        PlayMusic(rootpath1 + "/mus/" + albDataList[se - 1].Mus + " - " + albDataList[se - 1].Name + ".mp3");
                        info_mus.Content = albDataList[se - 1].Name + "-" + albDataList[se - 1].Mus;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + albDataList[se - 1].Name + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
                case MUS_PLAY:
                    if (!do_play(musDataList[se - 1].Name))
                    {
                        PlayMusic(rootpath1 + "/mus/" + str_mus + " - " + musDataList[se - 1].Name + ".mp3");
                        info_mus.Content = musDataList[se - 1].Name + "-" + str_mus;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + musDataList[se - 1].Name + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
                case COL_MUS_PLAY:
                    if (!do_play(dataList1[se - 1].Name))
                    {
                        PlayMusic(rootpath1 + "/mus/" + dataList1[se - 1].Mus + " - " + dataList1[se - 1].Name + ".mp3");
                        info_mus.Content = dataList1[se - 1].Name + "-" + dataList1[se - 1].Mus;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + dataList1[se - 1].Name + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
                case LIST_MUS_PLAY:
                    if (!do_play(dataList2[se - 1].Name))
                    {
                        PlayMusic(rootpath1 + "/mus/" + dataList2[se - 1].Mus + " - " + dataList2[se - 1].Name + ".mp3");
                        info_mus.Content = dataList2[se - 1].Name + "-" + dataList2[se - 1].Mus;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + dataList2[se - 1].Name + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
            }
            //PlayMusic(rootpath1 + "/mus/" +dataList[se+1].Mus+" - "+dataList[se + 1].Name + ".mp3");
            mp.Play();
            var t = this.btn_play.GetValue(TemplateProperty) as ControlTemplate;
            var b = t.FindName("btnbg", this.btn_play) as Image;
            b.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/暂停1.png"));
            LyricShow.pauseOrContinueLyricShow(false);
        }

        //下一首顺序播放
        private void do_next_Song()
        {
            int se = 0;
            mp.Close();
            //死循环的另外一种表达方式
            for (int i = 0; i < thePlayingStateList.Count; i++)
            {
                if (thePlayingStateList[i].ToString().Equals("true"))
                {
                    thePlayingStateList[i] = "false";
                    if (i >= (thePlayingStateList.Count - 1)) { i -= thePlayingStateList.Count; }
                    thePlayingStateList[i + 1] = "true";
                    se = i;
                    break;
                }
            }
            switch (state_playing)
            {
                case ALL_PLAY:
                    if (!do_play(dataList[se + 1].Name))
                    {
                        PlayMusic(rootpath1 + "/mus/" + dataList[se + 1].Mus + " - " + dataList[se + 1].Name + ".mp3");
                        info_mus.Content = dataList[se + 1].Name + "-" + dataList[se + 1].Mus;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + dataList[se + 1].Name + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
                case ALB_PLAY:
                    if (!do_play(albDataList[se + 1].Name))
                    {
                        PlayMusic(rootpath1 + "/mus/" + albDataList[se + 1].Mus + " - " + albDataList[se + 1].Name + ".mp3");
                        info_mus.Content = albDataList[se + 1].Name + "-" + albDataList[se + 1].Mus;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + albDataList[se + 1].Name + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
                case MUS_PLAY:
                    if (!do_play(musDataList[se + 1].Name))
                    {
                        PlayMusic(rootpath1 + "/mus/" + str_mus + " - " + musDataList[se + 1].Name + ".mp3");
                        info_mus.Content = musDataList[se + 1].Name + "-" + str_mus;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + musDataList[se + 1].Name + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
                case COL_MUS_PLAY:
                    if (!do_play(dataList1[se + 1].Name))
                    {
                        PlayMusic(rootpath1 + "/mus/" + dataList1[se + 1].Mus + " - " + dataList1[se + 1].Name + ".mp3");
                        info_mus.Content = dataList1[se + 1].Name + "-" + dataList1[se + 1].Mus;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + dataList1[se + 1].Name + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
                case LIST_MUS_PLAY:
                    if (!do_play(dataList2[se + 1].Name))
                    {
                        PlayMusic(rootpath1 + "/mus/" + dataList2[se + 1].Mus + " - " + dataList2[se + 1].Name + ".mp3");
                        info_mus.Content = dataList2[se + 1].Name + "-" + dataList2[se + 1].Mus;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + dataList2[se + 1].Name + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
            }
            //PlayMusic(rootpath1 + "/mus/" +dataList[se+1].Mus+" - "+dataList[se + 1].Name + ".mp3");
            mp.Play();
            var t = this.btn_play.GetValue(TemplateProperty) as ControlTemplate;
            var b = t.FindName("btnbg", this.btn_play) as Image;
            b.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/暂停1.png"));
            LyricShow.pauseOrContinueLyricShow(false);
        }
        private void do_single_Song()
        {
            //se为selected
            int se = 0;
            mp.Close();
            for (int i = 0; i < stateList.Count; i++)
            {
                if (stateList[i].ToString().Equals("true"))
                {
                    se = i;
                    break;
                }
            }
            switch (state_playing)
            {
                case ALL_PLAY:
                    if (!do_play(dataList[se].Name))
                    {
                        PlayMusic(rootpath1 + "/mus/" + dataList[se ].Mus + " - " + dataList[se ].Name + ".mp3");
                        info_mus.Content = dataList[se ].Name + "-" + dataList[se ].Mus;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + dataList[se ].Name + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
                case ALB_PLAY:
                    if (!do_play(albDataList[se + 1].Name))
                    {
                        PlayMusic(rootpath1 + "/mus/" + albDataList[se + 1].Mus + " - " + albDataList[se + 1].Name + ".mp3");
                        info_mus.Content = albDataList[se + 1].Name + "-" + albDataList[se + 1].Mus;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + albDataList[se + 1].Name + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
                case MUS_PLAY:
                    if (!do_play(musDataList[se + 1].Name))
                    {
                        PlayMusic(rootpath1 + "/mus/" + str_mus + " - " + musDataList[se + 1].Name + ".mp3");
                        info_mus.Content = musDataList[se + 1].Name + "-" + str_mus;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + musDataList[se + 1].Name + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
                case COL_MUS_PLAY:
                    if (!do_play(dataList1[se + 1].Name))
                    {
                        PlayMusic(rootpath1 + "/mus/" + dataList1[se + 1].Mus + " - " + dataList1[se + 1].Name + ".mp3");
                        info_mus.Content = dataList1[se + 1].Name + "-" + dataList1[se + 1].Mus;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + dataList1[se + 1].Name + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
                case LIST_MUS_PLAY:
                    if (!do_play(dataList2[se + 1].Name))
                    {
                        PlayMusic(rootpath1 + "/mus/" + dataList2[se + 1].Mus + " - " + dataList2[se + 1].Name + ".mp3");
                        info_mus.Content = dataList2[se + 1].Name + "-" + dataList2[se + 1].Mus;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + dataList2[se + 1].Name + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
            }
            //PlayMusic(rootpath1 + "/mus/" + dataList[se].Name + ".mp3");
            mp.Play();
            var t = this.btn_play.GetValue(TemplateProperty) as ControlTemplate;
            var b = t.FindName("btnbg", this.btn_play) as Image;
            b.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/暂停1.png"));
            LyricShow.pauseOrContinueLyricShow(false);
        }
        private void do_random_Song()
        {
            int se = 0;
            mp.Close();
            for (int i = 0; i < stateList.Count; i++)
            {
                if (stateList[i].ToString().Equals("true"))
                {
                    stateList[i] = "false";
                    se = i;
                    Random ran = new Random();
                    se = ran.Next(stateList.Count);
                    while (se == i)
                    {
                        se = ran.Next(stateList.Count);
                        if (se != i) break;
                    }
                    stateList[se] = "true";
                    break;
                }
            }
            switch (state_playing)
            {
                case ALL_PLAY:
                    if (!do_play(dataList[se + 1].Name))
                    {
                        PlayMusic(rootpath1 + "/mus/" + dataList[se + 1].Mus + " - " + dataList[se + 1].Name + ".mp3");
                        info_mus.Content = dataList[se + 1].Name + "-" + dataList[se + 1].Mus;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + dataList[se + 1].Name + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
                case ALB_PLAY:
                    if (!do_play(albDataList[se + 1].Name))
                    {
                        PlayMusic(rootpath1 + "/mus/" + albDataList[se + 1].Mus + " - " + albDataList[se + 1].Name + ".mp3");
                        info_mus.Content = albDataList[se + 1].Name + "-" + albDataList[se + 1].Mus;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + albDataList[se + 1].Name + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
                case MUS_PLAY:
                    if (!do_play(musDataList[se + 1].Name))
                    {
                        PlayMusic(rootpath1 + "/mus/" + str_mus + " - " + musDataList[se + 1].Name + ".mp3");
                        info_mus.Content = musDataList[se + 1].Name + "-" + str_mus;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + musDataList[se + 1].Name + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
                case COL_MUS_PLAY:
                    if (!do_play(dataList1[se + 1].Name))
                    {
                        PlayMusic(rootpath1 + "/mus/" + dataList1[se + 1].Mus + " - " + dataList1[se + 1].Name + ".mp3");
                        info_mus.Content = dataList1[se + 1].Name + "-" + dataList1[se + 1].Mus;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + dataList1[se + 1].Name + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
                case LIST_MUS_PLAY:
                    if (!do_play(dataList2[se + 1].Name))
                    {
                        PlayMusic(rootpath1 + "/mus/" + dataList2[se + 1].Mus + " - " + dataList2[se + 1].Name + ".mp3");
                        info_mus.Content = dataList2[se + 1].Name + "-" + dataList2[se + 1].Mus;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + dataList2[se + 1].Name + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
            }
            //PlayMusic(rootpath1 + "/mus/" + dataList[se].Name + ".mp3");
            mp.Play();
            var t = this.btn_play.GetValue(TemplateProperty) as ControlTemplate;
            var b = t.FindName("btnbg", this.btn_play) as Image;
            b.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/暂停1.png"));
            LyricShow.pauseOrContinueLyricShow(false);
        }

        //进行播放
        int count = 0;
        int se;
        string str_mus;
        private void just_do_play_music()
        {
            switch (state_playing)
            {
                case ALL_PLAY:
                    AllData d = all_list.SelectedItem as AllData;
                    se = all_list.SelectedIndex;
                    stateList[se] = "true";
                    string str = d.Name;
                    string geshou = d.Mus;
                    if (!do_play(str))
                    {
                        PlayMusic(rootpath1 + "/mus/" + geshou + " - " + str + ".mp3");
                        info_mus.Content = str + "-" + geshou;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + str + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
                case ALB_PLAY:
                    AlbData d1 = alb_list.SelectedItem as AlbData;
                    se = alb_list.SelectedIndex;
                    stateList_alb[se] = "true";
                    string str1 = d1.Name;
                    string geshou1 = d1.Mus;
                    if (!do_play(str1))
                    {
                        PlayMusic(rootpath1 + "/mus/" + geshou1 + " - " + str1 + ".mp3");
                        info_mus.Content = str1 + "-" + geshou1;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + str1 + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
                case MUS_PLAY:
                    MusData d2 = mus_list.SelectedItem as MusData;
                    se = mus_list.SelectedIndex;
                    stateList_mus[se] = "true";
                    string str2 = d2.Name;
                    //string geshou2 = d2.Mus;
                    //string geshou2 = my_mus.SelectedItem.ToString();
                    if (!do_play(str2))
                    {
                        PlayMusic(rootpath1 + "/mus/" + str_mus + " - " + str2 + ".mp3");
                        info_mus.Content = str2 + "-" + str_mus;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + str2 + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
                case COL_MUS_PLAY:
                    AllData d3 = all_list.SelectedItem as AllData;
                    se = all_list.SelectedIndex;
                    //stateList[se] = "true";
                    stateList_mus_col[se] = "true";
                    string str3 = d3.Name;
                    string geshou3 = d3.Mus;
                    if (!do_play(str3))
                    {
                        PlayMusic(rootpath1 + "/mus/" + geshou3 + " - " + str3 + ".mp3");
                        info_mus.Content = str3 + "-" + geshou3;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + str3 + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
                case LIST_MUS_PLAY:
                    AllData d4 = my_song_list.SelectedItem as AllData;
                    se = my_song_list.SelectedIndex;
                    //stateList[se] = "true";
                    stateList_mus_list[se] = "true";
                    string str4 = d4.Name;
                    string geshou4 = d4.Mus;
                    if (!do_play(str4))
                    {
                        PlayMusic(rootpath1 + "/mus/" + geshou4 + " - " + str4 + ".mp3");
                        info_mus.Content = str4 + "-" + geshou4;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + str4 + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
            }

            dTimer.Start();

            //mp.Play();
            //isPlay = true;
            //isPageChange = false;
        }
        private void do_play_music()
        {
            //for (int j = 0; j < stateList.Count; j++) { stateList[j] = "false"; }
            //解决在全部音乐界面时，暂停情况下不能进行上一首播放的情况
            if (!isPlay || isPageChange)
            {
                if (count == 0 || isPageChange)
                {
                    just_do_play_music();
                }
                mp.Play();
                isPlay = true;
                isPageChange = false;
                //Img_play1.Source=new BitmapImage(new Uri("pack://application:,,,/imgs/暂停.png"));
                var t = this.btn_play.GetValue(TemplateProperty) as ControlTemplate;
                var b = t.FindName("btnbg", this.btn_play) as Image;
                b.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/暂停1.png"));//可以修改这个值
                //};
                //btn_play = new Button
                //{
                //    //不能用相对路径
                //    Image.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/暂停.png"))
                //};
                LyricShow.pauseOrContinueLyricShow(false);
                //btnbg.Source = new BitmapImage(new Uri("/imgs/播放2.png"));
                //bofang.Content = "暂停";
            }
            else
            {
                mp.Pause();
                var t = this.btn_play.GetValue(TemplateProperty) as ControlTemplate;
                var b = t.FindName("btnbg", this.btn_play) as Image;
                b.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/播放1-1.png"));//可以修改这个值
                //btn_play.Background = new ImageBrush
                //{
                //    //不能用相对路径
                //    ImageSource = new BitmapImage(new Uri("pack://application:,,,/imgs/播放2.png"))
                //};
                LyricShow.pauseOrContinueLyricShow(true);
                //btnbg.Source = new BitmapImage(new Uri("/imgs/暂停.png"));
                //bofang.Content = "播放";
                isPlay = false;
            }
            count++;
        }
        private void btn_play_Click(object sender, RoutedEventArgs e)
        {
            do_play_music();
            result_count = -1;
            ICollectionView dataView = CollectionViewSource.GetDefaultView(all_list.ItemsSource);
            dataView.Refresh();
        }
        //上一首播放
        private void btn_former_Click(object sender, RoutedEventArgs e)
        {
            do_former_Song();
        }

        //下一首播放
        private void btn_next_Click(object sender, RoutedEventArgs e)
        {
            do_next_Song();
        }
        int count_danru1 = 0;
        //本地音乐库
        private void btn_danru1_Click(object sender, RoutedEventArgs e)
        {
            all_list.ItemsSource = dataList;
            //这里存在一个bug,开始的时候不能点击 我的全部音乐 这一按钮？似乎不存在!
            count_danru1++;
            //if (count_danru1 > 0 && isPageChange == true) { isPageChange = true; }
            if (count_danru1 > 0) { isPageChange = true; }
            setPlayStateList(stateList);
            state_playing = ALL_PLAY;
            addMusic.Visibility = Visibility.Visible;
            do_all_play_mus.Margin = new Thickness(0,63,573,10);
            add_to_list_all.Margin = new Thickness(236, 64, 0, 10);
            Change3.Visibility = Visibility.Hidden;
            Change4.Visibility = Visibility.Hidden;
            Change2.Visibility = Visibility.Hidden;
            Change1.Visibility = Visibility.Visible;
        }

        //我的歌手
        private void btn_danru2_Click(object sender, RoutedEventArgs e)
        {
            int num = my_mus.Items.Count;//清空listbox
            for (int k = 0; k < num; k++)
            {
                my_mus.Items.RemoveAt(0);
            }
            my_mus.Items.Add(dataList[0].Mus);//先添加第一个歌手
            for (int i = 1; i < dataList.Capacity / 4; i++)
            {
                bool isSame = true;
                for (int j = 0; j < i; j++)
                {
                    if (dataList[i].Mus == dataList[j].Mus)
                    {
                        isSame = false;
                    }
                }
                if (isSame) { my_mus.Items.Add(dataList[i].Mus); }
            }
            if (!isOpen)
            {
                this.Pop_danru2.IsOpen = true;
                isOpen = true;
            }
            else
            {
                this.Pop_danru2.IsOpen = false;
                isOpen = false;
            }
        }

        //我的专辑
        private void btn_danru3_Click(object sender, RoutedEventArgs e)
        {
            int num = my_album.Items.Count;
            for (int k = 0; k < num; k++)
            {
                my_album.Items.RemoveAt(0);
            }
            my_album.Items.Add(dataList[0].Alb);
            for (int i = 1; i < dataList.Capacity / 4; i++)
            {
                bool isSame = true;
                for (int j = 0; j < i; j++)
                {
                    if (dataList[i].Alb == dataList[j].Alb)
                    {
                        isSame = false;
                    }
                }
                if (isSame) { my_album.Items.Add(dataList[i].Alb); }
            }
            if (!isOpen)
            {
                this.Pop_danru3.IsOpen = true;
                isOpen = true;
            }
            else
            {
                this.Pop_danru3.IsOpen = false;
                isOpen = false;
            }
        }

        //我的专辑
        private void my_album_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int cou = 0;//albDataList中的项数
            string str = my_album.SelectedItem.ToString();
            albDataList = new List<AlbData>();
            stateList_alb  = new ArrayList();
            for (int i = 0; i < dataList.Capacity / 4; i++)
            {
                if (dataList[i].Alb == str)
                {
                    albDataList.Add(new AlbData());
                    albDataList[cou].Mus = dataList[i].Mus;
                    albDataList[cou].Name = dataList[i].Name;
                    albDataList[cou].Time = dataList[i].Time;
                    stateList_alb.Add("false");
                    cou++;
                }
            }
            alb_list.ItemsSource = albDataList;
            setPlayStateList(stateList_alb);
            state_playing = ALB_PLAY;
            isPageChange = true;
            alb_name.Content = str;//显示专辑名称
            alb_mus_name.Content = albDataList[0].Mus;//显示歌手
            this.btn_Collect_Alb.Background = Brushes.White;
            Change1.Visibility = Visibility.Hidden;
            Change3.Visibility = Visibility.Hidden;
            Change4.Visibility = Visibility.Hidden;
            Change2.Visibility = Visibility.Visible;
        }
        //我的歌手
        private void my_mus_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            stateList_mus = new ArrayList();
            int cou = 0;//albDataList中的项数
            string str = my_mus.SelectedItem.ToString();
            //保存歌手的名字
            str_mus = str;
            change_mus.Content = str_mus;
            musDataList = new List<MusData>();
            for (int i = 0; i < dataList.Capacity / 4; i++)
            {
                if (dataList[i].Mus == str)
                {
                    musDataList.Add(new MusData());
                    musDataList[cou].Alb = dataList[i].Alb;
                    musDataList[cou].Name = dataList[i].Name;
                    musDataList[cou].Time = dataList[i].Time;
                    stateList_mus.Add("false");
                    cou++;
                }
            }
            mus_list.ItemsSource = musDataList;
            setPlayStateList(stateList_mus);
            state_playing = MUS_PLAY;
            isPageChange = true;
            Change1.Visibility = Visibility.Hidden;
            Change4.Visibility = Visibility.Hidden;
            Change2.Visibility = Visibility.Hidden;
            Change3.Visibility = Visibility.Visible;
        }

        //int localCount = 0;
        //从本地添加音乐
        private void addMusic_Click(object sender, RoutedEventArgs e)
        {
            //int localCount = localMusicList.Capacity / 2;
            int localCount = localMusicList.Count;

            XmlDocument xdd = new XmlDocument();
            xdd.Load(rootpath1+"/mylist.xml");
            XmlElement root = xdd.DocumentElement;
            XmlElement newMusic = xdd.CreateElement("music4");
            XmlElement newName = xdd.CreateElement("name");
            XmlElement newMusician = xdd.CreateElement("musician");
            XmlElement newAlbum = xdd.CreateElement("album");
            XmlElement newTime = xdd.CreateElement("time");
            XmlElement newIscollect = xdd.CreateElement("iscollect");
            XmlElement newIsalbcollect = xdd.CreateElement("isalbcollect");
            XmlElement newSonglist = xdd.CreateElement("songlist");
            Microsoft.Win32.OpenFileDialog op = new Microsoft.Win32.OpenFileDialog();
            op.Filter = "音乐文件(*.mp3)|*.mp3";
            op.Title = "音乐播放器";
            op.ShowDialog();
            //System.Windows.MessageBox.Show(op.SafeFileName);//文件名
            //System.Windows.MessageBox.Show(op.FileName);//路径


            string localName = op.SafeFileName;
            //localName.Substring(0,1);
            //localName.IndexOf(".");
            //System.Windows.MessageBox.Show(localName.Substring(0, localName.IndexOf(".")));

            if (localName != null && localName != "")
            {
                ShellClass sh = new ShellClass();
                Folder dir = sh.NameSpace(Path.GetDirectoryName(op.FileName));
                FolderItem item = dir.ParseName(Path.GetFileName(op.FileName));

                string realName = localName.Substring(0, localName.IndexOf("."));
                localMusicList.Add(new LocalMus());
                //localMusicList[localCount].Name = realName;
                localMusicList[localCount].Name = dir.GetDetailsOf(item, 21);
                localMusicList[localCount].Path = op.FileName;
                //int j = localMusicList.Capacity / 2;
                //int j=localMusicList.Count;
                //XmlText name = xdd.CreateTextNode(realName);

                XmlText name = xdd.CreateTextNode(dir.GetDetailsOf(item, 21));
                XmlText musician = xdd.CreateTextNode(dir.GetDetailsOf(item, 13));
                XmlText album = xdd.CreateTextNode(dir.GetDetailsOf(item, 14));
                XmlText time = xdd.CreateTextNode(dir.GetDetailsOf(item, 27));
                XmlText iscollect = xdd.CreateTextNode("false");
                XmlText isalbcollect = xdd.CreateTextNode("false");
                XmlText songlist = xdd.CreateTextNode("null");

                newMusic.AppendChild(newName);
                newMusic.AppendChild(newMusician);
                newMusic.AppendChild(newAlbum);
                newMusic.AppendChild(newTime);
                newMusic.AppendChild(newIscollect);
                newMusic.AppendChild(newIsalbcollect);
                newMusic.AppendChild(newSonglist);

                newName.AppendChild(name);
                newMusician.AppendChild(musician);
                newAlbum.AppendChild(album);
                newTime.AppendChild(time);
                newIscollect.AppendChild(iscollect);
                newIsalbcollect.AppendChild(isalbcollect);
                newSonglist.AppendChild(songlist);

                root.AppendChild(newMusic);
                xdd.Save(rootpath1 + "/mylist.xml");
                Create_list();
                this.all_list.ItemsSource = dataList;

                XmlDocument xdd1 = new XmlDocument();
                xdd1.Load(rootpath1 + "/LocalList.xml");
                XmlElement root1 = xdd1.DocumentElement;
                XmlElement newMusic1 = xdd1.CreateElement("music");
                XmlElement newName1 = xdd1.CreateElement("name");
                XmlElement newPath1 = xdd1.CreateElement("path");

                //XmlText name1 = xdd1.CreateTextNode(realName);
                XmlText name1 = xdd1.CreateTextNode(dir.GetDetailsOf(item, 21));
                XmlText path1 = xdd1.CreateTextNode(op.FileName);

                newMusic1.AppendChild(newName1);
                newMusic1.AppendChild(newPath1);

                newName1.AppendChild(name1);
                newPath1.AppendChild(path1);

                root1.AppendChild(newMusic1);
                xdd1.Save(rootpath1 + "/LocalList.xml");
            }
            //localCount++;
        }

        private void random_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            state = RANDOM_PLAY;
        }

        private void order_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            state = ORDER_PLAY;
        }

        private void single_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            state = SINGLE_PLAY;
        }

        private void btn_playmode_Click(object sender, RoutedEventArgs e)
        {
            if (!isOpen)
            {
                this.Pop_Play_State.IsOpen = true;
                isOpen = true;
            }
            else
            {
                this.Pop_Play_State.IsOpen = false;
                isOpen = false;
            }
        }

        //收藏单曲
        private void btn_collect_Click(object sender, RoutedEventArgs e)
        {
            string des = info_mus.Content.ToString();
            string na = null, mus = null;
            string des_xml;
            //dataList = new List<AllData>();
            XmlDocument xdd = new XmlDocument();
            xdd.Load(rootpath1+"/mylist.xml");
            //XmlElement xe = xdd.DocumentElement;
            XmlNode root = xdd.SelectSingleNode("all_song");
            XmlNodeList listNodes = root.ChildNodes;
            //listNodes = xdd.SelectNodes("/bookstore/book/price");
            //遍历所有子节点
            int i = 0;
            foreach (XmlNode xn in listNodes)
            {
                XmlElement xe = (XmlElement)xn;
                //Console.WriteLine("节点的ID为： " + xe.GetAttribute("id"));
                XmlNodeList subList = xe.ChildNodes;
                foreach (XmlNode xmlNode in subList)
                {
                    //dataList.Add(new AllData());
                    if ("name".Equals(xmlNode.Name))
                    {
                        na = xmlNode.InnerText;
                    }
                    if ("musician".Equals(xmlNode.Name))
                    {
                        mus = xmlNode.InnerText;
                    }
                    if ("album".Equals(xmlNode.Name))
                    {
                       
                    }
                    if ("time".Equals(xmlNode.Name))
                    {
                        
                    }
                    if ("iscollect".Equals(xmlNode.Name))
                    {
                        //des_xml = na + "-" + mus;
                        des_xml = na;
                        if (des_xml.Equals(des))
                        {
                            //MessageBox.Show(des_xml);
                            xmlNode.InnerText = "true";
                            //卧槽，改完之后又忘记保存xml了！！
                        }
                    }
                }
                i++;
            }
            xdd.Save(rootpath1+"/mylist.xml");
        }

        //收藏专辑
        private void btn_Collect_Alb_Click(object sender, RoutedEventArgs e)
        {
            string nameOfAlb = alb_name.Content.ToString();
            bool isFirstAlbStateChanged = false;

            string alb_xml = null;
            //dataList = new List<AllData>();
            XmlDocument xdd = new XmlDocument();
            xdd.Load(rootpath1+"/mylist.xml");
            //XmlElement xe = xdd.DocumentElement;
            XmlNode root = xdd.SelectSingleNode("all_song");
            XmlNodeList listNodes = root.ChildNodes;
            //listNodes = xdd.SelectNodes("/bookstore/book/price");
            //遍历所有子节点
            foreach (XmlNode xn in listNodes)
            {
                XmlElement xe = (XmlElement)xn;
                //Console.WriteLine("节点的ID为： " + xe.GetAttribute("id"));
                XmlNodeList subList = xe.ChildNodes;
                foreach (XmlNode xmlNode in subList)
                {
                    //dataList.Add(new AllData());
                    if ("name".Equals(xmlNode.Name))
                    {
                        
                    }
                    if ("musician".Equals(xmlNode.Name))
                    {
                        
                    }
                    if ("album".Equals(xmlNode.Name))
                    {
                        alb_xml = xmlNode.InnerText;
                    }
                    if ("time".Equals(xmlNode.Name))
                    {

                    }
                    if ("iscollect".Equals(xmlNode.Name))
                    {
                        
                    }
                    if ("isalbcollect".Equals(xmlNode.Name))
                    {
                        if (nameOfAlb.Equals(alb_xml))
                        {
                            xmlNode.InnerText = "true";
                            isFirstAlbStateChanged = true;
                        }
                    }
                }
                if (isFirstAlbStateChanged) break;
            }
            xdd.Save(rootpath1+"/mylist.xml");
        }

        //显示我收藏的单曲
        private void btn_collect_mus_Click(object sender, RoutedEventArgs e)
        {
            //解决Bug:未设置其他界面不可见！！！

            int cou = 0;//收藏歌曲的个数
            string na = null, mus = null, alb = null,time=null;
            stateList_mus_col = new ArrayList();
            //重新创建一个列表
            dataList1 = new List<AllData>();
            XmlDocument xdd = new XmlDocument();
            xdd.Load(rootpath1+"/mylist.xml");
            //XmlElement xe = xdd.DocumentElement;
            XmlNode root = xdd.SelectSingleNode("all_song");
            XmlNodeList listNodes = root.ChildNodes;
            //listNodes = xdd.SelectNodes("/bookstore/book/price");
            //遍历所有子节点
            foreach (XmlNode xn in listNodes)
            {
                XmlElement xe = (XmlElement)xn;
                //Console.WriteLine("节点的ID为： " + xe.GetAttribute("id"));
                XmlNodeList subList = xe.ChildNodes;
                foreach (XmlNode xmlNode in subList)
                {
                    if ("name".Equals(xmlNode.Name))
                    {
                        na = xmlNode.InnerText;
                    }
                    if ("musician".Equals(xmlNode.Name))
                    {
                        mus = xmlNode.InnerText;
                    }
                    if ("album".Equals(xmlNode.Name))
                    {
                        alb = xmlNode.InnerText;
                    }
                    if ("time".Equals(xmlNode.Name))
                    {
                        time = xmlNode.InnerText;
                    }
                    if ("iscollect".Equals(xmlNode.Name))
                    {
                        if (xmlNode.InnerText.Equals("true"))
                        {
                            dataList1.Add(new AllData());
                            dataList1[cou].Name = na;
                            dataList1[cou].Mus = mus;
                            dataList1[cou].Alb = alb;
                            dataList1[cou].Time = time;
                            stateList_mus_col.Add("false");
                            //stateList_alb.Add("false");
                            cou++;
                        }
                    }
                }
            }
            //多次收藏之后，《怎么了》下一首突然跳到了模特一首歌，然后一直不能切换
            //造成这个的原因可能是切换到收藏界面之后不能正常的播放
            all_list.ItemsSource = dataList1;
            setPlayStateList(stateList_mus_col);
            state_playing = COL_MUS_PLAY;
            isPageChange = true;
            addMusic.Visibility = Visibility.Hidden;
            do_all_play_mus.Margin = new Thickness(0, 64, 655, 9);
            add_to_list_all.Margin = new Thickness(176, 63, 0, 11);
            
            Change3.Visibility = Visibility.Hidden;
            Change4.Visibility = Visibility.Hidden;
            Change2.Visibility = Visibility.Hidden;
            Change1.Visibility = Visibility.Visible;
        }

        //显示我收藏的专辑列表
        private void btn_collect_alb_Click_1(object sender, RoutedEventArgs e)
        {
            string alb_name = null;
            int num = my_album_collect.Items.Count;
            for (int k = 0; k < num; k++)
            {
                my_album_collect.Items.RemoveAt(0);
            }

            XmlDocument xdd = new XmlDocument();
            xdd.Load(rootpath1+"/mylist.xml");
            
            XmlNode root = xdd.SelectSingleNode("all_song");
            XmlNodeList listNodes = root.ChildNodes;
            foreach (XmlNode xn in listNodes)
            {
                XmlElement xe = (XmlElement)xn;
                XmlNodeList subList = xe.ChildNodes;
                foreach (XmlNode xmlNode in subList)
                {
                    if ("name".Equals(xmlNode.Name))
                    {
                    }
                    if ("musician".Equals(xmlNode.Name))
                    {
                    }
                    if ("album".Equals(xmlNode.Name))
                    {
                        alb_name = xmlNode.InnerText;
                    }
                    if ("time".Equals(xmlNode.Name))
                    {
                    }
                    if ("iscollect".Equals(xmlNode.Name))
                    {
                        
                    }
                    if ("isalbcollect".Equals(xmlNode.Name))
                    {
                        if (xmlNode.InnerText.Equals("true"))
                        {
                            my_album_collect.Items.Add(alb_name);
                        }
                    }
                }
            }
            if (!isOpen)
            {
                this.Pop_alb_collect.IsOpen = true;
                isOpen = true;
            }
            else
            {
                this.Pop_alb_collect.IsOpen = false;
                isOpen = false;
            }
        }

        //显示我收藏的专辑
        private void my_album_collect_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int cou = 0;//albDataList中的项数
            string str = my_album_collect.SelectedItem.ToString();
            albDataList = new List<AlbData>();
            stateList_alb = new ArrayList();
            for (int i = 0; i < dataList.Capacity / 4; i++)
            {
                if (dataList[i].Alb == str)
                {
                    albDataList.Add(new AlbData());
                    albDataList[cou].Mus = dataList[i].Mus;
                    albDataList[cou].Name = dataList[i].Name;
                    albDataList[cou].Time = dataList[i].Time;
                    stateList_alb.Add("false");
                    cou++;
                }
            }
            alb_list.ItemsSource = albDataList;
            setPlayStateList(stateList_alb);
            state_playing = ALB_PLAY;
            isPageChange = true;
            alb_name.Content = str;//显示专辑名称
            alb_mus_name.Content = albDataList[0].Mus;//显示歌手
            this.btn_Collect_Alb.Background = Brushes.Red;
            Change1.Visibility = Visibility.Hidden;
            Change3.Visibility = Visibility.Hidden;
            Change4.Visibility = Visibility.Hidden;
            Change2.Visibility = Visibility.Visible;
        }

        //创建歌单
        private void btn_tianjia_Click(object sender, RoutedEventArgs e)
        {
            WindowList wl = new WindowList();
            wl.ShowDialog();//当打开的窗口关闭后才返回和show()不一样
            TreeViewItem tri = new TreeViewItem();
            tri.Header = WindowList.my_listname;
            tree_song_list.Items.Add(tri);

            if (WindowList.my_listname != null && WindowList.my_listname != "")//""不行，null才行
            //if (!WindowList.my_listname.Equals(null))
            {
                //改变xml的值
                XmlDocument xdd = new XmlDocument();
                xdd.Load(rootpath1+"/listshow.xml");
                XmlElement root = xdd.DocumentElement;

                XmlElement newMusic = xdd.CreateElement("list");

                newMusic.InnerText = WindowList.my_listname;

                root.AppendChild(newMusic);
                xdd.Save(rootpath1+"/listshow.xml");
            }
            
        }

        //显示我创建的歌单的歌曲
        //进行双击时，一定要先选中！
        private void tree_song_list_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem tvi = new TreeViewItem();
            tvi = tree_song_list.SelectedItem as TreeViewItem;//e.Source as TreeViewItem;
            string str = tvi.Header.ToString();//MessageBox.Show(s);

            int cou = 0;//收藏歌曲的个数
            string na = null, mus = null, alb = null, time = null;
            stateList_mus_list = new ArrayList();

            //重新创建一个列表
            dataList2 = new List<AllData>();

            XmlDocument xdd = new XmlDocument();
            xdd.Load(rootpath1+"/mylist.xml");
            //XmlElement xe = xdd.DocumentElement;
            XmlNode root = xdd.SelectSingleNode("all_song");
            XmlNodeList listNodes = root.ChildNodes;
            //listNodes = xdd.SelectNodes("/bookstore/book/price");
            //遍历所有子节点
            foreach (XmlNode xn in listNodes)
            {
                XmlElement xe = (XmlElement)xn;
                //Console.WriteLine("节点的ID为： " + xe.GetAttribute("id"));
                XmlNodeList subList = xe.ChildNodes;
                foreach (XmlNode xmlNode in subList)
                {
                    if ("name".Equals(xmlNode.Name))
                    {
                        na = xmlNode.InnerText;
                    }
                    if ("musician".Equals(xmlNode.Name))
                    {
                        mus = xmlNode.InnerText;
                    }
                    if ("album".Equals(xmlNode.Name))
                    {
                        alb = xmlNode.InnerText;
                    }
                    if ("time".Equals(xmlNode.Name))
                    {
                        time = xmlNode.InnerText;
                    }
                    if ("iscollect".Equals(xmlNode.Name))
                    {
                    }
                    if ("isalbcollect".Equals(xmlNode.Name))
                    {
                    }
                    if ("songlist".Equals(xmlNode.Name))
                    {
                        if (xmlNode.InnerText.Equals(str))
                        {
                            dataList2.Add(new AllData());
                            dataList2[cou].Name = na;
                            dataList2[cou].Mus = mus;
                            dataList2[cou].Alb = alb;
                            dataList2[cou].Time = time;
                            stateList_mus_list.Add("false");
                            //stateList_alb.Add("false");
                            cou++;
                        }
                    }
                }
            }
            //多次收藏之后，《怎么了》下一首突然跳到了模特一首歌，然后一直不能切换
            //造成这个的原因可能是切换到收藏界面之后不能正常的播放

            //all_list.ItemsSource = dataList1;
            my_song_list.ItemsSource = dataList2;
            setPlayStateList(stateList_mus_list);
            state_playing = LIST_MUS_PLAY;
            isPageChange = true;
            change_mylist.Content = str;
            Change3.Visibility = Visibility.Hidden;
            Change1.Visibility = Visibility.Hidden;
            Change2.Visibility = Visibility.Hidden;
            Change4.Visibility = Visibility.Visible;
        }

        private void show_song_list()//展现所有的歌单listbox
        {
            int num = my_list_show.Items.Count;
            for (int k = 0; k < num; k++)
            {
                my_list_show.Items.RemoveAt(0);
            }

            XmlDocument xdd = new XmlDocument();
            xdd.Load(rootpath1+"/listshow.xml");

            XmlNode root = xdd.SelectSingleNode("definelist");
            XmlNodeList listNodes = root.ChildNodes;
            foreach (XmlNode xn in listNodes)
            {
                my_list_show.Items.Add(xn.InnerText);
            }
            if (!isOpen)//add_to_list_mus;add_to_list_all;add_to_list_alb
            {
                this.Pop_show_list.IsOpen = true;
                isOpen = true;
            }
            else
            {
                this.Pop_show_list.IsOpen = false;
                isOpen = false;
            }
        }

        private void add_list(string str_list)//添加到创建的播放列表
        {
            switch (state_playing)
            {
                case ALL_PLAY:
                    AllData d = all_list.SelectedItem as AllData;
                    string str = d.Name;//歌名
                    string str_xml = null;
                    XmlDocument xdd = new XmlDocument();
                    xdd.Load(rootpath1+"/mylist.xml");

                    XmlNode root = xdd.SelectSingleNode("all_song");
                    XmlNodeList listNodes = root.ChildNodes;
                    foreach (XmlNode xn in listNodes)
                    {
                        XmlElement xe = (XmlElement)xn;
                        XmlNodeList subList = xe.ChildNodes;
                        foreach (XmlNode xmlNode in subList)
                        {
                            if ("name".Equals(xmlNode.Name))
                            {
                                str_xml = xmlNode.InnerText;
                            }
                            if ("musician".Equals(xmlNode.Name))
                            {
                            }
                            if ("album".Equals(xmlNode.Name))
                            {
                            }
                            if ("time".Equals(xmlNode.Name))
                            {
                            }
                            if ("iscollect".Equals(xmlNode.Name))
                            {
                            }
                            if ("isalbcollect".Equals(xmlNode.Name))
                            {
                            }
                            if ("songlist".Equals(xmlNode.Name))
                            {
                                if (str_xml.Equals(str))
                                {
                                    xmlNode.InnerText = str_list;
                                }
                            }
                        }
                    }
                    xdd.Save(rootpath1+"/mylist.xml");
                    break;

                case ALB_PLAY://?
                    AlbData d1 = alb_list.SelectedItem as AlbData;
                    string str1 = d1.Name;//歌名
                    string str_xml1 = null;

                    XmlDocument xdd1 = new XmlDocument();
                    xdd1.Load(rootpath1+"/mylist.xml");

                    XmlNode root1 = xdd1.SelectSingleNode("all_song");
                    XmlNodeList listNodes1 = root1.ChildNodes;
                    foreach (XmlNode xn in listNodes1)
                    {
                        XmlElement xe = (XmlElement)xn;
                        XmlNodeList subList = xe.ChildNodes;
                        foreach (XmlNode xmlNode in subList)
                        {
                            if ("name".Equals(xmlNode.Name))
                            {
                                str_xml1 = xmlNode.InnerText;
                            }
                            if ("musician".Equals(xmlNode.Name))
                            {
                            }
                            if ("album".Equals(xmlNode.Name))
                            {
                            }
                            if ("time".Equals(xmlNode.Name))
                            {
                            }
                            if ("iscollect".Equals(xmlNode.Name))
                            {
                            }
                            if ("isalbcollect".Equals(xmlNode.Name))
                            {
                            }
                            if ("songlist".Equals(xmlNode.Name))
                            {
                                if (str_xml1.Equals(str1))
                                {
                                    xmlNode.InnerText = str_list;
                                }
                            }
                        }
                    }
                    xdd1.Save(rootpath1+"/mylist.xml");
                    break;

                case MUS_PLAY://?
                    MusData d2 = mus_list.SelectedItem as MusData;

                    string str2 = d2.Name;//歌名
                    string str_xml2 = null;

                    XmlDocument xdd2 = new XmlDocument();
                    xdd2.Load(rootpath1+"/mylist.xml");

                    XmlNode root2 = xdd2.SelectSingleNode("all_song");
                    XmlNodeList listNodes2 = root2.ChildNodes;
                    foreach (XmlNode xn in listNodes2)
                    {
                        XmlElement xe = (XmlElement)xn;
                        XmlNodeList subList = xe.ChildNodes;
                        foreach (XmlNode xmlNode in subList)
                        {
                            if ("name".Equals(xmlNode.Name))
                            {
                                str_xml2 = xmlNode.InnerText;
                            }
                            if ("musician".Equals(xmlNode.Name))
                            {
                            }
                            if ("album".Equals(xmlNode.Name))
                            {
                            }
                            if ("time".Equals(xmlNode.Name))
                            {
                            }
                            if ("iscollect".Equals(xmlNode.Name))
                            {
                            }
                            if ("isalbcollect".Equals(xmlNode.Name))
                            {
                            }
                            if ("songlist".Equals(xmlNode.Name))
                            {
                                if (str_xml2.Equals(str2))
                                {
                                    xmlNode.InnerText = str_list;
                                }
                            }
                        }
                    }
                    xdd2.Save(rootpath1+"/mylist.xml");
                    break;

            }
        }

        private void my_list_show_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string str = my_list_show.SelectedItem.ToString();
            add_list(str);
        }

        private void add_to_list_all_Click(object sender, RoutedEventArgs e)//添加至歌单
        {
            //已知bug：中途添加歌曲到歌单时，不能进行识别播放！！！？
            //错误原因在于每次跳转到歌单界面时所有数据都会重新改变(使得状态数组均为false)
            //即使有歌曲之前正在播放
            show_song_list();
        }

        private void add_to_list_alb_Click(object sender, RoutedEventArgs e)
        {
            Pop_show_list.PlacementTarget = add_to_list_alb;
            show_song_list();
        }

        private void add_to_list_mus_Click(object sender, RoutedEventArgs e)
        {
            Pop_show_list.PlacementTarget = add_to_list_mus;
            show_song_list();
        }

        double value;
        private void m_pg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Point po = e.GetPosition();
            //Point formPoint = this.PointToClient(Control.MousePosition);
            Point po = e.GetPosition(m_pg);
            value = (po.X / m_pg.Width)*100;
            m_pg.Value = value;
            //MessageBox.Show(value.ToString());
            e.Handled = true;
        }

        private void m_pg_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("AS");
            double totoaltime_now = mp.NaturalDuration.TimeSpan.TotalMilliseconds*(value/100);
            int minutes = (int)(totoaltime_now/1000)/60;
            int seconds = (int)(totoaltime_now / 1000) % 60;
            int milliseconds = (int)totoaltime_now / 1000;
            TimeSpan ts = new TimeSpan(0,0,minutes,seconds,milliseconds);
            mp.Position = ts;
        }

        //设置音量
        private void btn_voice_Click(object sender, RoutedEventArgs e)
        {
            //未解决bug：所有popup都公用了同一个isOpen变量
            if (!isOpen)//add_to_list_mus;add_to_list_all;add_to_list_alb
            {
                this.Pop_volume.IsOpen = true;
                isOpen = true;
            }
            else
            {
                this.Pop_volume.IsOpen = false;
                isOpen = false;
            }
        }

        private void slider_volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //mp.Volume = slider_volume.Value*100000000000000;
            slider_volume.Interval = 1;
            slider_volume.TickFrequency = 3;
            mp.Volume = slider_volume.Value/10;
           // MessageBox.Show(slider_volume.Value.ToString());
        }


        //private void btnOpenLyric_Click(object sender, RoutedEventArgs e)
        //{
        //    Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
        //    ofd.Filter = "歌词文件(*.lrc)|*.lrc";
        //    if (ofd.ShowDialog(this) == true)
        //    {
        //        getLT.getLyricAndLyricTimeByLyricPath(ofd.FileName);
        //        LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);//解析歌词->得到歌词时间和歌词              
        //    }
        //}

        DeskLyricWin deskLyricWin;
        private void desLyric_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.Filter = "歌词文件(*.lrc)|*.lrc";
            if (ofd.ShowDialog(this) == true)
            {
                getLT.getLyricAndLyricTimeByLyricPath(ofd.FileName);
                LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);//解析歌词->得到歌词时间和歌词              
            }
            if (LyricShow.IsOpenDeskLyric == false)
            {
                deskLyricWin = new DeskLyricWin();
                deskLyricWin.Show();
                LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
            }
        }

        bool isLysicShow = false;
        private void btn_music_lyric_Click(object sender, RoutedEventArgs e)
        {
            if (!isLysicShow)
            {
                Change5.Visibility = Visibility.Visible;
               // Change1.Visibility = Visibility.Hidden;
                isLysicShow = true;
            }
            else
            {
                Change5.Visibility = Visibility.Hidden;
                // Change1.Visibility = Visibility.Hidden;
                isLysicShow = false;
            }
        }

        private void all_list_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //btn_play.Background = new ImageBrush
            //{
            //    //不能用相对路径
            //    ImageSource = new BitmapImage(new Uri("pack://application:,,,/imgs/暂停.png"))
            //};
            //btnbg.Source = new BitmapImage(new Uri("/imgs/播放2.png",UriKind.Relative));
            for (int i = 0; i < thePlayingStateList.Count; i++)
            {
                if (thePlayingStateList[i].ToString().Equals("true"))
                {
                    thePlayingStateList[i] = "false";
                    break;
                }
            }
            just_do_play_music();
            mp.Play();
            var t = this.btn_play.GetValue(TemplateProperty) as ControlTemplate;
            var b = t.FindName("btnbg", this.btn_play) as Image;
            b.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/暂停1.png"));
            LyricShow.pauseOrContinueLyricShow(false);
            isPlay = true;
            isPageChange = false;
            result_count = -1;
            ICollectionView dataView = CollectionViewSource.GetDefaultView(all_list.ItemsSource);
        }

        public static int result_count = -1;
        private void search_music_Click(object sender, RoutedEventArgs e)
        {
            if (search_key.Text == null && search_key.Text == "")
            {
                MessageBox.Show("歌曲名不能为空");
            }
            else
            {
                //值为77
                for (int i = 0; i < dataList.Count; i++)
                {
                    if (dataList[i].Name == search_key.Text)
                    {
                        result_count = i; break;
                    }
                }
                ICollectionView dataView = CollectionViewSource.GetDefaultView(all_list.ItemsSource);
                dataView.Refresh();
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            AllData d = all_list.SelectedItem as AllData;
            se = all_list.SelectedIndex;
            //all_list.Items.RemoveAt(se);
            //当 ItemsSource 正在使用时操作无效。改用 ItemsControl.ItemsSource 访问和修改元素。
            //进行删除时，首先更新mylist.xml和状态数组以及存放值的类
            //然后更新我的收藏(单曲和专辑)和我的歌单以及歌手，专辑分类等等

            XmlDocument xdd = new XmlDocument();
            xdd.Load(rootpath1 + "/mylist.xml");
            //XmlElement xe = xdd.DocumentElement;
            XmlNode root = xdd.SelectSingleNode("all_song");
            XmlNodeList listNodes = root.ChildNodes;
            //listNodes = xdd.SelectNodes("/bookstore/book/price");
            //遍历所有子节点
            
            foreach (XmlNode xn in listNodes)
            {
                bool isSearched = false;
                XmlElement xe = (XmlElement)xn;
                XmlNodeList subList = xe.ChildNodes;
                foreach (XmlNode xmlNode in subList)
                {
                    if ("name".Equals(xmlNode.Name))
                    {
                        if(xmlNode.InnerText==dataList[se].Name)
                            isSearched = true; 
                        break;
                    }
                }
                if (isSearched)
                {
                    root.RemoveChild(xn);
                }
            }
            xdd.Save(rootpath1 + "/mylist.xml");

            

            XmlDocument xdd11 = new XmlDocument();
            xdd11.Load(rootpath1 + "/LocalList.xml");
            //XmlElement xe = xdd.DocumentElement;
            XmlNode root11 = xdd11.SelectSingleNode("locmus");
            XmlNodeList listNodes11 = root11.ChildNodes;
            foreach (XmlNode xn in listNodes11)
            {
                bool isSearched = false;
                XmlElement xe = (XmlElement)xn;
                XmlNodeList subList = xe.ChildNodes;
                foreach (XmlNode xmlNode in subList)
                {
                    if ("name".Equals(xmlNode.Name))
                    {
                        if (xmlNode.InnerText == dataList[se].Name)
                            isSearched = true;
                        break;
                    }
                }
                if (isSearched)
                {
                    root11.RemoveChild(xn);
                }
            }
            xdd11.Save(rootpath1 + "/LocalList.xml");


            Create_list();//删除之后更新数据，注意：删除的时候不能有歌曲正在播放，否则会有bug

            this.all_list.ItemsSource = dataList;

            //更新从本地导入的音乐列表
            XmlDocument xdd1 = new XmlDocument();
            xdd1.Load(rootpath1 + "/LocalList.xml");
            //XmlElement xe = xdd.DocumentElement;
            XmlNode root1 = xdd1.SelectSingleNode("locmus");
            XmlNodeList listNodes1 = root1.ChildNodes;
            int i1 = 0;
            foreach (XmlNode xn in listNodes1)
            {
                XmlElement xe = (XmlElement)xn;
                XmlNodeList subList = xe.ChildNodes;
                foreach (XmlNode xmlNode in subList)
                {
                    localMusicList.Add(new LocalMus());
                    if ("name".Equals(xmlNode.Name))
                    {
                        localMusicList[i1].Name = xmlNode.InnerText;
                    }
                    if ("path".Equals(xmlNode.Name))
                    {
                        localMusicList[i1].Path = xmlNode.InnerText;
                    }
                }
                i1++;
            }
            //xdd1.Save(rootpath1+"/LocalList.xml");




        }

        private void do_all_play_mus_Click(object sender, RoutedEventArgs e)
        {
            switch (state_playing)
            {
                case ALL_PLAY:
                    stateList[0] = "true";
                    string str = dataList[0].Name;
                    string geshou = dataList[0].Mus;
                    if (!do_play(str))
                    {
                        PlayMusic(rootpath1 + "/mus/" + geshou + " - " + str + ".mp3");
                        info_mus.Content = str + "-" + geshou;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + str + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
                case ALB_PLAY:
                    stateList_alb[0] = "true";
                    string str1 = albDataList[0].Name;
                    string geshou1 = albDataList[0].Mus;
                    if (!do_play(str1))
                    {
                        PlayMusic(rootpath1 + "/mus/" + geshou1 + " - " + str1 + ".mp3");
                        info_mus.Content = str1 + "-" + geshou1;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + str1 + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
                case MUS_PLAY:
                    stateList_mus[0] = "true";
                    string str2 = musDataList[0].Name;
                    //string geshou2 = d2.Mus;
                    //string geshou2 = my_mus.SelectedItem.ToString();
                    if (!do_play(str2))
                    {
                        PlayMusic(rootpath1 + "/mus/" + str_mus + " - " + str2 + ".mp3");
                        info_mus.Content = str2 + "-" + str_mus;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + str2 + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
                case COL_MUS_PLAY:
                    stateList_mus_col[0] = "true";
                    string str3 = dataList1[0].Name;
                    string geshou3 = dataList1[0].Mus;
                    if (!do_play(str3))
                    {
                        PlayMusic(rootpath1 + "/mus/" + geshou3 + " - " + str3 + ".mp3");
                        info_mus.Content = str3 + "-" + geshou3;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + str3 + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
                case LIST_MUS_PLAY:
                    stateList_mus_list[0] = "true";
                    string str4 = dataList[2].Name;
                    string geshou4 = dataList[2].Mus;
                    if (!do_play(str4))
                    {
                        PlayMusic(rootpath1 + "/mus/" + geshou4 + " - " + str4 + ".mp3");
                        info_mus.Content = str4 + "-" + geshou4;
                    }
                    getLT.getLyricAndLyricTimeByLyricPath(rootpath1 + "/mus/" + str4 + ".lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);
                    break;
            }
            mp.Play();
            var t = this.btn_play.GetValue(TemplateProperty) as ControlTemplate;
            var b = t.FindName("btnbg", this.btn_play) as Image;
            b.Source = new BitmapImage(new Uri("pack://application:,,,/imgs/暂停1.png"));
            LyricShow.pauseOrContinueLyricShow(false);
            isPlay = true;
            isPageChange = false;
            dTimer.Start();
        }

        //删除歌单
        private void delete_list_Click(object sender, RoutedEventArgs e)
        {
            TreeViewItem tvi = new TreeViewItem();
            tvi = tree_song_list.SelectedItem as TreeViewItem;//e.Source as TreeViewItem;
            string str = tvi.Header.ToString();//MessageBox.Show(s);

            XmlDocument xdd = new XmlDocument();
            xdd.Load(rootpath1 + "/listshow.xml");
            //XmlElement xe = xdd.DocumentElement;
            XmlNode root = xdd.SelectSingleNode("definelist");
            XmlNodeList listNodes = root.ChildNodes;

            foreach (XmlNode xn in listNodes)
            {
                if (xn.InnerText==str)
                {
                    root.RemoveChild(xn);
                }
            }
            xdd.Save(rootpath1 + "/listshow.xml");

            tree_song_list.Items.Clear();
            InitSongList();
        }
    }//#33DA00FD  #624C41 黄色#C8C657

    public sealed class BackgroundC : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
             CultureInfo culture)
        {
            ListViewItem item = (ListViewItem)value;
            ListView listView =
                ItemsControl.ItemsControlFromItemContainer(item) as ListView;


            //值为77
            int index = listView.ItemContainerGenerator.IndexFromContainer(item);

            if (index == MainWindow.result_count)
            {
                return Brushes.PowderBlue;
            }
            else
            {
                return Brushes.White;
            }
        }
        // Get the index of a ListViewItem
        //int index = listView.ItemContainerGenerator.IndexFromContainer(item);

        //if (index % 2 == 0)
        //{
        //    Color color = new Color();
        //    color.R = 220;
        //    color.G = 220;
        //    color.B = 220;
        //    SolidColorBrush scb = new SolidColorBrush();
        //    scb.Color = color;
        //    scb.Opacity = 1;
        //    return scb;
        //}
        //else
        //{
        //    return Brushes.PowderBlue;
        //}


        //if (index == MainWindow.result_count)
        //{
        //    return Brushes.PowderBlue;
        //}
        //else
        //{
        //    return Brushes.White;
        //}
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}