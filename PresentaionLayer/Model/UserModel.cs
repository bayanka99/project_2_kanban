using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Presentation.Model
{
    public class UserModel : NotifiableModelObject
    {
        public ObservableCollection<BoardModel> Boards { get; set; }
        private UserModel(BackendController controller, ObservableCollection<BoardModel> boards) : base(controller)
        {
            Boards = boards;
            Boards.CollectionChanged += HandleChange;
        }

        public UserModel(BackendController controller, string email) : base(controller)
        {
            this.Email = email;
            Boards = new ObservableCollection<BoardModel>(controller.GetBoards(email).Value.ToList()
               .Select((c, i) => new BoardModel(controller,email, c)).ToList());
            Boards.CollectionChanged += HandleChange;
        }

        internal BoardModel AddBoard(string userCreator, string boardName)
        {
            BoardModel bm = new BoardModel(Controller, userCreator, userCreator, boardName);
            Boards.Add(bm);
            bm.loadColumnCollection();
            return bm;
        }

        internal BoardModel JoinBoard(string userCreator, string boardName)
        {
            Controller.JoinBoard(Email, userCreator, boardName);
            BoardModel bm = new BoardModel(Controller, Email, new Tuple<string, string>(userCreator, boardName));
            Boards.Add(bm);
            return bm;
        }


        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged("Email");
            }
        }


        private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            //read more here: https://stackoverflow.com/questions/4279185/what-is-the-use-of-observablecollection-in-net/4279274#4279274
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (BoardModel y in e.OldItems)
                {
                    Controller.RemoveBoard(Email, y.BoardCreator, y.BoardName);
                }

            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (BoardModel y in e.NewItems)
                {
                    if(y.BoardCreator==Email)
                        Controller.AddBoard(y.BoardCreator, y.BoardName);
                }

            }
        }

        internal void RemoveBoard(BoardModel selectedBoard)
        {
            Boards.Remove(selectedBoard);
        }

        internal void logout()
        {
            Controller.Logout(Email);
        }
    }
}
