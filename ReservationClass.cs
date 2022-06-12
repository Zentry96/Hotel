using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Hotel
{
    class ReservationClass
    {
        DBconnect connect = new DBconnect();

        public DataTable roomByType(int type)
        {
            string selectQuery = "SELECT * FROM `room` WHERE `RoomType`=@type AND `RoomStatus`='Slobodna'";
            MySqlCommand command = new MySqlCommand(selectQuery, connect.GetConnection());
            command.Parameters.Add("@type", MySqlDbType.Int32).Value = type;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        public bool setReservRoom(string roomid, string status)
        {
            string updateQuery = "UPDATE `room` SET `RoomStatus`=@status WHERE `RoomID`=@roomid";
            MySqlCommand command = new MySqlCommand(updateQuery, connect.GetConnection());
            command.Parameters.Add("@roomid", MySqlDbType.VarChar).Value = roomid;
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
        public bool addReservation(int roomid, int guestid, DateTime datein, DateTime dateout)
        {
            string insertQuerry = "INSERT INTO `reservation`(`GuestID`, `RoomID`, `DateIn`, `DateOut`) VALUES (@guestID, @roomID, @datein, @dateout)";
            MySqlCommand command = new MySqlCommand(insertQuerry, connect.GetConnection());
            command.Parameters.Add("@guestID", MySqlDbType.Int32).Value = roomid;
            command.Parameters.Add("@roomID", MySqlDbType.Int32).Value = guestid;
            command.Parameters.Add("@datein", MySqlDbType.Date).Value = datein;
            command.Parameters.Add("@dateout", MySqlDbType.Date).Value = dateout;

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

        public DataTable getReserv()
        {
            string selectQuery = "SELECT * FROM `reservation`";
            MySqlCommand command = new MySqlCommand(selectQuery, connect.GetConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }
        public bool removeReserv(int id)
        {
            string insertQuerry = "DELETE FROM `reservation` WHERE `ReservationID` =@id";
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

        public bool editReserv(int reservationid, int guestid, int roomid, DateTime datein, DateTime dateout)
        {
            string editQuerry = "UPDATE `reservation` SET `GuestID`=@guestid,`RoomID`=@roomid,`DateIn`=@datein,`DateOut`=@dateout WHERE `ReservationID`='@reservationid'";
            MySqlCommand command = new MySqlCommand(editQuerry, connect.GetConnection());
            command.Parameters.Add("@reservationid", MySqlDbType.Int32).Value = reservationid;
            command.Parameters.Add("@guestid", MySqlDbType.Int32).Value = guestid;
            command.Parameters.Add("@roomid", MySqlDbType.Int32).Value = roomid;
            command.Parameters.Add("@datein", MySqlDbType.DateTime).Value = datein;
            command.Parameters.Add("@dateout", MySqlDbType.DateTime).Value = dateout;

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
