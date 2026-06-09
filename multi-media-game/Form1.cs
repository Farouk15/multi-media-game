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
        List<Cpen> LActs = new List<Cpen>();
        Bitmap off;
        Timer tt = new Timer();
        int time = 0;
        int g = 0;
        int press = 0;
        public Form1()
        {
            InitializeComponent();
            this.Paint += Form1_Paint; 
            tt.Tick += Tt_Tick; ;
            tt.Interval = 100;
            tt.Start();
        }

        private void Tt_Tick(object sender, EventArgs e)
        {
            if (press == 1)
            {
                if (time < 50)
                {
                    animateLoader();
                }
                if (time >= 50)       
                {
                    tt.Stop();          
                    this.Hide();
                    mainForm f = new mainForm(g);
                    f.Show();
                }

                time++;
            }
            DrawDubb(this.CreateGraphics());

        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics); 

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.BackgroundImage = new Bitmap("main.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);

        }

        void animateLoader()
        {
            Cpen ptarv = LActs[0];
            if (ptarv.X < this.ClientSize.Width - 60)
            {
                ptarv.X +=35;

            }
        }
        void createLoader()
        {
            Cpen pnn = new Cpen();
            pnn.X = this.ClientSize.Width /7;   
            pnn.Y = this.ClientSize.Height - 100;

            pnn.p = new Pen(Color.DarkGreen, 20);        
            LActs.Add(pnn);                       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createLoader();
            button1.Enabled = false; 

            press = 1;
            time = 0;

        }
        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
        void DrawScene(Graphics g) 
        {
            g.Clear(Color.Transparent);
            for (int i = 0; i < LActs.Count; i++)
            {
                Cpen pnn = LActs[i];
                g.DrawLine(pnn.p, 50, pnn.Y, pnn.X, pnn.Y);
            }
        }

    }
    public class Cpen
    {
        public int X, Y;
        public Pen p;
    }
}
