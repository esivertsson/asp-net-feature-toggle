namespace AspNetFeatureToggle
{
    public class BasicToggleType
    {
        public string Name { get; set; }

        public bool Enabled { get; set; }

        public virtual bool IsEnabled(RequestData requestData)
        {
            return Enabled;
        }
    }
}
