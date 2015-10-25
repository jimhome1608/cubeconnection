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
            led_cube.random_colors();
            //led_cube.rain(500);
            Console.ReadKey();
            led_cube.all_off();
            led_cube.close();
        }

        static void play_game()
        {
            int score = 0;

            SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            synthesizer.Volume = 100;  // 0...100
            synthesizer.Rate = -2;     // -10...10

            Console.WriteLine("Connected OK");

            const String BACKGROUND_COLOUR = "BLACK";
            led_cube.game_reset_target_led(64);
            led_cube.all_colour(BACKGROUND_COLOUR);
            led_cube.user_led.set_colour("red");
            led_cube.target_led.set_colour("blue");
            led_cube.target_led.random_address();
            led_cube.game_upload();
            Console.WriteLine("Press ESCAPE to quit");
            Console.WriteLine("Up, Down, Left Right, PageUp and PageDn to move the RED Man");
            while (true)
            {
                ConsoleKeyInfo key_pressed = Console.ReadKey();

                if (key_pressed.Key == ConsoleKey.Home)
                {
                    led_cube.user_led.set_address(0, 0, 0);
                    led_cube.all_colour(BACKGROUND_COLOUR);
                    led_cube.game_upload();
                }
                if (key_pressed.Key == ConsoleKey.End)
                {
                    led_cube.user_led.set_address(3, 3, 3);
                    led_cube.all_colour(BACKGROUND_COLOUR);
                    led_cube.game_upload();
                }
                if (key_pressed.Key == ConsoleKey.UpArrow)
                {
                    if ((key_pressed.Modifiers & ConsoleModifiers.Shift) != 0)
                        led_cube.user_led.up(1);
                    else
                        led_cube.user_led.back(1);
                    led_cube.all_colour(BACKGROUND_COLOUR);
                    led_cube.game_upload();
                }
                if (key_pressed.Key == ConsoleKey.DownArrow)
                {
                    if ((key_pressed.Modifiers & ConsoleModifiers.Shift) != 0)
                        led_cube.user_led.down(1);
                    else
                        led_cube.user_led.forward(1);
                    led_cube.all_colour(BACKGROUND_COLOUR);
                    led_cube.game_upload();
                }
                if (key_pressed.Key == ConsoleKey.PageDown)
                {
                    led_cube.user_led.down(1);
                    led_cube.all_colour(BACKGROUND_COLOUR);
                    led_cube.game_upload();
                }
                if (key_pressed.Key == ConsoleKey.RightArrow)
                {
                    led_cube.user_led.right(1);
                    led_cube.all_colour(BACKGROUND_COLOUR);
                    led_cube.game_upload();
                }
                if (key_pressed.Key == ConsoleKey.LeftArrow)
                {
                    led_cube.user_led.left(1);
                    led_cube.all_colour(BACKGROUND_COLOUR);
                    led_cube.game_upload();
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
                        led_cube.game_reset_target_led(64);
                        led_cube.target_led.set_colour("blue");
                        led_cube.all_colour(BACKGROUND_COLOUR);
                        led_cube.game_upload();
                    }
                    else
                    {
                        // set rangdom colors when loose.
                        score--;
                        synthesizer.SpeakAsync("Player looses. You're score is " + score);
                        Console.WriteLine("You Loose");
                        led_cube.all_colour(102, 51, 0);
                        Thread.Sleep(500);
                        led_cube.all_colour(0, 0, 0);
                        led_cube.game_reset_target_led(64);
                        led_cube.target_led.set_colour("blue");
                        led_cube.all_colour(BACKGROUND_COLOUR);
                        led_cube.game_upload();
                        // Synchronous
                    }
                    Console.WriteLine("youre score is " + score);
                }

            }

        }


       
    }
}