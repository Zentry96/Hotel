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

        public bool addReservation(int roomid, int guestid, DateTime datein, DateTime dateout)
        {
            string insertQuerry = "INSERT INTO `reservation`(`GuestID`, `RoomID`, `DateIn`, `DateOut`) VALUES (@guestID, @roomID, @datein, @dateout)";
            MySqlCommand command = new MySqlCommand(insertQuerry, connect.GetConnection());
            command.Parameters.Add("@roomID", MySqlDbType.Int32).Value = guestid;
            command.Parameters.Add("@guestID", MySqlDbType.Int32).Value = roomid;
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
    }
}
