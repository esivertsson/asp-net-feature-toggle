using System.Configuration;

namespace AspNetFeatureToggle.Configuration
{
    public class FeatureElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public virtual string Name
        {
            get
            {
                return this["name"] as string;
            }

            // Setter is present but empty, so that child-classes can override it

            set
            {
            }
        }

        [ConfigurationProperty("enabled", IsRequired = true)]
        public virtual bool? Enabled
        {
            get
            {
                return this["enabled"] as bool?;
            }

            // Setter is present but empty, so that child-classes can override it
            set
            {
            }
        }

        [ConfigurationProperty("userListPath", IsRequired = false)]
        public virtual string UserListPath
        {
            get
            {
                return this["userListPath"] as string;
            }

            // Setter is present but empty, so that child-classes can override it
            set
            {
            }
        }
    }
}
