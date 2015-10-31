﻿using System;
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
        public LedList leds = new LedList();

        private Random rnd = new Random();


        public LedCube()
        {
            all_leds = new Led();
            all_leds.x = ALL_LEDS_X;
        }

        public void add_moisture()
        {
            
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    Led l = leds.led_by_address(x, y, 3);
                    Led l2 = leds.led_by_address(x+1, y, 3);
                    if (l2 == null)
                        l2 = leds.led_by_address(x - 1, y, 3);
                    if (l2.blue == 0)
                    {
                        l2.blue = 10;
                        l2.color_has_changed = true;
                    }
                        
                    l.blue = l.blue + rnd.Next(0, l2.blue);
                    if (l.blue > 255)
                        l.blue = 255;
                    l.color_has_changed = true;
                }
            }
            push_to_hardware(false);
            
        }

        public void moisture_falls_at_value(int _falling_value)
        {
            // search on the ground and make dissapear
            for (int x = 0; x < 4; x++)
            {
                for (int y=0;y<4;y++)
                {
                    Led l = leds.led_by_address(x, y, 0);
                    if (l.blue >= _falling_value)
                        l.turn_off();
                }
            }
            for (int z=1; z < 4; z++) { 
                for (int x = 0; x < 4; x++)
                {
                    for (int y = 0; y < 4; y++)
                    {
                        Led l = leds.led_by_address(x, y, z);
                        if (l.blue >= _falling_value)
                        {
                            Led l2 = leds.led_by_address(x, y, z-1);
                            l2.assign_color(l);
                            l.turn_off();
                        }                        
                    }
                }
            }
            push_to_hardware(false);

        }

        public void random_colors()
        {
            for (int l=0; l< 64; l++)
            {
                leds.leds[l].set_random_color();
                push_to_hardware(false);
            }
            

        }

        public Boolean x_line(int y, int z)
        {
            leds.x_line(y, z);
            push_to_hardware(false);
            return true;
        }

        public void clear()
        {
            leds.clear();
        }


        public void push_to_hardware(Boolean force_all)
        {
            String _cmd = "";
            Led _led;
            all_leds.set_colour("black");
            serial_port.Write(all_leds.cmd());
            for (int x = 0; x < leds.leds.Count; x++)
            {
                if (force_all)
                {
                    _led = leds.leds[x];
                    _cmd = _cmd + _led.cmd();
                }
                else
                {
                    _led = leds.leds[x];
                    if (_led.color_has_changed)
                      _cmd = _cmd + _led.cmd();
                }

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
