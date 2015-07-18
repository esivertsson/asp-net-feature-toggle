using System;
using System.Collections.Generic;
using AspNetFeatureToggle;
using AspNetFeatureToggle.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Mocks;

namespace Tests
{
    [TestClass]
    public class RandomToggleTypeTests
    {
        [TestMethod]
        public void When_Feature_Is_Defined_With_Random_Factor_One_Then_Always_Return_True()
        {
            // Setup
            const string FEATURE_NAME = "Feature1";
            FeatureToggle.Initialize(
                new FeatureCollection
                    {
                        NewFeature(FEATURE_NAME, true, "1.0")
                    },
                new UserListReaderMock());

            // Execute
            bool result = FeatureToggle.IsEnabled(FEATURE_NAME);

            // Verify
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void When_Feature_Is_Defined_With_Random_Factor_Zero_Then_Always_Return_False()
        {
            // Setup
            const string FEATURE_NAME = "Feature1";
            FeatureToggle.Initialize(
                new FeatureCollection
                    {
                        NewFeature(FEATURE_NAME, true, "0.0")
                    },
                new UserListReaderMock());

            // Execute
            bool result = FeatureToggle.IsEnabled(FEATURE_NAME);

            // Verify
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void When_RandomFactor_Is_10_Percent_Then_About_10_Percent_Of_Users_Will_Have_Feature_Enabled()
        {
            // Setup
            const string FEATURE_NAME = "Feature1";
            const string TEN_PERCENT_RANDOM_FACTOR = "0.1";
            FeatureToggle.Initialize(
                new FeatureCollection
                    {
                        NewFeature(FEATURE_NAME, true, TEN_PERCENT_RANDOM_FACTOR)
                    },
                new UserListReaderMock());

            // Execute
            const int NR_OF_REQUESTS = 100;
            var resultList = new List<bool>();
            for (int i = 0; i < NR_OF_REQUESTS; i++)
            {
                resultList.Add(FeatureToggle.IsEnabled(FEATURE_NAME));
            }
            
            int nrIsEnabled = resultList.FindAll(b => b == true).Count;
            int nrIsDisabled = resultList.FindAll(b => b == false).Count;
            Console.WriteLine(nrIsEnabled + "/" + nrIsDisabled);
            
            // Verify
            Assert.IsFalse(nrIsDisabled == 0);
            Assert.IsFalse(nrIsDisabled == NR_OF_REQUESTS);
            Assert.IsTrue(nrIsDisabled > 80);

            Assert.IsFalse(nrIsEnabled == 0);
            Assert.IsFalse(nrIsEnabled == NR_OF_REQUESTS);
            Assert.IsTrue(nrIsEnabled > 3);

        }

        private FeatureElement NewFeature(string name, bool isEnabled, string randomFactor)
        {
            return new FeatureElementMock
            {
                Name = name,
                Enabled = isEnabled,
                RandomFactor = randomFactor
            };
        }
    }
}
