using ProgramCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace koreatech_bachelor_Post_Project.Binding.ObjectViewModel
{
    public class PostBodyViewModel : NotifyPropertyChanged
    {
        #region Model
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

        private ObservableCollection<Attachments> _attachment;
        public ObservableCollection<Attachments> Attachment
        {
            get
            {
                return _attachment;
            }
            set
            {
                _attachment = value;
            }
        }

        private string _bodys;
        public string Bodys
        {
            get
            {
                return _bodys;
            }
            set
            {
                _bodys = value;
                OnPropertyChanged("Bodys");
            }
        }
        #endregion

        public PostBodyViewModel() { }
        public PostBodyViewModel(PostBodyEntity src)
        {
            instance = new PostBodyViewModel();
            instance.Title = src.Title;
            instance.Publisher = src.Publisher;
            instance.Time = src.Time;
            instance.Views = src.Views;
            if(src.Attachment != null)
                instance.Attachment = new ObservableCollection<Attachments>(src.Attachment);
            instance.Bodys = src.Bodys;
        }
        private PostBodyViewModel instance;

        public PostBodyViewModel GetInstance()
        {
            if (instance == null)
                instance = new PostBodyViewModel();
            return instance;
        }
    }
}
