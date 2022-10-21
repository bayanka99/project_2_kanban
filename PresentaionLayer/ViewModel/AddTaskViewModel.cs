using PresentaionLayer;
using Presentation.Model;
using System;
using System.Windows;
using System.Windows.Media;

namespace Presentation.ViewModel
{
    class AddTaskViewModel : NotifiableObject
    {


        public Presentation.Model.BackendController controller;
        public BoardModel board { get; private set; }
        public UserModel user { get; private set; }


        public AddTaskViewModel(BoardModel board, UserModel user)
        {
            this.controller = board.Controller;
            this.board = board;
            this.user = user;
            DueDate = DateTime.Now;
        }


        private string title;
        public string Title
        {
            get => title;
            set
            {
                this.title = value;
                RaisePropertyChanged("Title");
            }
        }


        private string description;
        public string Description
        {
            get => description;
            set
            {
                this.description = value;
                RaisePropertyChanged("Description");
            }
        }
        private DateTime dueDate;
        public DateTime DueDate
        {
            get => dueDate;
            set
            {
                this.dueDate = value;
                RaisePropertyChanged("DueDate");
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

        internal bool AddTask()
        {
            Message = "";
            try
            {
                board.AddTask(board.usermail, Title, Description,DueDate);
                // controller.AddColumn(board.usermail, board.creator_mail, board.boardname, newcolumnord, newcolumnname);
               // AddTask(string userEmail, string creatorEmail, string boardName, string title, string description, DateTime dueDate)
                Message = "Task added succefully!";
                return true;

            }
            catch (Exception e)
            {
                Message = e.Message;
                return false;
            }


        }




    }
}
