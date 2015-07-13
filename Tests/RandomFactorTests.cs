using System;
using System.Collections.Generic;
using AspNetFeatureToggle;
using AspNetFeatureToggle.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Mocks;

namespace Tests
{
    [TestClass]
    public class RandomFactorTests
    {
        [TestMethod]
        public void When_Feature_Is_Defined_With_Random_Factor_One_Then_Return_True()
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
        public void When_Feature_Is_Defined_With_Random_Factor_Zero_Then_Return_False()
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
            FeatureToggle.Initialize(
                new FeatureCollection
                    {
                        NewFeature(FEATURE_NAME, true, "0.1")
                    },
                new UserListReaderMock());

            // Execute
            int nrOfRequests = 100;
            var resultList = new List<bool>();
            for (int i = 0; i < nrOfRequests; i++)
            {
                resultList.Add(FeatureToggle.IsEnabled(FEATURE_NAME));
            }
            
            int isEnabled = resultList.FindAll(b => b == true).Count;
            int isDisabled = resultList.FindAll(b => b == false).Count;
            Console.WriteLine(isEnabled + "/" + isDisabled);
            
            // Verify
            Assert.IsFalse(isDisabled == 0);
            Assert.IsFalse(isDisabled == nrOfRequests);
            Assert.IsTrue(isDisabled > 80);

            Assert.IsFalse(isEnabled == 0);
            Assert.IsFalse(isEnabled == nrOfRequests);
            Assert.IsTrue(isEnabled > 3);

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
