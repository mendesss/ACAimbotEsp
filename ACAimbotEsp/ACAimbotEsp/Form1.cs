using System.Runtime.InteropServices;
using System.Threading;
using ezOverLay;





namespace ACAimbotEsp
{
    public partial class Form1 : Form
    {
        ez ez = new ez();
        methods? m;
        Entity localPlayer = new Entity();
        List<Entity> entities = new List<Entity>();

        [DllImport("user32.dll")]

        static extern short GetAsyncKeyState(Keys vKey);



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            m = new methods();
            if (m != null)
            {

                ez.SetInvi(this);
                ez.DoStuff("AssaultCube", this);

                Thread thread = new Thread(Main) { IsBackground = true };
                thread.Start();
            }

            localPlayer = m.ReadLocalPlayer();
            entities = m.ReadEntities(localPlayer);

            int i = 0;
        }


        void Main()
        {
            while (true)
            {
                localPlayer = m.ReadLocalPlayer();
                entities = m.ReadEntities(localPlayer);

                entities = entities.OrderBy(o => o.mag).ToList();
                if (GetAsyncKeyState(Keys.XButton2) < 0)
                {
                    if (entities.Count > 0)
                    {
                        foreach(var ent in entities)
                        {
                            if (ent.team != localPlayer.team)
                            {
                                var angles = m.CalcAngles(localPlayer, ent);
                                m.Aim(localPlayer, angles.X, angles.Y);
                                break;
                            }
                        }
                    }
                }

                Form1 f = this;
                f.Refresh();
            }




        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen red = new Pen(Color.Red, 3);
            Pen green = new Pen(Color.Green, 3);


            foreach(var ent in entities.ToList())
            {
                var wtsFeet = m.WorldToScreen(m.ReadMatrix(),ent.feet,this.Width,this.Height);
                var wtsHead = m.WorldToScreen(m.ReadMatrix(), ent.head, this.Width, this.Height);

                if (wtsFeet.X > 0)
                {
                    if (localPlayer.team == ent.team)
                    {
                        g.DrawLine(green,new Point(Width/2, Height), wtsFeet);
                        g.DrawRectangle(green, m.CalcRect(wtsFeet, wtsHead));
                    }
                    else
                    {
                        g.DrawLine(red,new Point(Width/2, Height), wtsFeet);
                        g.DrawRectangle(red, m.CalcRect(wtsFeet, wtsHead));
                    }
                }

            }
        }
    }
}