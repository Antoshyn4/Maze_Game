using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Dors
    {
        public int x;
        public int y;
        public bool B;
        public bool Check(int x, int y, Dors[] dor)
        {
            foreach (var item in dor)
            {
                if (item.x == x && item.y == y)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
