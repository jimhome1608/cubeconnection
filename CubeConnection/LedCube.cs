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
                Thread.Sleep(100);
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
        public int up (int jump_size)
        {
            
            z = z + jump_size;
            if (z > 3)
            {
                z = 3;
            }
            return z;
        }
        public int down(int jump_size)
        {

            z = z - jump_size;
            if (z < 0)
            {
                z = 0;
            }
            return z;
        }
        public int right(int jump_size)
        {

            x = x + jump_size;
            if (x > 3)
            {
                x = 3;
            }
            return x;
        }
        public int left(int jump_size)
        {

            x = x - jump_size;
            if (x < 0)
            {
                x = 0;
            }
            return x;
        }
        public int back(int jump_size)
        {

            y = y + jump_size;
            if (y > 3)
            {
                y = 3;
            }
            return y;
        }
        public int forward(int jump_size)
        {

            y = y - jump_size;
            if (y < 0)
            {
                y = 0;
            }
            return y;
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
            serial_port.PortName = "COM7";
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
