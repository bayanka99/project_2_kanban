using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    class BoardDTO : DTO
    {
        public const string BoardsIDColumnName = "id";
        public const string BoardsnameColumnName = "boardname";
        public const string BoardsemailColumnName = "email";
        public const string BoardsmembersColumnName = "members";
        public const string BoardsColsNames = "colsname";
        public const string BoardsColsLimits = "colslimit";

        public const string BoardsColLimit0ColumnName = "collimit0";
        public const string BoardsColLimit1ColumnName = "collimit1";
        public const string BoardsColLimit2ColumnName = "collimit2";

        private string Boardname;
        public string boardname
        {
            get => Boardname; set { Boardname = value; dc.Update(Id, BoardsnameColumnName, value); }
        }
        private string Email;
        public string email
        {
            get => Email; set { Email = value; dc.Update(Id, BoardsemailColumnName, value); }
        }
        private string Members;
        public string members
        {
            get => Members; set { Members = value; dc.Update(Id, BoardsmembersColumnName, value); }
        }
        private string Collimit0;
        public string collimit0
        {
            get => Collimit0; set { Collimit0 = value; dc.Update(Id, BoardsColLimit0ColumnName, value); }
        }
        private string Collimit1;
        public string collimit1
        {
            get => Collimit1; set { Collimit1 = value; dc.Update(Id, BoardsColLimit1ColumnName, value); }
        }
        private string Collimit2;
        public string collimit2
        {
            get => Collimit2; set { Collimit2 = value; dc.Update(Id, BoardsColLimit2ColumnName, value); }
        }
        private string Collimts;
        public string collimts
        {
            get => Collimts; set { Collimts = value; dc.Update(Id, BoardsColsLimits, value); }
        }
        private string Colnames;
        public string colnames
        {
            get => Colnames; set { Colnames = value; dc.Update(Id, BoardsColsNames, value); }
        }

        public BoardDTO(long id, string boardname, string boardcreatoremail, string members, string collimit0, string collimit1, string collimit2, string colnames, string collims) : base(new BoardDalController())
        {
            this.Id = id;
            Boardname = boardname;
            Email = boardcreatoremail;
            Members = members;
            Collimit0 = collimit0;
            Collimit1 = collimit1;
            Collimit2 = collimit2;
            Collimts = collims;
            Colnames = colnames;
        }


    }
}



