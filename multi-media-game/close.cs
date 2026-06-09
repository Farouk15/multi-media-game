using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace multi_media_game
{
    public partial class close : Form
    {

        int flagClose = 0;
        public close()
        {
            InitializeComponent();
        }

        private void close_Load(object sender, EventArgs e)
        {
            this.Location = new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            flagClose = 1;
            this.Close();
            mainForm f = new mainForm(flagClose);
        }
    }
}
