using System;
using System.Configuration;

namespace AspNetFeatureToggle.Configuration
{
    public class FeatureToggleSection : ConfigurationSection
    {
        public static string Section
        {
            get { return "featureToggle"; }
        }

        public static FeatureToggleSection Config
        {
            get
            {
                return ConfigurationManager.GetSection(Section) as FeatureToggleSection;
            }
        }

        private FeatureToggleSection()
        {
        }

        [ConfigurationProperty("featureList", IsRequired = false)]
        public FeatureCollection FeatureList
        {
            get
            {
                return this["featureList"] == null ? new FeatureCollection() : this["featureList"] as FeatureCollection;
            }
        }
    }
}   
