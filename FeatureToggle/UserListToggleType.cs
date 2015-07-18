
using System.Collections.Generic;

namespace AspNetFeatureToggle
{
    public class UserListToggleType : BasicToggleType
    {
        public IEnumerable<string> UserNamesList { get; set; } 
    }
}
