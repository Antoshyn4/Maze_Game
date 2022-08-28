using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public int size = 17;
        public int TimeLeft = 10;
        public bool open = false;
        Person p = new Person(3, 1);
        Pole[,] pole = new Pole[22, 22];
        Dors[] doors = new Dors[6];
        Model model = new Model(1, 22);
        //private int level;

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint, true);
            UpdateStyles();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            model.Start(doors, pole, size, p);
            textBox2.Visible = false;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            model.filling(pole, size, p, doors, textBox1);
            if (model.Draw(pole, size, g) == 1)
                this.Close();

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Refresh();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            model.input(p, pole, e.KeyValue);
            if (model.Logic(p, size, doors, pole, timer2, timer3, textBox2))
            {
                TimeLeft = 10;
            }
            if (e.KeyValue == 27)
            {
                Application.Exit();
            }
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            model.Time(doors);
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (TimeLeft <= 0)
            {
                textBox2.Visible = false;
            }
            else if (!doors[0].B)
            {
                TimeLeft -= 1;
                textBox2.Visible = true;
                textBox2.Text = "Дверь закроется через " + TimeLeft.ToString();
            }
            else
                textBox2.Visible = false;
            model.Points /= 1.01;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            button2.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button2.Visible = false;
            textBox1.Visible = false;
            button1.Visible = false;
            button3.Visible = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            model.level = 99;
            Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            Refresh();
        }
    }
}
