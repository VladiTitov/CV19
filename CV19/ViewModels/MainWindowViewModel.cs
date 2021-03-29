using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using CV19.Infrastructure.Commands;
using CV19.Models;
using CV19.Models.Decanat;
using CV19.ViewModels.Base;

namespace CV19.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        /*---------------------------------------------------------------------------------------------*/

        #region Студенты и группы

        public ObservableCollection<Group> Groups { get; }

        #region Выбранная группа

        private Group _selectedGroup;

        public Group SelectedGroup
        {
            get => _selectedGroup;
            set => Set(ref _selectedGroup, value);
        }
        #endregion

        #endregion


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
            get => title;
            set => Set(ref title, value);
        }

        #endregion

        /*---------------------------------------------------------------------------------------------*/

        #region Commands

        #region CloseApplicationCommand
        public ICommand CloseApplicationCommand { get; }

        private bool CanOnCloseApplicationCommandExecuted(object p) => true;

        private void OnCloseApplicationCommandExecuted(object p) => Application.Current.Shutdown();
        #endregion

        #region Команда добавления

        public  ICommand CreateNewGroupCommand { get; }

        private bool CanCreateGroupCommandExecute(object p) => true;

        private void OnCreateGroupCommandExecute(object p)
        {
            var group_max_index = Groups.Count + 1;
            var new_group = new Group
            {
                Name = $"Группа {group_max_index}",
                Students = new ObservableCollection<Student>()
            };

            Groups.Add(new_group);
        }

        #endregion

        #region Команда удаления

        public  ICommand DeleteGroupCommand { get; }

        private bool CanDeleteGroupCommandExecute(object p) => p is Group group && Groups.Contains(group);

        private void OnDeleteGroupCommandExecute(object p)
        {
            if (!(p is Group group)) return;
            var group_index = Groups.IndexOf(group);
            Groups.Remove(group);
            if (group_index < Groups.Count) SelectedGroup = Groups[group_index];
        }

        #endregion

        #endregion

        /*---------------------------------------------------------------------------------------------*/

        #region Logic
        public MainWindowViewModel()
        {
            #region Commands

            CloseApplicationCommand = new LambdaCommands(OnCloseApplicationCommandExecuted, CanOnCloseApplicationCommandExecuted);

            CreateNewGroupCommand = new LambdaCommands(OnCreateGroupCommandExecute, CanCreateGroupCommandExecute);

            DeleteGroupCommand = new LambdaCommands(OnDeleteGroupCommandExecute, CanDeleteGroupCommandExecute);

            #endregion

            #region Координаты

            var data_points = new List<DataPoint>((int)(360 / 0.1));
            for (var x = 0d; x <= 360; x += 0.1)
            {
                const double to_rad = Math.PI / 180;
                var y = Math.Sin(2 * Math.PI * x * to_rad);

                data_points.Add(new DataPoint { XValue = x, YValue = y });
            }

            TestDataPoints = data_points;

            #endregion

            #region Студенты
            var student_index = 1;

            var students = Enumerable.Range(1, 10).Select(i => new Student
            {
                Name = $"Name {student_index}",
                Surname = $"Surname {student_index}",
                Patronymic = $"Patronymic {student_index++}",
                BirtDay = DateTime.Now,
                Rating = 0

            });

            var groups = Enumerable.Range(1, 20).Select(i => new Group
            {
                Name = $"Group {i}",
                Students = new ObservableCollection<Student>(students)
            });

            Groups = new ObservableCollection<Group>(groups);

            #endregion

        }

        #endregion


    }
}
