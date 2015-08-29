using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace CubeConnection
{
    class Program
    {
       


        static void Main(string[] args)
        {

            LedCube led_cube = new LedCube();

            if (!led_cube.open()) 
                return;

            Console.WriteLine("Connected OK");

            led_cube.all_colour(135,206,255);
            led_cube.upload();
            Thread.Sleep(1000);
            led_cube.all_colour("Green");
            led_cube.set_address(1,1,1);
            led_cube.set_colour("red");
            led_cube.upload();

            Console.ReadKey();
            led_cube.all_off();
            led_cube.close();
        }
    }
}