using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;


namespace Hotel
{
    class RoomClass
    {
        DBconnect connect = new DBconnect();

        public DataTable getRoomType()
        {
            string selectQuery = "SELECT * FROM `room_category`";
            MySqlCommand command = new MySqlCommand(selectQuery, connect.GetConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        public bool addRoom(int id, int type, string phone, string status)
        {
            string insertQuerry = "INSERT INTO `room`(`RoomID`, `RoomType`, `RoomPhone`, `RoomStatus`) VALUES (@id,@type,@phone,@status)";
            MySqlCommand command = new MySqlCommand(insertQuerry, connect.GetConnection());
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@type", MySqlDbType.Int32).Value = type;
            command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@status", MySqlDbType.VarChar).Value = status;

            connect.OpenCon();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.CloseCon();
                return true;
            }
            else
            {
                connect.CloseCon();
                return false;
            }
        }

        public DataTable getRoomList()
        {
            string selectQuery = "SELECT * FROM `room`";
            MySqlCommand command = new MySqlCommand(selectQuery, connect.GetConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }
        public bool editRoom(int id, int type, string phone, string status)
        {
            string editQuerry = "UPDATE `room` SET `RoomType`=@type,`RoomPhone`=@phone,`RoomStatus`=@status WHERE `RoomID`=@id";
            MySqlCommand command = new MySqlCommand(editQuerry, connect.GetConnection());
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@type", MySqlDbType.Int32).Value = type;
            command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@status", MySqlDbType.VarChar).Value = status;

            connect.OpenCon();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.CloseCon();
                return true;
            }
            else
            {
                connect.CloseCon();
                return false;
            }
        }

        public bool removeRoom(int id)
        {
            string insertQuerry = "DELETE FROM `room` WHERE `RoomID` =@id";
            MySqlCommand command = new MySqlCommand(insertQuerry, connect.GetConnection());
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            connect.OpenCon();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.CloseCon();
                return true;
            }
            else
            {
                connect.CloseCon();
                return false;
            }
        }
    }
}
