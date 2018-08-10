using FaraWork_Revolution.XTask.Entities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace FaraWork_Revolution.XTask.ViewModels
{
    public class TaskHolderSampleViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public TaskHolderSampleViewModel()
        {
            Title = "Nice Looking ListView";
            Entities.XTask t = new Entities.XTask();
            t.Color = new SolidColorBrush(Colors.Yellow);
            t.Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";
            t.ID = Guid.NewGuid();

            Entities.XTask t2 = new Entities.XTask()
            {
                Color = new SolidColorBrush(Colors.Red),
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                ID = Guid.NewGuid()
            };
            Tasks.Add(t);
            Tasks.Add(t2);
        }
        private string _title = "";
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        private ObservableCollection<Entities.XTask> _tasks = new ObservableCollection<Entities.XTask>();
        public ObservableCollection<Entities.XTask> Tasks
        {
            get
            {
                return _tasks;
            }
            set
            {
                _tasks = value;
                OnPropertyChanged(nameof(Tasks));
            }
        }
    }
}
