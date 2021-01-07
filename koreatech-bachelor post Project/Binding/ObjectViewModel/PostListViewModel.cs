using ProgramCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace koreatech_bachelor_Post_Project.Binding.ObjectViewModel
{
    public class PostListViewModel: NotifyPropertyChanged
    {
        #region Model
        private int _number;
        public int Number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
                OnPropertyChanged("Number");
            }
        }
        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        private string _time;
        public string Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                OnPropertyChanged("Time");
            }
        }
        private string _publisher;
        public string Publisher
        {
            get
            {
                return _publisher;
            }
            set
            {
                _publisher = value;
                OnPropertyChanged("Publisher");
            }
        }

        private int _views;
        public int Views
        {
            get
            {
                return _views;
            }
            set
            {
                _views = value;
                OnPropertyChanged("Views");
            }
        }

        #endregion

        private static ObservableCollection<PostListEntity> instance;

        public static ObservableCollection<PostListEntity> GetInstance()
        {
            if (instance == null)
                instance = new ObservableCollection<PostListEntity>();

            return instance;
        }
    }

    public class PageViewModel : NotifyPropertyChanged
    {
        #region Model
        private int _presentpage = 1;
        public int PresentPage
        {
            get
            {
                return _presentpage;
            }
            set
            {
                _presentpage = value;
                OnPropertyChanged("PresentPage");
            }
        }

        private int _maxpage;
        public int MaxPage
        {
            get
            {
                return _maxpage;
            }
            set
            {
                _maxpage = value;
                OnPropertyChanged("MaxPage");
            }
        }
        #endregion

        private static PageViewModel instance;

        public static PageViewModel GetInstance()
        {
            if (instance == null)
                instance = new PageViewModel();

            return instance;
        }
    }
    public class PostListModel
    {
        public static int BoardId
        {
            get; set;
        }
        public static int PresentPage
        {
            get
            {
                return PageViewModel.GetInstance().PresentPage;
            }
            set
            {
                PageViewModel.GetInstance().PresentPage = value;
            }
        }
        public static int MaxPage
        {
            get
            {
                return PageViewModel.GetInstance().MaxPage;
            }
            set
            {
                PageViewModel.GetInstance().MaxPage = value;
            }
        }
        public static List<PostListEntity> DataList
        {
            get; set;
        }
        public static void SetSource(List<PostListEntity> src)
        {
            DataList = src;
            PostListViewModel.GetInstance().Clear();
            for(int i = 0; i < src.Count; i++)
                PostListViewModel.GetInstance().Add(DataList[i]);
           
        }
    }
}
