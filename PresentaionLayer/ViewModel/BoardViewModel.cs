using PresentaionLayer;
using Presentation.Model;
using System;
using System.Windows;
using System.Windows.Media;

namespace Frontend.ViewModel

{
    internal class BoardViewModel : NotifiableObject
    {

        
        public Presentation.Model.BackendController controller;
        public BoardModel board { get; private set; }
        public UserModel user { get; private set; }


        private ColumnModel _selectedcolumn;
        public ColumnModel SelectedColumn
        {
            get
            {
                
                return _selectedcolumn;
            }
            set
            {
                _selectedcolumn = value;
               EnableForward = value != null;
                RaisePropertyChanged("SelectedColumn");
            }
        }



        public BoardViewModel(BoardModel board, UserModel user)
        {
            this.controller = board.Controller;
            this.board = board;
            this.user = user;
        }

        public string BoardName { get; private set; }

        private string newcolumnname;
        public string NewColumnName
        {
            get => newcolumnname;
            set
            {
                this.newcolumnname = value;
                RaisePropertyChanged("NewColumnName");
            }
        }

        
        private string colrename;
        public string ColumnReName
        {
            get => colrename;
            set
            {
                this.colrename = value;
                RaisePropertyChanged("ColumnReName");
            }
        }


        private int shiftsize;
        public int ShiftSize
        {
            get => shiftsize;
            set
            {
                this.shiftsize = value;
                RaisePropertyChanged("ShiftSize");
            }
        }


        private int newcolumnord;
        public int NewColumnOrd
        {
            get => newcolumnord;
            set
            {
                this.newcolumnord = value;
                RaisePropertyChanged("NewColumnOrd");
            }
        }


        private int newlimit;
        public int NewLimit
        {
            get => newlimit;
            set
            {
                this.newlimit = value;
                RaisePropertyChanged("NewColumnOrd");
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


        /*
        public SolidColorBrush BackgroundColor {
            get
            {
                return new SolidColorBrush(user.Email.Contains("Bayan") ? Colors.Blue : Colors.Red);                
            }
        }

        */

        ///
        public ColumnModel column { get; private set; }
        public string Title { get; private set; }
        // private MessageModel _selectedMessage;
        /*
         public MessageModel SelectedMessage
         {
             get
             {
                 return _selectedMessage;
             }
             set
             {
                 _selectedMessage = value;
                 EnableForward = value != null;
                 RaisePropertyChanged("SelectedTask");
             }
         }*/
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
        private bool _enableForwardTask = false;
        public bool EnableForwardTask
        {
            get => _enableForwardTask;
            private set
            {
                _enableForwardTask = value;
                RaisePropertyChanged("EnableForwardTask");
            }
        }



        internal void AddColumn()
        {
            Message = "";
            try
            {
                board.AddColumn(board.usermail, board.BoardCreator, board.BoardName, newcolumnord, newcolumnname);
               // controller.AddColumn(board.usermail, board.creator_mail, board.boardname, newcolumnord, newcolumnname);

                Message = "column added succefully!"; 


            }
            catch (Exception e)
            {
                Message = e.Message;
            }


        }

        internal void LimitColumn()
        {
            Message = "";
            try
            {
                board.Limitcolumn(SelectedColumn, NewLimit);
                // controller.AddColumn(board.usermail, board.creator_mail, board.boardname, newcolumnord, newcolumnname);

                Message = "column added succefully!";


            }
            catch (Exception e)
            {
                Message = e.Message;
            }
        }


        internal void DeleteCol()
        {
            Message = "";
            try
            {
                board.Deletecolumn(_selectedcolumn);
                //controller.RemoveColumn(board.usermail, board.creator_mail, board.boardname,SelectedColumn.colord);
                Message = "column deleted succefully!";


            }
            catch (Exception e)
            {
                Message = e.Message;
            }
        }

        internal void RenameColumn()
        {
            Message = "";
            try
            {
                board.RenameColumn(SelectedColumn,NewColumnName);
              // controller.RenameColumn(board.usermail, board.creator_mail, board.boardname, SelectedColumn.colord,colrename);
                Message = "column Renamed succefully!";


            }
            catch (Exception e)
            {
                Message = e.Message;
            }
        }


        internal void MoveCol()
        {
            Message = "";
            try
            {
                board.Movecol(SelectedColumn,ShiftSize);
                //controller.MoveColumn(board.usermail, board.creator_mail, board.boardname, SelectedColumn.colord, ShiftSize);
                Message = "column shifted succefully!";


            }
            catch (Exception e)
            {
                Message = e.Message;
            }
        }

        internal bool AdvenceTask()
        {
            Message = "";
            try
            {
                board.AdvenceTask(user.Email);
                // controller.AddColumn(board.usermail, board.creator_mail, board.boardname, newcolumnord, newcolumnname);
                // AddTask(string userEmail, string creatorEmail, string boardName, string title, string description, DateTime dueDate)
                board.loadColumnCollection();
                Message = "Task advenced succefully!";
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