using AiKismet.SearchableStream;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SearchableStringStreamTests
{
    [TestClass]
    public class LastIndexOf
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullNeedle()
        {
            // Arrange
            var haystackByteArray = Encoding.ASCII.GetBytes("This haystack does not contain a n-e-e-d-l-e");
            string nullNeedle = null;

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var sStream = new SearchableStringStream(memStream))
            {
                // Act
                var foundPosition = sStream.LastIndexOf(nullNeedle);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EmptyNeedle()
        {
            // Arrange
            var haystackByteArray = Encoding.ASCII.GetBytes("This haystack does not contain a n-e-e-d-l-e");
            var emptyNeedle = string.Empty;

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var sStream = new SearchableStringStream(memStream))
            {
                // Act
                var foundPosition = sStream.LastIndexOf(emptyNeedle);
            }
        }

        [TestMethod]
        public void EmptyStream()
        {
            // Arrange
            var emptyByteArray = new byte[0];
            var needle = "needle";

            using (var memStream = new MemoryStream(emptyByteArray))
            using(var sStream = new SearchableStringStream(memStream))
            {
                // Act
                var foundPosition = sStream.LastIndexOf(needle);

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
            using(var sStream = new SearchableStringStream(memStream))
            {
                // Act
                var foundPosition = sStream.LastIndexOf(needle);

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
            using (var sStream = new SearchableStringStream(memStream))
            {
                // Act
                var foundPosition = sStream.LastIndexOf(needle);

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
            using (var sStream = new SearchableStringStream(memStream))
            {
                // Act
                var foundPosition = sStream.LastIndexOf(needle);

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
            using (var sStream = new SearchableStringStream(memStream))
            {
                // Act
                var foundPosition = sStream.LastIndexOf(needle);

                // Assert
                Assert.AreEqual(69, foundPosition);
            }
        }
    }
}
