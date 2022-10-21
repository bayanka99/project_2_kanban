using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    public class ColumnModel : NotifiableModelObject
    {
       

        public ObservableCollection<TaskModel> Taskcollection { get; set; }
        public string name { get; set; }
        private int numberOfTasks;
        public int limit { get; set; }
        public int colord { get; set; }

        public ColumnModel(BackendController controller, ObservableCollection<TaskModel> taskcollection, int columorder, string columnname) : base(controller)
        {
            this.colord = columorder;
            Taskcollection = taskcollection;
            this.name = columnname;
            this.limit = -1;
            Taskcollection.CollectionChanged += HandleChange;
        }

        private TaskModel _selectedtask;
        public TaskModel SelectedTask
        {
            get
            {

                return _selectedtask;
            }
            set
            {
                _selectedtask = value;
                EnableForward = value != null;
                RaisePropertyChanged("SelectedTask");
            }
        }
        private bool _enableForward = false;
        public bool EnableForward
        {
            get => _enableForward;
            private set
            {
                _enableForward = value;
                RaisePropertyChanged("EnableForward");
            }
        }
        internal void AdvanceTaskk(TaskModel task, string usermail, string creatorofboard, string boadname, int colomnord)
        {
            //this.Taskcollection.Remove(task);
            this.Controller.AdvanceTask(usermail, creatorofboard, boadname, colomnord, task.Id);


        }

        internal void AssignTask(TaskModel task, string usermail, string creatorofboard, string boadname, int colomnord, string newassignemail)
        {
            this.Taskcollection.Remove(task);
            this.Controller.AssignTask(usermail, creatorofboard, boadname, colomnord, task.Id, newassignemail);
            task.EmailAssignee = newassignemail;
            this.Taskcollection.Add(task);
        }

        internal void AddTask(string usermail, string creatorofBoard, string boardname,string newtasktitle, string newtaskdesc, DateTime newtaskdudate)
        {
            
            TaskModel newtaskmode = this.Controller.AddTask(usermail, creatorofBoard, boardname, newtasktitle, newtaskdesc, newtaskdudate);
            this.Taskcollection.Add(newtaskmode);
        }


        private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    foreach (TaskModel y in e.OldItems)
                    {

                        //Controller.remo(this.usermail, this.creator_mail, this.boardname, y.colord);
                    }

                    break;

                case NotifyCollectionChangedAction.Add:
                    foreach (TaskModel y in e.NewItems)
                    {

                        //Controller.AddColumn(this.usermail, this.creator_mail, this.boardname, y.colord, y.name);// maybe there will be anothre copy, check boardviewmodel
                    }

                    break;

            }
        }

        
    }
}
