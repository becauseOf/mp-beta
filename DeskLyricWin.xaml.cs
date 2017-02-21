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
    /// DeskLyricWin.xaml 的交互逻辑
    /// </summary>
    public partial class DeskLyricWin : Window
    {
        public DeskLyricWin()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == System.Windows.Input.MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            rectangleDeskLyricBack.Visibility = Visibility.Visible;
            btn_close.Visibility = Visibility.Visible;
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            rectangleDeskLyricBack.Visibility = Visibility.Hidden;
            btn_close.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
                LyricShow.closeDeskLyric();
                this.Close();
        }
    }
}
