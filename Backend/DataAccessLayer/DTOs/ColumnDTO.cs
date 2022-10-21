using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    class ColumnDTO:DTO
    {
        public const string ColumnsIDColumnName = "Id";
        public const string ColumnNameColumnName = "ColumnName";
        public const string ColumnOrdColumnName = "ColumnOrd";
        public const string Boardid = "Boardid";
        public const string Limit = "Limit";

        private string ColumnName;
        public string columnName
        {
            get => ColumnName; set { ColumnName = value; dc.Update(Id, ColumnNameColumnName, value); }
        }
        private string ColumnOrd;
        public string columnOrd
        {
            get => ColumnOrd; set { ColumnOrd = value; dc.Update(Id, ColumnOrdColumnName, value); }
        }

        private int BoardId;
        public int boardid
        {
            get => BoardId; set { BoardId = value; dc.Update(Id, Boardid, value); }
        }

        private int limit;
        public int LIMIT
        {
            get => limit; set { limit = value; dc.Update(Id, Limit, value); }
        }







        public ColumnDTO(int id,string columnName, string columnOrd,int boardid,int limit) : base(new UserDalController())
        {
            this.Id = id;
            this.BoardId = boardid;
            this.LIMIT = limit;
            this.columnName = columnName;
            this.columnOrd = columnOrd;
        }

    }
}
