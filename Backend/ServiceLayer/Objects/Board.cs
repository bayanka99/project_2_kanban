//using IntroSE.Kanban.Backend.ServiceLayer.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public struct Board
    {
        public readonly int Id;
        public readonly string Boardname;
        public readonly string Boardcreator;
        private Dictionary<string, Column> columns;
        private List<string> members;




        internal Board(string boardname, string boardcreator)
        {
            Random rand = new Random();
            this.Id = rand.Next((Int32)(100 >> 32), (Int32)(100000000 >> 32));
            this.Boardname = boardname;
            this.Boardcreator = boardcreator;
            this.members = new List<string>();
            this.members.Add(boardcreator);
            this.columns = new Dictionary<string, Column>();
            //columns.Add("backlog", new Column("backlog"));? what is this?
            //columns.Add("in progress", new Column("in progress"));
            //columns.Add("done", new Column("done"));

        }
    }
}
