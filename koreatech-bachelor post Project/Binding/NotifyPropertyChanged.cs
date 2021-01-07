using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace koreatech_bachelor_Post_Project.Binding
{
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
