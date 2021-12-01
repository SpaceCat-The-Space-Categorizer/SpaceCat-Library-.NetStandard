using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;

namespace SpaceCat
{
    public struct RecentBuilding
    {
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }

        public string FilePath { get; set; }
        public DateTime TimeAccessed { get; set; }

        public RecentBuilding(Building accessedBuilding)
        {
            Name = accessedBuilding.Name;
            DateCreated = accessedBuilding.DateCreated;

            FilePath = Path.Combine(Persistence.BuildingsFolder, accessedBuilding.Name + ".json") ;
            TimeAccessed = DateTime.Now;
        }

        [JsonConstructor]
        public RecentBuilding(string name, DateTime dateCreated, string filePath, DateTime timeAccessed)
        {
            Name = name;
            DateCreated = dateCreated;
            FilePath = filePath;
            TimeAccessed = timeAccessed;
        }
    }
}
