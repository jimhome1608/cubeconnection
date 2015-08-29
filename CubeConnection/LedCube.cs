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

        public void upload ()
        {
            serial_port.Write(cmd());
        }

        public void all_off()
        {
            x = 4;
            set_colour("black");
            upload();
        }

        public void all_colour(int r,int g,int b)
        {
            x = 4;
            set_colour(r,g,b);
            upload();
        }

        public void all_colour(String color_name)
        {
            x = 4;
            set_colour(color_name);
            upload();

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
