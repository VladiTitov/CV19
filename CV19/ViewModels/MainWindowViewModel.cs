using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region Тестовые данные координат

        private IEnumerable<DataPoint> _testDataPoints;

        public IEnumerable<DataPoint> TestDataPoints
        {
            get => _testDataPoints;
            set => Set(ref _testDataPoints, value);
        }
        #endregion

        #region Заголовок окна

        private string title = "Анализ статистики CV19";

        public string Title
        {
            get => Title;
            set => Set(ref title, value);
        }

        #endregion

        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }

        private bool CanOnCloseApplicationCommandExecuted(object p) => true;

        private void OnCloseApplicationCommandExecuted(object p) => Application.Current.Shutdown();
        #endregion 

        public MainWindowViewModel()
        {
            #region Commands

            CloseApplicationCommand = new LambdaCommands(OnCloseApplicationCommandExecuted, CanOnCloseApplicationCommandExecuted);

            #endregion

            var data_points = new List<DataPoint>((int)(360 / 0.1));
            for (var x = 0d; x <= 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(2 * Math.PI * x * to_rad);

                data_points.Add(new DataPoint { XValue = x, YValue = y });
            }

            TestDataPoints = data_points;
        }

    }
}
