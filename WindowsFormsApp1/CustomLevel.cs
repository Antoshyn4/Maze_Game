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
    public partial class CustomLevel : Form
    {
        public int x;
        public int y;
        public int selectedItems;
        public Bitmap Player = Resource1.Игрокpng;
        public Bitmap Wall = Resource1.Стена1;
        public Bitmap A = Resource1.Коричневая;
        public Bitmap B = Resource1.Чёрная;
        public Bitmap C = Resource1.Красная;
        public Bitmap a = Resource1.коричневйы_ключ;
        public Bitmap b = Resource1.черный_ключ;
        public Bitmap c = Resource1.красный_ключч;
        public Bitmap T = Resource1.Портал;
        public Bitmap TE = Resource1.Конец;
        Person p = new Person(3, 1);
        Pole[,] pole = new Pole[22, 22];
        Dors[] doors = new Dors[6];
        Model model = new Model(1, 22);
        private int size = 22;
        private int sch = 0;

        public CustomLevel()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 22; i++)
            {
                for (int z = 0; z < 22; z++)
                {
                    pole[i, z] = new Pole();
                }
            }
            timer1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button1.Enabled = false;
            this.BackgroundImage = Resource1.Фон;
            listBox1.Visible = true;
            timer1.Enabled = true;
            button2.Visible = false;
            button2.Enabled = false;
            button3.Visible = true;
            textBox1.Visible = true;
        }

        private void Form3_MouseClick(object sender, MouseEventArgs e)
        {
            x = e.X/90;
            y = e.Y/90;
        }

        private void Form3_Paint(object sender, PaintEventArgs e)
        {
            if (timer1.Enabled)
            {
                Graphics g = e.Graphics;
                pole[x, y].type = selectedItems;
                model.key = true;
                model.Draw(pole,size,g);   
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int a = listBox1.SelectedIndex;
            switch (a)
            {
                case 0:
                    selectedItems = 1;
                    break;
                case 1:
                    selectedItems = 2;
                    break;
                case 2:
                    selectedItems = 4;
                    break;
                case 3:
                    selectedItems = 5;
                    break;
                case 4:
                    selectedItems = 6;
                    break;
                case 5:
                    selectedItems = 7;
                    break;
                case 6:
                    selectedItems = 8;
                    break;
                case 7:
                    selectedItems = 9;
                    break;
                case 8:
                    selectedItems = 10;
                    break;
                case 9:
                    selectedItems = 11;
                    break;
                case 10:
                    selectedItems = 0;
                    break;
                default:
                    break;
            } 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Refresh();
        }   
        private void button3_Click(object sender, EventArgs e)
        {
            bool f = false;
            int index = 0;
            int[] mas = new int[6];
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    sch = pole[x, y].type; 
                    if (sch >= 4 && sch <= 6)
                    {
                        foreach (var item in mas)
                        {
                            f = false;
                            if (sch == item)
                            {
                                f = true;
                                break;
                            }
                        }
                        if (f)
                        {
                            f = false;
                            break;
                        }
                        for (int Y = 0; Y < size; Y++)
                        {
                            for (int X= 0; X < size; X++)
                            {
                                if (pole[X, Y].type == sch + 3)
                                {
                                    index++;
                                    mas[index - 1] = sch;
                                    f = true;
                                    break;
                                }
                            }
                            if (f)
                            {
                                f = false;
                                break;
                            }
                        }
                    }
                }
            }
            if (index >= 3)
            {
                textBox2.Visible = false;
                string path = @"C:\Users\dimam\source\repos\WindowsFormsApp1\WindowsFormsApp1\bin\Debug\";
                path += textBox1.Text;
                path += ".txt";
                if (File.Exists(path))
                {
                    File.WriteAllText(path, "");
                }
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    for (int i = 0; i < size; i++)
                    {
                        for (int z = 0; z < size; z++)
                        {
                            writer.WriteLine(pole[z, i].type);
                        }
                    }
                }
                Application.Restart();
            }
            else
            {
                textBox2.Visible = true;
                textBox2.Text = "Не хватает необходимого элемента (на уровне обязательно должно быть 3 ключа и 3 двери), добавьте недостающий элемент и нажмите сохранить";
                sch = 0;
            }
        }
    }
}
