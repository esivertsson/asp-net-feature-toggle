using System.Configuration;

namespace AspNetFeatureToggle.Configuration
{
    public class FeatureCollection : ConfigurationElementCollection
    {
        public void Add(FeatureElement newElement)
        {
            BaseAdd(newElement);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new FeatureElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FeatureElement)element).Name;
        }
    }
}
