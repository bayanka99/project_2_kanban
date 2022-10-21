using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    static class ColumnConstants
    {
        public const int UNLIMIT = -1;
    }
    internal class Column
    {
        private string name;
        public string Name { get => name; set => name = value; }
        private List<Task> tasks;
        public List<Task> getTasks { get => tasks; }
        private int numberOfTasks;
        public int getNumberOfTasks { get => numberOfTasks; }
        private int maxNumberOfTasks { get; set; }
        public int getLimit { get=> maxNumberOfTasks; }
        public Column() { }

        internal Column(string name)
        {
            this.name = name;
            this.numberOfTasks = 0;
            this.maxNumberOfTasks = ColumnConstants.UNLIMIT;
            tasks = new List<Task>();
        }

        

        /*
        This function adds the task to the column
        Input:Task task - The task you add to the column
        Output:None
        */
        public void addTask(Task task, Boolean isDot)
        {
            if (ColumnConstants.UNLIMIT == maxNumberOfTasks | numberOfTasks < maxNumberOfTasks)
            {
                tasks.Add(task);
                if(!isDot)
                   numberOfTasks += 1;
            }
            else
            {
                throw new ArgumentException("Cannot add a task to a column, because a column is full of tasks");
            }
        }

        /*
        This function delete the task to the column
        Input:Task task - The task you delete to the column
        Output:None
        */
        public void deleteTask(Task task)
        {
            if (tasks.Remove(task))
            {
                numberOfTasks -= 1;
            }
            else//the task doesn't exits
            {
                throw new ArgumentException("The task doesn't exits");
            }
        }

        

        /*
        This function set the limited number of tasks
        Input:int - the limit number of tasks
        Output:None
        */
        internal void setLimit(int limit)
        {
            if(ColumnConstants.UNLIMIT == limit)
            {
                this.maxNumberOfTasks = limit;
            }
            else if (limit < 1)
            {
                throw new ArgumentException("The set limit is invalid You must enter a limit greater than 0 or -1 for unlimit!");
            }
            else if (limit < this.numberOfTasks)
            {
                throw new ArgumentException("This column already limited");
            }
            else
            {
                this.maxNumberOfTasks = limit;
            }
        }

        /*
        This function checks whether the task is in the column
        Input:Task task - The task tested
        Output:bool - task is in the column
        */
        internal bool containTask(Task task)
        {
            return tasks.Contains(task);
        }
        /*
        This function checks whether the task is in the column
        Input:Task task - The task tested
        Output:bool - task is in the column
        */
        internal bool containTask(int task_id)
        {
            foreach(Task task in tasks)
            {
                if(task.getID == task_id)
                {
                    return true;
                }
            }
            return false;
        }
        public Task getTask(int task_id)
        {
            foreach (Task task in tasks)
            {
                if (task.getID == task_id)
                {
                    return task;
                }
            }
            throw new ArgumentException("The task doesn't exist");
        }
    }
}
