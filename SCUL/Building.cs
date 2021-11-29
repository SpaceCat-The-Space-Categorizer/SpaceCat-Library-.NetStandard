using System;
using System.Collections.Generic;

namespace SpaceCat
{
    public class Building
    {
        //The name of the building to be surveyed
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public List<Floor> Floors { get; set; }
        public List<FurnitureBlueprint> FurniturePresets { get; set; }
        public DatabaseFactory DatabaseHandler { get; set; }
        //exists as a cludge for survey numbers to send to DB
        internal int SurveyNumber { get; set; }

        public Building(string name)
        {
            Name = name;
            DateCreated = DateTime.Now;
            Floors = new List<Floor>();
            FurniturePresets = new List<FurnitureBlueprint>();
            DatabaseHandler = new DatabaseFactory(name);
            SurveyNumber = 0;
        }

        public void AddFloor(Floor newFloor)
        {
            Floors.Add(newFloor);
            newFloor.FloorBuilding = this;
        }

        public void RemoveFloor(Floor removedFloor)
        {
            Floors.Remove(removedFloor);
            removedFloor.FloorBuilding = null;
        }

        public void AddFurniturePreset(FurnitureBlueprint newPreset)
        {
            FurniturePresets.Add(newPreset);
        }

        public void RemoveFurniturePreset(FurnitureBlueprint removedPreset)
        {
            FurniturePresets.Remove(removedPreset);
        }

        public void CompleteMap()
        {
            foreach (Floor buildingFloor in Floors)
            {
                foreach (Area floorArea in buildingFloor.Areas)
                {
                    DatabaseHandler.InsertArea(floorArea);
                }
            }

            DateCreated = DateTime.Now;
        }

        //This function sends all areas in the building to the Database
        //Then clears the survey data
        //This should preserve the map for use in future surveys
        public void CompleteSurvey()
        {
            SurveyNumber = DatabaseHandler.GetNewSurveyNumber();
            foreach(Floor buildingFloor in Floors)
            {
                foreach(Area floorArea in buildingFloor.Areas)
                {
                    DatabaseHandler.InsertAreaSurvey(floorArea.GetSurveyData(SurveyNumber));
                    floorArea.ClearSurveyData();
                }
            }
            SurveyNumber++;
        }
    }
}
