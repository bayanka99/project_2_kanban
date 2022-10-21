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
    class UserDalController : DalController
    {

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private const string MessageTableName = "Users";

        public UserDalController() : base(MessageTableName)
        {

        }


        public List<UserDTO> SelectAllUsers()
        {
            List<UserDTO> result = Select().Cast<UserDTO>().ToList();

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



        public bool Insert(UserDTO user)
        {

            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {MessageTableName} ({UserDTO.UsersemailColumnName},{UserDTO.UserspasswordColumnName}) " +
                        $"VALUES (@emailVal,@passwordVal);";

                  
                    SQLiteParameter useremailParam = new SQLiteParameter(@"emailVal", user.email);
                    SQLiteParameter userpasswordParam = new SQLiteParameter(@"passwordVal", user.password);

               
                    command.Parameters.Add(useremailParam);
                    command.Parameters.Add(userpasswordParam);
                    command.Prepare();

                    res = command.ExecuteNonQuery();
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
            UserDTO result = new UserDTO(reader.GetString(0),reader.GetString(1));
            return result;

        }

     
    }

}

