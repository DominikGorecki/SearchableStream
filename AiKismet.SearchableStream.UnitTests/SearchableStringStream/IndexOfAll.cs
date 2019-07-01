using AiKismet.SearchableStream;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SeekableStringStreamTests
{
    [TestClass]
    public class IndexOfAll
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullNeedle()
        {
            // Arrange
            var haystackByteArray = Encoding.ASCII.GetBytes("This haystack does not contain a n-e-e-d-l-e");

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var searchableStringStream = new SearchableStringStream(memStream))
            {
                // Act
                var foundPosition = searchableStringStream.IndexOfAll(null);
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
            using (var searchableStringStream = new SearchableStringStream(memStream))
            {
                // Act
                var foundPosition = searchableStringStream.IndexOfAll(emptyNeedle);
            }
        }

        [TestMethod]
        public void EmptyStream()
        {
            // Arrange
            var emptyByteArray = new byte[0];
            var needleByteArray = "needle";

            using (var memStream = new MemoryStream(emptyByteArray))
            using(var searchableStringStream = new SearchableStringStream(memStream))
            {
                // Act
                var foundPositions = searchableStringStream.IndexOfAll(needleByteArray);

                // Assert
                Assert.AreEqual(0, foundPositions.Length);
            }
        }

        [TestMethod]
        public void NotInStream()
        {
            // Arrange
            var haystackByteArray = Encoding.ASCII.GetBytes("This haystack does not contain a n-e-e-d-l-e");
            var needleByteArray = "needle";

            using(var memStream = new MemoryStream(haystackByteArray))
            using(var searchableStringStream = new SearchableStringStream(memStream))
            {
                // Act
                var foundPositions = searchableStringStream.IndexOfAll(needleByteArray);

                // Assert
                Assert.AreEqual(0, foundPositions.Length);
            }
        }

       [TestMethod]
        public void SingleOccuranceAtBeginning()
        {
            // Arrange
            var haystackByteArray = Encoding.ASCII.GetBytes("needle in a haystack");
            var needleByteArray = "needle";

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var searchableStringStream = new SearchableStringStream(memStream))
            {
                // Act
                var foundPositions = searchableStringStream.IndexOfAll(needleByteArray);

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
            var needleByteArray = "needle";

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var searchableStringStream = new SearchableStringStream(memStream))
            {
                // Act
                var foundPositions = searchableStringStream.IndexOfAll(needleByteArray);

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
            var needleByteArray = "needle";

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var searchableStringStream = new SearchableStringStream(memStream))
            {
                // Act
                var foundPositions = searchableStringStream.IndexOfAll(needleByteArray);

                // Assert
                Assert.AreEqual(3, foundPositions.Length);
                Assert.AreEqual(21, foundPositions[0]);
                Assert.AreEqual(45, foundPositions[1]);
                Assert.AreEqual(69, foundPositions[2]);
            }
        }

        [TestMethod]
        public void MultipleOccuranceSelectTwo()
        {
            // Arrange
            var haystackByteArray = Encoding.ASCII.GetBytes("This hay stack has a needle here and another needle here and another needle here");
            var needleByteArray = "needle";

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var searchableStringStream = new SearchableStringStream(memStream))
            {
                // Act
                var foundPositions = searchableStringStream.IndexOfAll(needleByteArray, 2);

                // Assert
                Assert.AreEqual(2, foundPositions.Length);
                Assert.AreEqual(21, foundPositions[0]);
                Assert.AreEqual(45, foundPositions[1]);
            }
        }

        [TestMethod]
        public void MultipleOccurance_UTF8()
        {
            // Arrange
            var haystackByteArray = Encoding.UTF8.GetBytes("This hay stack has a needle here and another needle here and another needle here");
            var needleByteArray = "needle";

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var searchableStringStream = new SearchableStringStream(memStream, Encoding.UTF8))
            {
                // Act
                var foundPositions = searchableStringStream.IndexOfAll(needleByteArray);

                // Assert
                Assert.AreEqual(3, foundPositions.Length);
                Assert.AreEqual(21, foundPositions[0]);
                Assert.AreEqual(45, foundPositions[1]);
                Assert.AreEqual(69, foundPositions[2]);
            }
        }

        [TestMethod]
        public void MultipleOccuranceSelectTwo_UTF8()
        {
            // Arrange
            var haystackByteArray = Encoding.UTF8.GetBytes("This hay stack has a needle here and another needle here and another needle here");
            var needleByteArray = "needle";

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var searchableStringStream = new SearchableStringStream(memStream, Encoding.UTF8))
            {
                // Act
                var foundPositions = searchableStringStream.IndexOfAll(needleByteArray, 2);

                // Assert
                Assert.AreEqual(2, foundPositions.Length);
                Assert.AreEqual(21, foundPositions[0]);
                Assert.AreEqual(45, foundPositions[1]);
            }
        }

        [TestMethod]
        public void MultipleOccurance_Unicode()
        {
            // Arrange
            var haystackByteArray = Encoding.Unicode.GetBytes("This hay stack has a needle here and another needle here and another needle here");
            var needleByteArray = "needle";

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var searchableStringStream = new SearchableStringStream(memStream, Encoding.Unicode))
            {
                // Act
                var foundPositions = searchableStringStream.IndexOfAll(needleByteArray);

                // Assert
                Assert.AreEqual(3, foundPositions.Length);
                Assert.AreEqual(21, foundPositions[0]);
                Assert.AreEqual(45, foundPositions[1]);
                Assert.AreEqual(69, foundPositions[2]);
            }
        }

        [TestMethod]
        public void MultipleOccuranceSelectTwo_Unicode()
        {
            // Arrange
            var haystackByteArray = Encoding.Unicode.GetBytes("This hay stack has a needle here and another needle here and another needle here");
            var needleByteArray = "needle";

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var searchableStringStream = new SearchableStringStream(memStream, Encoding.Unicode))
            {
                // Act
                var foundPositions = searchableStringStream.IndexOfAll(needleByteArray, 2);

                // Assert
                Assert.AreEqual(2, foundPositions.Length);
                Assert.AreEqual(21, foundPositions[0]);
                Assert.AreEqual(45, foundPositions[1]);
            }
        }
    }
}
