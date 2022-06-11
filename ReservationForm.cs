using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel
{
    public partial class ReservationForm : Form
    {
        RoomClass room = new RoomClass();
        ReservationClass reservation = new ReservationClass();
        public ReservationForm()
        {
            InitializeComponent();
        }

        private void ReservationForm_Load(object sender, EventArgs e)
        {
            comboBox_roomType.DataSource = room.getRoomType();
            comboBox_roomType.DisplayMember = "RoomType";
            comboBox_roomType.ValueMember = "CategoryID";

            int type = Convert.ToInt32(comboBox_roomType.SelectedValue.ToString());
            comboBox_roomID.DataSource = reservation.roomByType(type);
            comboBox_roomID.DisplayMember = "RoomID";
            comboBox_roomID.ValueMember = "RoomID";

            dataGridView_reserv.DataSource = reservation.getReserv();
        }

        private void comboBox_roomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int type = Convert.ToInt32(comboBox_roomType.SelectedValue.ToString());
                comboBox_roomID.DataSource = reservation.roomByType(type);
                comboBox_roomID.DisplayMember = "RoomID";
                comboBox_roomID.ValueMember = "RoomID";
            }
            catch (Exception)
            {
                
            }
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            try
            {
                int guestid = Convert.ToInt32(textBox_guestid.Text);
                int roomid = Convert.ToInt32(comboBox_roomID.SelectedValue.ToString());
                DateTime datein = dateTimePicker_dateIn.Value;
                DateTime dateout = dateTimePicker_dateOut.Value;

                if (reservation.addReservation(guestid, roomid, datein, dateout))
                {
                    MessageBox.Show("Rezervacija uspešno napravljena", "Dodavanje rezervacije", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Rezervacija nije uspela", "Problem pri rezervaciji", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "greška pri dodavanju", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
