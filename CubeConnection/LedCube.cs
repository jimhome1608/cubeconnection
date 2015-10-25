using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;


namespace CubeConnection
{
    public class LedCube
    {
        private const int ALL_LEDS_X = 4;

        SerialPort serial_port;
        Led all_leds;
        LedList leds;


       public LedCube()
        {
            leds = new LedList();
            all_leds = new Led();
            all_leds.x = ALL_LEDS_X;
        }


        public void random_colors()
        {
            Led _led;
            Random rnd = new Random();
            for (int x=0; x< 4; x++)
            {
                _led = leds.led_by_address(x, 0, 0);
                _led.blue =255;
                push_to_hardware();
                Thread.Sleep(1000);
                clear();
            }
            
        }

        public Boolean x_line(int y, int z)
        {
            leds.x_line(y, z);
            push_to_hardware();
            return true;
        }

        public void clear()
        {
            leds.clear();
        }


        public void push_to_hardware()
        {
            String _cmd = "";
            Led _led;
            all_leds.set_colour("black");
            serial_port.Write(all_leds.cmd());
            for (int x = 0; x < leds.leds.Count; x++)
            {
                _led = leds.leds[x];
                _cmd = _cmd + _led.cmd();
            }
            serial_port.Write(_cmd);

        }

        public void all_off()
        {
            all_leds.set_colour("black");
            //Console.WriteLine(all_leds.cmd());
            //Console.ReadKey();
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
