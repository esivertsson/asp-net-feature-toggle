using System.Collections.Generic;
using System.Linq;
using AspNetFeatureToggle.Configuration;

namespace AspNetFeatureToggle
{
    public class FeatureToggle
    {
        private static List<FeatureData> FeatureToggles { get; set; }

        public static void Initialize()
        {
            Initialize(FeatureToggleSection.Config.FeatureList, new FileUserListReader());
        }

        public static void Initialize(FeatureCollection featureList, IUserListReader userListReader)
        {
            FeatureToggles = new List<FeatureData>();
            foreach (FeatureElement feature in featureList)
            {
                // The toggle is off by default
                bool enabled = feature.Enabled.HasValue && feature.Enabled.Value;

                var userList = userListReader.GetUserNamesFromList(feature.UserListPath);

                FeatureToggles.Add(new FeatureData { Name = feature.Name, Enabled = enabled, UserNamesList = userList });
            }
        }

        public static bool IsEnabled(string featureName)
        {
            if (FeatureToggles == null)
            {
                Initialize();
            }

            if (string.IsNullOrEmpty(featureName) || !FeatureToggles.Any(f => f.Name.ToLower() == featureName.ToLower()))
            {
                return false;
            }
            
            bool enabled = false;
            foreach (var featureToggle in FeatureToggles)
            {
                if (featureToggle.Name.ToLower() == featureName.ToLower())
                {
                    enabled = featureToggle.Enabled;
                }
            }

            return enabled;
        }

        public static bool IsEnabled(string featureName, string userName)
        {
            return IsEnabled(featureName) && UserIsInFeatureList(featureName, userName);
        }

        private static bool UserIsInFeatureList(string featureName, string userName)
        {
            foreach (var featureToggle in FeatureToggles)
            {
                if (featureToggle.Name.ToLower() == featureName.ToLower())
                {
                    return featureToggle.UserNamesList.Any(u => u.ToLower() == userName.ToLower());
                }
            }

            // Feature name was not found in FeatureToggle-list
            return false;
        }
    }
}
