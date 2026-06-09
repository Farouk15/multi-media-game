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
    public class CACTor
    {
        public int X, Y;
        public Rectangle src;
        public Rectangle Dst;
        public List<Bitmap> Imgs;
        public int IF;
        public int f7arka;
        public int dir;
    }
    public class MuImage
    {
        public Rectangle src;
        public Rectangle Dst;
        public Bitmap sora;
        public bool isselected;

    }
    public class CEdges
    {
        public int X, Y;
        public int W;
        public Pen p;
    }

    public partial class mainForm : Form
    {
        List<CACTor> LActs = new List<CACTor>();
        List<CACTor> coins = new List<CACTor>();
        List<CEdges> edges = new List<CEdges>();

        List<MuImage> lm = new List<MuImage>();

        Bitmap off;
        Timer tt = new Timer();
        int scrollX = 0;
        int f = 0;

        public mainForm(int fo)
        {

            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.KeyDown += MainForm_KeyDown;
            this.Paint += MainForm_Paint;
            tt.Tick += Tt_Tick;
            tt.Start();
            f = fo;
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            CACTor ptrv = LActs[0];
            scrollX = ptrv.X - 200;
            if (e.KeyCode == Keys.Right)
            {
                if (ptrv.X + ptrv.Imgs[0].Width < lm[0].sora.Width)
                {


                    ptrv.X += 10;
                    if (ptrv.IF < 2)
                    {
                        ptrv.IF++;
                    }
                    else
                    {
                        ptrv.IF = 0;
                    }
                    ptrv.dir = 1;
                }
            }
            if (e.KeyCode == Keys.Left)
            {


                if (ptrv.X > 0)
                {


                    ptrv.X -= 10;

                    if (ptrv.IF >= 9 && ptrv.IF < 11)
                    {
                        ptrv.IF++;
                    }
                    else
                    {
                        ptrv.IF = 9;
                    }
                    ptrv.dir = 2;
                }
            }
            if (e.KeyCode == Keys.F)
            {
                if (ptrv.dir == 1)
                {
                    ptrv.IF = 3;
                }
                else
                {
                    ptrv.IF = 12;
                }
            }
            if (e.KeyCode == Keys.J && ptrv.f7arka != 3 && ptrv.f7arka != 2)
            {

                if (ptrv.dir == 1)
                {

                    ptrv.IF = 5;
                }
                else
                {

                    ptrv.IF = 14;
                }
                ptrv.f7arka = 2;
            }
            if (e.KeyCode == Keys.C)
            {
                if (ptrv.dir == 1)
                {
                    ptrv.IF = 6;
                }
                else
                {
                    ptrv.IF = 15;
                }
            }

        }

        private void Tt_Tick(object sender, EventArgs e)
        {
            CACTor ptrv = LActs[0];
            //hero
            herojump();
            //helicopter
            moveheli();
            //tiger
            movetiger();
            DrawDubb(this.CreateGraphics());

        }

        private void mainForm_Load(object sender, EventArgs e)
        {

            this.label1.BackColor = Color.Transparent;
            if (f == 1)
            {
                this.Hide();
            }
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            createHero();
            createMap();
            createhelicopter();
            createtiger();
        }
        void DrawDubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);

            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
        void DrawScene(Graphics g)
        {
            g.Clear(Color.Black);
            //background
            for (int i = 0; i < lm.Count; i++)
            {
                MuImage pnn = lm[i];
                pnn.src = new Rectangle(scrollX, 0, this.ClientSize.Width, pnn.sora.Height);
                pnn.Dst = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
                g.DrawImage(pnn.sora, pnn.Dst, pnn.src, GraphicsUnit.Pixel);
            }
            //charachters
            for (int i = 0; i < LActs.Count; i++)
            {
                CACTor pTrv = LActs[i];
                g.DrawImage(pTrv.Imgs[pTrv.IF], pTrv.X - scrollX, pTrv.Y);
            }


        }
        void createMap()
        {
            MuImage temp = new MuImage();
            temp.sora = new Bitmap("map.png");
            temp.src = new Rectangle(0, 0, temp.sora.Width, temp.sora.Height);
            temp.Dst = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            lm.Add(temp);
        }
        void createHero()
        {
            CACTor pnn = new CACTor();

            pnn.X = this.ClientSize.Width / 10;
            pnn.Y = this.ClientSize.Height / 2 + this.ClientSize.Height / 4 - 90;
            pnn.Imgs = new List<Bitmap>();
            pnn.IF = 0;
            pnn.dir = 1;
            pnn.f7arka = 1;
            for (int i = 0; i < 18; i++)
            {
                Bitmap pnnSora = new Bitmap("" + (i + 1) + "hh.png");
                pnnSora.MakeTransparent();

                pnn.Imgs.Add(pnnSora);

            }

            LActs.Add(pnn);
        }
        void herojump()
        {
            CACTor ptrv = LActs[0];
            if (ptrv.f7arka == 2)
            {
                if (ptrv.Y >= this.ClientSize.Height / 2 + this.ClientSize.Height / 4 - 190)
                {
                    ptrv.Y -= 20;
                    if (ptrv.dir == 1)
                    {
                        ptrv.X += 20;
                    }
                    if (ptrv.dir == 2)
                    {
                        ptrv.X -= 20;
                    }
                }
                if (ptrv.Y == this.ClientSize.Height / 2 + this.ClientSize.Height / 4 - 190)
                {
                    ptrv.f7arka = 3;

                }
            }
            if (ptrv.f7arka == 3)
            {
                if (ptrv.Y <= this.ClientSize.Height / 2 + this.ClientSize.Height / 4 - 90)
                {
                    ptrv.Y += 20;
                    if (ptrv.dir == 1)
                    {
                        ptrv.X += 20;
                    }
                    if (ptrv.dir == 2)
                    {
                        ptrv.X -= 20;
                    }
                }
                if (ptrv.Y == this.ClientSize.Height / 2 + this.ClientSize.Height / 4 - 90)
                {
                    ptrv.f7arka = 1;
                    if (ptrv.dir == 1)
                    {
                        ptrv.IF = 0;
                    }
                    if (ptrv.dir == 2)
                    {
                        ptrv.IF = 9;
                    }
                }
            }
        }
        void createtiger()
        {
            CACTor pnn = new CACTor();

            pnn.X = lm[0].sora.Width / 2;
            pnn.Y = this.ClientSize.Height / 2 + this.ClientSize.Height / 4 - 90;
            pnn.Imgs = new List<Bitmap>();
            pnn.IF = 0;
            pnn.dir = 1;
            pnn.f7arka = 1;
            for (int i = 0; i < 12; i++)
            {
                Bitmap pnnSora = new Bitmap("" + (i + 1) + "t.png");
                pnnSora.MakeTransparent();

                pnn.Imgs.Add(pnnSora);

            }

            LActs.Add(pnn);
        }
        void movetiger()
        {
            CACTor ptrv = LActs[2];
            if (ptrv.X > lm[0].sora.Width / 2 && ptrv.dir == 2)
            {
                ptrv.X -= 10;
                if (ptrv.IF >= 6 && ptrv.IF < 9)
                {
                    ptrv.IF++;

                }
                if (ptrv.IF == 9)
                {
                    ptrv.IF = 6;
                }
                if (ptrv.X <= lm[0].sora.Width / 2 && ptrv.dir == 2)
                {
                    ptrv.dir = 1;
                    ptrv.IF = 0;

                }

            }
            if (ptrv.X + ptrv.Imgs[0].Width < lm[0].sora.Width && ptrv.dir == 1)
            {
                ptrv.X += 10;
                if (ptrv.IF < 3)
                {
                    ptrv.IF++;

                }
                if (ptrv.IF == 3)
                {
                    ptrv.IF = 0;
                }
                if (ptrv.X + ptrv.Imgs[0].Width >= lm[0].sora.Width && ptrv.dir == 1)
                {
                    ptrv.dir = 2;
                    ptrv.IF = 6;

                }
            }

        }
        void attacktiger()
        {
            CACTor ptrvt = LActs[2];
            CACTor ptrvh = LActs[0];
        }
        void createhelicopter()
        {
            CACTor pnn = new CACTor();
            pnn.X = this.ClientSize.Width / 2;
            pnn.Y = this.ClientSize.Height / 8;
            pnn.Imgs = new List<Bitmap>();
            pnn.IF = 0;
            pnn.dir = 1;
            pnn.f7arka = 1;
            for (int i = 0; i < 10; i++)
            {
                Bitmap pnnSora = new Bitmap("" + (i + 1) + "h.png");
                pnnSora.MakeTransparent();

                pnn.Imgs.Add(pnnSora);

            }
            LActs.Add(pnn);

        }
        void moveheli()
        {
            CACTor ptrv = LActs[1];
            if (ptrv.X > 0 && ptrv.dir == 2)
            {
                ptrv.X -= 10;
                if (ptrv.IF >= 5 && ptrv.IF < 8)
                {
                    ptrv.IF++;

                }
                if (ptrv.IF == 8)
                {
                    ptrv.IF = 5;
                }
                if (ptrv.X <= 0)
                {
                    ptrv.dir = 1;
                    ptrv.IF = 0;
                }

            }
            if (ptrv.X + ptrv.Imgs[0].Width < lm[0].sora.Width && ptrv.dir == 1)
            {
                ptrv.X += 10;
                if (ptrv.IF < 3)
                {
                    ptrv.IF++;

                }
                if (ptrv.IF == 3)
                {
                    ptrv.IF = 0;
                }
                if (ptrv.X + ptrv.Imgs[0].Width >= lm[0].sora.Width && ptrv.dir == 1)
                {
                    ptrv.dir = 2;
                    ptrv.IF = 6;
                }

            }



        }

        private void label1_Click(object sender, EventArgs e)
        {
            close c = new close();
            c.Show();
        }
    }


}