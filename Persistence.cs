using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpaceCat
{
    public static class Persistence
    {
        //Stores the base directory
        //Essentially just a pseudonym for the full base directory call
        //I think because this is a static class this won't break filepaths if we move the folder
        public static string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        //Currently Unused
        public enum PathType
        {
            Absolute,
            Relative
        }

        public static string PathBuilder(string filename)
        {
            return Path.Combine(BaseDirectory, filename);
        }

        //Takeas a filepath and a building and writes that building to a json file
        public static void SaveBuilding(string filepath, Building buildingToSave)
        {
            FileStream fs;
            //Checks to see if this file exists. If it does, save overwrites it
            if (File.Exists(filepath)) fs = new FileStream(filepath, FileMode.Truncate);
            //If the file does not exist, we make a new building file
            else fs = new FileStream(filepath, FileMode.CreateNew);

            string jsonString = JsonSerializer.Serialize<Building>(buildingToSave);
            byte[] byteArray = Encoding.UTF8.GetBytes(jsonString);

            fs.Write(byteArray, 0, byteArray.Length);
            fs.Close();
        }

        public static Building LoadBuilding(string filepath)
        {
            FileStream fs;
            if (!File.Exists(filepath)) throw new Exception("File not found");
            else
            {
                fs = new FileStream(filepath, FileMode.Open);
            }

            StreamReader inStream = new StreamReader(fs);
            string jsonString = inStream.ReadToEnd();

            return JsonSerializer.Deserialize<Building>(jsonString);
        }
    }
}
