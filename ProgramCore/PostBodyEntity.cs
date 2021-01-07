using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramCore
{
    public class Attachments
    {
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
            }
        }

        private string _url;
        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
            }
        }
    }
    public class PostBodyEntity
    {
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
            }
        }

        private List<Attachments> _attachment;
        public List<Attachments> Attachment
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

        private string _body;
        public string Body
        {
            get
            {
                return _body;
            }
            set
            {
                _body = value;
            }
        }
    }
}
