using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;

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
        public int SurveyNumber { get; set; }
        public string AdditionalNotes { get; set; }

        public Building(string name)
        {
            Name = name;
            DateCreated = DateTime.Now;
            Floors = new List<Floor>();
            FurniturePresets = new List<FurnitureBlueprint>();
            DatabaseHandler = new DatabaseFactory(name);
            SurveyNumber = 0;
        }

        [JsonConstructor]
        public Building(string name, DateTime dateCreated, List<Floor> floors, List<FurnitureBlueprint> furniturePresets, DatabaseFactory databaseHandler, int surveyNumber)
        {
            Name = name;
            DateCreated = dateCreated;
            Floors = floors;
            FurniturePresets = furniturePresets;
            DatabaseHandler = databaseHandler;
            SurveyNumber = surveyNumber;
        }

        public void AddFloor(Floor newFloor)
        {
            Floors.Add(newFloor);
        }

        public void RemoveFloor(Floor removedFloor)
        {
            Floors.Remove(removedFloor);
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
                    if (floorArea.AreaID == -1)
                    {
                        floorArea.AreaID = DatabaseHandler.GetNewAreaID();
                        DatabaseHandler.InsertArea(floorArea, buildingFloor, this);
                    }
                    else
                    {
                        DatabaseHandler.ModifyArea(floorArea, buildingFloor, this);
                    }
                }
            }

            DateCreated = DateTime.Now;
        }

        //This function sends all areas in the building to the Database
        //Then clears the survey data
        //This should preserve the map for use in future surveys
        public void CompleteSurvey()
        {
            try
            {
                SurveyNumber = DatabaseHandler.GetNewSurveyNumber();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Constructed file path is: " + DatabaseHandler.GetConstructedFilePath());
                Debug.WriteLine("Error is occurring in GetNewSurveyNumber.");
                Debug.WriteLine(e);
            }
            foreach(Floor buildingFloor in Floors)
            {
                foreach(Area floorArea in buildingFloor.Areas)
                {
                    try
                    {
                        DatabaseHandler.InsertAreaSurvey(floorArea.GetSurveyData(SurveyNumber));
                    }
                    catch(Exception e)
                    {
                        Debug.WriteLine("Constructed file path is: " + DatabaseHandler.GetConstructedFilePath());
                        Debug.WriteLine("Error is occurring in InsertAreaSurvey.");
                        Debug.WriteLine(e);
                    }
                    floorArea.ClearSurveyData();
                }
            }
        }
    }
}
