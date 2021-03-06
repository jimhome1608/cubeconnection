﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CubeConnection
{
    public class LedList
    {

        public List<Led> leds = new List<Led>();
        public Led current_led;
        public ColourRGB current_color;


        public LedList() // constructor for LedCube Object
        {
            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    for (int z = 0; z < 4; z++)
                    {
                        Led led = new Led();
                        led.set_address(x, y, z);
                        leds.Add(led);
                    }
                }
            }
            current_led = led_by_address(0, 0, 0);
            current_color = new ColourRGB();
        }

        public void clear()
        {
            for (int l = 0; l <64; l++)
            {
                leds[l].turn_off();
            }
            current_color.set_colour("black");
        }

        public Boolean make_floor(int z)
        {
            if (move_to(0, 0, z) == null)
                return false;
            current_led.assign_color(current_color);
            while (move_right(1))
                current_led.assign_color(current_color);
            move_back(1);
            current_led.assign_color(current_color);
            while (move_left(1))
                current_led.assign_color(current_color);
            move_back(1);
            current_led.assign_color(current_color);
            while (move_right(1))
                current_led.assign_color(current_color);
            move_back(1);
            current_led.assign_color(current_color);
            while (move_left(1))
                current_led.assign_color(current_color);
            return true;
        }

        public Boolean make_wall_x(int x)
        {
            if (move_to(x, 0, 0) == null)
                return false;
            current_led.assign_color(current_color);
            while (move_up(1))
                current_led.assign_color(current_color);
            move_back(1);
            current_led.assign_color(current_color);
            while (move_down(1))
                current_led.assign_color(current_color);;
            move_back(1);
            current_led.assign_color(current_color);
            while (move_up(1))
                current_led.assign_color(current_color);;
            move_back(1);
            current_led.assign_color(current_color);;
            while (move_down(1))
                current_led.assign_color(current_color);;
            return true;
        }

        public Boolean make_wall_y(int y)
        {
            if (move_to(0, y, 0) == null)
                return false;
             current_led.assign_color(current_color);;
            while (move_right(1))
                current_led.assign_color(current_color);;
            move_up(1);
            current_led.assign_color(current_color);;
            while (move_left(1))
                current_led.assign_color(current_color);;
            move_up(1);
            current_led.assign_color(current_color);;
            while (move_right(1))
                current_led.assign_color(current_color);;
            move_up(1);
            current_led.assign_color(current_color);;
            while (move_left(1))
                current_led.assign_color(current_color);;
            return true;
        }





        public Boolean x_line(int y, int z)
        {
            if (move_to(0, y, z) == null)
                return false;
            current_led.assign_color(current_color);;
            while (move_right(1))
                current_led.assign_color(current_color);;
            return true;

        }

        public bool move_to(int x, int y, int z)
        {
            if (led_by_address(x, y, z) == null)
                return false;
            current_led = led_by_address(x, y, z);
            return true;
        }

        public bool move_up(int jump_size)
        {
            int z = current_led.z + jump_size;
            if (z > 3)
                return false;
            if (z < 0)
                return false;
            current_led = led_by_address(current_led.x, current_led.y, z);
            return true;
        }

        public bool move_down(int jump_size)
        {
            return move_up(0-jump_size);
        }

        public bool move_right(int jump_size)
        {
            int x = current_led.x + jump_size;
            if (x > 3)
                return false;
            if (x < 0)
                return false;
            current_led = led_by_address(x, current_led.y, current_led.z);
            return true;
        }

        public bool move_left(int jump_size)
        {

            return move_right(0 - jump_size);
        }

        public bool move_back(int jump_size)
        {
            int y = current_led.y + jump_size;
            if (y > 3)
                return false;
            if (y < 0)
                return false;
            current_led = led_by_address(current_led.x, y, current_led.z);
            return true;
        }

        public bool move_forward(int jump_size)
        {
            return move_back(0 - jump_size);
        }

        public Led led_by_address(int x, int y, int z)
        {
            for (int l = 0; l<64; l++)
            {
                if (leds[l].x != x)
                    continue;
                if (leds[l].y != y)
                    continue;
                if (leds[l].z != z)
                    continue;
                return leds[l];
            }
            return null;
        }

    }
}
