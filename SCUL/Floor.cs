using System;
using System.Collections.Generic;

namespace SpaceCat
{
    public class Floor
    {
        public int FloorNumber {get; set;}
        public string FloorName { get; set; }
        public string FloorBlueprintFilepath {get; set;}
        public List<Area> Areas {get; set; }

        public Floor(int floorNumber, Building floorBuilding = null, string floorBlueprintFilepath = null, List<Area> areas = null)
        {
            FloorNumber = floorNumber;
            FloorBlueprintFilepath = floorBlueprintFilepath;
            if (areas != null) Areas = areas;
            else Areas = new List<Area>();
        }

        public void AddArea(Area area)
        {
            Areas.Add(area);
        }

        public void RemoveArea(Area area)
        {
            Areas.Remove(area);
        }
    }
}
