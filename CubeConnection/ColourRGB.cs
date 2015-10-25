using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeConnection
{
    public class ColourRGB
    {

       public int red = 0;
       public int green = 0;
       public int blue = 0;

        public void assign_color(ColourRGB _rgb)
        {
            red = _rgb.red;
            green = _rgb.green;
            blue = _rgb.blue;
        }

        public void set_random_color()
        {
            Random rnd = new Random();
            red =   rnd.Next(0, 255); 
            green = rnd.Next(0, 255);
            blue = rnd.Next(0, 255);
        }

        public Boolean has_color()
        {
            if ( red != 0) return true;
            if (green != 0) return true;
            if (blue != 0) return true;
            return false;
        }

        public override string ToString()
        {
            return "red:" + red.ToString() + " green:" + green.ToString() + "  blue:" + blue.ToString();
        }

        public bool has_colour()
        {
            if (red != 0)
            {
                return true;
            }
            if (green != 0)
            {
                return true;
            }
            if (blue != 0)
            {
                return true;
            }
            return false;
        }


        public bool add_colour(int r, int g, int b)
        {
            red = red + r;
            green = green + g;
            blue = blue + b;

            bool temp_result = true;

            if (red > 255)
                temp_result = false;

            if (green > 255)
                temp_result = false;

            if (blue > 255)
                temp_result = false;

            if (red < 0)
                temp_result = false;

            if (green < 0)
                temp_result = false;

            if (blue < 0)
                temp_result = false;

            if (blue > 255)
                blue = 255;
            if (blue < 0)
                blue = 0;

            if (green > 255)
                green = 255;
            if (green < 0)
                green = 0;

            if (red > 255)
                red = 255;
            if (red < 0)
                red = 0;

            return temp_result;
        }




        public void set_colour(int r, int g, int b)
        {
            red = r;
            green = g;
            blue = b;
        }


        public  void set_colour(string colour_name)
        {
            red = 0;
            green = 0;
            blue = 0;
            if (colour_name.ToLower() == "black")
                return;
            if (colour_name.ToLower() == "blue")
                blue = 255;
            if (colour_name.ToLower() == "red")
                red = 255;
            if (colour_name.ToLower() == "green")
                green = 255;
            if (colour_name.ToLower() == "purple")
            {
                red = 255;
                blue = 255;
            }
            if (colour_name.ToLower() == "sky blue")
            {
                red = 135;
                green = 206;
                blue = 255;
            }
           
        }


       
        public void inverse()
        {
            red = 255 - red;
            green = 255 - green;
            blue = 255 - blue;
        }



    }
}
