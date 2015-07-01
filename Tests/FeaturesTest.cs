using AspNetFeatureToggle;
using AspNetFeatureToggle.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class FeaturesTest
    {
        [TestMethod]
        [ExpectedException(typeof(System.Exception))]
        public void When_FeatureToggle_Is_UnInitialized_Then_Throw_Exception()
        {
            // Setup
            const string FEATURE_NAME = "featureA";

            // Execute
            FeatureToggle.Check(FEATURE_NAME);
        }

        [TestMethod]
        public void When_Feature_Is_Undefined_Then_Return_False()
        {
            // Setup
            FeatureToggle.Initialize(new FeatureCollection());
            const string UNDEFINED_FEATURE = "Undefined";
            
            // Execute
            bool result = FeatureToggle.Check(UNDEFINED_FEATURE);

            // Verify
            Assert.IsFalse(result);
        }

       [TestMethod]
        public void When_Feature_Name_Is_Empty_Or_Null_Then_Return_False()
        {
            // Execute
            bool result = FeatureToggle.Check(string.Empty);

            // Verify
            Assert.IsFalse(result);

            // Execute
            result = FeatureToggle.Check(null);

            // Verify
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void When_Feature_Is_Defined_And_ToggleOn_Is_True_Then_Return_True()
        {
            // Setup
            const string FEATURE_NAME = "Feature1";
            FeatureToggle.Initialize(new FeatureCollection
                                         {
                                             NewFeature(FEATURE_NAME, true)
                                         });

            // Execute
            bool result = FeatureToggle.Check(FEATURE_NAME);

            // Verify
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void When_Feature_Is_Defined_But_ToggleOn_Is_False_Then_Return_False()
        {
            // Setup
            const string FEATURE_NAME = "Feature1";
            FeatureToggle.Initialize(new FeatureCollection
                                         {
                                             NewFeature(FEATURE_NAME, false)
                                         });

            // Execute
            bool result = FeatureToggle.Check(FEATURE_NAME);

            // Verify
            Assert.IsFalse(result);
        }

        private FeatureElement NewFeature(string name, bool toggleOn)
        {
            return new FeatureElement
                       {
                           UseForTest = true, 
                           Name = name, 
                           ToggleOn = toggleOn
                       };
        }
    }
}
