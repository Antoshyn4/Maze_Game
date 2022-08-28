using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public class Model
    {
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
        public double Points = 5000;
        public int local = 0;
        private int sss = 0;
        private int moves { get; set; }
        public int l = 999, li, level;
        public bool key { get; set; }
        public Model(int l, int size)
        {
            level = l;
        }
        public void input(Person p, Pole[,] m, int keyValue)
        {
            p.ox = p.x;
            p.oy = p.y;
            switch (keyValue)
            {
                case 87:
                    p.y--;
                    break;
                case 65:
                    p.x--;
                    break;
                case 83:
                    p.y++;
                    break;
                case 68:
                    p.x++;
                    break;
                default:
                    break;
            }
            moves++;
        }
        public bool Logic(Person p, int size, Dors[] dor, Pole[,] m, System.Windows.Forms.Timer timer2, System.Windows.Forms.Timer timer3, System.Windows.Forms.TextBox textBox2)
        {
            bool r = false;
            if (p.x < 0)
                p.x = 0;
            if (p.y < 0)
                p.y = 0;
            if (p.x >= size)
                p.x = size - 1;
            if (p.y >= size)
                p.y = size - 1;
            for (int i = 0; i < dor.Length; i++)
            {
                if (p.x == dor[i].x && p.y == dor[i].y && dor[i].B == true && i >= 3 && (dor[i - 1].B == false || i == 3))
                {
                    dor[i].B = false;
                    dor[i - 3].B = false;
                    m[dor[i].x, dor[i].y].type = 0;
                    l = moves;
                    li = i;
                    timer2.Stop();
                    timer2.Start();
                    timer3.Enabled = true;
                    r = true;
                    textBox2.Visible = false;
                }
            }
            if (m[p.x, p.y].type != 0 && m[p.x, p.y].type != 10 && m[p.x, p.y].type != 11)
            {
                p.y = p.oy;
                p.x = p.ox;
            }
            if (p.x == dor[2].x && p.y == dor[2].y)
            {
                End(dor, m, size, p, true );
            }
            if (m[p.x, p.y].type == 10 || m[p.x, p.y].type == 11)
            {
                if (m[p.x, p.y].type == 10)
                {
                    p.x = m[0, 0].IndexX(m, 11);
                    p.y = m[0, 0].IndexY(m, 11);
                }
                else
                {
                    p.x = m[0, 0].IndexX(m, 10);
                    p.y = m[0, 0].IndexY(m, 10);
                }
            }
            if (moves - l == 40)
            {
                l = 999;
                dor[li].B = true;
                dor[li - 3].B = true;
            }
            return r;
        }
        public int Draw(Pole[,] m, int size, Graphics g)
        {
            if ((level == 3 || level == 100) && local == 0)
            {
                g.DrawImage(TE, new Rectangle(0, 0, 1600, 900));
                Points /=Math.Pow(1.0025,moves);
                Points = (int)Points;
                if (!File.Exists("table1"))
                {
                    File.Create("table1");
                    File.Decrypt("table1");
                }
                string a = File.ReadAllText("table1");
                string b = "\n";
                File.WriteAllText("table1", a + Points.ToString() + b);
                local = 1;
                return local;
            }
            if (!key)
                return local;
            for (int i = 0; i < size; i++)  
            {
                for (int z = 0; z < size; z++)
                {
                    switch (m[z, i].type)
                    {
                        case 1:
                            g.DrawImage(Player, new Rectangle(z * 90, i * 90, 90, 90));
                            m[z, i].vis = 'o';
                            break;
                        case 2:
                            g.DrawImage(Wall, new Rectangle(z * 90, i * 90, 90, 90));
                            m[z, i].vis = '#';
                            break;
                        case 3:
                            m[z, i].vis = 'o';
                            break;
                        case 4:
                            g.DrawImage(A, new Rectangle(z * 90, i * 90, 90, 90));
                            m[z, i].vis = 'A';
                            break;
                        case 5:
                            g.DrawImage(B, new Rectangle(z * 90, i * 90, 90, 90));
                            m[z, i].vis = 'B';
                            break;
                        case 6:
                            g.DrawImage(C, new Rectangle(z * 90, i * 90, 90, 90));
                            m[z, i].vis = 'C';
                            break;
                        case 7:
                            g.DrawImage(a, new Rectangle(z * 90, i * 90, 90, 90));
                            m[z, i].vis = 'a';
                            break;
                        case 8:
                            g.DrawImage(b, new Rectangle(z * 90, i * 90, 90, 90));
                            m[z, i].vis = 'b';
                            break;
                        case 9:
                            g.DrawImage(c, new Rectangle(z * 90, i * 90, 90, 90));
                            m[z, i].vis = 'c';
                            break;
                        case 10:
                            g.DrawImage(T, new Rectangle(z * 90, i * 90, 90, 90));
                            m[z, i].vis = 'T';
                            break;
                        case 11:
                            g.DrawImage(T, new Rectangle(z * 90, i * 90, 90, 90));
                            m[z, i].vis = 't';
                            break;
                        default:
                            m[z, i].vis = ' ';
                            break;
                    }
                }
            }
            return local;
        }
        public void Start(Dors[] dor, Pole[,] pole, int size, Person p)
        {
            p.x = 1;
            p.y = 1;
            for (int i = 0; i < dor.Length; i++)
            {
                dor[i] = new Dors();
            }
            for (int z = 0; z < size + 5; z++)
            {
                for (int i = 0; i < size + 5; i++)
                {
                    pole[i, z] = new Pole();
                }
            }
            if (level == 2)
            {
                p.x = 9;
                p.y = 7;
                dor[0].x = 3;
                dor[0].y = 7;
                dor[1].x = 1;
                dor[1].y = 3;
                dor[2].x = 13;
                dor[2].y = 3;
                dor[3].x = 12;
                dor[3].y = 5;
                dor[4].x = 1;
                dor[4].y = 6;
                dor[5].x = 1;
                dor[5].y = 1;
            }
            else if (level == 1)
            {
                p.x = 1;
                p.y = 1;
                dor[0].x = 11;
                dor[0].y = 3;
                dor[1].x = 13;
                dor[1].y = 7;
                dor[2].x = 16;
                dor[2].y = 2;
                dor[3].x = 3;
                dor[3].y = 7;
                dor[4].x = 5;
                dor[4].y = 7;
                dor[5].x = 8;
                dor[5].y = 7;
            }
            for (int i = 0; i < dor.Length; i++)
            {
                dor[i].B = true;
            }
            key = true;
        }
         public void filling(Pole[,] pole, int size, Person p, Dors[] dor, System.Windows.Forms.TextBox textBox1)
        {
            
            if (level == 99)
            {
                
                string path = @"C:\Users\dimam\source\repos\WindowsFormsApp1\WindowsFormsApp1\bin\Debug\";
                path += textBox1.Text;
                path += ".txt";
                if (File.Exists(path))
                {
                    using (StreamReader reader = File.OpenText(path))
                    {
                        for (int i = 0; i < size + 5; i++)
                        {
                            for (int z = 0; z < size + 5; z++)
                            {
                                int temp = int.Parse(reader.ReadLine());
                                pole[z, i].type = temp;
                                if (z == p.x && i == p.y)
                                {
                                    pole[z, i].type = 1;
                                }
                            }
                        }
                    }
                    if (sss == 0)
                    {
                        for (int i = 0; i < dor.Length; i++)
                        {
                            dor[i].B = false;
                        }
                        for (int y = 0; y < size; y++)
                        {
                            for (int x = 0; x < size; x++)
                            {

                                switch (pole[x, y].type)
                                {
                                    case 4:
                                        dor[0].x = x;
                                        dor[0].y = y;
                                        dor[0].B = true;
                                        break;
                                    case 5:
                                        dor[1].x = x;
                                        dor[1].y = y;
                                        dor[1].B = true;
                                        break;
                                    case 6:
                                        dor[2].x = x;
                                        dor[2].y = y;
                                        dor[2].B = true;
                                        break;
                                    case 7:
                                        dor[3].x = x;
                                        dor[3].y = y;
                                        dor[3].B = true;
                                        break;
                                    case 8:
                                        dor[4].x = x;
                                        dor[4].y = y;
                                        dor[4].B = true;
                                        break;
                                    case 9:
                                        dor[5].x = x;
                                        dor[5].y = y;
                                        dor[5].B = true;
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        sss = 1;
                    }
                    for (int i = 0; i < dor.Length; i++)
                    {
                        if (dor[i].B == true)
                        {
                            pole[dor[i].x, dor[i].y].type = i + 4;
                        }
                        else
                        {
                            if (dor[i].x == p.x && dor[i].y == p.y)
                            {
                                pole[dor[i].x, dor[i].y].type = 1;
                            }
                            else
                                pole[dor[i].x, dor[i].y].type = 0;
                        }
                    }
                }
            }
            else
            {
                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        pole[x, y].type = 0;
                    }
                }
                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        if (y == p.y && x == p.x)
                            pole[x, y].type = 1;
                        else if (level == 2)
                        {
                            if (((y == 0 || y == 8) && x < 13 || (x == 0 || x == 13) && y <= 8 || x == 2 && (y >= 1 && y <= 2)) && !dor[0].Check(x, y, dor) || (y == 6 && (x >= 2 && x <= 3)) || y == 6 && (x >= 9 && x <= 12) || y == 4 && (x >= 8 && x <= 12) || x == 8 && (y >= 4 && y <= 6) || x == 1 && y == 5)
                                pole[x, y].type = 2;
                            else if (y == 5 && x == 9)
                                pole[x, y].type = 10;
                            else if (y == 4 && x == 1)
                                pole[x, y].type = 11;
                            for (int i = 0; i < dor.Length; i++)
                            {
                                if (dor[i].x == x && dor[i].y == y && dor[i].B == true)
                                {
                                    pole[x, y].type = i + 4;
                                }
                            }
                        }
                        else if (level == 1)
                        {
                            if (y == 0 || y == 8 || x == 0 || (x == 16 && y != 2) || y == 2 && ((x >= 1 && x <= 4) || x == 6 || x == 12) || y == 1 && (x == 6 || x == 15) || y == 3 && (x == 8 || x == 10 || x >= 13) || y == 4 && (x == 10 || x == 11 || x == 7 || (x >= 2 && x <= 4)) || y == 5 && ((x >= 4 && x <= 6) || (x >= 9 && x <= 13)) || y == 6 && ((x <= 14 && x >= 13) || x == 8 || x == 9 || x == 2 || x == 3 || x == 4) || y == 7 && (x == 6 || x == 7 || x == 4 || x == 11))
                                pole[x, y].type = 2;
                            for (int i = 0; i < dor.Length; i++)
                            {
                                if (dor[i].x == x && dor[i].y == y && dor[i].B == true)
                                {
                                    pole[x, y].type = i + 4;
                                }
                            }
                        }
                    }
                }
            }
        }
        public void End( Dors[] doors, Pole[,] pole, int size,  Person p, bool a = false)
        {
            if (a)
            {
                level++;
                Points *= 2;
                key = false;
                Start(doors, pole, size, p);
            }
        }
        public void Time(Dors[] dors)
        {
            for (int i = dors.Length - 1; i > 0 ; i--)
            {
                if (!dors[i].B)
                {
                    dors[i].B = true;
                    dors[i - 3].B = true;
                    return;
                }
            }
        }
    }
}
