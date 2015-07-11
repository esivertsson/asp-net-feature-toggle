using AspNetFeatureToggle;
using AspNetFeatureToggle.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Mocks;

namespace Tests
{
    [TestClass]
    public class FeaturesTests
    {
        [TestMethod]
        public void When_Feature_Is_Undefined_Then_Return_False()
        {
            // Setup
            FeatureToggle.Initialize(new FeatureCollection(), new UserListReaderMock());
            const string UNDEFINED_FEATURE = "Undefined";
            
            // Execute
            bool result = FeatureToggle.IsEnabled(UNDEFINED_FEATURE);

            // Verify
            Assert.IsFalse(result);
        }

       [TestMethod]
        public void When_Feature_Name_Is_Empty_Or_Null_Then_Return_False()
        {
            // Execute
            FeatureToggle.Initialize(new FeatureCollection(), new UserListReaderMock());
            bool result = FeatureToggle.IsEnabled(string.Empty);

            // Verify
            Assert.IsFalse(result);

            // Execute
            result = FeatureToggle.IsEnabled(null);

            // Verify
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void When_Feature_Is_Defined_And_Enabled_Is_True_Then_Return_True()
        {
            // Setup
            const string FEATURE_NAME = "Feature1";
            FeatureToggle.Initialize(
                new FeatureCollection
                    {
                        NewFeature(FEATURE_NAME, true)
                    },
                new UserListReaderMock());

            // Execute
            bool result = FeatureToggle.IsEnabled(FEATURE_NAME);

            // Verify
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void When_Feature_Name_Has_Differing_Case_Then_Names_Still_Matches()
        {
            // Setup
            const string FEATURE_NAME = "Feature1";
            FeatureToggle.Initialize(
                new FeatureCollection
                    {
                        NewFeature(FEATURE_NAME, true)
                    },
                new UserListReaderMock());

            // Execute
            bool result = FeatureToggle.IsEnabled(FEATURE_NAME.ToLower());

            // Verify
            Assert.IsTrue(result);
        }
        
        [TestMethod]
        public void When_Feature_Is_Defined_But_Enabled_Is_False_Then_Return_False()
        {
            // Setup
            const string FEATURE_NAME = "Feature1";
            FeatureToggle.Initialize(
                new FeatureCollection
                    {
                        NewFeature(FEATURE_NAME, false)
                    },
                new UserListReaderMock());

            // Execute
            bool result = FeatureToggle.IsEnabled(FEATURE_NAME);

            // Verify
            Assert.IsFalse(result);
        }

        private FeatureElement NewFeature(string name, bool enabled)
        {
            return new FeatureElementMock
                       {
                           Name = name, 
                           Enabled = enabled
                       };
        }
    }
}
