using System.ComponentModel;

namespace BusRouterUI.ViewModels
{
    public class ViewModelsBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void Dispose()
        {
            PropertyChanged = null;
        }
    }
}