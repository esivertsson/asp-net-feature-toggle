using System;
using System.Collections.Generic;
using System.IO;

namespace AspNetFeatureToggle
{
    public class FileUserListReader : IUserListReader
    {
        /// <summary>
        /// Gets a list of user name from the file specified in FeatureToggle configuration.
        /// </summary>
        /// <param name="userListSource">The user list file name</param>
        /// <exception cref="System.ArgumentException">When user list file is not found</exception>
        /// <returns>A list of user names</returns>
        public IEnumerable<string> GetUserNamesFromList(string userListSource)
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

            // Normalize line endings: http://stackoverflow.com/questions/841396/what-is-a-quick-way-to-force-crlf-in-c-sharp-net
            string formattedFileContent = fileContent.Replace("\r\n", "\n").Replace("\r", "\n");

            var validUserNames = new List<string>();
            foreach (string entry in formattedFileContent.Split('\n'))
            {
                if (!string.IsNullOrEmpty(entry))
                {
                    validUserNames.Add(entry);
                }
            }

            return validUserNames;
        }
    }
}
