using System;
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
using System.Windows.Shapes;

namespace StudyOfWpfApplication1
{
    /// <summary>
    /// WindowList.xaml 的交互逻辑
    /// </summary>
    public partial class WindowList : Window
    {
        public static string my_listname = null;
        public WindowList()
        {
            InitializeComponent();
        }
        public void DragWindow(object sender, MouseButtonEventArgs args)
        {
            this.DragMove();
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            my_listname = lisname.Text.ToString();
            
            this.Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
