using System;
using System.Collections.Generic;
using AspNetFeatureToggle.Configuration;

namespace AspNetFeatureToggle
{
    public class FeatureToggle
    {
        private static Dictionary<string, bool> FeatureToggles { get; set; }

        public static void Initialize()
        {
            Initialize(FeatureToggleSection.Config.FeatureList);
        }

        public static void Initialize(FeatureCollection featureList)
        {
            FeatureToggles = new Dictionary<string, bool>();
            foreach (FeatureElement feature in featureList)
            {
                // The toggle is off by default
                bool toggleOn = feature.ToggleOn.HasValue && feature.ToggleOn.Value;

                FeatureToggles.Add(feature.Name, toggleOn);
            }
        }

        public static bool Check(string name)
        {
            if (FeatureToggles == null)
            {
                throw new Exception("FeatureToggle must be initialized, by doing FeatureToggle.Initialize();");
            }

            if (string.IsNullOrEmpty(name) || !FeatureToggles.ContainsKey(name))
            {
                return false;
            }

            bool toggleOn;
            FeatureToggles.TryGetValue(name, out toggleOn);

            return toggleOn;
        }
    }
}
