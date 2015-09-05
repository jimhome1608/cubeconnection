using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;


namespace CubeConnection
{
    class LedCube : Led
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

        public void reset_target_led()
        {
            int idx = 0;
            Random rnd = new Random();
            target_led.set_colour("blue");
            int d = rnd.Next(0, 6);
            while (idx < 20)
            {
                idx++;
                if (idx % 2 == 0)
                {
                    d = rnd.Next(0, 6);
                }
                if (d == 0)
                {
                    target_led.up(1);
                }
                if (d == 1)
                {
                    target_led.down(1);
                }
                if (d == 2)
                {
                    target_led.left(1);
                }
                if (d == 3)
                {
                    target_led.right(1);
                }
                if (d == 4)
                {
                    target_led.forward(1);
                }
                if (d == 5)
                {
                    target_led.back(1);
                }
                all_off();
                serial_port.Write(target_led.cmd());
                Thread.Sleep(50);
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
            try
            {
                serial_port.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection: FAILED");
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                return false;
            }
            return true;
        }

    }
}
