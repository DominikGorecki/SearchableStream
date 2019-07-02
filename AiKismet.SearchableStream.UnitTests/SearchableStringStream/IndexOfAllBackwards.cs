using AiKismet.SearchableStream;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SearchableStringStreamTests
{
    [TestClass]
    public class IndexOfAllBackwards 
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullNeedle()
        {
            var haystackByteArray = Encoding.ASCII.GetBytes("This haystack does not contain a n-e-e-d-l-e");

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var sStream = new SearchableStringStream(memStream))
            {
                // Act
                var foundPosition = sStream.IndexOfAllBackwards(null);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EmptyNeedle()
        {
            // Arrange
            var haystackByteArray = Encoding.ASCII.GetBytes("This haystack does not contain a n-e-e-d-l-e");
            var emptyNeedle = "";

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var sStream = new SearchableStringStream(memStream))
            {
                // Act
                var foundPosition = sStream.IndexOfAllBackwards(emptyNeedle);
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
                sStream.Seek(0, SeekOrigin.End);
                // Act
                var foundPositions = sStream.IndexOfAllBackwards(needle);

                // Assert
                Assert.AreEqual(0, foundPositions.Length);
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
                sStream.Seek(0, SeekOrigin.End);

                // Act
                var foundPositions = sStream.IndexOfAllBackwards(needle);

                // Assert
                Assert.AreEqual(0, foundPositions.Length);
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
                sStream.Seek(0, SeekOrigin.End);

                // Act
                var foundPositions = sStream.IndexOfAllBackwards(needle);

                // Assert
                Assert.AreEqual(1, foundPositions.Length);
                Assert.AreEqual(0, foundPositions[0]);
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
                sStream.Seek(0, SeekOrigin.End);
                // Act
                var foundPositions = sStream.IndexOfAllBackwards(needle);

                // Assert
                Assert.AreEqual(1, foundPositions.Length);
                Assert.AreEqual(16, foundPositions[0]);
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
                sStream.Seek(0, SeekOrigin.End);
                // Act
                var foundPositions = sStream.IndexOfAllBackwards(needle);

                // Assert
                Assert.AreEqual(3, foundPositions.Length);
                Assert.AreEqual(21, foundPositions[2]);
                Assert.AreEqual(45, foundPositions[1]);
                Assert.AreEqual(69, foundPositions[0]);
            }
        }

        [TestMethod]
        public void MultipleOccuranceSelectTwo()
        {
            // Arrange
            var haystackByteArray = Encoding.ASCII.GetBytes("This hay stack has a needle here and another needle here and another needle here");
            var needle = "needle";

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var sStream = new SearchableStringStream(memStream))
            {
                sStream.Seek(0, SeekOrigin.End);
                // Act
                var foundPositions = sStream.IndexOfAllBackwards(needle, 2);

                // Assert
                Assert.AreEqual(2, foundPositions.Length);
                Assert.AreEqual(45, foundPositions[1]);
                Assert.AreEqual(69, foundPositions[0]);
            }
        }
    }
}
