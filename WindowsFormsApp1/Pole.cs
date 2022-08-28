using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Pole
    {
        public int type;
        public char vis;
        public Pole(int t, char v)
        {
            type = t;
            vis = v;
        }
        public Pole()
        {

        }
        public int IndexX(Pole[,] m, int a)
        {
            for (int x = 0; x < Math.Sqrt(m.Length); x++)
            {
                for (int y = 0; y < Math.Sqrt(m.Length); y++)
                {
                    if (m[x, y].type == a)
                    {
                        return x;
                    }
                }
            }
            return -1;
        }
        public int IndexY(Pole[,] m, int a)
        {
            for (int x = 0; x < Math.Sqrt(m.Length); x++)
            {
                for (int y = 0; y < Math.Sqrt(m.Length); y++)
                {
                    if (m[x, y].type == a)
                    {
                        return y;
                    }
                }
            }
            return -1;
        }
        public int IndexX(Pole[,] m, char a)
        {
            for (int x = 0; x < Math.Sqrt(m.Length); x++)
            {
                for (int y = 0; y < Math.Sqrt(m.Length); y++)
                {
                    if (m[x, y].vis == a)
                    {
                        return x;
                    }
                }
            }
            return -1;
        }
        public int IndexY(Pole[,] m, char a)
        {
            for (int x = 0; x < Math.Sqrt(m.Length); x++)
            {
                for (int y = 0; y < Math.Sqrt(m.Length); y++)
                {
                    if (m[x, y].vis == a)
                    {
                        return y;
                    }
                }
            }
            return -1;
        }
        
    }
}
