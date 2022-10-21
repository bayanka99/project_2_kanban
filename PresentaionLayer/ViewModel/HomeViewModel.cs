using PresentaionLayer;
using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Presentation.ViewModel
{
    class HomeViewModel : NotifiableObject
    {

        public UserModel User { get; private set; }
        
        private string boardCreator;
        private bool _isAdmin;
        private string boardName;
        
        public HomeViewModel(UserModel User)
        {
            this.User = User;
            HelloMsg = "Hello " + User.Email;
            _isAdmin = false;
        }

        public string HelloMsg { get; private set; }

        private BoardModel _selectedBoard;
        public BoardModel SelectedBoard
        {
            get
            {
                return _selectedBoard;
            }
            set
            {
                _selectedBoard = value;
                EnableForward = value != null;
                EnableDelete = (value != null && User.Email.Equals(value.BoardCreator));
                RaisePropertyChanged("SelectedBoard");
            }
        }
        private bool _enableForward = false;

        internal BoardModel ChooseBoard()
        {
            try
            {
                return SelectedBoard;
            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot choose this board = " + e.Message);
                return null;
            }
        }

        internal UserModel JoinBoard()
        {
            return User;
        }

        internal void RemoveBoard()
        {
            try
            {
                User.RemoveBoard(SelectedBoard);
            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot this board." + e.Message);
            }
        }

        public bool EnableForward
        {
            get => _enableForward;
            private set
            {
                _enableForward = value;
                RaisePropertyChanged("EnableForward");
            }
        }
        public bool EnableDelete
        {
            get => _isAdmin;
            private set
            {
                _isAdmin = value;
                RaisePropertyChanged("EnableDelete");
            }
        }
        internal void Logout()
        {
            User.logout();
        }

    }
}
