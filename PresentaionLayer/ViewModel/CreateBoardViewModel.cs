using PresentaionLayer;
using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Presentation.ViewModel
{
    class CreateBoardViewModel : NotifiableObject
    {
        private UserModel user;
        public UserModel User{get => user;}
        
        public CreateBoardViewModel(UserModel user)
        {
            this.user = user;
            _boardName = "";
        }
        private string _boardName;
        public string BoardName
        {
            get => _boardName;
            set
            {
                this._boardName = value;
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
        internal BoardModel AddBoard()
        {
            Message = "";
            try
            {
                BoardModel bm = user.AddBoard(user.Email, _boardName);
                Message = "Registered successfully";
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
