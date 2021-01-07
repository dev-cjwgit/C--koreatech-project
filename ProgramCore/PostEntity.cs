using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramCore
{
    public class PostEntity
    {
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

    }
}
