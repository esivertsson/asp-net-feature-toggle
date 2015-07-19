using System;
using System.IO;
using System.Linq;
using AspNetFeatureToggle;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class FileUserListReaderTests
    {
        [TestMethod]
        public void When_Filename_Is_Empty_Then_UserList_Is_Empty()
        {
            // Setup
            var reader = new FileUserListReader();

            // Execute
            var result = reader.GetUserNamesFromList(string.Empty);

            // Verify
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void When_File_Is_Empty_Then_UserList_Is_Empty()
        {
            // Setup
            var fileName = CreateFileWithContent(string.Empty);
            var reader = new FileUserListReader();

            // Execute
            var result = reader.GetUserNamesFromList(fileName);

            // Verify
            Assert.AreEqual(0, result.Count());

            // TearDown
            File.Delete(fileName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "User list file NoFile.config could not be found")]
        public void When_File_Does_Not_Exist_Then_ArgumentException_Is_Thrown()
        {
            // Setup
            var reader = new FileUserListReader();

            // Execute
            reader.GetUserNamesFromList("NoFile.config");
        }

        [TestMethod]
        public void When_File_Contains_Valid_Content_Then_UserList_Contains_User_Names()
        {
            // Setup
            const string FILE_CONTENT = "User1\nUser2";
            var fileName = CreateFileWithContent(FILE_CONTENT);

            var reader = new FileUserListReader();

            // Execute
            var result = reader.GetUserNamesFromList(fileName);

            // Verify
            Assert.AreEqual(2, result.Count());
            CollectionAssert.Contains(result.ToArray(), "User1");
            CollectionAssert.Contains(result.ToArray(), "User2");

            // TearDown
            File.Delete(fileName);
        }

        [TestMethod]
        public void When_File_Contains_Different_Line_Endings_Then_UserList_Contains_User_Names()
        {
            // Setup
            const string FILE_CONTENT = "User1\r\nUser2\rUser3\nUser4";
            var fileName = CreateFileWithContent(FILE_CONTENT);

            var reader = new FileUserListReader();

            // Execute
            var result = reader.GetUserNamesFromList(fileName);

            // Verify
            Assert.AreEqual(4, result.Count());
            CollectionAssert.Contains(result.ToArray(), "User1");
            CollectionAssert.Contains(result.ToArray(), "User2");
            CollectionAssert.Contains(result.ToArray(), "User3");
            CollectionAssert.Contains(result.ToArray(), "User4");

            // TearDown
            File.Delete(fileName);
        }

        [TestMethod]
        public void When_File_Contains_Empty_Lines_Then_UserList_Only_Contains_Actual_User_Names()
        {
            // Setup
            const string FILE_CONTENT = "User1\r\nUser2\n\n";
            var fileName = CreateFileWithContent(FILE_CONTENT);

            var reader = new FileUserListReader();

            // Execute
            var result = reader.GetUserNamesFromList(fileName);

            // Verify
            Assert.AreEqual(2, result.Count());
            CollectionAssert.Contains(result.ToArray(), "User1");
            CollectionAssert.Contains(result.ToArray(), "User2");

            // TearDown
            File.Delete(fileName);
        }

        private static string CreateFileWithContent(string fileContent)
        {
            var fileName = Guid.NewGuid() + ".txt";
            var fileWriter = File.CreateText(fileName);

            fileWriter.Write(fileContent);
            fileWriter.Flush();
            fileWriter.Close();

            return fileName;
        }
    }
}
