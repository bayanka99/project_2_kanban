using IntroSE.Kanban.Backend.ServiceLayer;
using System;

namespace Presentation.Model
{
    public class TaskModel : NotifiableModelObject
    {


        private int _id;
        public int Id
        {
            get => _id;
            private set => _id = value;
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
                RaisePropertyChanged("Descreption");
                //Controller.UpdateBody(UserEmail, Id, value);
            }
        }



        private DateTime _duedate;
        public DateTime Duedate
        {
            get => _duedate;
            set
            {
                this._duedate = value;
                RaisePropertyChanged("duedate");
                //Controller.UpdateBody(UserEmail, Id, value);
            }
        }



        private DateTime _creationtime;
        public DateTime Creationtime
        {
            get => _creationtime;
            set
            {
                this._creationtime = value;
                RaisePropertyChanged("creationtime");
                //Controller.UpdateBody(UserEmail, Id, value);
            }
        }

        private string _emailassignee;
        public string EmailAssignee
        {
            get => _emailassignee;
            set
            {
                this._emailassignee = value;
                RaisePropertyChanged("emailassignee");
                //Controller.UpdateBody(UserEmail, Id, value);
            }
        }



        private string usermail;
        public string UserMail
        {
            get => usermail;
            set
            {
                this.usermail = value;
                RaisePropertyChanged("usermail");
                //Controller.UpdateBody(UserEmail, Id, value);
            }
        }


        private string creatorofboard;
        public string CreatorOfBoard
        {
            get => creatorofboard;
            set
            {
                this.creatorofboard = value;
                RaisePropertyChanged("creatorofboard");
                //Controller.UpdateBody(UserEmail, Id, value);
            }
        }

        private string boardname;
        public string BoardName
        {
            get => boardname;
            set
            {
                this.boardname = value;
                RaisePropertyChanged("boardname");
                //Controller.UpdateBody(UserEmail, Id, value);
            }
        }
        private string colord;
        public string Colord
        {
            get => colord;
            set
            {
                this.colord = value;
                RaisePropertyChanged("Colord");
                //Controller.UpdateBody(UserEmail, Id, value);
            }
        }


        public TaskModel(BackendController controller, int id, string title, string desc, string emailassignee, DateTime creationTime, DateTime duedate, string usermail, string creatorofboard, string boardname, string colord) : base(controller)
        {
            Colord = colord;
            Id = id;
            Title = title;
            Descreption = desc;
            EmailAssignee = emailassignee;
            Creationtime = creationTime;
            Duedate = duedate;
            UserMail = usermail;
            CreatorOfBoard = creatorofboard;
            BoardName = boardname;


        }




        public void UpdateTaskDueDate(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {

            this.Controller.UpdateTaskDueDate(userEmail, creatorEmail, boardName, columnOrdinal, taskId, dueDate);
        }

        public void UpdateTaskTitle(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string title)
        {
            this.Controller.UpdateTaskTitle(userEmail, creatorEmail, boardName, columnOrdinal, taskId, title);
        }

        public void UpdateTaskDescription(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string description)
        {
            this.Controller.UpdateTaskTitle(userEmail, creatorEmail, boardName, columnOrdinal, taskId, description);
        }


        public bool check_percentage()
        {
            double timespan = (this.Duedate - this.Creationtime).TotalMinutes;
            double timeremaining = (this.Duedate - new DateTime()).TotalMinutes;
            return timeremaining / timespan >= 0.75;
        }
        // public TaskModel(BackendController controller, (int Id, string Title, string Body) message, UserModel user) : this(controller, message.Id, message.Title, message.Body, user.Email) { }



    }
}