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
        public int bullets = 10;
        public int health;
        public int spikeDamage = 2;

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
        List<CACTor> LActTiger = new List<CACTor>();
        List<CACTor> LActsgates = new List<CACTor>();
        List<CACTor> LActsVEn = new List<CACTor>();
        List<CACTor> LActsHeroBullet = new List<CACTor>();
        List<CACTor> LActsSpikes = new List<CACTor>();
        List<CACTor> LActsBUlletsHeli = new List<CACTor>();
        List<CACTor> coins = new List<CACTor>();
        List<CACTor> pC = new List<CACTor>();
        List<CEdges> edges = new List<CEdges>();
        List<MuImage> lm = new List<MuImage>();
        List<CACTor> slm = new List<CACTor>();
        List<CACTor> platform = new List<CACTor>();


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
        int mission = 1 ;
        int tH =0 ;
        string Password = "";
        int Meess = 0;
        int Onetime = 0;
        int KillTiger = 0;
        int spikeD=0;
        int spikeR=0;
        int tS = 0;
        int tot = 0;
        int tot2 = 0;

        int flagplat=0;
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
            
            if( scrollX < this.ClientSize.Width/2 )
            {
                scrollX = ptrv.X - 60;

            }
            CACTor muPtrav = LActsgates[0];
            CACTor heroPtrav = LActs[0];
            if (e.KeyCode == Keys.M)
            {
                if(heroPtrav.X >= muPtrav.X && heroPtrav.X <= muPtrav.X + muPtrav.Imgs[0].Width
                    && ptrv.Y<muPtrav.Y + muPtrav.Imgs[0].Height / 2)
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
            if (e.KeyCode == Keys.Up)
            {
                if (ptrv.X + ptrv.Imgs[0].Width/2 >= slm[0].X
                    && ptrv.X < slm[0].X + slm[0].Imgs[0].Width/2)
                {
                    if (ptrv.Y + ptrv.Imgs[0].Height/2 +50 >= slm[0].Y)
                    {
                        ptrv.Y -= 10;
                    }
                }
            }
            if (e.KeyCode == Keys.Down)
            {
                if (ptrv.X + ptrv.Imgs[0].Width/2 >= slm[0].X
                    && ptrv.X < slm[0].X + slm[0].Imgs[0].Width/2)
                {
                    if (ptrv.Y <=(this.ClientSize.Height / 2 + this.ClientSize.Height / 4 - 90))
                    {
                        ptrv.Y += 10;
                    }
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
            
            if (e.KeyCode == Keys.F)
            {
                createBullethero();
            }


            if (e.KeyCode == Keys.D)
            {
                if(Onetime == 0)
                {
                    ControlHealth();
                    Onetime = 1;
                }
            }
            //platform
            if (e.KeyCode == Keys.T)
            {
                platform[1].dir = 2;
            }
            if (e.KeyCode == Keys.G)
            {
                platform[1].dir = 1;
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////
        private void Tt_Tick(object sender, EventArgs e)
        {
            CACTor ptrv = LActs[0];

            if (ptrv.health <= 0) 
            {
                LActs.Clear(); 
                createHero(); 

                if(levelState == 0)
                {
                    createhelicopter(); 
                    createtiger(); 
                }
                scrollX = 0;   
                return; 
            }

            herojump();
            ForMessage();
    
            if (levelState == 0)
            {
                moveheli(); 
                movetiger();  
                attacktiger(); 
            }
            if(levelState == 1)
            {
                moveSpikes(tS);
                tS++;
            }

            CACTor ptrvGate = LActsgates[0];
            if(t % 4 == 0)
            {
                ptrvGate.IF +=1;
            }
            if (t % 30 == 0 && levelState == 0)
            {
                CreateBUlletsForHELI(tH); 
            }

            MoveBulletForHEli(); 
            moveBulletHero();
            tH++;
            if(ptrvGate.IF >= ptrvGate.Imgs.Count)
            {
                ptrvGate.IF = 0;
            }
            t++;
            for (int i = 0; i < coins.Count; i++)
            {
                coins[i].IF = (coins[i].IF + 1) % 8;
            }
            Damage();
            getCoins();
            level2();
            gameOver();
            if (flagplat==1)
            {


                moveplatform();
            }
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

            if(levelState == 0)
            {
                createslm();
                createhelicopter();
                createCoins(incX);
                createtiger();
                createGate();
            }
            //
            attacktiger();
            DrawDubb(this.CreateGraphics());
        }
        /////////////////////////////////////////////////////////////////////////
        ///
        void gameOver()
        {
            CACTor ptravH = LActs[0];
            MuImage muPtrav = lm[0];

            if(ptravH.health == 0)
            {
                muPtrav.sora = new Bitmap("maps/map3.png");
                //LActs.Clear();
                
            }
        }
        void ForMessage()
        {
            if (LActs.Count == 0) { 
                return; 
            }
            CACTor heroPtrav = LActs[0];
    
            if (LActsgates.Count > 0)
            {
                CACTor muPtrav = LActsgates[0];
                if (heroPtrav.X >= muPtrav.X && heroPtrav.X <= muPtrav.X + muPtrav.Imgs[0].Width)
                {
                    Meess = 1;
                }
            }

            if (LActsVEn.Count > 0)
            {
                CACTor venPtrav = LActsVEn[0]; 

                if (heroPtrav.X >= venPtrav.X && heroPtrav.X <= venPtrav.X + venPtrav.Imgs[0].Width)
                {
                    if (Meess != 3) 
                    {
                        Meess = 2;
                    }
                }
                else if (Meess != 1) 
                {
                    Meess = 0;
                }
            }
            else if (Meess != 1)
            {
                Meess = 0;
            }
        }

        void level2()
        {
            MuImage muPtrav = lm[0];
            CACTor heroPtrav = LActs[0];
            CACTor ptrvHEli = LActs[1];
            if (lm.Count == 0 || LActs.Count < 2){
                return;
            }
            if(move ==1)
            {
                Meess = 0;
                muPtrav.sora = new Bitmap("maps/map2.png");
                mission = 2;
                heroPtrav.X = startPointX;
                scrollX = 0;
                move = 0;
                levelState = 1;
                coins.Clear();
                createVendingmachine();
                createSpikes();
                createplatform();
                flagplat = 1;
                //createPC();
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
        ///      
        void moveSpikes(int t)
        {
            for(int i = 0 ; i < LActsSpikes.Count ; i++)
            {
                CACTor ptravS = LActsSpikes[i];
                if(i % 2 == 0)
                {
                    if(spikeD == 0)
                    {
                        ptravS.Y -=10;
                        tot+=10;
                        if(tot > 280)
                        {
                            spikeD = 1;
                        }
                    }
                    if(spikeD == 1)
                    {
                        ptravS.Y +=10;
                        tot -=10;
                        if(tot < 0)
                        {
                            spikeD = 0;
                        }
                    }
                }
                else
                {
                    if(tS > 30)
                    {
                        if(spikeR == 0)
                        {
                            ptravS.Y -=10;
                            tot2+=10;
                            if(tot2 > 180)
                            {
                                spikeR = 1;
                            }
                        }
                        if(spikeR == 1)
                        {
                            ptravS.Y +=10;
                            tot2 -=10;
                            if(tot2 < 0)
                            {
                                spikeR = 0;
                            }
                        }
                    }

                }
            }
        }
        void createSpikes()
        {
            int adX = 0;
            for (int b =0 ; b < 3 ; b++)
            {
                CACTor pnn = new CACTor();


                pnn.Imgs = new List<Bitmap>();
                pnn.IF = 0;
                pnn.dir = 1;
                pnn.f7arka = 1;
                for (int i = 0; i < 1; i++)
                {
                    Bitmap pnnSora = new Bitmap("spike/" + "spike.png");
                    pnnSora.MakeTransparent(Color.White);
                    pnn.Imgs.Add(pnnSora);

                }
                adX = pnn.Imgs[0].Width;

                pnn.X = this.ClientSize.Width / 2 + b * adX;
                pnn.Y = this.ClientSize.Height / 2 + this.ClientSize.Height / 4 + 220;
                LActsSpikes.Add(pnn);
            }

        }
        void createslm()
        {
            CACTor pnn = new CACTor();


            pnn.Imgs = new List<Bitmap>();
            Bitmap pnnSora = new Bitmap("slm/" + "slm.png");
            pnnSora.MakeTransparent(Color.White);
            pnn.Imgs.Add(pnnSora);


            pnn.X = lm[0].sora.Width-(pnn.Imgs[0].Width*3);
            pnn.Y = this.ClientSize.Height - pnn.Imgs[0].Height - 100;
            slm.Add(pnn);
            pnn = new CACTor();


            pnn.Imgs = new List<Bitmap>();
            pnnSora = new Bitmap("slm/" + "tree.png");
            pnnSora.MakeTransparent(Color.White);
            pnn.Imgs.Add(pnnSora);


            pnn.X = lm[0].sora.Width- pnn.Imgs[0].Width ;
            pnn.Y = slm[0].Y -pnn.Imgs[0].Height/6 ;
            slm.Add(pnn);
        }
        void createplatform()
        {
            CACTor pnn = new CACTor();


            pnn.Imgs = new List<Bitmap>();
            Bitmap pnnSora = new Bitmap("slm/" + "mtree.png");
            pnnSora.MakeTransparent(Color.White);
            pnn.Imgs.Add(pnnSora);


            pnn.X = 0;
            pnn.Y = slm[1].Y;
            platform.Add(pnn);
            pnn = new CACTor();


            pnn.Imgs = new List<Bitmap>();
            pnnSora = new Bitmap("slm/" + "platform.png");
            pnnSora.MakeTransparent(Color.White);
            pnn.Imgs.Add(pnnSora);

            pnn.dir = 0;
            pnn.X = platform[0].X +platform[0].Imgs[0].Width;
            pnn.Y = platform[0].Y;
            platform.Add(pnn);
        }
        void moveplatform()
        {
            CACTor ptrv = platform[1];
            if (ptrv.Y + ptrv.Imgs[0].Height <= this.ClientSize.Height && ptrv.dir == 1)
            {
                ptrv.Y += 5;
            }
            if (ptrv.Y>= platform[0].Y && ptrv.dir == 2)
            {
                ptrv.Y -= 5;
            }
        }
        void Damage()
        {
            CACTor heroPtrav = LActs[0];
            CACTor muPtrav = LActsgates[0];

            for (int i = LActsBUlletsHeli.Count - 1; i >= 0; i--)
            {
                CACTor ptravBU = LActsBUlletsHeli[i];
        
                if (ptravBU.X > heroPtrav.X && 
                    ptravBU.X < heroPtrav.X + heroPtrav.Imgs[0].Width && 
                    ptravBU.Y > heroPtrav.Y && 
                    ptravBU.Y < heroPtrav.Y + heroPtrav.Imgs[0].Height)
                {
                    if (ptravBU.IF == 0)
                    {
                        if(heroPtrav.health >= 0)
                        {
                            heroPtrav.health--; 

                        }
                        LActsBUlletsHeli.RemoveAt(i);
                    }

                }
            }

            for(int i = 0 ; i< LActsSpikes.Count() ;i++)
            {
                CACTor ptravS = LActsSpikes[0];
                if (ptravS.X > heroPtrav.X && 
                ptravS.X < heroPtrav.X + heroPtrav.Imgs[0].Width && 
                ptravS.Y > heroPtrav.Y && 
                ptravS.Y < heroPtrav.Y + heroPtrav.Imgs[0].Height)
                {  
                    if(heroPtrav.health >= 0)
                    {
                        if(ptravS.spikeDamage >= 0)
                        {
                            heroPtrav.health--; 
                            ptravS.spikeDamage--;
                        }
                    }                
                }
            }
        }
        void moveBulletHero()
        {



                for (int i = 0; i < LActsHeroBullet.Count; i++)
                {
                    CACTor ptraB = LActsHeroBullet[i];
                    if (ptraB.dir == 1)
                    {


                        ptraB.X += 60;
                    }
                    if (ptraB.dir==2)
                    {
                        ptraB.X -= 60;

                    }
                    if (LActs != null && LActs.Count > 0 && ptraB.X > LActs[0].X + 780)
                    {
                        LActsHeroBullet.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        for (int k = 0; k < LActTiger.Count; k++)
                        {
                            CACTor ptrvTiger = LActTiger[k];

                            if (ptraB.X > ptrvTiger.X && ptraB.X < ptrvTiger.X + ptrvTiger.Imgs[0].Width
                                && ptraB.Y > ptrvTiger.Y && ptraB.Y < ptrvTiger.Y + ptrvTiger.Imgs[0].Height)
                            {
                                ptrvTiger.health--;

                                if (ptrvTiger.health <= 0)
                                {
                                    LActTiger.RemoveAt(k);
                                    k--;
                                    Meess = 4;

                                    if (KillTiger <= 2)
                                    {
                                        KillTiger++;
                                    }
                                }

                                LActsHeroBullet.RemoveAt(i);
                                i--;

                                break;
                            }
                        }
                    }
                }
            
        }
        void createBullethero()
        {
            CACTor heroPtrav= LActs[0];
            if(heroPtrav.bullets > 0)
            {
                heroPtrav.bullets--;

                CACTor pnn = new CACTor();
                pnn.Y = heroPtrav.Y + 75;
                pnn.Imgs = new List<Bitmap>();
                pnn.IF = 0;
                if (heroPtrav.dir == 1)
                {


                    pnn.dir = 1;
                    pnn.X = heroPtrav.X + 10 + heroPtrav.Imgs[0].Width;

                }
                if (heroPtrav.dir == 2)
                {


                    pnn.dir = 2;
                    pnn.X = heroPtrav.X ;

                }
                pnn.f7arka = 1;
                pnn.health = 0;
                pnn.coins = 0;

                for (int i = 0; i < 1; i++)
                {
                    Bitmap pnnSora = new Bitmap("heroBullert/" + "HeroBu.png");
                    pnnSora.MakeTransparent(Color.White);
                    pnn.Imgs.Add(pnnSora);

                }
                LActsHeroBullet.Add(pnn);
            }

        }
        void createHero()
        {
            CACTor pnn = new CACTor();

            pnn.X = startPointX;
            pnn.Y = this.ClientSize.Height / 2 + this.ClientSize.Height / 4 - 90;
            pnn.Imgs = new List<Bitmap>();
            pnn.IF = 0;
            pnn.dir = 1;
            pnn.f7arka = 1;
            pnn.health = 3;
            pnn.coins = 0;

            for (int i = 0; i < 18; i++)
            {
                Bitmap pnnSora = new Bitmap("hero/" + (i + 1) + "hh.png");
                pnnSora.MakeTransparent();

                pnn.Imgs.Add(pnnSora);

            }

            LActs.Add(pnn);
        }
        void ControlHealth()
        {
            CACTor heroPtrav= LActs[0];
            CACTor muPtrav = LActsVEn[0];

            if(heroPtrav.X >= muPtrav.X && heroPtrav.X <= muPtrav.X + muPtrav.Imgs[0].Width)
            {
                if(heroPtrav.coins == 3)
                {
                    heroPtrav.health +=2;
                    heroPtrav.bullets +=5;
                    heroPtrav.coins-=3;
                    Meess = 3;
                }


            }
        }
        void createPC()
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

            for (int i = 0; i < 1; i++)
            {
                Bitmap pnnSora = new Bitmap("pc/" + "pc.png");
                pnnSora.MakeTransparent(Color.White);
                pnn.Imgs.Add(pnnSora);

            }
            pC.Add(pnn);
        }
        void MoveBulletForHEli()
        {
            for (int i = LActsBUlletsHeli.Count - 1; i >= 0; i--)
            {
                if (LActsBUlletsHeli[i].Y > this.ClientSize.Height / 2 + this.ClientSize.Height / 4 - 50)
                {
                    LActsBUlletsHeli[i].IF = 1;
                    LActsBUlletsHeli[i].f7arka++; 

                    if (LActsBUlletsHeli[i].f7arka > 20) 
                    {
                        LActsBUlletsHeli.RemoveAt(i);
                    }
                }
                else
                {
                    LActsBUlletsHeli[i].Y += 30;
                }
            }
        }
        void CreateBUlletsForHELI(int t )
        {
            t = 0;
            CACTor pnn = new CACTor();
            pnn.Imgs = new List<Bitmap>(); 
            pnn.IF = 0;                   

            CACTor ptrvheli = LActs[1];   
    
            for (int i = 0; i < 2; i++)
            {
                Bitmap pnnSora = new Bitmap("bomp/b" + (i +1) + ".png"); 
                pnnSora.MakeTransparent();
                pnn.Imgs.Add(pnnSora);
            }
    
            pnn.X = ptrvheli.X + (ptrvheli.Imgs[0].Width / 2) - 10;
            pnn.Y = ptrvheli.Y + 80;
    
            LActsBUlletsHeli.Add(pnn);
        }
        void createVendingmachine()
        {
            CACTor pnn = new CACTor();

            pnn.X =  lm[0].sora.Width/25;
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
            pnn.Y = this.ClientSize.Height-pnn.Imgs[0].Height-100;

            LActsVEn.Add(pnn);
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
            pnn.X = lm[0].sora.Width- pnn.Imgs[0].Width;

            pnn.Y = slm[1].Imgs[0].Height-(pnn.Imgs[0].Height /2);

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
            pnn.health = 2;
            pnn.coins = 0;

            for (int i = 0; i < 12; i++)
            {
                Bitmap pnnSora = new Bitmap("tiger/" + (i + 1) + "t.png");
                pnnSora.MakeTransparent();
                pnn.Imgs.Add(pnnSora);
            }

            LActTiger.Add(pnn);
        }
        void movetiger()
        {

            if(LActTiger.Count == 0)
            {
                return;
            }
              CACTor ptrv = LActTiger[0];

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



        }
        void attacktiger()
        {
            if (LActTiger.Count == 0 || LActs.Count == 0)
            {
                return;
            }
            CACTor ptrvt =  LActTiger[0];
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
                ptrvh.health--;
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
            if (LActs.Count < 2){
                return;
            }
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
            if (levelState == 0)
            {



                for (int i = 0; i < slm.Count; i++)
                {
                    CACTor pTrv = slm[i];
                    g.DrawImage(pTrv.Imgs[pTrv.IF], pTrv.X - scrollX, pTrv.Y);
                }
                for (int i = 0; i < LActsgates.Count; i++)
                {
                    CACTor pTrv = LActsgates[i];
                    g.DrawImage(pTrv.Imgs[pTrv.IF], pTrv.X - scrollX, pTrv.Y);
                }
            }
            for (int i = 0; i < platform.Count; i++)
            {
                CACTor pTrv = platform[i];
                g.DrawImage(pTrv.Imgs[pTrv.IF], pTrv.X - scrollX, pTrv.Y);
            }
            //charachters
            for (int i = 0; i < LActs.Count; i++)
            {
                CACTor pTrv = LActs[i];
                if (i == 1 && levelState == 0)
                {
                    g.DrawImage(pTrv.Imgs[pTrv.IF], pTrv.X - scrollX, pTrv.Y);
                }
                if (i == 0)
                {
                    g.DrawImage(pTrv.Imgs[pTrv.IF], pTrv.X - scrollX, pTrv.Y);

                }
            }
            /// coins
            for (int i = 0; i < coins.Count; i++)
            {
                CACTor pTrv = coins[i];
                g.DrawImage(pTrv.Imgs[pTrv.IF], pTrv.X - scrollX, pTrv.Y);
            }
            for (int i = 0; i < pC.Count; i++)
            {
                CACTor pTrv = pC[i];
                g.DrawImage(pTrv.Imgs[pTrv.IF], pTrv.X - scrollX, pTrv.Y);
            }
            if (levelState == 0)
            {
                for (int i = 0; i < LActsBUlletsHeli.Count; i++)
                {
                    CACTor pTrv = LActsBUlletsHeli[i];
                    g.DrawImage(pTrv.Imgs[pTrv.IF], pTrv.X - scrollX, pTrv.Y);
                }
            }
            for (int i = 0; i < LActsVEn.Count; i++)
            {
                CACTor pTrv = LActsVEn[i];
                g.DrawImage(pTrv.Imgs[pTrv.IF], pTrv.X - scrollX, pTrv.Y);
            }
            for (int i = 0; i < LActsHeroBullet.Count; i++)
            {
                CACTor pTrv = LActsHeroBullet[i];
                g.DrawImage(pTrv.Imgs[pTrv.IF], pTrv.X - scrollX, pTrv.Y);
            }
            for (int i = 0; i < LActTiger.Count; i++)
            {
                CACTor pTrv = LActTiger[i];
                g.DrawImage(pTrv.Imgs[pTrv.IF], pTrv.X - scrollX, pTrv.Y);
            }
            for (int i = 0; i < LActsSpikes.Count; i++)
            {
                CACTor pTrv = LActsSpikes[i];
                g.DrawImage(pTrv.Imgs[pTrv.IF], pTrv.X - scrollX, pTrv.Y);
            }

            if (LActs.Count == 0){
                return;
            }
            CACTor hero = LActs[0];

            Font f = new Font("Arial", 20, FontStyle.Bold);
            Brush b = Brushes.Red;
            g.DrawString("mmo: " +  hero.bullets, f, b, this.ClientSize.Width / 3 - 400, 20);

            g.DrawString("Health: " +  hero.health, f, b, this.ClientSize.Width / 3, 20);
            g.DrawString("Coins: " + hero.coins, f, b, this.ClientSize.Width / 3 + 150, 20);
            g.DrawString("Mession: " + mission, f, b, this.ClientSize.Width / 3 - 250, 20);
            if (Meess == 1 )
            {
                g.DrawString("message: " + "press m to move", f, b, this.ClientSize.Width / 3 + 350, 20);

            }
            if (Meess == 2 && Onetime != 1 )
            {
                g.DrawString("message: " + "press d to Drink", f, b, this.ClientSize.Width / 3 + 350, 20);
                g.DrawString("message: " + "you Must have 3 coins", f, b, this.ClientSize.Width / 3 + 350, 50);


            }
            if (Meess == 3)
            {
                g.DrawString("message: " + "Your heath increased by 2", f, b, this.ClientSize.Width / 3 + 350, 20);
                g.DrawString("message: " + "Your Ammor increased by 5", f, b, this.ClientSize.Width / 3 + 350, 50);
            }
            if (Meess == 4)
            {
                g.DrawString("message: " + "You killed the tiger", f, b, this.ClientSize.Width / 3 + 350, 20);

            }


        }

    }


}