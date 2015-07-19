using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetFeatureToggle
{
    public class UserListToggleType : BasicToggleType
    {
        public IEnumerable<string> UserNamesList { get; set; }

        public override bool IsEnabled(RequestData requestData)
        {
            return base.IsEnabled(requestData) && UserIsInFeatureList(requestData.UserName);
        }

        private bool UserIsInFeatureList(string userName)
        {
            return this.UserNamesList.Any(u => String.Equals(u, userName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
