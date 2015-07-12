using System.Collections.Generic;

namespace AspNetFeatureToggle
{
    public interface IUserListReader
    {
        IEnumerable<string> GetUserNamesFromList(string userListSource);
    }
}
