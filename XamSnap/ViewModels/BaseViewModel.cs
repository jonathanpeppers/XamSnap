using System;
using Xamarin.Forms;

namespace XamSnap
{
    public class BaseViewModel : BindableObject
    {
        protected readonly IWebService service = DependencyService.Get<IWebService>();
        protected readonly ISettings settings = DependencyService.Get<ISettings>();

        bool isBusy = false;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }
    }
}

