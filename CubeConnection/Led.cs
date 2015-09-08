using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeConnection
{
    public class Led : ColourRGB
    {
        public int x = 0;
        public int y = 0;
        public int z = 0;

        public bool up(int jump_size)
        {
            z = z + jump_size;
            if (z > 3)
            {
                z = 3;
                return false;
            }
            return true;
        }
        public bool down(int jump_size)
        {

            z = z - jump_size;
            if (z < 0)
            {
                z = 0;
                return false;
            }
            return true;
        }
        public bool right(int jump_size)
        {

            x = x + jump_size;
            if (x > 3)
            {
                x = 3;
                return false;
            }
            return true;
        }
        public bool left(int jump_size)
        {

            x = x - jump_size;
            if (x < 0)
            {
                x = 0;
                return false;
            }
            return true;
        }
        public bool back(int jump_size)
        {

            y = y + jump_size;
            if (y > 3)
            {
                y = 3;
                return false;
            }
            return true;
        }

        public bool forward(int jump_size)
        {

            y = y - jump_size;
            if (y < 0)
            {
                y = 0;
                return false;
            }
            return true;
        }

        public void set_address(int _x, int _y, int _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        public void random_address()
        {
            Random rnd = new Random();
            x = rnd.Next(0, 4);
            y = rnd.Next(0, 4);
            z = rnd.Next(0, 4);
        }

        public  string cmd()
        {
            return "{" + x.ToString() + y.ToString() + z.ToString() + red.ToString("X2") + green.ToString("X2") + blue.ToString("X2") + "}";
        }
    }
}
