using System;
using System.Collections.Generic;
using System.IO;

namespace AspNetFeatureToggle
{
    public class FileUserListReader : IUserListReader
    {
        public List<string> GetUserNamesFromList(string userListSource)
        {
            if (string.IsNullOrEmpty(userListSource))
            {
                return new List<string>();                
            }

            if (!File.Exists(userListSource))
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                throw new ArgumentException(string.Format("User list file {0} could not be found in directory {1}", userListSource, currentDirectory));
            }

            string fileContent = File.ReadAllText(userListSource);

            if (string.IsNullOrEmpty(fileContent))
            {
                return new List<string>();                                
            }

            string formattedFileContent = fileContent.Replace("\r\n", "\n").Replace("\r", "\n");

            return new List<string>(formattedFileContent.Split('\n'));
        }
    }
}
