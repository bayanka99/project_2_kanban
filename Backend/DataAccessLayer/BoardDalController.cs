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
    class BoardDalController : DalController
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private const string MessageTableName = "Boards";


        public BoardDalController() : base(MessageTableName)
        {

        }


        public List<BoardDTO> SelectAlBoards()
        {
            List<BoardDTO> result = Select().Cast<BoardDTO>().ToList();

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



        public bool Insert(BoardDTO board)
        {

            using (var connection = new SQLiteConnection(_connectionString))
            {
                int res = -1;
                SQLiteCommand command = new SQLiteCommand(null, connection);
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {MessageTableName} ({BoardDTO.BoardsIDColumnName},{BoardDTO.BoardsnameColumnName},{BoardDTO.BoardsemailColumnName},{BoardDTO.BoardsmembersColumnName} ,{BoardDTO.BoardsColLimit0ColumnName} ,{BoardDTO.BoardsColLimit1ColumnName},{BoardDTO.BoardsColLimit2ColumnName},{BoardDTO.BoardsColsNames},{BoardDTO.BoardsColsLimits} )" +
                        $"VALUES (@boardidVal,@boardnameVal,@emailVal,@membersVal,@lcol0Val,@lcol1Val,@lcol2Val,@lcolnames,@lcollims);";

                    SQLiteParameter boardidParam = new SQLiteParameter(@"boardidVal", board.Id);
                    SQLiteParameter boardnameParam = new SQLiteParameter(@"boardnameVal", board.boardname);
                    SQLiteParameter boardcreatorParam = new SQLiteParameter(@"emailVal", board.email);
                    SQLiteParameter membersParam = new SQLiteParameter(@"membersVal", board.members);
                    SQLiteParameter lcol0Param = new SQLiteParameter(@"lcol0Val", board.collimit0);
                    SQLiteParameter lcol1Param = new SQLiteParameter(@"lcol1Val", board.collimit1);
                    SQLiteParameter lcol2Param = new SQLiteParameter(@"lcol2Val", board.collimit2);
                    SQLiteParameter lcolnames = new SQLiteParameter(@"lcolnames", board.colnames);
                    SQLiteParameter lcollims = new SQLiteParameter(@"lcollims", board.collimts);


                    command.Parameters.Add(boardidParam);
                    command.Parameters.Add(boardnameParam);
                    command.Parameters.Add(boardcreatorParam);
                    command.Parameters.Add(membersParam);
                    command.Parameters.Add(lcol0Param);
                    command.Parameters.Add(lcol1Param);
                    command.Parameters.Add(lcol2Param);
                    command.Parameters.Add(lcolnames);
                    command.Parameters.Add(lcollims);

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
            BoardDTO result = new BoardDTO(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8));
            return result;

        }

      
    }
}
