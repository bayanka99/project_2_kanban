using IntroSE.Kanban.Backend.DataAccessLayer.DTOs;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    class ColumnDalController : DalController
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private const string MessageTableName = "Columns";


        public ColumnDalController() : base(MessageTableName)
        {

        }


        public List<ColumnDTO> SelectAlColumns()
        {
            List<ColumnDTO> result = Select().Cast<ColumnDTO>().ToList();

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



        public bool Insert(ColumnDTO column)
        {

            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {MessageTableName} ({ColumnDTO.ColumnNameColumnName},{ColumnDTO.ColumnOrdColumnName},{ColumnDTO.ColumnsIDColumnName},{ColumnDTO.Limit} ,{ColumnDTO.Boardid} )" +
                        $"VALUES (@columnameVal,@columnordVal,@columnidVal,@columnlimitVal,@boardidVal);";

                    SQLiteParameter colnameParam = new SQLiteParameter(@"columnameVal", column.columnName);
                    SQLiteParameter colordParam = new SQLiteParameter(@"columnordVal", column.columnOrd);
                    SQLiteParameter colidParam = new SQLiteParameter(@"columnidVal", column.Id);
                    SQLiteParameter colimitParam = new SQLiteParameter(@"columnlimitVal", column.LIMIT);
                    SQLiteParameter boardidParam = new SQLiteParameter(@"boardidVal", column.boardid);
                    


                    command.Parameters.Add(colnameParam);
                    command.Parameters.Add(colordParam);
                    command.Parameters.Add(colidParam);
                    command.Parameters.Add(colimitParam);
                    command.Parameters.Add(boardidParam);
                  

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
            ColumnDTO result = new ColumnDTO(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetString(4));
            return result;

        }


    }
}
}
