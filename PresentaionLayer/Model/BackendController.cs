using System;
using System.Collections.Generic;
using System.Windows;

using IntroSE.Kanban.Backend.ServiceLayer;
using System.Collections.ObjectModel;
using System.Linq;

namespace Presentation.Model
{
    public class BackendController
    {
        private Service Service { get; set; }
        public BackendController(Service service)
        {
            this.Service = service;
        }

        public BackendController()
        {
            this.Service = new Service();
            Service.LoadData();
        }

        public UserModel Login(string username, string password)
        {
            Response<User> user = Service.Login(username, password);
            if (user.ErrorOccured)
            {
                throw new Exception(user.ErrorMessage);
            }
            return new UserModel(this, username);
        }


        /*
        internal (int Id, string Title, string Body) GetMessage(string email, int messageId)
        {
            Message ans = Service.GetMessage(email, messageId).Value;
            return (ans.Id, ans.Title, ans.Body);
        }
        */


        internal void Register(string username, string password)
        {
            Response res = Service.Register(username, password);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
        }

        internal BoardModel GetBoard(object email, object creatorMail, object boardName)
        {
            throw new NotImplementedException();
        }

        public Response Logout(string userEmail)
        {
            Response res = Service.Logout(userEmail);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res;
        }

        public Response LimitColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int limit)
        {
            Response res = Service.LimitColumn(userEmail, creatorEmail, boardName, columnOrdinal, limit);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res;
        }

        /// <summary>
        /// Get the limit of a specific column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column location. The first column location is identified by 0, the location increases by 1 for each column</param>
        /// <returns>The limit of the column.</returns>
        public Response<int> GetColumnLimit(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the name of a specific column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column location. The first column location is identified by 0, the location increases by 1 for each column</param>
        /// <returns>The name of the column.</returns>
        public Response<string> GetColumnName(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            Response<string> res = Service.GetColumnName(userEmail, creatorEmail, boardName, columnOrdinal);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res;
        }

        public ObservableCollection<ColumnModel> GetColumns(string email, string creatorEmail, string boardName)
        {
            Response<IList<Column>> columnslist = Service.GetColumns(email, creatorEmail, boardName);

            if (columnslist.ErrorOccured)
            {
                throw new Exception(columnslist.ErrorMessage);
            }


            ObservableCollection<ColumnModel> obsevercolumns = new ObservableCollection<ColumnModel>();

            for (int i = 0; i < columnslist.Value.Count; i = i + 1)// getcolumns get all the columns by order - he does not mix them
            {
                Response<IList<Task>> listoftask = Service.GetColumn(email, creatorEmail, boardName, i);
                if (listoftask.ErrorOccured)
                {
                    throw new Exception(listoftask.ErrorMessage);
                }
                ObservableCollection<TaskModel> observetaks = new ObservableCollection<TaskModel>();
                foreach (Task t in listoftask.Value)
                {
                    observetaks.Add(new TaskModel(this, t.Id, t.Title, t.Description, t.emailAssignee, t.CreationTime, t.DueDate, email, creatorEmail, boardName,t.colord));
                }

                string colmodname = this.GetColumnName(email, creatorEmail, boardName, i).Value;
                ColumnModel colmod = new ColumnModel(this, observetaks, i, colmodname);
                obsevercolumns.Add(colmod);

            }

            return obsevercolumns;


        }


        public TaskModel AddTask(string userEmail, string creatorEmail, string boardName, string title, string description, DateTime dueDate)
        {
            Response<Task> res = Service.AddTask(userEmail, creatorEmail, boardName, title, description, dueDate);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            TaskModel taskmod = new TaskModel(this, res.Value.Id, title, description, res.Value.emailAssignee, res.Value.CreationTime, dueDate, userEmail, creatorEmail, boardName,res.Value.colord);
            return taskmod;
        }




        public Response UpdateTaskDueDate(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            Response res = Service.UpdateTaskDueDate(userEmail, creatorEmail, boardName, columnOrdinal, taskId, dueDate);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res;
        }



        public Response UpdateTaskTitle(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string title)
        {
            Response res = Service.UpdateTaskTitle(userEmail, creatorEmail, boardName, columnOrdinal, taskId, title);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res;
        }



        public Response UpdateTaskDescription(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string description)
        {
            Response res = Service.UpdateTaskDescription(userEmail, creatorEmail, boardName, columnOrdinal, taskId, description);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res;
        }


        public Response AdvanceTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId)
        {
            Response res = Service.AdvanceTask(userEmail, creatorEmail, boardName, columnOrdinal, taskId);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res;

        }
        /// <summary>
        /// Returns a column given it's name
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column location. The first column location is identified by 0, the location increases by 1 for each column</param>
        /// <returns>A response object with a value set to the Column, The response should contain a error message in case of an error</returns>
        public Response<IList<Task>> GetColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Creates a new board for the logged-in user.
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="boardName">The name of the new board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AddBoard(string userEmail, string boardName)
        {

            Response res = Service.AddBoard(userEmail, boardName);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res;

        }

        /// <summary>
        /// Adds a board created by another user to the logged-in user. 
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the new board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response JoinBoard(string userEmail, string creatorEmail, string boardName)
        {
            Response res = Service.JoinBoard(userEmail, creatorEmail, boardName);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res;

        }

        /// <summary>
        /// Removes a board.
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response RemoveBoard(string userEmail, string creatorEmail, string boardName)
        {
            Response res = Service.RemoveBoard(userEmail, creatorEmail, boardName);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res;
        }


        /// <summary>
        /// Returns all the in-progress tasks of the logged-in user is assigned to.
        /// </summary>
        /// <param name="userEmail">Email of the logged in user</param>
        /// <returns>A response object with a value set to the list of tasks, The response should contain a error message in case of an error</returns>
        public Response<IList<Task>> InProgressTasks(string userEmail)
        {
            throw new NotImplementedException();
        }


        public Response AssignTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string emailAssignee)
        {
            Response res = Service.AssignTask(userEmail, creatorEmail, boardName, columnOrdinal, taskId, emailAssignee);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res;
        }

        /// <summary>
        /// Returns the list of board of a user. The user must be logged-in. The function returns all the board names the user created or joined.
        /// </summary>
        /// <param name="userEmail">The email of the user. Must be logged-in.</param>
        /// <returns>A response object with a value set to the board, instead the response should contain a error message in case of an error</returns>
        public Response<IList<String>> GetBoardNames(string userEmail)
        {
            Response<IList<String>> res = Service.GetBoardNames(userEmail);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res;
        }
        public Response<IList<Tuple<string, string>>> GetBoards(string userEmail)
        {
            Response<IList<Tuple<string, string>>> ret = Service.GetBoards(userEmail);
            if (ret.ErrorOccured)
            {
                throw new Exception(ret.ErrorMessage);
            }
            return ret;
        }

        //public Response<IList<Board>> GetBoards(string userEmail)
        //{
        //    Response<IList<Board>> ret = Service.GetBoardNamesByBOBJ(userEmail);

        //    if (ret.ErrorOccured)
        //    {
        //        throw new Exception(ret.ErrorMessage);
        //    }

        //    return ret;
        //}




        // ************ New code starts here ***************





        public Response AddColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, string columnName)
        {

            Response res = Service.AddColumn(userEmail, creatorEmail, boardName, columnOrdinal, columnName);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res;
        }




        public Response RemoveColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            Response res = Service.RemoveColumn(userEmail, creatorEmail, boardName, columnOrdinal);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res;
        }



        public Response RenameColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, string newColumnName)
        {
            Response res = Service.RenameColumn(userEmail, creatorEmail, boardName, columnOrdinal, newColumnName);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res;
        }


        public Response MoveColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int shiftSize)
        {
            Response res = Service.MoveColumn(userEmail, creatorEmail, boardName, columnOrdinal, shiftSize);
            if (res.ErrorOccured)
            {
                throw new Exception(res.ErrorMessage);
            }
            return res;
        }



    }
}