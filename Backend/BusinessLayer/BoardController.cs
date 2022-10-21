using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.DataAccessLayer;
using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using log4net;







namespace IntroSE.Kanban.Backend.BusinessLayer
{

    class BoardController
    {
        
        private Dictionary<Tuple<string, string>, Board> boards_dict = new Dictionary<Tuple<string, string>, Board>();
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private UserController userController;
        private BoardDalController boardDal=new BoardDalController();
        private TaskDalController taskDal = new TaskDalController();
        private UserDalController userDal = new UserDalController();
        private Boolean ispersictent;


        //Constructor:
        public BoardController(UserController uc) { this.userController = uc;
            this.ispersictent = false;
        }



        /// <summary>
        /// this function loads the data from the DALayer
        /// </summary>
        internal void loadData()
        {

            List<BoardDTO> boardDTOs = boardDal.SelectAlBoards();
            foreach (BoardDTO board in boardDTOs)
            {
                if (!boards_dict.ContainsKey(Tuple.Create(board.email, board.boardname)))
                {
                    List<TaskDTO> taskDTOs = taskDal.SelectTasksByBoardId(board.Id.ToString());
                    Board BLBoard = new Board(board, taskDTOs);

                    for (int colOrd = 0; colOrd < 3; colOrd += 1)
                    {
                        foreach (TaskDTO task in taskDTOs)
                        {
                            Task newTask = new Task(task);

                            GetColumn(BLBoard, colOrd).Add(newTask);

                        }
                    }

                    boards_dict.Add(Tuple.Create(BLBoard.getAdminUser, BLBoard.getName), BLBoard);
                }
            }
            this.ispersictent = true;

        }

        /// <summary>
        /// this function deletes the data from the DALayer
        /// </summary>
        internal void deleteData()
        {
            boardDal.DeleteData();
            taskDal.DeleteData();
            boards_dict = new Dictionary<Tuple<string, string>, Board>();
        }


        /*
        This function checks if the string input is vaild
        Input:
            1) string str - the string that tested
            2) string varName
        Output:None
        */
        private void validInput(string str, string varName)
        {
            if (str == null | str == "")
            {
                throw new ArgumentException(varName + " is null or emptysapce, please change!");
            }
        }

        

        /*
        This function checks if the board exists and returns an error as expected
        Input:
            1) string email - The email checked
            2) bool exceptedExists - is board exists (true) or opisate
        Output:None
        */
        internal Boolean isBoardExists(string emailcreator, string boardname)
        {
            Tuple<string, string> key = Tuple.Create(emailcreator, boardname);
            return boards_dict.ContainsKey(key);
        }

        /*
        This function creates a new Task
        Input:
            1) string email
            2) string password
            3)string description
            4)DateTime dueDate
        Output:Task - The task that create if the input is valid
        */
        private Task newTaskValidation(string title, string description, DateTime dueDate, string assignee)
        {

            Task newTask = new Task(title, description, dueDate, assignee);
            return newTask;
        }

        /*
        This function creates a new board and checks if it is valid
        Input:
            1) string email
            2) string password
        Output:User - The user that create if the input is valid
        */
        private Board newBoardValidation(string email, string boardName)
        {
            Board newBoard = new Board(boardName, email);
            return newBoard;
        }

        /*
        This function checks if the user is connected
        Input:string userEmail 
        Output:None
        */
        internal void userLogedIn(string email)
        {
            userController.isUserLogedIn(email,true);
            userController.isAnotherUserLogedIn(email);
        }

        /*
        This function returns a list of all the tasks in the column
        Input:Column col
        Output:List<ServiceLayer.Task> - all the tasks in the column
        */
        private List<Task> columnToTasksList(Column col)
        {
            List<Task> tasksList = new List<Task>();
            foreach (Task task in col.getTasks)
            {
                tasksList.Add(new Task(task.getID, task.getTitle, task.getDescription, task.getDueDate,task.getAssignee));
            }
            return tasksList;
        }
        /*
        This function updates the task dueDate
        Input:
            1) Task task - The task is updated
            2) DateTime dueDate - The new dueDate
        Output:Task - The updated task
        */
        private Task updateTask(Task task, DateTime dueDate)
        {
            Task newTask = task;
            newTask.editTaskData(task.getTitle, task.getDescription, dueDate);

            return newTask;
        }
        /*
        This function updates the task title or description
        Input:
            1)Task task - The task is updated
            2)string fieldName - The field that need to update
            3)string field - The new value of the feild
        Output:Task - The updated task
        */
        private Task updateTask(Task task, string fieldName, string field)
        {
            Task newTask = task;
            if (fieldName == "Title")
            {
                newTask.editTaskData(field, task.getDescription, task.getDueDate);
            }
            else if (fieldName == "Description")
            {
                newTask.editTaskData(task.getTitle, field, task.getDueDate);
            }
            return newTask;
        }
        public Task AddTask(string email, string creatorEmail, string boardName, string title, string description, DateTime dueDate)
        {
            if (!ispersictent)
                loadData();
            validInput(email, "Email");
            validInput(boardName, "BoardName");
            validInput(creatorEmail, "CreatorEmail");
            userLogedIn(email);
            Board board = GetBoard(creatorEmail, boardName);
            isMemeber(email, board);
            Task newTask = newTaskValidation(title, description, dueDate, creatorEmail);
            board.addTask(newTask);
            taskDal.Insert(new DataAccessLayer.DTOs.TaskDTO(board.getId, creatorEmail, boardName, newTask.getID, title, newTask.getCreationTime.ToString(), newTask.getDescription, "false", "backlog","0", email, dueDate.ToString()));

            
            
            log.Info(email + " added task to " + boardName + " board");
            return newTask;
        }

        /*
        This function returns the list of all tasks in the "In Process" column which useremail is assiggned to
        Input:None
        Output:List<Task> - list of all tasks in the "In Process" column which useremail is assiggned to
        */
        public List<Task> InProgressTasks(string userEmail)
        {
            validInput(userEmail, "UserEmail");
            userLogedIn(userEmail);
            List <Task> allinProgressTasks = new List<Task>();
            foreach (Board board in boards_dict.Values)
            {
                if(board.isMemeber(userEmail))
                {
                    List<Task> currentinProgressTasks= board.getInProgress();
                    foreach (Task task in currentinProgressTasks)
                    {
                        if (task.getAssignee.Equals(userEmail))
                        {
                            allinProgressTasks.Add(task);
                        }
                    }
                    
                }
            }
            return allinProgressTasks;
        }



        /*
        This function creates a new board
        Input:
            1)string boardCreator - Email of the user who try to create new board
            2)string boardName - Name of the new board
        Output:None
        */
        public void AddBoard(string boardCreator, string boardName)
        {
            if (!ispersictent)
                loadData();
            validInput(boardName, "BoardName");
            validInput(boardCreator, "BoardCreator");
            userLogedIn(boardCreator);
            if(isBoardExists(boardCreator, boardName))
            {
                throw new ArgumentException("this board " + boardName + " already exists for user " + boardCreator);
            }
            Board board = newBoardValidation(boardCreator, boardName);
            boardDal.Insert(new DataAccessLayer.DTOs.BoardDTO(board.getId,boardName, boardCreator,board.getMembersInString(), "-1","-1","-1", board.getColsNameInString(), board.getColsLimitsInString()));
            Tuple<string, string> key = Tuple.Create(boardCreator, boardName);
            boards_dict[key] = board;

            log.Info(boardCreator + " added his first board" + boardName);
        }

        private void removeTasksOfBoard(Board board)
        {
            for(int colOrd = 0; colOrd < 3; colOrd += 1)
            {
                foreach(Task task in GetColumn(board, colOrd))
                {
                    taskDal.Delete(new DataAccessLayer.DTOs.TaskDTO(board.getId, task.getCreatorMail, board.getName, task.getID, task.getTitle, task.getCreationTime.ToString(), task.getDescription, (task.getCurrentColumn == "in progress").ToString(), task.getCurrentColumn,task.getCurrentColumnord, task.getAssignee, task.getDueDate.ToString()));
                }
            }
        }
        /*
        This function delete board
        Input:
            1)string boardRemover - Email of the user who try to delete the board
            2)string boardName - Name of the board
        Output:None
        */
        public void RemoveBoard(string boardRemover, string creatorEmail, string boardName)
        {

            validInput(boardName, "BoardName");
            validInput(boardRemover, "BoardRemover");
            validInput(creatorEmail, "CreatorEmail");
            userLogedIn(boardRemover);
            Board board = GetBoard(creatorEmail, boardName);
            if(board.isCreator(boardRemover))
            {
                
                boards_dict.Remove(Tuple.Create(creatorEmail, boardName));
                removeTasksOfBoard(board);
                boardDal.Delete(new DataAccessLayer.DTOs.BoardDTO(board.getId, boardName, boardRemover, "", "", "", "","",""));
                log.Info("User " + boardRemover + " removed board " + boardName);
            }
            else
            {
                throw new ArgumentException(boardRemover + " is not the creator of board " + boardName + " so he can't delete it");
            }
        }
        /*
        This function update task dueDate field
        Input:
            1)string boardRemover - The email of the user 
            2)string boardName - The borad name 
            3)int columnOrdinal - The col number of the task
            4)int taskId - The id of the task that we try to advance
            5)DateTime dueDate - The new dueDate
        Output:None
        */
        public void UpdateTaskDueDate(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, DateTime dueDate)
        {
            validInput(userEmail, "UserEmail");
            validInput(boardName, "BoardName");
            validInput(creatorEmail, "CreatorEmail");
            userLogedIn(userEmail);
            Board board = GetBoard(creatorEmail, boardName);
            Task task = GetTask(creatorEmail, boardName, columnOrdinal, taskId);
            if (task.getAssignee.Equals(userEmail))
            {
                Task newTask = updateTask(task, dueDate);
                board.updateTask(newTask, columnOrdinal);
                taskDal.Update(taskId, TaskDTO.TasksdueDateColumnName, dueDate.ToString());

            }
            else
            {
                throw new ArgumentException(userEmail + " is not this task assignee so he can't edit the task");
            }
        }


        private Task GetTask(string creatorEmail, string boardName, int columnOrdinal, int taskId)
        {
            return GetBoard(creatorEmail, boardName).getTask(columnOrdinal, taskId);
        }
        /*
        This function update task title field
        Input:
            1)string boardRemover - The email of the user 
            2)string boardName - The borad name 
            3)int columnOrdinal - The col number of the task
            4)int taskId - The id of the task that we try to advance
            5)string title - The new title
        Output:None
        */
        public void UpdateTaskTitle(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string title)
        {
            validInput(userEmail, "UserEmail");
            validInput(boardName, "BoardName");
            validInput(creatorEmail, "CreatorEmail");
            userLogedIn(userEmail);
            Board board = GetBoard(creatorEmail, boardName);
            Task task = GetTask(creatorEmail, boardName, columnOrdinal, taskId);
            if (task.getAssignee.Equals(userEmail))
            {
                Task newTask = updateTask(task, "Title", title);
                board.updateTask(newTask, columnOrdinal);
                taskDal.Update(taskId, TaskDTO.TaskstitalColumnName, title);

            }
            else
            {
                throw new ArgumentException(userEmail + " is not this task assignee so he can't edit the task");
            }

        }

        public void AssignTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string emailAssignee)
        {

            validInput(userEmail, "UserEmail");
            validInput(creatorEmail, "CreatorEmail");
            validInput(boardName, "BoardName");
            validInput(emailAssignee, "EmailAssignee");
            userLogedIn(userEmail);
            Board board = GetBoard(creatorEmail, boardName);
            isMemeber(userEmail, board);
            if (!board.getCol(columnOrdinal).getTask(taskId).getAssignee.Equals(emailAssignee))
            {
                board.getCol(columnOrdinal).getTask(taskId).setAssignee(emailAssignee);
                taskDal.Update(taskId, TaskDTO.TasksassigneeColumnName, emailAssignee);
            }
            else
            {
                throw new ArgumentException(emailAssignee + " is already assigend to task with ID " + taskId);
            }

        }

        internal List<Board> GetBoardNames(string userEmail)
        {
            validInput(userEmail, "UserEmail");
            userLogedIn(userEmail);
            List<Board> allinboardsTasks = new List<Board>();
            foreach (Board board in boards_dict.Values)
            {
                if (board.isMemeber(userEmail))
                {
                    allinboardsTasks.Add(board);

                }
            }
            return allinboardsTasks;

        }

        /*
This function update task title description
Input:
   1)string boardRemover - The email of the user 
   2)string boardName - The borad name 
   3)int columnOrdinal - The col number of the task
   4)int taskId - The id of the task that we try to advance
   5)string description - The new description
Output:None
*/
        public void UpdateTaskDescription(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId, string description)
        {
            validInput(userEmail, "UserEmail");
            validInput(boardName, "BoardName");
            validInput(creatorEmail, "CreatorEmail");
            userLogedIn(userEmail);
            Board board = GetBoard(creatorEmail, boardName);
            Task task = GetTask(creatorEmail, boardName, columnOrdinal, taskId);
            if (task.getAssignee.Equals(userEmail))
            {
                Task newTask = updateTask(task, "Description", description);
                
                board.updateTask(newTask, columnOrdinal);
                taskDal.Update(taskId, TaskDTO.TasksdescriptionColumnName, description);
            }
            else
            {
                throw new ArgumentException(userEmail + " is not this task assignee so he can't edit the task");
            }
        }

        internal void RemoveColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal)
        {
            validInput(boardName, "BoardName");
            validInput(userEmail, "Email");
            validInput(creatorEmail, "CreatorEmail");
            userLogedIn(userEmail);
            Board board = GetBoard(creatorEmail, boardName);
            isMemeber(userEmail, board);
            board.RemoveColumn(columnOrdinal); ;
            boardDal.Update(board.getId, BoardDTO.BoardsColsLimits, board.getColsLimitsInString());
            boardDal.Update(board.getId, BoardDTO.BoardsColsNames, board.getColsNameInString());
        }

        /*
This function advances a task to the next column in the table
Input:
1)string boardRemover - The email of the user 
2)string boardName - The borad name 
3)int columnOrdinal - The col number of the task
4)int taskId - The id of the task that we try to advance
Output:None
*/
        public void AdvanceTask(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int taskId)
        {
            validInput(userEmail, "UserEmail");
            validInput(boardName, "BoardName");
            validInput(creatorEmail, "CreatorEmail");
            userLogedIn(userEmail);
            Board board = GetBoard(creatorEmail, boardName);
            if (GetTask(creatorEmail, boardName, columnOrdinal, taskId).getAssignee.Equals(userEmail))
            {
                
                board.moveTask(GetTask(creatorEmail, boardName, columnOrdinal, taskId), columnOrdinal);
                taskDal.Update(taskId, TaskDTO.TaskscurrentColumnNameColumnName, board.getColName(columnOrdinal + 1));
                taskDal.Update(taskId, TaskDTO.TasksinProgressColumnName, "true");
            }
            else
            {
                throw new ArgumentException("Only the task assignee can move tasks!");
            }
         
        }

        internal void AddColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, string columnName)
        {
            validInput(boardName, "BoardName");
            validInput(userEmail, "Email");
            validInput(creatorEmail, "CreatorEmail");
            userLogedIn(userEmail);
            Board board = GetBoard(creatorEmail, boardName);
            isMemeber(userEmail, board);
            board.AddColumn(columnOrdinal, columnName);
            boardDal.Update(board.getId, BoardDTO.BoardsColsLimits, board.getColsLimitsInString());
            boardDal.Update(board.getId, BoardDTO.BoardsColsNames, board.getColsNameInString());
        }

        /*
        This function returns the column name
        Input:
            1)string email - The user who asked for the column name
            2)string boardName - Name of the board
            3)int columnOrdinal - The ordinal number of the column the used asked for
        Output:None
        */
        public String GetColumnName(string email,string creatorEmail, string boardName, int columnOrdinal)
        {
            validInput(boardName, "BoardName");
            validInput(email, "Email");
            validInput(creatorEmail, "CreatorEmail");
            userLogedIn(email);
            Board board = GetBoard(creatorEmail, boardName);
            isMemeber(email,board);
            return board.getColName(columnOrdinal);
        }

        internal void RenameColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, string newColumnName)
        {
            validInput(boardName, "BoardName");
            validInput(userEmail, "Email");
            validInput(creatorEmail, "CreatorEmail");
            userLogedIn(userEmail);
            Board board = GetBoard(creatorEmail, boardName);
            isMemeber(userEmail, board);
            board.RenameColumn(columnOrdinal, newColumnName);
            boardDal.Update(board.getId, BoardDTO.BoardsColsNames, board.getColsNameInString());
        }

        /*
        This function returns the column tasks
        Input:
            1)string email - The user who asked for the column tasks
            2)string boardName - Name of the board
            3)int columnOrdinal - The ordinal number of the column the used asked for
        Output:List<ServiceLayer.Task> - list of all tasks in the column
        */
        public List<Task> GetColumn(string email, string creatorEmail, string boardName, int columnOrdinal)
        {

            validInput(boardName, "BoardName");
            validInput(email, "Email");
            validInput(creatorEmail, "CreatorEmail");
            userLogedIn(email);
            Board board = GetBoard(creatorEmail, boardName);
            isMemeber(email, board);
            return GetColumn(board, columnOrdinal);
        }

        public List<Column> GetColumns(string email, string creatorEmail, string boardName)
        {

            validInput(boardName, "BoardName");
            validInput(email, "Email");
            validInput(creatorEmail, "CreatorEmail");
            userLogedIn(email);
            Board board = GetBoard(creatorEmail, boardName);
            isMemeber(email, board);
            return board.GetColumns();
        }

        private List<Task> GetColumn(Board board, int columnOrdinal)
        {
            return columnToTasksList(board.getCol(columnOrdinal));
        }

        internal void MoveColumn(string userEmail, string creatorEmail, string boardName, int columnOrdinal, int shiftSize)
        {
            validInput(boardName, "BoardName");
            validInput(userEmail, "Email");
            validInput(creatorEmail, "CreatorEmail");
            userLogedIn(userEmail);
            Board board = GetBoard(creatorEmail, boardName);
            isMemeber(userEmail, board);
            board.MoveColumn(columnOrdinal, shiftSize);
            boardDal.Update(board.getId, BoardDTO.BoardsColsLimits, board.getColsLimitsInString());
            boardDal.Update(board.getId, BoardDTO.BoardsColsNames, board.getColsNameInString());
        }

        /*
This function returns the column tasks limit
Input:
   1)string email - The user who asked for the column asks limit
   2)string boardName - Name of the board
   3)int columnOrdinal - The ordinal number of the column the used asked for
Output:int - the tasks limit of the asked column
*/
        public int GetColumnLimit(string email, string creatorEmail,string boardName, int columnOrdinal)
        {
            validInput(boardName, "BoardName");
            validInput(email, "Email");
            validInput(creatorEmail, "CreatorEmail");
            userLogedIn(email);
            Board board = GetBoard(creatorEmail, boardName);
            isMemeber(email,board);
            return board.getCol(columnOrdinal).getLimit;
        }

        /*
        This function limits the number of tasks in the column
        Input:
            1)string email - The user who asked for the column try to limit the number of tasks in the column
            2)string boardName - Name of the board
            3)int columnOrdinal - The ordinal number of the column the used asked for
            4)int limit - The new limit of number of tasks in the column
        Output:None
        */
        public void LimitColumn(string email, string creatorEmail, string boardName, int columnOrdinal, int limit)
        {
            validInput(boardName, "BoardName");
            validInput(email, "Email");
            validInput(creatorEmail, "CreatorEmail");
            userLogedIn(email);
            Board board = GetBoard(creatorEmail, boardName);
            isMemeber(email,board);
            board.getCol(columnOrdinal).setLimit(limit);
            boardDal.Update(board.getId, BoardDTO.BoardsColsLimits, board.getColsLimitsInString());
           
            /*
            BoardDTO bDto = new BoardDTO(board.getId, board.getName, board.getAdminUser, board.getMembersInString(), "", "", "");
                if (columnOrdinal == 0)
                    bDto.collimit0 = limit.ToString();
                if (columnOrdinal == 1)
                    bDto.collimit1 = limit.ToString();
                if (columnOrdinal == 2)
                    bDto.collimit2 = limit.ToString();
            */

        }

        /*
        This function returns the asked board from the dictionary
        Input:
            1)string email - The user email
            2)string boardName - Name of the board
        Output:Board
        */
        internal Board GetBoard(string email, string boardName)
        {
            validInput(email, "Email");
            validInput(boardName, "BoardName");
            if (!isBoardExists(email, boardName))
            {
                throw new ArgumentException("the board " + boardName + " does not exists");
            }
            Tuple<string, string> key = Tuple.Create(email, boardName);
            return boards_dict[key];
        }

        internal void JoinBoard(string userEmail, string creatorEmail, string boardName)
        {
            validInput(userEmail,"userEmail");
            validInput(boardName, "BoardName");
            validInput(creatorEmail, "creatorEmail");
            userLogedIn(userEmail);
            Board board = GetBoard(creatorEmail, boardName);
            board.JoinBoard(userEmail);
            boardDal.Update(board.getId, BoardDTO.BoardsmembersColumnName, board.getMembersInString());
        }


        private void isMemeber(string member, Board board)
        {
            if (!board.isMemeber(member))
            {
                throw new ArgumentException("User " + member + " is not a member of board " + board.getName);
            }
        }

    }
}
