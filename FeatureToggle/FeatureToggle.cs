using System;
using System.Collections.Generic;
using System.Linq;
using AspNetFeatureToggle.Configuration;

namespace AspNetFeatureToggle
{
    public class FeatureToggle
    {
        private static List<FeatureDataBase> FeatureToggles { get; set; }

        public static void Initialize()
        {
            Initialize(FeatureToggleSection.Config.FeatureList, new FileUserListReader());
        }

        public static void Initialize(FeatureCollection featureList, IUserListReader userListReader)
        {
            FeatureToggles = new List<FeatureDataBase>();
            foreach (FeatureElement feature in featureList)
            {
                // The toggle is disabled by default, so if HasValue is false, then toggle is disabled.
                bool enabled = feature.Enabled.HasValue && feature.Enabled.Value;

                if (!string.IsNullOrEmpty(feature.UserListPath))
                {
                    var userList = userListReader.GetUserNamesFromList(feature.UserListPath);
                    FeatureToggles.Add(new UserListFeatureData { Name = feature.Name, Enabled = enabled, UserNamesList = userList });                 
                }
                else
                {
                    FeatureToggles.Add(new FeatureDataBase { Name = feature.Name, Enabled = enabled });
                }
            }
        }

        public static bool IsEnabled(string featureName)
        {
            if (FeatureToggles == null)
            {
                // Class has not been initialized, run Initialize() and continue.
                Initialize();
            }

            if (string.IsNullOrEmpty(featureName) || !FeatureToggles.Any(f => String.Equals(f.Name, featureName, StringComparison.CurrentCultureIgnoreCase)))
            {
                return false;
            }
            
            foreach (var featureToggle in FeatureToggles)
            {
                if (String.Equals(featureToggle.Name, featureName, StringComparison.CurrentCultureIgnoreCase))
                {
                    return featureToggle.Enabled;
                }
            }

            return false;
        }

        public static bool IsEnabled(string featureName, string userName)
        {
            return IsEnabled(featureName) && UserIsInFeatureList(featureName, userName);
        }

        private static bool UserIsInFeatureList(string featureName, string userName)
        {
            return FeatureToggles.Where(featureToggle => String.Equals(featureToggle.Name, featureName, StringComparison.CurrentCultureIgnoreCase))
                                 .OfType<UserListFeatureData>()
                                 .Select(userListFeature => userListFeature.UserNamesList.Any(u => String.Equals(u, userName, StringComparison.CurrentCultureIgnoreCase)))
                                 .FirstOrDefault();
        }
    }
}
