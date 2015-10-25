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
            led_cube.random_colors();
            //led_cube.push_to_hardware();
            Thread.Sleep(5000);
            while (!Console.KeyAvailable)
            {            
                for (int y = 0; y < 4; y++)
                {
                    led_cube.leds.current_color.set_colour("red");
                    led_cube.leds.make_wall_y(y);
                    led_cube.push_to_hardware();
                    Thread.Sleep(500);
                    led_cube.all_off();
                    led_cube.leds.clear();
                }
                for (int x = 0; x < 4; x++)
                {
                    led_cube.leds.current_color.set_colour("blue");
                    led_cube.leds.make_wall_x(x);
                    led_cube.push_to_hardware();
                    Thread.Sleep(500);
                    led_cube.all_off();
                    led_cube.leds.clear();
                }
                for (int z= 0; z < 4; z++)
                {
                    led_cube.leds.current_color.set_colour("green");
                    led_cube.leds.make_floor(z);
                    led_cube.push_to_hardware();
                    Thread.Sleep(500);
                    led_cube.all_off();
                    led_cube.leds.clear();
                }
            }
            //led_cube.random_colors();
            //led_cube.rain(500);
            Console.ReadKey();
            led_cube.all_off();
            led_cube.close();
        }


       
    }
}