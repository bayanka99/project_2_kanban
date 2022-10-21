using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
   public class Column
    {
        public readonly string name;
        private List<Task> tasks;
        private int numberOfTasks;
        
        public readonly int columnord;

        internal Column(string name,int colord)
        {
            this.name = name;
            this.numberOfTasks = 0;
            this.columnord = colord;
            
            tasks = new List<Task>();
        }


        
        internal void SetTasks(List<Task> listoftasks)
        {
            this.tasks = listoftasks;
            this.numberOfTasks = this.numberOfTasks + listoftasks.Count();
        }

     

    }
}
