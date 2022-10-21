using System;
using System.Reflection;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using log4net;

namespace IntroSE.Kanban.Backend.BusinessLayer
{

    static class TaskConstants
    {
        public const int MAX_LENGTH_TITLE = 50;
        public const int MAX_LENGTH_DESCRIPTION = 300;

    }
    public class Task
    {
        //Fields
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly int ID;
        public int getID { get => ID; }
        private readonly DateTime creationTime;
        public DateTime getCreationTime { get => creationTime; }
        private DateTime dueDate;
        public DateTime getDueDate { get => dueDate; }
        private string boardname;
        public string getBoardname { get => boardname; }
        private string creatorMail;
        public string getCreatorMail { get => creatorMail; }

        private string title;
        public string getTitle { get => title; }
        private string description { get; set; }
        public string getDescription { get => description; }
        private string currentColumn;
        public string getCurrentColumn { get => currentColumn; }
        private string currentColumnord;
        public string getCurrentColumnord { get => currentColumnord; }
        private string assignee;
        public string getAssignee { get => assignee; }

        public Task(string title, string description, DateTime dueDate, string creator)
        {
            validationDueDate(dueDate);
            valdationTitle(title);
            valdationDescription(description);
            
            this.creatorMail = creator;
            this.assignee = creator;
            this.ID = IDRandom(new Random());
            this.title = title;
            this.description = description;
            this.dueDate = dueDate;
            this.creationTime = DateTime.Now;
        }
        public Task(int ID, string title, string description, DateTime dueDate, string creator)
        {
            validationDueDate(dueDate);
            valdationTitle(title);
            valdationDescription(description);
            this.creatorMail = creator;
            this.assignee = creator;
            this.ID = ID;
            this.title = title;
            this.description = description;
            this.dueDate = dueDate;
            this.creationTime = DateTime.Now;
        }
        internal Task(TaskDTO taskDTO)
        {

            this.assignee = taskDTO.taskassignee;
            this.ID = int.Parse(taskDTO.Id.ToString());
            this.creatorMail = taskDTO.taskemail;
            this.boardname = taskDTO.boardname;
            this.title = taskDTO.tasktitle;
            this.currentColumn = taskDTO.taskcurrentColumnName;
            this.currentColumnord = taskDTO.taskcurrentColumnOrd;

            this.description = taskDTO.taskdescription;
            this.dueDate = DateTime.Parse(taskDTO.taskdueDate);
            this.creationTime = DateTime.Parse(taskDTO.taskcreationTime);
        }

      
        
        internal void setDueDate(DateTime dueDate)
        {
            validationDueDate(dueDate);
            this.dueDate = dueDate;
        }
        
        
        
        internal void setTitle(string title)
        {
            valdationTitle(title);
            this.title = title;
        }

        
        internal void setDescription(string description)
        {
            valdationDescription(description);
            this.description = description;
        }

        /*
        This function checks whether the input of dueDate is valid
        Input:DateTime dueDate - The new value of dueDate
        Output:None
        */
        private void validationDueDate(DateTime dueDate)
        {
            if (dueDate <= this.creationTime)
            {
                throw new ArgumentException("The dueDate passed!");
            }
            else if (dueDate < DateTime.Now)
            {
                throw new ArgumentException("dueDate passed");
            }
        }

        /*
        This function checks whether the input of title is valid
        Input:string title - The new value of title
        Output:None
        */
        private void valdationTitle(string title)
        {
            if (title==null || title.Length == 0 | title.Length > TaskConstants.MAX_LENGTH_TITLE)
            {
                throw new ArgumentException("Prolong title!");
            }

        }

        /*
        This function checks whether the input of description is valid
        Input:string description - The new value of description
        Output:None
        */
        private void valdationDescription(string description)
        {
            if (description==null || description.Length > TaskConstants.MAX_LENGTH_TITLE)
            {
                throw new ArgumentException("Prolong title!");
            }
        }

        /*
        This function updates the information of the task
        Input:
            1)string title - The new title if this field change
            2)string description - The new description if this field change
            3)DateTime dueDate - The new dueDate if this field change
        Output:None
        */
        public void editTaskData(string title, string description, DateTime dueDate) 
        {
            validationDueDate(dueDate);
            valdationTitle(title);
            valdationDescription(description);
            this.title = title;
            this.description = description;
            this.dueDate = dueDate;
        }

        public void setAssignee(string assigneemail)
        {
            this.assignee = assigneemail;
        }

        int IDRandom(Random rand)
        {
            int result = rand.Next((Int32)(100 >> 32), (Int32)(100000000 >> 32));
            result = (result << 32);
            result = result | (int)rand.Next((Int32)100, (Int32)100000000);
            return result;
        }
    }
}
