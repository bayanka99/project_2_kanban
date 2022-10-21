using PresentaionLayer;
using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Presentation
{
    class ColumnViewModel : NotifiableObject
    {
        public Presentation.Model.BackendController controller;
      
        public readonly ColumnModel column;
        public readonly string boardname;
        public readonly string creatorofboard;
        public readonly string usermail;




        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                this._message = value;
                RaisePropertyChanged("Message");
            }
        }

      


        public string Title { get; private set; }
        public ColumnViewModel(ColumnModel col)
        {
            this.controller = col.Controller;
            this.column = col;
            Title = "Board for ";
          
            // Forum = user.GetForum();
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
                RaisePropertyChanged("selected task");
            }
        }

        private string assignto;
        public string Assignto
        {
            get => assignto;
            set
            {
                this.assignto = value;
                RaisePropertyChanged("NewColumnName");
            }
        }


        private string newtasktitle;
        public string NewTaskTitle
        {
            get => newtasktitle;
            set
            {
                this.newtasktitle = value;
                EnableForward = value != null;
                RaisePropertyChanged("NewTaskTitle");
            }
        }

        private string newtaskdesc;
        public string NewTaskDescription
        {
            get => newtaskdesc;
            set
            {
                this.newtaskdesc = value;
                EnableForward = value != null;
                RaisePropertyChanged("NewTaskDescription");
            }
        }



        private DateTime newtaskduedate;
        public DateTime NewTaskDuedate
        {
            get => newtaskduedate;
            set
            {
                this.newtaskduedate = value;
               
                RaisePropertyChanged("NewTaskDuedate");
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

        public SolidColorBrush BackgroundColor
        {
            get
            {

                    return new SolidColorBrush(DateTime.Now.CompareTo(NewTaskDuedate) <0 ? Colors.Blue : Colors.Orange);
                
            }
        }




        internal void AdvanceTask(string usermail, string creatorofBoard, string boardname)
        {
            Message = "";
            try
            {
                //column.AdvanceTask(SelectedTask, usermail, creatorofBoard, boardname, column.colord);
                //controller.AdvanceTask(usermail, creatorofBoard, boardname, column.colord,SelectedTask.Id);
                Message = "Task Advanced succefully!";


            }
            catch (Exception e)
            {
                Message = e.Message;
            }
        }
       
        internal void AssignTask(string usermail, string creatorofBoard, string boardname)
        {
            Message = "";
            try
            {
                column.AssignTask(SelectedTask, usermail, creatorofBoard, boardname, column.colord,Assignto);
                //controller.AssignTask(usermail, creatorofBoard, boardname, column.colord, SelectedTask.Id,assignto);
                Message = "Task Advanced succefully!";


            }
            catch (Exception e)
            {
                Message = e.Message;
            }
        }

        internal void AddTask(string usermail, string creatorofBoard, string boardname)
        {
            Message = "";
            try
            {
                //column.Addtask(usermail, creatorofBoard, boardname, column.colord,newtasktitle,newtaskdesc,newtaskduedate);
                //controller.AssignTask(usermail, creatorofBoard, boardname, column.colord, SelectedTask.Id,assignto);
                Message = "Task Advanced succefully!";


            }
            catch (Exception e)
            {
                Message = e.Message;
            }
        }





    }
}
