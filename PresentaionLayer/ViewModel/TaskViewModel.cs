using PresentaionLayer;
using Presentation.Model;
using System;
using System.Windows;
using System.Windows.Media;


namespace Presentation.ViewModel
{
    internal class TaskViewModel : NotifiableObject
    {
        public Presentation.Model.BackendController controller;
        public readonly TaskModel task;
        public readonly string boardname;
        public readonly string creatorofboard;
        public readonly string usermail;
        public readonly int colomord;
        // public readonly string boardname;
        //public readonly string creatorofboard;
        //public readonly string usermail;

        public TaskViewModel(TaskModel taskmod, string usermail, string boardcreator, string nameofboard,int colord)
        {
            this.controller = taskmod.Controller;
            this.task = task;
            this.boardname = nameofboard;
            this.creatorofboard = boardcreator;
            this.usermail = usermail;
            this.colomord = colord;
            this.Id = task.Id.ToString();
            this.Duedate = task.Duedate.ToString();
            this.Creationtime = task.Creationtime.ToString();
            this.Title = task.Title;
            this.Descreption = task.Descreption;
            this.EmailAssignee = task.EmailAssignee;
            this.Minute = "0";
            this.Year = new DateTime().Year.ToString();
            
            
            //Title = "Board for ";

            // Forum = user.GetForum();
        }


        public string Id { get; private set; }
        public string Duedate { get; private set; }
        public string Creationtime { get; private set; }


        private string _year;
        public string Year
        {
            get => _year;
            set
            {
                this._year = value;
                EnableForward = value != null;
                RaisePropertyChanged("Year");
            }
        }


        private string _month;
        public string Month
        {
            get => _month;
            set
            {
                this._month = value;
                EnableForward = value != null;
                RaisePropertyChanged("Month");
            }
        }



        private string _day;
        public string Day
        {
            get => _day;
            set
            {
                this._day = value;
                EnableForward = value != null;
                RaisePropertyChanged("Day");
            }
        }



        private string _hour;
        public string Hour
        {
            get => _hour;
            set
            {
                this._hour = value;
                EnableForward = value != null;
                RaisePropertyChanged("Hour");
            }
        }



        private string _minute;
        public string Minute
        {
            get => _minute;
            set
            {
                this._minute = value;
                EnableForward = value != null;
                RaisePropertyChanged("Minute");
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





        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                this._title = value;
                RaisePropertyChanged("Title");
            }
        }
        private string _descreption;
        public string Descreption
        {
            get => _descreption;
            set
            {
                this._descreption = value;
                RaisePropertyChanged("descreption");
                //Controller.UpdateBody(UserEmail, Id, value);
            }
        }




        private string _emailassignee;
        public string EmailAssignee
        {
            get => _emailassignee;
            private set
            {
                this._emailassignee = value;
                RaisePropertyChanged("emailassignee");
                //Controller.UpdateBody(UserEmail, Id, value);
            }
        }



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


        internal void UpdateTitle()
        {
            Message = "";
            try
            { 
                task.UpdateTaskTitle(usermail, creatorofboard, boardname, colomord, task.Id, Title);
                Message = "Updated Task title succefully!";
            }
            catch (Exception e)
            {
                Message = e.Message;
            }


        }


        internal void UpdateDuedate()
        {
            Message = "";
            try
            {
                DateTime newduedate = new DateTime(int.Parse(Year), int.Parse(Month), int.Parse(Day),int.Parse(Hour),int.Parse(Minute),0);
                task.UpdateTaskDueDate(usermail, creatorofboard, boardname, colomord, task.Id, newduedate);
                Message = "Updated Task Descreption succefully!";


            }
            catch (Exception e)
            {
                Message = e.Message;
            }


        }


        internal void UpdateDesc()
        {
            Message = "";
            try
            {
                task.UpdateTaskTitle(usermail, creatorofboard, boardname, colomord, task.Id, Descreption);
                Message = "Updated Task Descreption succefully!";


            }
            catch (Exception e)
            {
                Message = e.Message;
            }


        }





    }
}
