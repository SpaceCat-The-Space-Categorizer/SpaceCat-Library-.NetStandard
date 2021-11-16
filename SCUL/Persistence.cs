using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace SpaceCat
{
    public static class Persistence
    {
        public static string BaseFilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static string PERSISTENCE = "persistence.json";

        public static string BuildPath(string fileName, string folderName = null)
        {
            if (folderName == null) return Path.Combine(BaseFilePath, fileName);
            else return Path.Combine(BaseFilePath, folderName, fileName);
        }
        public static void SaveRecentBuildings(List<RecentBuilding> recentBuildings)
        {
            try
            {
                using (FileStream fs = File.OpenWrite(BuildPath(PERSISTENCE)))
                {
                    byte[] byteArray = JsonSerializer.SerializeToUtf8Bytes(recentBuildings);
                    fs.Write(byteArray, 0, byteArray.Length);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return;
            }
        }

        public static List<RecentBuilding> LoadRecentBuildings()
        {
            try
            {
                using (FileStream fs = File.OpenRead(BuildPath(PERSISTENCE)))
                {
                    byte[] byteArray = new byte[2048];
                    int bytesRead = fs.Read(byteArray, 0, byteArray.Length);
                    string jsonString = Encoding.UTF8.GetString(byteArray);
                    return JsonSerializer.Deserialize<List<RecentBuilding>>(jsonString);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<RecentBuilding>();
            }
        }

    }
}
