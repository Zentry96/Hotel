using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

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

            getReservTable();
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

                if (datein < DateTime.Today)
                {
                    MessageBox.Show("Datum rezervacije mora početi najranije od danas", "Neispravan datum", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (dateout < datein)
                {
                    MessageBox.Show("Rok rezervacije mora biti na dan rezervacije ili kasnije", "Neispravan datum", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (reservation.addReservation(guestid, roomid, datein, dateout) && reservation.setReservRoom(roomid.ToString(), "Zauzeta"))
                    {
                        getReservTable();
                        MessageBox.Show("Rezervacija uspešno napravljena", "Dodavanje rezervacije", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Rezervacija nije uspela", "Problem pri rezervaciji", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "greška pri dodavanju", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        public void getReservTable()
        {
            dataGridView_reserv.DataSource = reservation.getReserv();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label7_MouseEnter(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Red;
        }

        private void label7_MouseLeave(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Orange;
        }

        private void dataGridView_reserv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox_reservid.Text = dataGridView_reserv.CurrentRow.Cells[0].Value.ToString();
            textBox_guestid.Text = dataGridView_reserv.CurrentRow.Cells[1].Value.ToString();
            comboBox_roomID.SelectedValue = dataGridView_reserv.CurrentRow.Cells[2].Value.ToString();


        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (textBox_reservid.Text == "")
            {
                MessageBox.Show("Kliknite na rezervaciju koju želite da obrišete", "Prazno polje ID rezervacije", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    string id = textBox_reservid.Text;

                    Boolean deleteReserv = reservation.removeReserv(Int32.Parse(id));
                    if (deleteReserv)
                    {
                        MessageBox.Show("Rezervacija obrisana iz baze", "Rezervacija obrisana", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        getReservTable();
                        button_clean.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("Brisanje sobe iz baze nije uspelo", "Greška pri brisanju", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button_clean_Click(object sender, EventArgs e)
        {
            textBox_guestid.Clear();
            comboBox_roomType.SelectedIndex = 0;
            comboBox_roomID.SelectedIndex = 0;
            textBox_reservid.Clear();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            int reservationid = Convert.ToInt32(textBox_reservid.Text);
            int guestid = Convert.ToInt32(textBox_guestid.Text);
            int roomid = Convert.ToInt32(comboBox_roomID.SelectedValue.ToString());
            DateTime datein = dateTimePicker_dateIn.Value;
            DateTime dateout = dateTimePicker_dateOut.Value;

            try
            {
                if (reservation.editReserv(reservationid, guestid, roomid, datein, dateout))
                {
                    MessageBox.Show("Rezervacija uspešno obnovljena", "Obnova podataka rezervacije", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    getReservTable();
                    button_clean.PerformClick();
                }
                else
                {
                    MessageBox.Show("Rezervacija nije obnovljena", "Greška pri obnovi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
