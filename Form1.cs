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
    public partial class Form1 : Form
    {
        int g = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.BackgroundImage = new Bitmap("main.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            mainForm f = new mainForm(g);
            f.Show();
        }
    }
}
