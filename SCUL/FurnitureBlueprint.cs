using System;

namespace SpaceCat
{
    public class FurnitureBlueprint
    {
        //This class is used in the front end to store
        //known types of furniture, and to create new instances
        //of furniture on the map


        //DISPLAY VARIABLES:
        //These variables affect the way this type of furniture would be displayed

        //Stores the filepath of the image of this piece of furniture
        public string Filepath { get; set; }

        //SURVEY VARIABLES
        //Stores the default settings of this type of furniture

        //The number of seats this piece of furniture has by default
        public int Seating
        {
            get
            {
                return Seating;
            }
            set
            {
                if (value <= 0) throw new Exception("Invalid Seating Argument");
                else Seating = value;
            }
        }

        //Constructor
        public FurnitureBlueprint(string filepath, int seating)
        {
            this.Filepath = filepath;
            this.Seating = seating;
        }

        //Call this function to return a new instance of this type of furniture
        public Furniture NewInstance()
        {
            return new Furniture(this.Filepath, this.Seating);
        }

    }
}