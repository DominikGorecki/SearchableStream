using AiKismet.SearchableStream;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SeekableStreamTests
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
            using (var seekableStream = new SearchableStream(memStream))
            {
                // Act
                var foundPosition = seekableStream.IndexOfAllBackwards(null);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EmptyNeedle()
        {
            // Arrange
            var haystackByteArray = Encoding.ASCII.GetBytes("This haystack does not contain a n-e-e-d-l-e");
            var emptyNeedle = new byte[0];

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var seekableStream = new SearchableStream(memStream))
            {
                // Act
                var foundPosition = seekableStream.IndexOfAllBackwards(emptyNeedle);
            }
        }

        [TestMethod]
        public void EmptyStream()
        {
            // Arrange
            var emptyByteArray = new byte[0];
            var needleByteArray = Encoding.ASCII.GetBytes("needle");

            using (var memStream = new MemoryStream(emptyByteArray))
            using(var seekableStream = new SearchableStream(memStream))
            {
                // Act
                var foundPositions = seekableStream.IndexOfAllBackwards(needleByteArray);

                // Assert
                Assert.AreEqual(0, foundPositions.Length);
            }
        }

        [TestMethod]
        public void NotInStream()
        {
            // Arrange
            var haystackByteArray = Encoding.ASCII.GetBytes("This haystack does not contain a n-e-e-d-l-e");
            var needleByteArray = Encoding.ASCII.GetBytes("needle");

            using(var memStream = new MemoryStream(haystackByteArray))
            using(var seekableStream = new SearchableStream(memStream))
            {
                // Act
                var foundPositions = seekableStream.IndexOfAllBackwards(needleByteArray);

                // Assert
                Assert.AreEqual(0, foundPositions.Length);
            }
        }

        [TestMethod]
        public void SingleOccuranceAtBeginning()
        {
            // Arrange
            var haystackByteArray = Encoding.ASCII.GetBytes("needle in a haystack");
            var needleByteArray = Encoding.ASCII.GetBytes("needle");

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var seekableStream = new SearchableStream(memStream))
            {
                // Act
                var foundPositions = seekableStream.IndexOfAllBackwards(needleByteArray);

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
            var needleByteArray = Encoding.ASCII.GetBytes("needle");

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var seekableStream = new SearchableStream(memStream))
            {
                // Act
                var foundPositions = seekableStream.IndexOfAllBackwards(needleByteArray);

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
            var needleByteArray = Encoding.ASCII.GetBytes("needle");

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var seekableStream = new SearchableStream(memStream))
            {
                // Act
                var foundPositions = seekableStream.IndexOfAllBackwards(needleByteArray);

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
            var needleByteArray = Encoding.ASCII.GetBytes("needle");

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var seekableStream = new SearchableStream(memStream))
            {
                // Act
                var foundPositions = seekableStream.IndexOfAllBackwards(needleByteArray, 2);

                // Assert
                Assert.AreEqual(2, foundPositions.Length);
                Assert.AreEqual(45, foundPositions[1]);
                Assert.AreEqual(69, foundPositions[0]);
            }
        }
    }
}
