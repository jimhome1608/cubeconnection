using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Speech.Synthesis;

namespace CubeConnection
{
    class Program
    {

        static LedCube led_cube = new LedCube();

        static void Main(string[] args)
        {
            if (!led_cube.open())
            {
                Console.ReadKey();
                return;
            }
            //play_game();
            led_cube.x_line(2, 2);
            led_cube.random_colors();
            //led_cube.rain(500);
            Console.ReadKey();
            led_cube.all_off();
            led_cube.close();
        }


       
    }
}