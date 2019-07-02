using AiKismet.SearchableStream;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

/// <summary>
/// Note: Keeping namespace short for Test Explorer
/// </summary>
namespace SearchableStringStreamTests 
{
    [TestClass]
    public class IndexOfTests 
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullNeedle()
        {
            // Arrange
            var haystackByteArray = Encoding.ASCII.GetBytes("This haystack does not contain a n-e-e-d-l-e");

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var searchableStringString = new SearchableStringStream(memStream))
            {
                // Act
                var foundPosition = searchableStringString.IndexOf(null);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EmptyNeedle()
        {
            // Arrange
            var haystackByteArray = Encoding.ASCII.GetBytes("This haystack does not contain a n-e-e-d-l-e");
            var needle = string.Empty;

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var searchableStringStream = new SearchableStringStream(memStream))
            {
                // Act
                var foundPosition = searchableStringStream.IndexOf(needle);
            }
        }

        [TestMethod]
        public void EmptyStream()
        {
            // Arrange
            var emptyByteArray = new byte[0];
            var needle = "needle";

            using (var memStream = new MemoryStream(emptyByteArray))
            using(var searchableStringStream = new SearchableStringStream(memStream))
            {
                // Act
                var foundPosition = searchableStringStream.IndexOf(needle);

                // Assert
                Assert.AreEqual(-1, foundPosition);
            }
        }

        [TestMethod]
        public void NotInStream()
        {
            // Arrange
            var haystackByteArray = Encoding.ASCII.GetBytes("This haystack does not contain a n-e-e-d-l-e");
            var needle = "needle";

            using(var memStream = new MemoryStream(haystackByteArray))
            using(var searchableStringStream = new SearchableStringStream(memStream))
            {
                // Act
                var foundPosition = searchableStringStream.IndexOf(needle);

                // Assert
                Assert.AreEqual(-1, foundPosition);
            }
        }

        [TestMethod]
        public void SingleOccuranceAtBeginning()
        {
            // Arrange
            var haystackByteArray = Encoding.ASCII.GetBytes("needle in a haystack");
            var needle = "needle";

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var searchableStringStream = new SearchableStringStream(memStream))
            {
                // Act
                var foundPosition = searchableStringStream.IndexOf(needle);

                // Assert
                Assert.AreEqual(0, foundPosition);
            }
        }

        [TestMethod]
        public void SingleOccuranceAtEnd()
        {
            // Arrange
            var haystackByteArray = Encoding.ASCII.GetBytes("haystack with a needle");
            var needle = "needle";

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var searchableStringStream = new SearchableStringStream(memStream))
            {
                // Act
                var foundPosition = searchableStringStream.IndexOf(needle);

                // Assert
                Assert.AreEqual(16, foundPosition);
            }
        }

        [TestMethod]
        public void MultipleOccurance()
        {
            // Arrange
            var haystackByteArray = Encoding.ASCII.GetBytes("This hay stack has a needle here and another needle here and another needle here");
            var needle = "needle";

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var searchableStringStream = new SearchableStringStream(memStream))
            {
                // Act
                var foundPosition = searchableStringStream.IndexOf(needle);

                // Assert
                Assert.AreEqual(21, foundPosition);
            }
        }

        [TestMethod]
        public void MultipleOccurance_UTF8()
        {
            // Arrange
            var haystackByteArray = Encoding.UTF8.GetBytes("This hay stack has a needle here and another needle here and another needle here");
            var needle = "needle";

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var searchableStringStream = new SearchableStringStream(memStream, Encoding.UTF8))
            {
                // Act
                var foundPosition = searchableStringStream.IndexOf(needle);

                // Assert
                Assert.AreEqual(21, foundPosition);
            }
        }

        [TestMethod]
        public void MultipleOccurance_Unicode()
        {
            // Arrange
            var haystackByteArray = Encoding.Unicode.GetBytes("This hay stack has a needle here and another needle here and another needle here");
            var needle = "needle";

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var searchableStringStream = new SearchableStringStream(memStream, Encoding.Unicode))
            {
                // Act
                var foundPosition = searchableStringStream.IndexOf(needle);

                // Assert
                Assert.AreEqual(42, foundPosition);
            }
        }
    }
}
