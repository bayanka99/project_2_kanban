
using Presentation.Model;
using System;

namespace PresentaionLayer
{
    class JoinBoardViewModel : NotifiableObject
    {
        private Presentation.Model.BackendController controller;
        private UserModel user;
        public BackendController Controller { get; private set; }

        public JoinBoardViewModel(UserModel user)
        {
            this.controller = user.Controller;
            this.user = user;
            
        }

        private string creatorMail;
        public string CreatorMail
        {
            get => creatorMail;
            set
            {
                this.creatorMail = value;
                RaisePropertyChanged("CreatorMail");
            }
        }
        private string boardName;
        public string BoardName
        {
            get => boardName;
            set
            {
                this.boardName = value;
                RaisePropertyChanged("BoardName");
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
        internal BoardModel JoinBoard()
        {
            try
            {
                BoardModel bm = user.JoinBoard(CreatorMail, BoardName);
                Message = "Join board successfully";
                return bm;

            }
            catch (Exception e)
            {
                Message = e.Message;
                return null;
            }
        }






    }
}
