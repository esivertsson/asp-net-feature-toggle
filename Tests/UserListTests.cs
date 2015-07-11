using AspNetFeatureToggle;
using AspNetFeatureToggle.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Mocks;

namespace Tests
{
    [TestClass]
    public class UserListTests
    {
        [TestMethod]
        public void When_Feature_Is_Defined_With_User_List_And_User_In_List_Makes_Call_Then_Return_True()
        {
            // Setup
            const string FEATURE_NAME = "Feature1";
            const string USER_NAME = "aUser";
            FeatureToggle.Initialize(
                new FeatureCollection
                    {
                        NewFeature(FEATURE_NAME, true, USER_NAME)
                    },
                new UserListReaderMock());

            // Execute
            bool result = FeatureToggle.IsEnabled(FEATURE_NAME, USER_NAME);

            // Verify
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void When_User_Name_Has_Differing_Case_Then_Names_Still_Matches()
        {
            // Setup
            const string FEATURE_NAME = "Feature1";
            const string USER_NAME = "aUser";
            FeatureToggle.Initialize(
                new FeatureCollection
                    {
                        NewFeature(FEATURE_NAME, true, USER_NAME)
                    },
                new UserListReaderMock());

            // Execute
            bool result = FeatureToggle.IsEnabled(FEATURE_NAME, USER_NAME.ToUpper());

            // Verify
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void When_Feature_Name_Has_Differing_Case_Then_Names_Still_Matches()
        {
            // Setup
            const string FEATURE_NAME = "Feature1";
            const string USER_NAME = "aUser";
            FeatureToggle.Initialize(
                new FeatureCollection
                    {
                        NewFeature(FEATURE_NAME, true, USER_NAME)
                    },
                new UserListReaderMock());

            // Execute
            bool result = FeatureToggle.IsEnabled(FEATURE_NAME.ToUpper(), USER_NAME);

            // Verify
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void When_Feature_Is_Defined_With_User_List_And_User_Is_Not_In_List_Then_Return_False()
        {
            // Setup
            const string FEATURE_NAME = "Feature1";
            const string USER_NAME = "aUser";
            FeatureToggle.Initialize(
                new FeatureCollection
                    {
                        NewFeature(FEATURE_NAME, true, USER_NAME)
                    },
                new UserListReaderMock());

            // Execute
            bool result = FeatureToggle.IsEnabled(FEATURE_NAME, "UserNotInList");

            // Verify
            Assert.IsFalse(result);
        }

        private FeatureElement NewFeature(string name, bool toggleOn, string userNames)
        {
            return new FeatureElementMock
            {
                Name = name,
                Enabled = toggleOn,
                UserListPath = userNames
            };
        }
    }
}
