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
       


        static void Main(string[] args)
        {
            LedCube led_cube = new LedCube();
            if (!led_cube.open()) 
                return;

            int score = 0;

            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Volume = 100;  // 0...100
            synthesizer.Rate = -2;     // -10...10

            Console.WriteLine("Connected OK");

            const String BACKGROUND_COLOUR = "BLACK";

            led_cube.all_colour(BACKGROUND_COLOUR);
            led_cube.set_colour("red");
            led_cube.target_led.set_colour("blue");
            led_cube.target_led.random_address();
            led_cube.upload();
            Console.WriteLine("Press ESCAPE to quit");
            Console.WriteLine("Up, Down, Left Right, PageUp and PageDn to move the RED Man");
            while (true)
            {
                ConsoleKeyInfo key_pressed = Console.ReadKey();

                if (key_pressed.Key == ConsoleKey.Home)
                {
                    led_cube.set_address(0, 0, 0);
                    led_cube.all_colour(BACKGROUND_COLOUR);
                    led_cube.upload();
                }
                if (key_pressed.Key == ConsoleKey.End)
                {
                    led_cube.set_address(3,3,3);
                    led_cube.all_colour(BACKGROUND_COLOUR);
                    led_cube.upload();
                }
                if (key_pressed.Key == ConsoleKey.UpArrow)
                {
                    if ((key_pressed.Modifiers & ConsoleModifiers.Shift) != 0)
                        led_cube.up(1);
                    else
                        led_cube.back(1);
                    led_cube.all_colour(BACKGROUND_COLOUR);
                    led_cube.upload();
                }
                if (key_pressed.Key == ConsoleKey.DownArrow)
                {
                    if ((key_pressed.Modifiers & ConsoleModifiers.Shift) != 0)
                        led_cube.down(1);
                    else
                        led_cube.forward(1);
                    led_cube.all_colour(BACKGROUND_COLOUR);
                    led_cube.upload();
                }
                if (key_pressed.Key == ConsoleKey.PageDown)
                {
                    led_cube.down(1);
                    led_cube.all_colour(BACKGROUND_COLOUR);
                    led_cube.upload();
                }
                if (key_pressed.Key == ConsoleKey.RightArrow)
                {
                    led_cube.right(1);
                    led_cube.all_colour(BACKGROUND_COLOUR);
                    led_cube.upload();
                }
                if (key_pressed.Key == ConsoleKey.LeftArrow)
                {
                    led_cube.left(1);
                    led_cube.all_colour(BACKGROUND_COLOUR);
                    led_cube.upload();
                }
                if (key_pressed.Key == ConsoleKey.Escape)
                {
                    break;
                }
                if (key_pressed.Key == ConsoleKey.Enter)
                {
                    if (led_cube.hit_target())
                    {
                        score++;
                        synthesizer.SpeakAsync("Player wins. You're score is " + score);
                        Console.WriteLine("You Win");
                        led_cube.all_colour("sky blue");
                        Thread.Sleep(500);
                        led_cube.all_colour(0, 0, 0);
                        led_cube.reset_target_led();
                        led_cube.target_led.set_colour("blue");
                        led_cube.all_colour(BACKGROUND_COLOUR);
                        led_cube.upload();
                    }
                    else
                    {
                        score--;
                        synthesizer.SpeakAsync("Player looses. You're score is " + score);
                        Console.WriteLine("You Loose");
                        led_cube.all_colour(102, 51, 0);
                        Thread.Sleep(500);
                        led_cube.all_colour(0, 0, 0);
                        led_cube.reset_target_led();
                        led_cube.target_led.set_colour("blue");
                        led_cube.all_colour(BACKGROUND_COLOUR);
                        led_cube.upload();
                        // Synchronous
                    }
                    Console.WriteLine("youre score is " + score);
                }
               
            }
            led_cube.all_off();
            led_cube.close();
        }
    }
}