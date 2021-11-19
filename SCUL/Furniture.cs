using System;
using System.Collections.Generic;

namespace SpaceCat
{
    public class Furniture : FurnitureBlueprint
    {
        //DISPLAY VARIABLES
        //Stores the variables relevant to displaying the specific instance of the object

        //Stores the point at the top left corner of the image relative to the location on the floor
        public Tuple<double, double> Corner { get; set; }
        //Stores the float that multiplies the width of the image when displayed
        private double _StretchX;
        public double StretchX { 
            get { return _StretchX; }
            set 
            {
                if (value == 0) throw new Exception("StretchX can't be 0");
                else _StretchX = value;
            }
        }
        //Stores the float that multiplies the height of the image when displayed
        private double _StretchY;
        public double StretchY
        {
            get { return _StretchY; }
            set
            {
                if (value == 0) throw new Exception("StretchY can't be 0");
                else _StretchY = value;
            }
        }

        //SURVEY VARIABLES:
        //These variables are changed during the survey process

        //Tracks the number of seats in use during this survey
        public int OccupiedSeats { get; set; }
        //Tracks if the tally of occupied seats is complete
        public bool Surveyed { get; set; }

        //Constructor
        public Furniture(string filepath, int seating) : base (filepath, seating)
        {
            //Display
            this.Filepath = filepath;
            this.Corner = new Tuple<double, double>(0, 0);
            this.StretchX = 1;
            this.StretchY = 1;
            //Survey
            this.Seating = seating;
            this.OccupiedSeats = 0;
            this.Surveyed = false;
        }

        public void ClearSurveyData()
        {
            OccupiedSeats = 0;
            Surveyed = false;
        }
    }
}
