using System.Collections.Generic;

namespace AspNetFeatureToggle
{
    public interface IUserListReader
    {
        List<string> GetUserNamesFromList(string userListSource);
    }
}
