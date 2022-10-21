
using System.Reflection;
using System.Collections.Generic;
using System;
using IntroSE.Kanban.Backend.BusinessLayer;
using log4net;
using log4net.Config;
using System.IO;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    internal class BoardService
    {
        private readonly BoardController boardController;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public BoardService(UserController uc)
        {
            boardController = new BoardController(uc);
            // Load configuration
            //Right click on log4net.config file and choose Properties. 
            //Then change option under Copy to Output Directory build action into Copy if newer or Copy always.
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            log.Info("Starting log!");
        }
        ///<summary>This method loads the data from the persistance.
        ///You should call this function when the program starts. </summary>
        public Response LoadData()
        {
            try
            {
                boardController.loadData();
                Response ret = new Response();
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);

                return new Response("LoadData failed!  " + e.Message);
            }
        }

        ///<summary>Removes all persistent data.</summary>
        public Response DeleteData()
        {
            try
            {
                boardController.deleteData();
                Response ret = new Response();
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);

                return new Response("Delete Data failed!  " + e.Message);
            }

        }


        /// <summary>
        /// Add a new task.
        /// </summary>
        /// <param name="email">Email of the user. The user must be logged in.</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="title">Title of the new task</param>
        /// <param name="description">Description of the new task</param>
        /// <param name="dueDate">The due date if the new task</param>
        /// <returns>A response object with a value set to the Task, instead the response should contain a error message in case of an error</returns>
        public Response<Task> AddTask(string email, string creatorEmail, string boardName, string title, string description, DateTime dueDate)
        {

            try
            {
                BusinessLayer.Task ts = boardController.AddTask(email, creatorEmail, boardName, title, description, dueDate);
                Task servicetask = new Task(ts.getID, ts.getCreationTime, ts.getTitle, ts.getDescription, ts.getDueDate, ts.getAssignee,ts.getCurrentColumnord);

                Response<Task> ret = Response<Task>.FromValue(servicetask);
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);
                Response<Task> ret = Response<Task>.FromError("task not added::" + e.Message);
                return ret;
            }
        }





        /// <summary>
        /// Update the due date of a task
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="dueDate">The new due date of the column</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDueDate(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {

            try
            {
                boardController.UpdateTaskDueDate(userEmail, creatorEmail, boardName, columnOrdinal, taskId, dueDate);

                Response ret = new Response();
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);
                Response ret = new Response(e.Message);
                return ret;
            }
        }

        /// <summary>
        /// Update task title
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="title">New title for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskTitle(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string title)
        {
            try
            {
                boardController.UpdateTaskTitle(userEmail, creatorEmail, boardName, columnOrdinal, taskId, title);

                Response ret = new Response();
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);
                Response ret = new Response(e.Message);
                return ret;
            }
        }

        /// <summary>
        /// Update the description of a task
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <param name="description">New description for the task</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response UpdateTaskDescription(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string description)
        {
            try
            {
                boardController.UpdateTaskDescription(userEmail, creatorEmail, boardName, columnOrdinal, taskId, description);

                Response ret = new Response();
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);
                Response ret = new Response(e.Message);
                return ret;
            }
        }

        /// <summary>
        /// Advance a task to the next column
        /// </summary>
        /// <param name="email">Email of user. Must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="taskId">The task to be updated identified task ID</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AdvanceTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId)
        {
            try
            {
                boardController.AdvanceTask(userEmail, creatorEmail, boardName, columnOrdinal, taskId);
                Response ret = new Response();
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);
                //Task task = new Task(taskId, DateTime.Now, "", "", DateTime.Now, userEmail, columnOrdinal);
                Response ret = new Response("task not moved::" + e.Message);
                return ret;
            }
        }

        /// <summary>
        /// Returns all the In progress tasks of the user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <returns>A response object with a value set to the list of tasks, The response should contain a error message in case of an error</returns>
        public Response<IList<Task>> InProgressTasks(string email)
        {
            try
            {
                List<BusinessLayer.Task> bustask = boardController.InProgressTasks(email);
                List<Task> servicetasks = new List<Task>();
                foreach (BusinessLayer.Task task in bustask)
                {
                    servicetasks.Add(new Task(task.getID, task.getCreationTime, task.getTitle, task.getDescription, task.getDueDate, task.getAssignee,task.getCurrentColumnord));
                }
                Response<IList<Task>> ret = Response<IList<Task>>.FromValue(servicetasks);
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);
                Response<IList<Task>> ret = Response<IList<Task>>.FromError("list not returnd::" + e.Message);
                return ret;
            }
        }

        /// <summary>
        /// Removes a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response RemoveBoard(string email, string creatorEmail, string name)
        {
            try
            {
                boardController.RemoveBoard(email, creatorEmail, name);
                Response ret = new Response();
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);
                User user = new User(email);
                Response ret = new Response(name + ":board not removed::" + e.Message);
                return ret;
            }
        }

        public Response AssignTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string emailAssignee)
        {
            try
            {
                boardController.AssignTask(userEmail, creatorEmail, boardName, columnOrdinal, taskId, emailAssignee);
                Response ret = new Response();
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);

                Response ret = new Response(emailAssignee + ":could not be an assignee::" + e.Message);
                return ret;
            }
        }


        /// <summary>
        /// Limit the number of tasks in a specific column
        /// </summary>
        /// <param name="email">The email address of the user, must be logged in</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <param name="limit">The new limit value. A value of -1 indicates no limit.</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response LimitColumn(string useremail, string creatorEmail, string boardName, int columnOrdinal, int limit)
        {
            try
            {
                boardController.LimitColumn(useremail, creatorEmail, boardName, columnOrdinal, limit);
                User user = new User(useremail);
                Response ret = new Response();
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);
                return new Response("limit column falied!  " + e.Message);
            }

        }

        /// <summary>
        /// Get the limit of a specific column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>The limit of the column.</returns>
        public Response<int> GetColumnLimit(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            try
            {
                Response<int> ret = Response<int>.FromValue(boardController.GetColumnLimit(userEmail, creatorEmail, boardName, columnOrdinal));
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);
                Response<int> ret = Response<int>.FromError("failed getting limit!::" + e.Message);
                return ret;
            }
        }

        /// <summary>
        /// Get the name of a specific column
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>The name of the column.</returns>
        public Response<string> GetColumnName(string email, string creatorEmail, string boardName, int columnOrdinal)
        {
            try
            {
                string ans = boardController.GetColumnName(email, creatorEmail, boardName, columnOrdinal);
                Response<string> ret = Response<string>.FromValue(ans);
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);
                Response<string> ret = Response<string>.FromError("colmun name not returned::" + e.Message);
                return ret;
            }
        }

        internal Response<IList<string>> GetBoardNames(string userEmail)
        {
            try
            {

                List<BusinessLayer.Board> c = boardController.GetBoardNames(userEmail);
                List<string> serviceboards = new List<string>();
                foreach (BusinessLayer.Board b in c)
                {
                    serviceboards.Add(b.getName);
                }
                Response<IList<string>> ret = Response<IList<string>>.FromValue(serviceboards);
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);
                Response<IList<string>> ret = Response<IList<string>>.FromError("GetBoardNames not returnd::" + e.Message);
                return ret;
            }
        }
        internal Response<IList<Tuple<string, string>>> GetBoards(string userEmail)
        {
            try
            {
                List<BusinessLayer.Board> c = boardController.GetBoardNames(userEmail);
                List<Tuple<string, string>> serviceboards = new List<Tuple<string, string>>();
                foreach (BusinessLayer.Board b in c)
                {
                    Tuple<string, string> board = Tuple.Create(b.getAdminUser, b.getName);
                    serviceboards.Add(board);
                }
                Response<IList<Tuple<string, string>>> ret = Response<IList<Tuple<string, string>>>.FromValue(serviceboards);
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);
                Response<IList<Tuple<string, string>>> ret = Response<IList<Tuple<string, string>>>.FromError("GetBoardNames not returnd::" + e.Message);
                return ret;
            }
        }
        internal Response<IList<Board>> GetBoardNamesByBOBJ(string userEmail)
        {
            try
            {

                List<BusinessLayer.Board> c = boardController.GetBoardNames(userEmail);
                List<Board> serviceboards = new List<Board>();
                foreach (BusinessLayer.Board b in c)
                {
                    serviceboards.Add(new Board(b.getName,b.getAdminUser));
                }
                Response<IList<Board>> ret = Response<IList<Board>>.FromValue(serviceboards);
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);
                Response<IList<Board>> ret = Response<IList<Board>>.FromError("GetBoardNames not returnd::" + e.Message);
                return ret;
            }
        }

        /// <summary>
        /// Returns a column given its name
        /// </summary>
        /// <param name="userEmail">Email of the current user. Must be logged in</param>
        /// <param name="creatorEmail">Email of the board creator</param>
        /// <param name="boardName">The name of the board</param>
        /// <param name="columnOrdinal">The column ID. The first column is identified by 0, the ID increases by 1 for each column</param>
        /// <returns>A response object with a value set to the Column, The response should contain a error message in case of an error</returns>
        public Response<IList<Task>> GetColumn(string email, string creatorEmail, string boardName, int columnOrdinal)
        {
            try
            {

                List<BusinessLayer.Task> c = boardController.GetColumn(email, creatorEmail, boardName, columnOrdinal);
                List<Task> servicetasks = new List<Task>();
                foreach (BusinessLayer.Task task in c)
                {
                    servicetasks.Add(new Task(task.getID, task.getCreationTime, task.getTitle, task.getDescription, task.getDueDate, task.getAssignee, columnOrdinal.ToString()));
                }
                Response<IList<Task>> ret = Response<IList<Task>>.FromValue(servicetasks);
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);
                Response<IList<Task>> ret = Response<IList<Task>>.FromError("GetColumn not returnd::" + e.Message);
                return ret;
            }
        }




        public Response<IList<Column>> GetColumns(string email, string creatorEmail, string boardName)
        {
            try
            {

                List<BusinessLayer.Column> c = boardController.GetColumns(email, creatorEmail, boardName);
                List<Column> servicecolumns = new List<Column>();
                int colord = 0;
                foreach (BusinessLayer.Column col in c)
                {
                    List<BusinessLayer.Task> businesstasks = col.getTasks;
                    List<Task> servicetasks = new List<Task>();
                    foreach (BusinessLayer.Task task in businesstasks)
                    {
                        servicetasks.Add(new Task(task.getID, task.getCreationTime, task.getTitle, task.getDescription, task.getDueDate, task.getAssignee, task.getCurrentColumnord));
                    }
                    Column servicecol = new Column(col.Name,colord);
                    colord = colord + 1;
                    servicecol.SetTasks(servicetasks);
                    servicecolumns.Add(servicecol);
                }
                Response<IList<Column>> ret = Response<IList<Column>>.FromValue(servicecolumns);
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);
                Response<IList<Column>> ret = Response<IList<Column>>.FromError("GetColumn not returnd::" + e.Message);
                return ret;
            }
        }



        internal Response RemoveColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            try
            {
                boardController.RemoveColumn(userEmail, creatorEmail, boardName, columnOrdinal);
                Response ret = new Response();
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);
                Response ret = new Response(e.Message);
                return ret;
            }
        }

        internal Response AddColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, string columnName)
        {
            try
            {
                boardController.AddColumn(userEmail, creatorEmail, boardName, columnOrdinal, columnName);
                Response ret = new Response();
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);
                Response ret = new Response(e.Message);
                return ret;
            }
        }

        /// <summary>
        /// Adds a board to the specific user.
        /// </summary>
        /// <param name="email">Email of the user. Must be logged in</param>
        /// <param name="name">The name of the new board</param>
        /// <returns>A response object. The response should contain a error message in case of an error</returns>
        public Response AddBoard(string email, string name)
        {

            try
            {
                boardController.AddBoard(email, name);
                Response ret = new Response();
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);
                User user = new User(email);
                Response ret = new Response(email + ":board not added:::" + e.Message);
                return ret;
            }
        }

        internal Response RenameColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, string newColumnName)
        {
            try
            {
                boardController.RenameColumn(userEmail, creatorEmail, boardName, columnOrdinal, newColumnName);
                Response ret = new Response();
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);
                Response ret = new Response(e.Message);
                return ret;
            }
        }

        public Response JoinBoard(string userEmail, string creatorEmail, string boardName)
        {
            try
            {
                boardController.JoinBoard(userEmail, creatorEmail, boardName);
                Response ret = new Response();
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);
                User user = new User(userEmail);
                Response ret = new Response(boardName + ":board not removed::" + e.Message);
                return ret;
            }

        }

        internal Response MoveColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int shiftSize)
        {
            try
            {
                boardController.MoveColumn(userEmail, creatorEmail, boardName, columnOrdinal, shiftSize);
                Response ret = new Response();
                return ret;
            }
            catch (Exception e)
            {
                log.Info(e.Message);
                Response ret = new Response(e.Message);
                return ret;
            }
        }

        /*
This function returns the BoardController
Input:None
Output:BoardController
*/
        internal BoardController sendBC()
        {
            return boardController;
        }







    }





}