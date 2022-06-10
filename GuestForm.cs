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
    public partial class GuestForm : Form
    {
        GuestClass guest = new GuestClass();
        public GuestForm()
        {
            InitializeComponent();
        }

        private void button_clean_Click(object sender, EventArgs e)
        {
            textBox_id.Clear();
            textBox_fName.Clear();
            textBox_lName.Clear();
            textBox_phone.Clear();
            textBox_city.Clear();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (textBox_id.Text == "" || textBox_fName.Text == "" || textBox_phone.Text == "" || textBox_lName.Text == "" || textBox_city.Text == "")
            {
                MessageBox.Show("Niste uneli sve podatke", "Nedostatak podataka", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    string id = textBox_id.Text;
                    string fname = textBox_fName.Text;
                    string lname = textBox_lName.Text;
                    string phone = textBox_phone.Text;
                    string city = textBox_city.Text;

                    Boolean insertGuest = guest.insertGuest(id, fname, lname, phone, city);
                    if (insertGuest)
                    {
                        MessageBox.Show("Novi gost uspešno dodat", "Gost sačuvan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Dodavanje novog gosta nije uspelo", "Greška pri dodavanju", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
