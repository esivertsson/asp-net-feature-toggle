using System;

namespace AspNetFeatureToggle
{
    public class RandomToggleType : BasicToggleType
    {
        private static readonly Random _randomGenerator = new Random();
       
        public float RandomFactor { get; set; }

        public override bool IsEnabled(RequestData requestData)
        {
            return base.IsEnabled(requestData) && _randomGenerator.NextDouble() <= this.RandomFactor;
        }
    }
}
