using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    class TaskDTO : DTO
    {
        public const string TasksBoardIDColumnName = "boardId";
        
        public const string TasksemailColumnName = "email";
        public const string TasksboardnameColumnName = "boardname";
        public const string TaskstitalColumnName = "titel";
        public const string TasksIDColumnName = "id";
        public const string TaskscreationTimeColumnName = "creationTime";
        public const string TasksdescriptionColumnName = "description";
        public const string TasksinProgressColumnName = "inProgress";
        public const string TaskscurrentColumnNameColumnName = "currentColumnName";
        public const string TaskscurrentColumnOrd = "currentColumnOrd";

        public const string TasksassigneeColumnName = "assignee";
        public const string TasksdueDateColumnName = "dueDate";



        private string Taskemail;
        public string taskemail
        {
            get => Taskemail; set { Taskemail = value; dc.Update(Id, TasksemailColumnName, value); }
        }
        private long Boardid;
        public long boardid
        {
            get => Boardid; set { Boardid = value; dc.Update(Id, TasksBoardIDColumnName, value); }
        }

        private string Boardname;
        public string boardname
        {
            get => Boardname; set { Boardname = value; dc.Update(Id, TasksboardnameColumnName, value); }
        }


        private string Tasktitle;
        public string tasktitle
        {
            get => Tasktitle; set { Tasktitle = value; dc.Update(Id, TaskstitalColumnName, value); }
        }

        private string TaskcreationTime;
        public string taskcreationTime
        {
            get => TaskcreationTime; set { TaskcreationTime = value; dc.Update(Id, TaskscreationTimeColumnName, value); }
        }

        private string Taskdescription;
        public string taskdescription
        {
            get => Taskdescription; set { Taskdescription = value; dc.Update(Id, TasksdescriptionColumnName, value); }
        }

        private string TaskinProgress;
        public string taskinProgress
        {
            get => TaskinProgress; set { TaskinProgress = value; dc.Update(Id, TasksinProgressColumnName, value); }
        }

        private string TaskcurrentColumnName;
        public string taskcurrentColumnName
        {
            get => TaskcurrentColumnName; set { TaskcurrentColumnName = value; dc.Update(Id, TaskscurrentColumnNameColumnName, value); }
        }
        private string TaskcurrentColumnOrd;
        public string taskcurrentColumnOrd
        {
            get => TaskcurrentColumnOrd; set { TaskcurrentColumnOrd = value; dc.Update(Id, TaskscurrentColumnOrd, value); }
        }

        private string Taskassignee;
        public string taskassignee
        {
            get => Taskassignee; set { Taskassignee = value; dc.Update(Id, TasksassigneeColumnName, value); }
        }

        private string TaskdueDate;
        public string taskdueDate
        {
            get => TaskdueDate; set { TaskdueDate = value; dc.Update(Id, TasksdueDateColumnName, value); }
        }


        //todo maybe need to remove at first creation
        public TaskDTO(long boardid,string taskcreatormail, string boardname,long id,string tital,string creatime,string desc,string inprog, string currentcolname, string currentcolord, string assign,string duedate) : base(new TaskDalController())
        {

            Boardid = boardid;
            Taskemail = taskcreatormail;
            Boardname = boardname;
            this.Id = id;
            Tasktitle = tital;
            TaskcreationTime = creatime;
            Taskdescription = desc;
            TaskinProgress = inprog;
            TaskcurrentColumnName = currentcolname;
            TaskcurrentColumnOrd = currentcolord;
            Taskassignee = assign;
            TaskdueDate = duedate;
            
        }




    }
}
