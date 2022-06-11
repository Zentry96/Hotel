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
    public partial class RoomForm : Form
    {
        RoomClass room = new RoomClass();
        public RoomForm()
        {
            InitializeComponent();
        }

        private void RoomForm_Load(object sender, EventArgs e)
        {
            comboBox_roomType.DataSource = room.getRoomType();
            comboBox_roomType.DisplayMember = "RoomType";
            comboBox_roomType.ValueMember = "CategoryID";
            getRoomList();
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox_id.Text);
            int type = Convert.ToInt32(comboBox_roomType.SelectedValue.ToString());
            string phone = textBox_phone.Text;
            string status = radioButton_slobodna.Checked ? "Slobodna" : "Zauzeta";

            try
            {
                if (room.addRoom(id, type, phone, status))
                {
                    MessageBox.Show("Soba uspešno dodata", "Dodavanje sobe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    getRoomList();
                    button_clean.PerformClick();
                }
                else
                {
                    MessageBox.Show("Soba nije dodata", "Greška pri dodavanju", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_clean_Click(object sender, EventArgs e)
        {
            textBox_id.Clear();
            comboBox_roomType.SelectedIndex = 0;
            textBox_phone.Clear();
        }

        private void getRoomList()
        {
            dataGridView_room.DataSource = room.getRoomList();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox_id.Text);
            int type = Convert.ToInt32(comboBox_roomType.SelectedValue.ToString());
            string phone = textBox_phone.Text;
            string status = radioButton_slobodna.Checked ? "Slobodna" : "Zauzeta";

            try
            {
                if (room.editRoom(id, type, phone, status))
                {
                    MessageBox.Show("Soba uspešno obnovljena", "Obnova podataka sobe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    getRoomList();
                    button_clean.PerformClick();
                }
                else
                {
                    MessageBox.Show("Soba nije obnovljena", "Greška pri obnovi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView_room_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox_id.Text = dataGridView_room.CurrentRow.Cells[0].Value.ToString();
            comboBox_roomType.SelectedValue = dataGridView_room.CurrentRow.Cells[1].Value.ToString();
            textBox_phone.Text = dataGridView_room.CurrentRow.Cells[2].Value.ToString();

            string rButton = dataGridView_room.CurrentRow.Cells[3].Value.ToString();
            if (rButton.Equals("Slobodna"))
            {
                radioButton_slobodna.Checked = true;
            }
            else
            {
                radioButton_zauzeta.Checked = true;
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            if (textBox_id.Text == "")
            {
                MessageBox.Show("Unesite ID sobe koju želite da obrišete", "Prazno polje za ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    string id = textBox_id.Text;

                    Boolean deleteRoom = room.removeRoom(Int32.Parse(id));
                    if (deleteRoom)
                    {
                        MessageBox.Show("Soba obrisana iz baze", "Soba obrisana", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        getRoomList();
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

        private void label7_MouseEnter(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Red;
        }

        private void label7_MouseLeave(object sender, EventArgs e)
        {
            label7.ForeColor = Color.Orange;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
