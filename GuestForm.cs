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
    }
}
