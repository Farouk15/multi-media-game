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
        public int coins;

        public int health;
        public Brush br;
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
        List<CACTor> LActsgates = new List<CACTor>();

        List<CACTor> coins = new List<CACTor>();
        List<CEdges> edges = new List<CEdges>();

        List<MuImage> lm = new List<MuImage>();

        Bitmap off;
        Timer tt = new Timer();
        int scrollX = 0;
        int f = 0;
        int ctdamage = 0;
        int t=0;
        int defenseMode = 0;
        int move = 0 ; 
        int startPointX = 0;
        int startPointY = 0;
        int incX=0;
        int levelState=0;
        public mainForm(int fo)
        {

            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.KeyDown += MainForm_KeyDown;
            this.Paint += MainForm_Paint;
            tt.Tick += Tt_Tick;
            tt.Start();
            f = fo;
            startPointX =this.ClientSize.Width / 10;
            startPointY = this.ClientSize.Height / 2 + this.ClientSize.Height / 4 - 90;

        }
        //////////////////////////////////////////////////////////////////////////////////////
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            DrawDubb(e.Graphics);
        }
        //////////////////////////////////////////////////////////////////////////////////////
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            CACTor ptrv = LActs[0];
            
            if( scrollX < this.ClientSize.Width - 330)
            {
                scrollX = ptrv.X - 60;

            }
            CACTor muPtrav = LActsgates[0];
            CACTor heroPtrav = LActs[0];
            if (e.KeyCode == Keys.M)
            {
                if(heroPtrav.X >= muPtrav.X && heroPtrav.X <= muPtrav.X + muPtrav.Imgs[0].Width)
                {
                    move = 1;

                }
            }
            if (e.KeyCode == Keys.Right)
            {
                if (ptrv.X + ptrv.Imgs[0].Width < lm[0].sora.Width)
                {


                    ptrv.X += 20;
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

                    ptrv.X -= 20;

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
            if (e.KeyCode == Keys.Space && ptrv.f7arka != 3 && ptrv.f7arka != 2)
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



            if (e.KeyCode == Keys.D)
            {
                defenseMode = 1;
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////
        private void Tt_Tick(object sender, EventArgs e)
        {

            CACTor ptrv = LActs[0];
            if (ptrv.health == 3)
            {

                for (int i = LActs.Count - 1; i >= 0; i--)
                {
                    LActs.Remove(LActs[i]);

                }
                if(levelState == 0)
                {
                    createhelicopter();
                    //createtiger();

                }
                scrollX = 0;

            }
            //hero
            herojump();
            //helicopter
            if (levelState == 0)
            {
                moveheli();

            }

            //tiger
            //movetiger();
            //attacktiger();
            CACTor ptrvGate = LActsgates[0];
            if(t % 4 == 0)
            {
                ptrvGate.IF +=1;

            }

            if(ptrvGate.IF >= ptrvGate.Imgs.Count)
            {
                ptrvGate.IF = 0;
            }
            t++;
            for (int i = 0; i < coins.Count; i++)
            {
                coins[i].IF = (coins[i].IF + 1) % 8;

            }

            getCoins();


            level2();
            DrawDubb(this.CreateGraphics());

        }
        //////////////////////////////////////////////////////////////////////////////
        private void mainForm_Load(object sender, EventArgs e)
        {

            this.label1.BackColor = Color.Transparent;
            if (f == 1)
            {
                this.Hide();
            }
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            createMap();

            createHero();
            createGate();

            if(levelState == 0)
            {
                createhelicopter();
                //createtiger();
                createCoins(incX);

            }
                        


            DrawDubb(this.CreateGraphics());


        }
        /////////////////////////////////////////////////////////////////////////
        void level2()
        {
            MuImage muPtrav = lm[0];
            CACTor heroPtrav = LActs[0];
            if(move ==1)
            {

                muPtrav.sora = new Bitmap("maps/map2.png");

                heroPtrav.X = startPointX;
                scrollX = 0;
                move = 0;
                levelState = 1;
                coins.Clear();
                createVendingmachine();

            }
        }
        ///////////////////////////////////////////////////////////////////
        void createMap()
        {
            MuImage temp = new MuImage();
            temp.sora = new Bitmap("maps/map.png");
            temp.src = new Rectangle(0, 0, temp.sora.Width, temp.sora.Height);
            temp.Dst = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            lm.Add(temp);
        }
        ////////////////////////////////////////////////////////////////////
        void createHero()
        {
            CACTor pnn = new CACTor();

            pnn.X = startPointX;
            pnn.Y = this.ClientSize.Height / 2 + this.ClientSize.Height / 4 - 90;
            pnn.Imgs = new List<Bitmap>();
            pnn.IF = 0;
            pnn.dir = 1;
            pnn.f7arka = 1;
            pnn.health = 0;
            pnn.coins = 0;

            for (int i = 0; i < 18; i++)
            {
                Bitmap pnnSora = new Bitmap("hero/" + (i + 1) + "hh.png");
                pnnSora.MakeTransparent();

                pnn.Imgs.Add(pnnSora);

            }

            LActs.Add(pnn);
        }

        void createVendingmachine()
        {
            CACTor pnn = new CACTor();

            pnn.X =  startPointX + 300;
            pnn.Imgs = new List<Bitmap>();
            pnn.IF = 0;
            pnn.dir = 1;
            pnn.f7arka = 1;
            for (int i = 0; i < 1; i++)
            {
                Bitmap pnnSora = new Bitmap("vending/ven.png");
                pnnSora.MakeTransparent();

                pnn.Imgs.Add(pnnSora);

            }
            pnn.Y = startPointY + 200;

            LActsgates.Add(pnn);
        }
        //////////////////////////////////////////////////////////////////////////
        void createCoins(int x)
        {
            CACTor heroPtrav= LActs[0];

            Random RR = new Random();
            for(int k = 0 ; k < 3 ; k++)
            {
                CACTor pnn = new CACTor();
                pnn.X = startPointX + 400 + x;
                pnn.Y =heroPtrav.Y - 75;
                pnn.IF = RR.Next(8);
                pnn.Imgs = new List<Bitmap>();

                for (int i = 0; i < 8; i++)
                {
                    Bitmap pnnSora = new Bitmap("coins/coin" + (i + 1) + ".png");
                    pnnSora.MakeTransparent(pnnSora.GetPixel(0, 0));
                    pnn.Imgs.Add(pnnSora);
                }

                coins.Add(pnn);
                x+=350;
            }

        }
        /////////////////////////////////////////////////////////////////////////
        void getCoins()
        {
            CACTor hero = LActs[0];
            
            for (int i = coins.Count - 1; i >= 0; i--)
            {
                CACTor coin = coins[i];

                if (hero.X + 100 > coin.X && hero.X < coin.X + 100 &&
                    hero.Y + 150 > coin.Y && hero.Y < coin.Y + 50)
                {
                    coins.RemoveAt(i); 
                    hero.coins++;
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////
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
        ////////////////////////////////////////////////////////////////////////
        void createGate()
        {
            CACTor pnn = new CACTor();

            pnn.X =  this.ClientSize.Height *3 - 200;
            pnn.Imgs = new List<Bitmap>();
            pnn.IF = 0;
            pnn.dir = 1;
            pnn.f7arka = 1;
            for (int i = 0; i < 6; i++)
            {
                Bitmap pnnSora = new Bitmap("gates/"+"g" + (i + 1) + ".png");
                pnnSora.MakeTransparent();

                pnn.Imgs.Add(pnnSora);

            }
            pnn.Y = this.ClientSize.Height / 2 + this.ClientSize.Height / 4 - 90 - pnn.Imgs[0].Height / 2;

            LActsgates.Add(pnn);
        }
        //////////////////////////////////////////////////////////////////////////////////
        void createtiger()
        {
            CACTor pnn = new CACTor();

            pnn.X = lm[0].sora.Width / 3;
            pnn.Y = this.ClientSize.Height / 2 + this.ClientSize.Height / 4 - 90;
            pnn.Imgs = new List<Bitmap>();
            pnn.IF = 0;
            pnn.dir = 1;
            pnn.f7arka = 1;
            for (int i = 0; i < 12; i++)
            {
                Bitmap pnnSora = new Bitmap("tiger/" + (i + 1) + "t.png");
                pnnSora.MakeTransparent();

                pnn.Imgs.Add(pnnSora);

            }

            LActs.Add(pnn);
        }
        void movetiger()
        {
            CACTor ptrv = LActs[2];
            if (ptrv.f7arka == 1)
            {


                if (ptrv.X > 0 && ptrv.dir == 2)
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

        }
        void attacktiger()
        {
            
            CACTor ptrvt = LActs[2];
            CACTor ptrvh = LActs[0];
            if (ptrvt.f7arka == 3)
            {
                if (ptrvh.dir == 2)
                {
                    ptrvt.IF = 6;
                }

                ptrvt.f7arka = 1;
            }
            if (ptrvt.X<=ptrvh.X+ptrvh.Imgs[0].Width -20
                && ptrvt.X > ptrvh.X)
            {
                ptrvh.f7arka = 4;
                ptrvh.IF = 7;
                ptrvh.health++;
            }
            if (ptrvh.f7arka == 4)
            {
                if (ctdamage <= 4)
                {
                    ctdamage++;
                    ptrvh.X -= 70;
                    scrollX -= 70;
                }
                if (ctdamage == 5)
                {
                    ctdamage = 0;
                    ptrvh.f7arka = 1;
                }
            }
            if (ptrvt.X <= ptrvh.X + ptrvh.Imgs[0].Width + 250
                && ptrvt.X > ptrvh.X && ptrvt.f7arka != 3)
            {
                ptrvt.dir = 2;
                ptrvt.f7arka = 2;
                if (ptrvt.IF < 9)
                {


                    ptrvt.IF = 9;
                }
            }
            else
            {
                ptrvt.f7arka = 1;
            }
            if (ptrvt.f7arka == 2)
            {
                ptrvt.X -= 25;
                if (ptrvt.IF >= 9 && ptrvt.IF<11)
                {
                    ptrvt.IF++;
                }
                if (ptrvt.IF == 11)
                {
                    ptrvt.IF = 9;
                }
            }
            if (ptrvt.X <= ptrvh.X + ptrvh.Imgs[0].Width + 30
                && ptrvt.X > ptrvh.X && ptrvt.f7arka != 3)
            {
                ptrvt.IF = 11;
                ptrvt.X -= 20;
                ptrvt.f7arka = 3;
            }

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
                Bitmap pnnSora = new Bitmap("heli/" + (i + 1) + "h.png");
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

            for (int i = 0; i < LActsgates.Count; i++)
            {
                CACTor pTrv = LActsgates[i];
                g.DrawImage(pTrv.Imgs[pTrv.IF], pTrv.X - scrollX, pTrv.Y);
            } 
            //charachters
            for (int i = 0; i < LActs.Count; i++)
            {
                CACTor pTrv = LActs[i];
                g.DrawImage(pTrv.Imgs[pTrv.IF], pTrv.X - scrollX, pTrv.Y);
            }
            /// coins
            for (int i = 0; i < coins.Count; i++)
            {
                CACTor pTrv = coins[i];
                g.DrawImage(pTrv.Imgs[pTrv.IF], pTrv.X - scrollX, pTrv.Y);
            }
            CACTor hero = LActs[0];

            Font f = new Font("Arial", 20, FontStyle.Bold);
            Brush b = Brushes.Red;
            g.DrawString("Health: " + (3 - hero.health), f, b, this.ClientSize.Width / 3, 20);
            g.DrawString("Coins: " + hero.coins, f, b, this.ClientSize.Width / 3 + 150, 20);
        }

    }


}