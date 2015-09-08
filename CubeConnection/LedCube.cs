using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;


namespace CubeConnection
{
    public class LedCube : Led
    {
        SerialPort serial_port;
        Led all_leds;
        public Led target_led;

        public LedCube() // constructor for LedCube Object
        {
            all_leds = new Led();
            all_leds.x = 4;
            target_led = new Led();
            target_led.set_address(2, 2, 2);
            target_led.set_colour("blue");

        }

        public void reset_target_led(int how_many_steps)
        {
            // no backtracking.  If go forward next move not back, etc.
            // 
            const int UP =      0;
            const int DOWN =    1;
            const int LEFT =    2;
            const int RIGHT =   3;
            const int FORWARD = 4;
            const int BACK =    5;
            int idx = 0;
            Random rnd = new Random();
            target_led.set_colour("blue");
            int previous_move = 0;
            int move = rnd.Next(0, 6);
            while (idx < how_many_steps)
            {
                idx++;
                if (idx % 2 == 0)
                {
                    move = rnd.Next(0, 6);
                }
                if (move == UP)
                {
                    if (!target_led.up(1))
                    {
                        move = rnd.Next(0, 6);
                        continue;
                    }
                    if (previous_move  == DOWN)
                    {
                        move = rnd.Next(0, 6);
                        continue;
                    }

                }
                if (move == DOWN)
                {
                    if (!target_led.down(1))
                    {
                        move = rnd.Next(0, 6);
                        continue;
                    }
                    if (previous_move == UP)
                    {
                        move = rnd.Next(0, 6);
                        continue;
                    }
                }
                if (move == LEFT)
                {
                    if (!target_led.left(1))
                    {
                        move = rnd.Next(0, 6);
                        continue;
                    }
                    if (previous_move == RIGHT)
                    {
                        move = rnd.Next(0, 6);
                        continue;
                    }
                }
                if (move == RIGHT)
                {
                    if (!target_led.right(1))
                    {
                        move = rnd.Next(0, 6);
                        continue;
                    }
                    if (previous_move == LEFT)
                    {
                        move = rnd.Next(0, 6);
                        continue;
                    }
                }
                if (move == FORWARD)
                {
                    if (!target_led.forward(1))
                    {
                        move = rnd.Next(0, 6);
                        continue;
                    }
                    if (previous_move == BACK)
                    {
                        move = rnd.Next(0, 6);
                        continue;
                    }
                }
                if (move == BACK)
                {
                    if (!target_led.back(1))
                    {
                        move = rnd.Next(0, 6);
                        continue;
                    }
                    if (previous_move == FORWARD)
                    {
                        move = rnd.Next(0, 6);
                        continue;
                    }
                }
                all_off();
                serial_port.Write(target_led.cmd());
                previous_move = move;
                Thread.Sleep(100);
            }
        }

        public Boolean hit_target()
        {
            if (x == target_led.x & y == target_led.y & z == target_led.z)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void upload ()
        {
            serial_port.Write(cmd());
            if (target_led.has_colour())
            {
                 serial_port.Write(target_led.cmd());
                Thread.Sleep(1000);
                target_led.set_colour("black");
                serial_port.Write(target_led.cmd());
            }
           


        }

        public void all_off()
        {
            all_leds.set_colour("black");
            serial_port.Write(all_leds.cmd());
        }

        public void all_colour(int r,int g,int b)
        {
            all_leds.set_colour(r,g,b);
            serial_port.Write(all_leds.cmd());
        }

        public void all_colour(String color_name)
        {
            all_leds.set_colour(color_name);
            serial_port.Write(all_leds.cmd());

        }

       

        public void close()
        {
            serial_port.Close();
            serial_port = null;
        }

        public bool open()
        {
            serial_port = new SerialPort();
            //good to enahce so searches for Cube COM3, COM4, etc, etc.
            serial_port.PortName = "COM3";
            serial_port.BaudRate = 38400;
            serial_port.RtsEnable = true;
            serial_port.ReadTimeout = 500;
            bool temp_result = false;
            for (int comport_number = 3; comport_number < 10; comport_number++)
            {
                serial_port.Close();
                serial_port.PortName = "COM" + comport_number.ToString();
                try
                {
                    Console.WriteLine("==================================================");
                    Console.WriteLine("Try connect Cube on COM" + comport_number.ToString());
                    serial_port.Open();
                    Console.WriteLine("Open Port OK: waiting for  {whoareyou} responce ");
                    serial_port.Write("{whoareyou}");
                    // serial_port.Write("{000FF00FF}");
                    Thread.Sleep(250); // have to wait for responce from cube
                    String reply_from_cube = serial_port.ReadExisting();
                    Console.WriteLine("reply from device");
                    Console.Write(reply_from_cube);
                    if (reply_from_cube.IndexOf("cubeofleds") < 0)
                    {
                        Console.WriteLine("this is not the cube");
                    }
                    else
                    {
                        Console.WriteLine("Cube Found: OK");
                        temp_result = true;
                        break;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Connection: FAILED");
                    Console.WriteLine(ex.Message);
                }

            }
            Console.WriteLine("==================================================");
            return temp_result;
        }

    }
}
