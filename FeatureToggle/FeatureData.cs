using System.Collections.Generic;

namespace AspNetFeatureToggle
{
    public class FeatureData
    {
        public string Name { get; set; }

        public bool Enabled { get; set; }

        public List<string> UserNamesList { get; set; } 
    }
}
