using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Hotel
{
    class GuestClass
    {
        DBconnect connect = new DBconnect();
        public bool insertGuest(string id, string fname, string lname, string phone, string city)
        {
            string insertQuerry = "INSERT INTO `guest`(`GuestID`, `GuestFirstName`, `GuestLastName`, `GuestPhone`, `GuestCity`) VALUES(@id, @fname, @lname, @phone, @city)";
            MySqlCommand command = new MySqlCommand(insertQuerry, connect.GetConnection());
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@fname", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@lname", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@city", MySqlDbType.VarChar).Value = city;

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
        public DataTable getGuest()
        {
            string selectQuery = "SELECT * FROM `guest`";
            MySqlCommand command = new MySqlCommand(selectQuery, connect.GetConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        public bool editGuest(string id, string fname, string lname, string phone, string city)
        {
            string editQuerry = "UPDATE `guest` SET `GuestFirstName`=@fname, `GuestLastName`=@lname, `GuestPhone`=@phone, `GuestCity`=@city WHERE `GuestID`=@id";
            MySqlCommand command = new MySqlCommand(editQuerry, connect.GetConnection());
            command.Parameters.Add("@id", MySqlDbType.Int16).Value = id;
            command.Parameters.Add("@fname", MySqlDbType.VarChar).Value = fname;
            command.Parameters.Add("@lname", MySqlDbType.VarChar).Value = lname;
            command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@city", MySqlDbType.VarChar).Value = city;

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

        public bool removeGuest(string id)
        {
            string insertQuerry = "DELETE FROM `guest` WHERE `GuestID` =@id";
            MySqlCommand command = new MySqlCommand(insertQuerry, connect.GetConnection());
            command.Parameters.Add("@id", MySqlDbType.Int16).Value = id;

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
