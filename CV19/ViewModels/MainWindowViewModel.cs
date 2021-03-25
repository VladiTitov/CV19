using System;
using System.Collections.Generic;
using System.Text;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private IEnumerable<DataPoint> _testDataPoints;

        public IEnumerable<DataPoint> TestDataPoints
        {
            get => _testDataPoints;
            set => Set(ref _testDataPoints, value);
        }
    }
}
