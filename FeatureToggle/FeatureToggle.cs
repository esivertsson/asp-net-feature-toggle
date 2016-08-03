using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AspNetFeatureToggle.Configuration;

namespace AspNetFeatureToggle
{
    public class FeatureToggle
    {
        private static List<BasicToggleType> FeatureToggles { get; set; }
        
        public static void Initialize()
        {
            var featureCollection = new FeatureCollection();

            if (FeatureToggleSection.Config != null)
            {
                featureCollection = FeatureToggleSection.Config.FeatureList;
            }

            Initialize(featureCollection, new FileUserListReader());
        }

        public static void Initialize(FeatureCollection featureList, IUserListReader userListReader)
        {
            FeatureToggles = new List<BasicToggleType>();
            foreach (FeatureElement feature in featureList)
            {
                var toggleType = CreateToggleType(feature, userListReader);
                FeatureToggles.Add(toggleType);
            }
        }

        public static bool IsEnabled(string featureName)
        {
            return IsEnabled(featureName, string.Empty);
        }

        public static bool IsEnabled(string featureName, string userName)
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
                    return featureToggle.IsEnabled(new RequestData { UserName = userName });
                }
            }

            return false;
        }

        private static BasicToggleType CreateToggleType(FeatureElement feature, IUserListReader userListReader)
        {
            BasicToggleType toggleType;

            // The toggle is disabled by default, so if HasValue is false, then toggle is disabled.
            bool enabled = feature.Enabled.HasValue && feature.Enabled.Value;

            if (!string.IsNullOrEmpty(feature.UserListPath))
            {
                var userList = userListReader.GetUserNamesFromList(feature.UserListPath);
                toggleType = new UserListToggleType { Name = feature.Name, Enabled = enabled, UserNamesList = userList };
            }
            else if (!string.IsNullOrEmpty(feature.RandomFactor))
            {
                // Convert string to float
                float factorValue = float.Parse(feature.RandomFactor, CultureInfo.InvariantCulture.NumberFormat);
                toggleType = new RandomToggleType { Name = feature.Name, Enabled = enabled, RandomFactor = factorValue };
            }
            else
            {
                toggleType = new BasicToggleType { Name = feature.Name, Enabled = enabled };
            }

            return toggleType;
        }
    }
}
