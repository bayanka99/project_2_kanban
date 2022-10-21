using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Presentation.Model
{
    public class BoardModel : NotifiableModelObject
    {
        public readonly string usermail;
        private string boardname;
        private string creator_mail;
        public readonly string colmunname;

        public ObservableCollection<ColumnModel> Columnscollection { get; set; }

        public string BoardName
        {
            get => boardname;
            set
            {
                this.boardname = value;
                RaisePropertyChanged("BoardName");
            }
        }

        public string BoardCreator
        {
            get => creator_mail;

            set
            {
                this.creator_mail = value;
                RaisePropertyChanged("BoardCreator");
            }
        }

        private BoardModel(BackendController controller, ObservableCollection<ColumnModel> columns) : base(controller)
        {
            Columnscollection = columns;
            Columnscollection.CollectionChanged += HandleChange;
        }
        public BoardModel(BackendController controller, string user, string boardCreator, string boardName) : base(controller)
        {
            this.usermail = user;
            this.boardname = boardName;
            this.creator_mail = boardCreator;
            Columnscollection = new ObservableCollection<ColumnModel>();
            Columnscollection.CollectionChanged += HandleChange;
        }
        public BoardModel(BackendController controller, string user, Tuple<string, string> board) : base(controller)
        {
            this.usermail = user;
            this.boardname = board.Item2;
            this.creator_mail = board.Item1;
            Columnscollection = new ObservableCollection<ColumnModel>(controller.GetColumns(this.usermail, this.creator_mail, this.boardname));
            Columnscollection.CollectionChanged += HandleChange;
        }

        internal void AddTask(string usermail, string title, string description, DateTime dueDate)
        {
            //TaskModel newtaskmode = this.Controller.AddTask(usermail, boardCreator, boardname, title, description, dueDate);
            this.Columnscollection[0].AddTask(usermail, BoardCreator, boardname, title, description, dueDate);
        }

        internal void AdvenceTask(string usermail)
        {
            ColumnModel c = (ColumnModel)Columnscollection.Where(c => c.SelectedTask != null).FirstOrDefault();
            if (c == null)
                throw new Exception("please select task");

            //TaskModel newtaskmode = this.Controller.AddTask(usermail, boardCreator, boardname, title, description, dueDate);
            //colmodel.AdvanceTask(colmodel.SelectedTask, usermail,creator_mail,BoardName,colmodel.colord);
            this.Controller.AdvanceTask(usermail, BoardCreator, BoardName, int.Parse(c.SelectedTask.Colord), c.SelectedTask.Id);
            
            loadColumnCollection();
        }

        public void loadColumnCollection()
        {
            Columnscollection = new ObservableCollection<ColumnModel>(Controller.GetColumns(this.usermail, this.creator_mail, this.boardname));
            //Columnscollection = new ObservableCollection<ColumnModel>(Columnscollection.OrderBy(i => i.colord));
            Columnscollection.CollectionChanged += HandleChange;

        }

        internal void AddColumn(string usermail, string creator_mail, string boardname,int newcolumnord, string newcolumnname)
        {
            ColumnModel colmod = new ColumnModel(this.Controller, new ObservableCollection<TaskModel>(), newcolumnord,newcolumnname);
            this.Columnscollection.Insert(newcolumnord,colmod);
        }

        internal void Deletecolumn(ColumnModel column)
        {
            this.Columnscollection.Remove(column);
        }

        internal void Limitcolumn(ColumnModel column,int newlimit)
        {
            this.Columnscollection.Remove(column);
            this.Controller.LimitColumn(this.usermail, this.creator_mail, this.boardname, column.colord, newlimit);
            column.limit = newlimit;
            this.Columnscollection.Add(column);
        }


        internal void Movecol(ColumnModel column,int sizeshift)
        {
            this.Columnscollection.Remove(column);
            this.Controller.MoveColumn(this.usermail, this.creator_mail, this.boardname, column.colord, sizeshift);
            this.Columnscollection.Add(column);
            
        }

        internal void RenameColumn(ColumnModel column,string newname)
        {
            this.Columnscollection.Remove(column);
            this.Controller.RenameColumn(this.usermail, this.creator_mail, this.boardname, column.colord, newname);
            column.name = newname;
            this.Columnscollection.Add(column);
        }



        private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        {


            //read more here: https://stackoverflow.com/questions/4279185/what-is-the-use-of-observablecollection-in-net/4279274#4279274

            switch (e.Action)
            {
                
                case NotifyCollectionChangedAction.Remove:
                    foreach (ColumnModel y in e.OldItems)
                    {

                        Controller.RemoveColumn(this.usermail, this.creator_mail, this.boardname, y.colord);
                    }

                    break;

                case NotifyCollectionChangedAction.Add:
                    foreach (ColumnModel y in e.NewItems)
                    {
                        //Columnscollection = new ObservableCollection<ColumnModel>(Columnscollection.OrderBy(i => i.colord));
                        //Columnscollection.CollectionChanged += HandleChange;
                        Controller.AddColumn(this.usermail, this.creator_mail, this.boardname, y.colord, y.name);// maybe there will be anothre copy, check boardviewmodel
                    }

                    break;
                case NotifyCollectionChangedAction.Move:
                    foreach (ColumnModel y in e.OldItems)
                    {
                        y.colord++;
                        //Columnscollection = new ObservableCollection<ColumnModel>(Columnscollection.OrderBy(i => i.colord));
                        //Columnscollection.CollectionChanged += HandleChange;
                        //Controller.AddColumn(this.usermail, this.creator_mail, this.boardname, y.colord, y.name);// maybe there will be anothre copy, check boardviewmodel
                    }

                    break;
            }


        }
    }
}
