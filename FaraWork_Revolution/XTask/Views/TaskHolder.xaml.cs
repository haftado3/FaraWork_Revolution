using FaraWork_Revolution.XTask.DragNDrop;
using FaraWork_Revolution.XTask.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FaraWork_Revolution.XTask.Views
{
    /// <summary>
    /// Interaction logic for TaskHolder.xaml
    /// </summary>
    public partial class TaskHolder : UserControl
    {
        DragNDrop.DragNDropManager<XTask.Entities.XTask> dragMgr;
        public TaskHolder()
        {
            InitializeComponent();
            this.Loaded += TaskHolderLoaded;
        }
        private ObservableCollection<XTask.Entities.XTask> CreateSimpleTasks()
        {
            ObservableCollection<XTask.Entities.XTask> temp = new ObservableCollection<Entities.XTask>();
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
            temp.Add(t);
            temp.Add(t2);
            return temp;
        }
        void TaskHolderLoaded (object sender,RoutedEventArgs e)
        {
            (this.DataContext as TaskHolderViewModel).Tasks = CreateSimpleTasks();
            dragMgr = new DragNDrop.DragNDropManager<Entities.XTask>(this.MlistView);
            this.MlistView.DragEnter += OnListViewDragEnter;
            this.MlistView.DragEnter += OnListViewDragEnter;
            this.MlistView.Drop += OnListViewDrop;
            this.MlistView.Drop += OnListViewDrop;
        }
        #region dragMgr_ProcessDrop

        // Performs custom drop logic for the top ListView.
        void dragMgr_ProcessDrop(object sender, ProcessDropEventArgs<Task> e)
        {
            // This shows how to customize the behavior of a drop.
            // Here we perform a swap, instead of just moving the dropped item.

            int higherIdx = Math.Max(e.OldIndex, e.NewIndex);
            int lowerIdx = Math.Min(e.OldIndex, e.NewIndex);

            if (lowerIdx < 0)
            {
                // The item came from the lower ListView
                // so just insert it.
                e.ItemsSource.Insert(higherIdx, e.DataItem);
            }
            else
            {
                // null values will cause an error when calling Move.
                // It looks like a bug in ObservableCollection to me.
                if (e.ItemsSource[lowerIdx] == null ||
                    e.ItemsSource[higherIdx] == null)
                    return;

                // The item came from the ListView into which
                // it was dropped, so swap it with the item
                // at the target index.
                e.ItemsSource.Move(lowerIdx, higherIdx);
                e.ItemsSource.Move(higherIdx - 1, lowerIdx);
            }

            // Set this to 'Move' so that the OnListViewDrop knows to 
            // remove the item from the other ListView.
            e.Effects = DragDropEffects.Move;
        }

        #endregion // dragMgr_ProcessDrop

        #region OnListViewDragEnter

        // Handles the DragEnter event for both ListViews.
        void OnListViewDragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
        }

        #endregion // OnListViewDragEnter

        #region OnListViewDrop

        // Handles the Drop event for both ListViews.
        void OnListViewDrop(object sender, DragEventArgs e)
        {
            if (e.Effects == DragDropEffects.None)
                return;

            Task task = e.Data.GetData(typeof(Task)) as Task;
            if (sender == this.MlistView)
            {
                if (this.dragMgr.IsDragInProgress)
                    return;

                // An item was dragged from the bottom ListView into the top ListView
                // so remove that item from the bottom ListView.
                //(this.listView2.ItemsSource as ObservableCollection<Task>).Remove(task);
            }
            else
            {
                //if (this.dragMgr2.IsDragInProgress)
                //    return;

                // An item was dragged from the top ListView into the bottom ListView
                // so remove that item from the top ListView.
                (this.MlistView.ItemsSource as ObservableCollection<Task>).Remove(task);
            }
        }

        #endregion // OnListViewDrop

    }
}
