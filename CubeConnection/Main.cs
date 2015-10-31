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
            //led_cube.leds.make_floor(2);
            // led_cube.random_colors();
            //led_cube.push_to_hardware();
            //Thread.Sleep(5000);
            
            while (!Console.KeyAvailable)
            {
                led_cube.add_moisture();
                Thread.Sleep(50);
               // if (rnd.Next(1,5) ==4)
                   led_cube.moisture_falls_at_value(100);
               // Thread.Sleep(50);

            }
            //led_cube.random_colors();
            //led_cube.rain(500);
            Console.ReadKey();
            led_cube.all_off();
            led_cube.close();
        }


       
    }
}