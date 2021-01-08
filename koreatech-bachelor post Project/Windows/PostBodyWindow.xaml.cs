using koreatech_bachelor_Post_Project.Binding.ObjectViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace koreatech_bachelor_Post_Project.Windows
{
    /// <summary>
    /// PostBodyWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PostBodyWindow : Window
    {
        public PostBodyWindow(PostBodyViewModel pagesrc)
        {
            InitializeComponent();
            PostBodyTitleTextBox.DataContext = pagesrc.GetInstance();
            PostBodyPublisherTextBox.DataContext = pagesrc.GetInstance();
            PostBodyTimeTextBox.DataContext = pagesrc.GetInstance();
            PostBodyViewsTextBox.DataContext = pagesrc.GetInstance();
            PostBodyBodyWebBrowser.DataContext = pagesrc.GetInstance();
            //PostBodyRichTextBox.DataContext = pagesrc.GetInstance();
        }

        private void PostBodyWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void AppCloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PostBodyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var anim = new DoubleAnimation(0, 1, (Duration)TimeSpan.FromSeconds(1));
            this.BeginAnimation(UIElement.OpacityProperty, anim);

        }

        private void PostBodyWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Closing -= PostBodyWindow_Closing;
            e.Cancel = true;
            var anim = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(1));
            anim.Completed += (s, _) => this.Close();
            this.BeginAnimation(UIElement.OpacityProperty, anim);
        }

        private void ContentControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            WindowState temp = (this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal);
            DoubleAnimation anim1 = new DoubleAnimation(1, 0, (Duration)TimeSpan.FromSeconds(0.33));
            DoubleAnimation anim2 = new DoubleAnimation(0, 1, (Duration)TimeSpan.FromSeconds(0.33));

            anim1.Completed += (s, _) =>
            {

                this.WindowState = temp;
                this.BeginAnimation(OpacityProperty, anim2);
            };

            this.BeginAnimation(OpacityProperty, anim1);
        }
    }
}
