using System.Configuration;

namespace AspNetFeatureToggle.Configuration
{
    public class FeatureToggleSection : ConfigurationSection
    {
        private FeatureToggleSection()
        {
        }

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
