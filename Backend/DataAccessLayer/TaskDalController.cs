using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class TaskDalController : DalController
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private const string MessageTableName = "Tasks";

        public TaskDalController() : base(MessageTableName)
        {

        }


        public List<TaskDTO> SelectAlTasks()
        {
            List<TaskDTO> result = Select().Cast<TaskDTO>().ToList();

            return result;
        }
        public List<TaskDTO> SelectTasksByBoardId(string boardId)
        {
            List<TaskDTO> result = Select(TaskDTO.TasksBoardIDColumnName,boardId).Cast<TaskDTO>().ToList();

            return result;
        }


        public void DeleteData()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"DELETE FROM {MessageTableName}";
                    command.ExecuteNonQuery();
                }

                finally
                {
                    connection.Close();
                }
            }
        }

        public bool Insert(TaskDTO task)
        {

            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {MessageTableName} ({TaskDTO.TasksBoardIDColumnName},{TaskDTO.TasksemailColumnName},{TaskDTO.TasksboardnameColumnName},{TaskDTO.TasksIDColumnName},{TaskDTO.TaskstitalColumnName},{TaskDTO.TaskscreationTimeColumnName},{TaskDTO.TasksdescriptionColumnName},{TaskDTO.TasksinProgressColumnName},{TaskDTO.TaskscurrentColumnNameColumnName},{TaskDTO.TaskscurrentColumnOrd},{TaskDTO.TasksassigneeColumnName},{TaskDTO.TasksdueDateColumnName} )" +
                        $"VALUES (@boardIDVal,@emailVal,@boardnameVal,@taskIDVal,@titalVal,@creationTimeVal,@descriptionVal,@inProgressVal,@currentColumnNameVal,@currentColumnOrdVal,@assigneeVal,@dueDateVal);";
                    SQLiteParameter taskemailParam = new SQLiteParameter(@"emailVal", task.taskemail);

                    SQLiteParameter taskboardIDParam = new SQLiteParameter(@"boardIDVal", task.boardid);
                    SQLiteParameter taskboardnameParam = new SQLiteParameter(@"boardnameVal", task.boardname);
                    SQLiteParameter taskidParam = new SQLiteParameter(@"taskIDVal", task.Id);
                    SQLiteParameter tasktitalParam = new SQLiteParameter(@"titalVal", task.tasktitle);
                    SQLiteParameter taskcreatimeParam = new SQLiteParameter(@"creationTimeVal", task.taskcreationTime);
                    SQLiteParameter taskdescParam = new SQLiteParameter(@"descriptionVal", task.taskdescription);
                    SQLiteParameter taskinprogParam = new SQLiteParameter(@"inProgressVal", task.taskinProgress);//todo need to remove
                    SQLiteParameter taskcurrcolnameParam = new SQLiteParameter(@"currentColumnNameVal", task.taskcurrentColumnName);
                    SQLiteParameter taskcurrcolordParam = new SQLiteParameter(@"currentColumnOrdVal", task.taskcurrentColumnOrd);
                    SQLiteParameter taskassigParam = new SQLiteParameter(@"assigneeVal", task.taskassignee);
                    SQLiteParameter taskduedateParam = new SQLiteParameter(@"dueDateVal", task.taskdueDate);
                   
                    command.Parameters.Add(taskboardIDParam);
                    command.Parameters.Add(taskemailParam);
                    command.Parameters.Add(taskboardnameParam);
                    command.Parameters.Add(taskidParam);
                    command.Parameters.Add(tasktitalParam);
                    command.Parameters.Add(taskcreatimeParam);
                    command.Parameters.Add(taskdescParam);
                    command.Parameters.Add(taskinprogParam);
                    command.Parameters.Add(taskcurrcolnameParam);
                    command.Parameters.Add(taskcurrcolordParam);
                    command.Parameters.Add(taskassigParam);
                    command.Parameters.Add(taskduedateParam);
                    command.Prepare();

                    res = command.ExecuteNonQuery();
                }
                catch(Exception e)
                {
                    log.Error("failed to create Task in DB ::::  " + e.Message);

                    //log error
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }
                return res > 0;
            }
        }

        protected override DTO ConvertReaderToObject(SQLiteDataReader reader)
        {
            TaskDTO result = new TaskDTO(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10), reader.GetString(11));
            return result;

        }


    }
}




