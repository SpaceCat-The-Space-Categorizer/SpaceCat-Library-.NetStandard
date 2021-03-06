using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SpaceCat
{
    public class Rectangle
    {
        //TODO: Ensure that TopLeft and BottomRight are actually the top left and bottom right of a rectangle.
        public Tuple<double, double> TopLeft { get; set; }
        public Tuple<double, double> BottomRight { get; set; }

        public Rectangle(double first_x, double first_y, double second_x, double second_y)
        {
            double top_left_x = 0, top_left_y = 0, bottom_right_x = 0, bottom_right_y = 0;
            if (first_x > second_x)
            {
                top_left_x = second_x;
                bottom_right_x = first_x;
            }
            else
            {
                top_left_x = first_x;
                bottom_right_x = second_x;
            }

            if(first_y > second_y)
            {
                top_left_y = first_y;
                bottom_right_y = second_y;
            }
            else
            {
                top_left_y = second_y;
                bottom_right_y = first_y;
            }

            TopLeft = new Tuple<double, double>(top_left_x, top_left_y);
            BottomRight = new Tuple<double, double>(bottom_right_x, bottom_right_y);
        }

        [JsonConstructor]
        public Rectangle(Tuple<double, double> topLeft, Tuple<double, double> bottomRight)
        {
            TopLeft = topLeft;
            BottomRight = bottomRight;
        }
    }
}
