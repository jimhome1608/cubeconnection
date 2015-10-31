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
       

        public void turn_off()
        {
            set_colour("black");
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
