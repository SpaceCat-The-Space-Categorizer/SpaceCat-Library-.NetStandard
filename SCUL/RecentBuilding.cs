using System;
using System.Collections.Generic;
using System.Text;

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

            FilePath = null;
            TimeAccessed = DateTime.Now;
        }
    }
}
