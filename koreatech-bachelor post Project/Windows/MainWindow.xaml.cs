using koreatech_bachelor_Post_Project.Binding.ObjectViewModel;
using koreatech_bachelor_Post_Project.Windows.Pages;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace koreatech_bachelor_Post_Project
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ContentPage.DataContext = ContentPageViewModel.GetInstance();
            ContentPageViewModel.GetInstance().Page = new LoginPage();
        }

        #region TitleBar Button
        private void AppCloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WindowsTitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
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
        #endregion

        #region Window Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var anim = new DoubleAnimation(0, 1, (Duration)TimeSpan.FromSeconds(1));
            this.BeginAnimation(UIElement.OpacityProperty, anim);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Closing -= Window_Closing;
            e.Cancel = true;
            var anim = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(1));
            anim.Completed += (s, _) => { Environment.Exit(0); };
            this.BeginAnimation(UIElement.OpacityProperty, anim);
            
        }

        #endregion

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            ContentPageViewModel.GetInstance().Page = new LoginPage();
        }

        private void PostButton_Click(object sender, RoutedEventArgs e)
        {
            ContentPageViewModel.GetInstance().Page = new PostPage();
        }
    }
}
