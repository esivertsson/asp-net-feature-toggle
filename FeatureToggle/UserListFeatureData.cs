
using System.Collections.Generic;

namespace AspNetFeatureToggle
{
    public class UserListFeatureData : FeatureDataBase
    {
        public IEnumerable<string> UserNamesList { get; set; } 
    }
}
