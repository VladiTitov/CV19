using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CV19.ViewModels.Base
{
    class ViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
