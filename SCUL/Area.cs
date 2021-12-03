using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SpaceCat
{
    public class Area
    {
        //The total number of seats in this area
        public int Capacity { get; set; }
        //The unique ID of the area
        //We need to retrieve this during map generation
        public int AreaID { get; set; }
        //The Name of the Area
        public string AreaName { get; set; }
        // A list of rectangles that define the boundaries of the area.
        public List<Rectangle> DefiningRectangles { get; set; }
        //A string containing the hex value for the area color
        public string Color { get; set; }
        //A list of tags that define what kind of area this is
        public List<String> Tags { get; set; }
        //A list of all furniture inside of an area
        public List<Furniture> ContainedFurniture {get;set;}
        //A string to contain any addititional notes
        public string AdditionalNotes { get; set; }

        public Area()
        {
            Capacity = 0;
            AreaID = -1;
            AreaName = "default";
            DefiningRectangles = new List<Rectangle>();
            Color = "000000";
            Tags = new List<string>();
            ContainedFurniture = new List<Furniture>();
            AdditionalNotes = "";
        }

        public Area(string color)
        {
            Capacity = 0;
            AreaID = -1;
            AreaName = "";
            DefiningRectangles = new List<Rectangle>();
            Color = color;
            Tags = new List<string>();
            ContainedFurniture = new List<Furniture>();
            AdditionalNotes = "";
        }

        [JsonConstructor]
        public Area(int capacity, int areaID, string areaName, List<Rectangle> definingRectangles, string color, List<string> tags, List<Furniture> containedFurniture, string additionalNotes)
        {
            Capacity = capacity;
            AreaID = areaID;
            AreaName = areaName;
            DefiningRectangles = definingRectangles;
            Color = color;
            Tags = tags;
            ContainedFurniture = containedFurniture;
            AdditionalNotes = additionalNotes;
        }

        public void AddFurniture(Furniture furniturePiece)
        {
            Capacity += furniturePiece.Seating;
            ContainedFurniture.Add(furniturePiece);
        }

        public void RemoveFurniture(Furniture furniturePiece)
        {
            Capacity -= furniturePiece.Seating;
            ContainedFurniture.Remove(furniturePiece);
        }

        public void AddAreaRectangle(Rectangle areaRectangle)
        {
            DefiningRectangles.Add(areaRectangle);
        }

        public void RemoveRectangle(Rectangle areaRectangle)
        {
            DefiningRectangles.Remove(areaRectangle);
        }

        public void ClearSurveyData()
        {
            foreach (Furniture furniturePiece in ContainedFurniture)
            {
                furniturePiece.ClearSurveyData();
                AdditionalNotes = null;
            }
        }

        //Returns a struct containing all information needed to add
        //an entry to the Area Survey table of the database
        public AreaSurvey GetSurveyData(int surveyNumber)
        {
            int FilledSeats = 0;
            foreach(Furniture furniturePiece in ContainedFurniture)
            {
                if (furniturePiece.Surveyed)
                {
                    FilledSeats += furniturePiece.OccupiedSeats;
                }
            }

            return new AreaSurvey(AreaID, surveyNumber, FilledSeats, AdditionalNotes);
        }

        public string TagsToString()
        {
            string tagsString = "";
            foreach (string tag in Tags)
            {
                tagsString += tag + " ";
            }
            return tagsString;
        }
    }
}
