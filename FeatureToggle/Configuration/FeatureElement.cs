using System.Configuration;

namespace AspNetFeatureToggle.Configuration
{
    public class FeatureElement : ConfigurationElement
    {
        private string _name;
        private bool? _toggleOn;

        public bool UseForTest { get; set; }

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                if (this.UseForTest)
                {
                    return this._name;
                }
                return this["name"] as string;
            }
            set
            {
                this._name = value;
            }
        }

        [ConfigurationProperty("toggleOn", IsRequired = false)]
        public bool? ToggleOn
        {
            get
            {
                if (this.UseForTest)
                {
                    return this._toggleOn;
                }
                return this["toggleOn"] as bool?;
            }
            set
            {
                this._toggleOn = value;
            }
        }
    }
}
