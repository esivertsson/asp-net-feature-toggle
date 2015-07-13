using AspNetFeatureToggle.Configuration;

namespace Tests.Mocks
{
    public class FeatureElementMock : FeatureElement
    {
        public override string Name { get; set; }

        public override bool? Enabled { get; set; }

        public override string UserListPath { get; set; }

        public override string RandomFactor { get; set; }
    }
}
