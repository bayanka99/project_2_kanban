using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace IntroSE.Kanban.Backend.BusinessLayer
{

    internal class Board
    {
        private int id { get; set; }
        public int getId { get => id; }
        private string adminUser { get; set; }
        public string getAdminUser { get => adminUser; }
        private string name { get; set; }
        public string getName { get => name; }

        private LinkedList<Column> columns;
        private List<string> members;

        public string getMembersInString()  {
            string ret = "";
            foreach (string s in members)
            {
                ret += s+",";
            }
            return ret.Substring(0, ret.Length-1);
        }
        public string getColsNameInString()
        {
            string ret = "";
            LinkedListNode<Column> c = columns.First;
            
            while (c!=null)
            {
                ret += c.Value.Name + ",";
                c = c.Next;
            }
            
            return ret.Substring(0, ret.Length - 1);
        }
        public string getColsLimitsInString()
        {
            string ret = "";
            LinkedListNode<Column> c = columns.First;
            
            while (c != null)
            {
                ret += c.Value.getLimit + ",";
                c = c.Next;
            }

            return ret.Substring(0, ret.Length - 1);
        }
        //Constructor
        internal Board(string name, string adminUser)
        {
            
            this.adminUser = adminUser;
            this.id = IDRandom(new Random());
            this.columns = new LinkedList<Column>();
            this.name = name;
            this.members = new List<string>();
            this.members.Add(adminUser);
            columns.AddLast(new Column("backlog"));
            columns.AddLast(new Column("in progress"));
            columns.AddLast(new Column("done"));
            //this.Persist();//save to file
        }
        internal Board(BoardDTO boardDTO , List<TaskDTO> tasksDTO)
        {
            this.adminUser = boardDTO.email;
            this.id = int.Parse(boardDTO.Id.ToString());
            this.columns = new LinkedList<Column>();
            this.name = boardDTO.boardname;

            string[] collimts = boardDTO.collimts.Split(',');
            string[] colnames = boardDTO.colnames.Split(',');
            for (int i = 0; i < collimts.Length; i++) {
                Column c= new Column(colnames[i]);
                c.setLimit(int.Parse(collimts[i]));
                columns.AddLast(c);
            }

            foreach (TaskDTO task in tasksDTO)
            {
                foreach (Column c in columns)
                {
                    if (c.Name == task.taskcurrentColumnName)
                    {
                        c.addTask(new Task(task), true);
                    }
                }
                
            }
            string[] membersS = boardDTO.members.Split(',');
            this.members = new List<string>();
            foreach (string member in membersS)
                members.Add(member);


            //this.Persist();//save to file
        }
        /*
        The function checks if the task exists in the column
        Input:
            1)Task task - The task being tested
            2)int colOrd - The current column of the task
        Output:None
        */
        private void isTaskExsitsInCol(Task task, int colOrd)
        {
            if(!getCol(colOrd).containTask(task))
            {
                throw new ArgumentException("The task is not in this column");
            }
        }

        /*
        The function advances the task
        Input:
            1)Task task - The task being advanced
            2)int colOrd - The current column of the task
        Output:None
        */
        public void moveTask(Task task, int colOrd)
        {
            
            isTaskExsitsInCol(task, colOrd);
            isDone(colOrd);
            LinkedListNode<Column> c;
            c = columns.First;
            for (int i = 0; i < colOrd; i++)
            {
                c = c.Next;
            }

            c.Next.Value.addTask(task,false);
            c.Value.deleteTask(task);
        }

        /*
        The function update the task
        Input:
            1)Task task - The task being update
            2)Task newTask - The new task
            2)int colOrd - The current column of the task
        Output:None
        */
        public void updateTask(Task newTask, int colOrd)
        {
            isDone(colOrd);
            LinkedListNode<Column> c = columns.First;
            for (int i = 0; i < colOrd; i++)
            {
                c = c.Next;
            }
            c.Next.Value.deleteTask(getTask(colOrd,newTask.getID));
            c.Value.addTask(newTask, false);
        }

        /*
        The function add new task to board
        Input:Task task - The task being added
        Output:None
        */
        public void addTask(Task task)
        {
            LinkedListNode<Column> c = columns.First;
            c.Value.addTask(task, false);
        }

        /*
        This function returns the requested column
        Input:int colOrd - ordinal of the requested column
        Output:Column - The requested column
        */
        internal Column getCol(int colOrd)
        {
            LinkedListNode<Column> c = columns.First;
            for (int i = 0; i < colOrd; i++)
            {
                c = c.Next;
            }
            return c.Value;
            
        }




        internal List<Column> GetColumns()
        {
            List<Column> listofcols = new List<Column>();
            foreach (Column c in columns)
            {
                listofcols.Add(c);

            }
            return listofcols;
        }

        /*
        This function returns the name of requested column
        Input:int colOrd - ordinal of the requested column
        Output:string - The requested column name
        */
        internal string getColName(int colOrd)
        {
            LinkedListNode<Column> c = columns.First;
            for (int i = 0; i < colOrd; i++)
            {
                c = c.Next;
            }
            return c.Value.Name;
            /*
            string ret;
            switch (colOrd)
            {
                case (int)ColumnName.Backlog:
                    ret = "backlog";
                    break;
                case (int)ColumnName.InProgress:
                    ret = "in progress";
                    break;
                case (int)ColumnName.Done:
                    ret = "done";
                    break;
                default:
                    throw new ArgumentException("Column ordinal must be a 0 (Backlog),1(InProgress) or 2(Done)");
            }
            return ret;*/
        }

        /*
        This function checks if the column is done
        Input:int colOrd - ordinal of the column
        Output:None
        */
        private void isDone(int colOrd)
        {

            if (columns.Count == colOrd)
            {
                throw new ArgumentException("The task done cannot move!");
            }
        }

        int IDRandom(Random rand)
        {
            int result = rand.Next((Int32)(100 >> 32), (Int32)(100000000 >> 32));
            result = (result << 32);
            result = result | (int)rand.Next((Int32)100, (Int32)100000000);
            return result;
        }

        /*
         checks if given string is the board creator
        */
        internal bool isCreator(string email)
        {
            return adminUser.Equals(email);
        }


        internal void JoinBoard(string userEmail)
        {
            if(members.Contains(userEmail))
            {
                throw new ArgumentException("User " + userEmail + " already a member of this board: " + name);
            }
            this.members.Add(userEmail);

        }

        internal bool isMemeber(string member)
        {
            return this.members.Contains(member);
        }

        internal Task getTask(int colOrd, int taskID)
        {
            LinkedListNode<Column> c = columns.First;
            for (int i = 0; i < colOrd; i++)
            {
                c = c.Next;
            }
            return c.Value.getTask(taskID);
        }
        internal List<Task> getInProgress()
        {
            List<Task> t = new List<Task>();
            LinkedListNode<Column> c = columns.First.Next;
            for (int i = 0; i < columns.Count-2; i++)
            {
                t.AddRange(c.Value.getTasks);
                c = c.Next;
            }
            return t;
        }

        internal void AddColumn(int columnOrdinal, string columnName)
        {
            //todo need to update db
            if(columnOrdinal==columns.Count)
                columns.AddLast(new Column(columnName));
            else {
                LinkedListNode<Column> c = columns.First;
                for (int i = 0; i < columnOrdinal; i++)
                {
                    c = c.Next;
                }
                columns.AddBefore(c,new Column(columnName));
            }
        }

        internal void RenameColumn(int columnOrdinal, string newColumnName)
        {
            //todo need to update db

            LinkedListNode<Column> c = columns.First;
            for (int i = 0; i < columnOrdinal; i++)
            {
                c = c.Next;
            }
            c.Value.Name = newColumnName;
        }

        internal void RemoveColumn(int columnOrdinal)
        {
            //todo need to update db
            if (columns.Count == 2)
            {
                throw new ArgumentException("cant remove column");
            }
            LinkedListNode<Column> c = columns.First;
            if (columnOrdinal == 0)
            {
                Column next = c.Next.Value;
                if(next.getTasks.Count+c.Value.getTasks.Count <= next.getLimit|| next.getLimit==-1)
                    foreach (Task t in c.Value.getTasks)
                    {
                        next.addTask(t,false);
                    }
                else
                    throw new ArgumentException("cant remove column");
            }
            else
            {
                for (int i = 0; i < columnOrdinal; i++)
                {
                    c = c.Next;
                }
                if(c==null)
                    throw new ArgumentException("cant remove Column");

                Column previous = c.Previous.Value;
                if (previous.getTasks.Count + c.Value.getTasks.Count <= previous.getLimit || previous.getLimit == -1)
                    foreach (Task t in c.Value.getTasks)
                    {
                        previous.addTask(t, false);
                    }
                else
                    throw new ArgumentException("cant remove Column");
            }
            columns.Remove(c);

        }

        internal void MoveColumn(int columnOrdinal, int shiftSize)
        {
            //todo need to update db

            LinkedListNode<Column> c = columns.First;
            for (int i = 0; i < columnOrdinal; i++)
            {
                c = c.Next;
            }
            columns.Remove(c);
            LinkedListNode<Column> c2 = columns.First;
            for (int i = 0; i < columnOrdinal+ shiftSize; i++)
            {
                c2 = c2.Next;
            }
            columns.AddBefore(c2, c);
        }



        //private void Persist()
        //{
        //    ToDalObject().Save();
        //}


    }
}
