﻿using AiKismet.SearchableStream;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SearchableStreamTests
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

            using (var memStream = new MemoryStream(haystackByteArray))
            using (var seekableStream = new SearchableStream(memStream))
            {
                // Act
                var foundPosition = seekableStream.LastIndexOf(null);
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
                var foundPosition = seekableStream.LastIndexOf(emptyNeedle);
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
                var foundPosition = seekableStream.LastIndexOf(needleByteArray);

                // Assert
                Assert.AreEqual(-1, foundPosition);
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
                var foundPosition = seekableStream.LastIndexOf(needleByteArray);

                // Assert
                Assert.AreEqual(-1, foundPosition);
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
                var foundPosition = seekableStream.LastIndexOf(needleByteArray);

                // Assert
                Assert.AreEqual(0, foundPosition);
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
                var foundPosition = seekableStream.LastIndexOf(needleByteArray);

                // Assert
                Assert.AreEqual(16, foundPosition);
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
                var foundPosition = seekableStream.LastIndexOf(needleByteArray);

                // Assert
                Assert.AreEqual(69, foundPosition);
            }
        }
    }
}
