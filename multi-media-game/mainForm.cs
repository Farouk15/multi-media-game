using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace multi_media_game
{

    public partial class mainForm : Form
    {
        List<CACTor> LActs = new List<CACTor>();
        List<CACTor> coins = new List<CACTor>();
        List<CEdges> edges = new List<CEdges>();

        List<MuImage> lm = new List<MuImage>();

        Bitmap off;
        Timer tt = new Timer();
        int time = 0;
        int flagJump = 0;
        int ctJ = 0;
        int jumpSpeed = 25;
        int groundY;
        int f = 0;


        public mainForm(int fo)
        {

            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.KeyDown += MainForm_KeyDown;
            this.KeyUp += MainForm_KeyUp;
            tt.Tick += Tt_Tick; 
            tt.Interval = 100;
            tt.Start();
            f = fo;
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Right)
            {

                CACTor hero = LActs[0];
                hero.X += 15;
                hero.IF = (hero.IF + 1) % 10;
                flagJump = 0;
                scrollX += 20;
                for (int i = 0; i < coins.Count; i++)
                {
                    coins[i].IF = (coins[i].IF + 1) % 8;

                }
                if (scrollX > lm[0].sora.Width - this.ClientSize.Width)
                {
                    scrollX = lm[0].sora.Width - this.ClientSize.Width;

                }
            }
            if (e.KeyCode == Keys.Left)
            {
                CACTor hero = LActs[0];
                hero.X -= 15;
                hero.IF = (hero.IF + 1) % 10;
                flagJump = 0;
                scrollX -= 20;
                for (int i = 0; i < coins.Count; i++)
                {
                    coins[i].IF = (coins[i].IF + 1) % 8;

                }
                if (scrollX < 0)
                {
                    scrollX = 0;
                }
            }

            /// jump ya mohammed
            if (e.KeyCode == Keys.Space && flagJump == 0 && ctJ == 0)
            {
                flagJump = 1;
            }


            DrawDubb(this.CreateGraphics());

        }

        private void Tt_Tick(object sender, EventArgs e)
        {
            if (off == null)
            {
                return;
            }
            Jump();
            for (int i = 0; i < coins.Count; i++)
            {
                coins[i].IF = (coins[i].IF + 1) % 8;

            }
            getCoins();


            time++;
            DrawDubb(this.CreateGraphics());

        }
        void Jump()
        {
            if (LActs.Count == 0)
            {
                return;
            }
                CACTor hero = LActs[0];

            if (flagJump == 1)
            {
                if (ctJ < 5)
                {
                    hero.Y -= jumpSpeed;
                    hero.X += jumpSpeed / 3;

                    hero.IF = (hero.IF + 1) % 10;
                    ctJ++;
                }
                else if (ctJ < 10)
                {
                    hero.Y += jumpSpeed;
                    hero.X += jumpSpeed / 3;

                    hero.IF = (hero.IF + 1) % 10;
                    ctJ++;
                }
                else
                {
                    hero.Y = groundY;
                    ctJ = 0;
                    flagJump = 0; 

                }
            }
        }
        private void mainForm_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            createMap();
            this.label1.BackColor = Color.Transparent;
            if(f == 1)
            {
                this.Hide(); 
            }
            createEdges();
            createCoins();
            creatHero();

            groundY = LActs[0].Y;  
            DrawDubb(this.CreateGraphics());
        }
        int scrollX = 0;
        void getCoins()
        {
            CACTor hero = LActs[0];

            for (int i = 0; i < coins.Count; i++)
            {
                CACTor coin = coins[i];

                if (hero.X + 80 > coin.X && hero.X < coin.X + 60 &&
                    hero.Y + 80 > coin.Y && hero.Y < coin.Y + 60)
                {
                    coins.RemoveAt(i); 

                }
            }
        }
        void createMap()
        {
            MuImage temp = new MuImage();
            temp.sora = new Bitmap("form2.png");
            temp.src = new Rectangle(0, 0, temp.sora.Width, temp.sora.Height);
            temp.Dst = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            lm.Add(temp);
        }

        void createEdges()
        {
            CEdges pnn = new CEdges();
            pnn.X = this.ClientSize.Height / 2 + 80;                          
            pnn.Y = this.ClientSize.Height - 200; 
            pnn.W = 100;                          
            pnn.p = new Pen(Color.DarkGreen, 10);
            edges.Add(pnn);
        }
        void createCoins()
        {
            CEdges ptravC = edges[0];
            Random RR = new Random();
            CACTor pnn = new CACTor();

            pnn.X = ptravC.X + 20;
            pnn.Y = ptravC.Y - 65;
            pnn.IF = RR.Next(8);
            pnn.Imgs = new List<Bitmap>();

            for (int i = 0; i < 8; i++)
            {
                Bitmap pnnSora = new Bitmap("coin" + (i + 1) + ".png");
                pnnSora.MakeTransparent(pnnSora.GetPixel(0, 0));
                pnn.Imgs.Add(pnnSora);
            }

            coins.Add(pnn);
        }
        void creatHero()
        {
            Random RR = new Random();
            CACTor pnn = new CACTor();

            pnn.X = this.ClientSize.Width / 20;
            pnn.Y = this.ClientSize.Height / 2 + this.ClientSize.Height / 4 - 70;
            pnn.IF = RR.Next(10);        
            pnn.Imgs = new List<Bitmap>();

            for (int i = 0; i < 10; i++)  
            {
                Bitmap pnnSora = new Bitmap("m" + (i + 1) + ".png");
                pnnSora.MakeTransparent(pnnSora.GetPixel(0, 0));
                pnn.Imgs.Add(pnnSora);
            }

            LActs.Add(pnn);
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

            for (int i = 0; i < lm.Count; i++)
            {
                MuImage pnn = lm[i];
                pnn.src = new Rectangle(scrollX, 0, this.ClientSize.Width, pnn.sora.Height);
                pnn.Dst = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
                g.DrawImage(pnn.sora, pnn.Dst, pnn.src, GraphicsUnit.Pixel);
            }

            for (int i = 0; i < LActs.Count; i++)
            {
                CACTor pTrv = LActs[i];
                g.DrawImage(pTrv.Imgs[pTrv.IF], pTrv.X, pTrv.Y);
            }
            for (int i = 0; i < coins.Count; i++)
            {
                CACTor pTrv = coins[i];
                g.DrawImage(pTrv.Imgs[pTrv.IF], pTrv.X, pTrv.Y);
            }
            for (int i = 0; i < edges.Count; i++)
            {
                CEdges pnn = edges[i];
                g.DrawLine(pnn.p, pnn.X, pnn.Y, pnn.X + pnn.W, pnn.Y);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            close c = new close();
            c.Show();
        }
    }
    //er7m den omy 
    public class CACTor
    {
        public int X, Y;
        public Rectangle src;
        public Rectangle Dst;
        public List<Bitmap> Imgs;
        public int IF;
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

}