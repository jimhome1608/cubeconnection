using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace CubeConnection
{
    class LedCube : Led
    {
        SerialPort serial_port;
        Led all_leds;

        public LedCube() // constructor for LedCube Object
        {
            all_leds = new Led();
            all_leds.x = 4;
        }

        public void upload ()
        {
            serial_port.Write(cmd());
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
            x = 4;
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
