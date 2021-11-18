using System;
using System.Collections.Generic;

namespace SpaceCat
{
    public class Floor
    {
        public Building floorBuilding { get; set; }
        public int FloorNumber {get; set;}
        public string FloorBlueprintFilepath {get; set;}
        public List<Area> Areas {get; set; }

        //Should work, but I am sus - rh 11/5/21
        public Floor(int floorNumber, string floorBlueprintFilepath = null, List<Area> areas = null)
        {
            FloorNumber = floorNumber;
            FloorBlueprintFilepath = floorBlueprintFilepath;
            if (!(areas == null)) Areas = areas;
            else Areas = new List<Area>();
        }

        public void AddArea(Area area)
        {
            Areas.Add(area);
            area.AreaFloor = this;
        }

        public void RemoveArea(Area area)
        {
            Areas.Remove(area);
            area.AreaFloor = null;
        }
    }
}
