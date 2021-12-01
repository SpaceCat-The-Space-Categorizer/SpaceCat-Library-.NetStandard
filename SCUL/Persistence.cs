using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;

namespace SpaceCat
{
    public static class Persistence
    {
        //IF THIS CHANGES WE NEED TO REMEMBER IT CHANGED
        public static string BaseFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SpaceCat");
        public static readonly string BuildingsFolder = Path.Combine(BaseFilePath, "Buildings");
        public static readonly string PersistenceFileLocation = Path.Combine(BaseFilePath, "persistence.json");
        public static readonly string RecentBuildingsFileLocation = Path.Combine(BaseFilePath, "recentbuildings.json");
        public static readonly string DatabaseFileLocation = Path.Combine(BaseFilePath, "database.db");
        
        public static bool ValidateEnvironment(bool repair = true)
        {
            bool isValid = true;
            try
            {
                //Check for BasePath folder
                Console.WriteLine("Checking for SpaceCat folder in AppData");
                if (!Directory.Exists(BaseFilePath))
                {
                    isValid = false;
                    Console.WriteLine("Could not find SpaceCat folder");
                    if (repair)
                    {
                        Console.WriteLine("Creating SpaceCat folder.");
                        Directory.CreateDirectory(BaseFilePath);
                        isValid = true;
                    }
                }
                else { Console.WriteLine("SpaceCat folder found."); }

                //Check for Buildings folder
                Console.WriteLine("Checking for Buildings folder");
                if (!Directory.Exists(BuildingsFolder))
                {
                    isValid = false;
                    Console.WriteLine("Could not find Buildings folder");
                    if (repair)
                    {
                        Console.WriteLine("Creating Buildings folder.");
                        Directory.CreateDirectory(BuildingsFolder);
                        isValid = true;
                    }
                }
                else { Console.WriteLine("Buildings folder found."); }

                //Check for persistence file
                Console.WriteLine("Checking for persistence file.");
                if (!File.Exists(PersistenceFileLocation))
                {
                    Console.WriteLine("Could not find persistence file.");
                    isValid = false;
                    if (repair)
                    {
                        Console.WriteLine("Creating persistence file.");
                        File.Create(PersistenceFileLocation);
                        isValid = true;
                    }
                }
                else { Console.WriteLine("Persistence file found."); }
            }
            catch (Exception e)
            {
                Debug.WriteLine("New exception thrown in ValidateEnvironment() at: " + DateTime.Now);
                Debug.WriteLine(e);
                return false;
            }
            return isValid;
        }

        public static bool SaveRecentBuildings(List<RecentBuilding> recentBuildings)
        {
            try
            {
                //Prepare to write
                string jsonString = JsonSerializer.Serialize(recentBuildings);
                Console.WriteLine("JSON representation: " + jsonString);

                //write to file
                Console.WriteLine("Writing recent buildings to file...");
                StreamWriter streamWriter = new StreamWriter(RecentBuildingsFileLocation);
                streamWriter.WriteLine(jsonString);
                streamWriter.Close();
                Console.WriteLine("Recent Buildings Saved!");
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("New exception thrown in SaveRecentBuildings() at: " + DateTime.Now);
                Debug.WriteLine(e);
                return false;
            }
        }

        public static List<RecentBuilding> LoadRecentBuildings()
        {
            try
            {
                StreamReader streamReader = new StreamReader(RecentBuildingsFileLocation);
                string jsonString = streamReader.ReadLine();
                Console.WriteLine("json loaded: " + jsonString);
                List<RecentBuilding> recentBuildings = JsonSerializer.Deserialize<List<RecentBuilding>>(jsonString);
                Console.WriteLine("Recent buildings loaded.");
                return recentBuildings;
            }
            catch (Exception e)
            {
                Debug.WriteLine("New exception thrown in LoadBuilding() at: " + DateTime.Now);
                Debug.WriteLine(e);
                return null;
            }

        }

        public static bool SaveBuilding(Building buildingToSave, string fileName = null)
        {
            try
            {
                //Determine file location
                string filePath;
                if (fileName == null) { filePath = Path.Combine(BuildingsFolder, buildingToSave.Name + ".json"); }
                else { filePath = Path.Combine(BuildingsFolder, fileName + ".json"); }
                Console.WriteLine("File path set to: " + filePath);

                //Prepare to write
                string jsonString = JsonSerializer.Serialize(buildingToSave);
                Console.WriteLine("JSON representation: " + jsonString);

                //write to file
                Console.WriteLine("Writing JSON to file...");
                StreamWriter streamWriter = new StreamWriter(filePath);
                streamWriter.WriteLine(jsonString);
                streamWriter.Close();
                Console.WriteLine("Building Saved!");
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("New exception thrown in SaveBuilding() at: " + DateTime.Now);
                Debug.WriteLine(e);
                return false;
            }
        }

        //Consider LoadBuildingByName and LoadBuildingByPath, or build that functionality into this 
        public static Building LoadBuilding(string fileName)
        {
            try
            {
                string filePath = Path.Combine(BuildingsFolder, fileName + ".json");
                StreamReader streamReader = new StreamReader(filePath);
                string jsonString = streamReader.ReadLine();
                Console.WriteLine("json loaded: " + jsonString);
                Building retBuilding = JsonSerializer.Deserialize<Building>(jsonString);
                Console.WriteLine("Loaded building: " + retBuilding.Name);
                return retBuilding;
            }
            catch (Exception e)
            {
                Debug.WriteLine("New exception thrown in LoadBuilding() at: " + DateTime.Now);
                Debug.WriteLine(e);
                return null;
            }
        }

    }
}
