using System.Collections.Generic;
using AspNetFeatureToggle;

namespace Tests.Mocks
{
    public class UserListReaderMock : IUserListReader
    {
        public List<string> GetUserNamesFromList(string userListSource)
        {
            if (userListSource == null)
            {
                userListSource = string.Empty;
            }

            return new List<string>(userListSource.Split(';'));
        }
    }
}
