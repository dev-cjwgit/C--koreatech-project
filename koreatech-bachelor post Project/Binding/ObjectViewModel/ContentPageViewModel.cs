using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace koreatech_bachelor_Post_Project.Binding.ObjectViewModel
{
    public class ContentPageViewModel : NotifyPropertyChanged
    {
        private UserControl _page;
        public UserControl Page
        {
            get
            {
                return _page;
            }
            set
            {
                if(Page == null)
                    _page = value;
                

                DoubleAnimation anim1 = new DoubleAnimation(1, 0, (Duration)TimeSpan.FromSeconds(0.5));
                DoubleAnimation anim2 = new DoubleAnimation(0, 1, (Duration)TimeSpan.FromSeconds(0.5));

                anim1.Completed += (s, _) =>
                {
                    _page = value;
                    OnPropertyChanged("Page");
                    Page.BeginAnimation(UIElement.OpacityProperty, anim2);

                };

                Page.BeginAnimation(UIElement.OpacityProperty, anim1);
            }
        }

        private static ContentPageViewModel instance;

        public static ContentPageViewModel GetInstance()
        {
            if (instance == null)
                instance = new ContentPageViewModel();

            return instance;
        }
    }
}
