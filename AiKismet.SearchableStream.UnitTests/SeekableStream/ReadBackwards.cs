using AiKismet.SearchableStream;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SeekableStreamTests
{
    [TestClass]
    public class ReadBackwards
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EmptyStream()
        {
            var emptyByteArray = new byte[0];
            var buffer = new byte[10];

            using (var memStream = new MemoryStream(emptyByteArray))
            using(var seekableStream = new SearchableStream(memStream))
            {
                // Act
                seekableStream.ReadBackwards(buffer, 0, buffer.Length);

            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EmptyBuffer()
        {
            var haystackByteArray = Encoding.ASCII.GetBytes("This haystack does not contain a n-e-e-d-l-e");
            var buffer = new byte[0];

            using (var memStream = new MemoryStream(haystackByteArray))
            using(var seekableStream = new SearchableStream(memStream))
            {
                seekableStream.Seek(0, SeekOrigin.End);
                // Act
                seekableStream.ReadBackwards(buffer, 0, 1);

            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void BufferTooSmallForReadCount()
        {
            var haystackByteArray = Encoding.ASCII.GetBytes("This haystack does not contain a n-e-e-d-l-e");
            var buffer = new byte[2];

            using (var memStream = new MemoryStream(haystackByteArray))
            using(var seekableStream = new SearchableStream(memStream))
            {
                seekableStream.Seek(0, SeekOrigin.End);
                // Act
                seekableStream.ReadBackwards(buffer, 0, 3);

            }
             
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullBuffer()
        {
            var haystackByteArray = Encoding.ASCII.GetBytes("This haystack does not contain a n-e-e-d-l-e");

            using (var memStream = new MemoryStream(haystackByteArray))
            using(var seekableStream = new SearchableStream(memStream))
            {
                seekableStream.Seek(0, SeekOrigin.End);
                // Act
                seekableStream.ReadBackwards(null, 0, 3);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadBiggerThanReadable()
        {
            var haystackByteArray = Encoding.ASCII.GetBytes("12345");
            var buffer = new byte[10];

            using (var memStream = new MemoryStream(haystackByteArray))
            using(var seekableStream = new SearchableStream(memStream))
            {
                seekableStream.Seek(0, SeekOrigin.End);
                // Act
                seekableStream.ReadBackwards(buffer, 0, 10);
            }
 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void OffsetBiggerThanBuffer()
        {
            var haystackByteArray = Encoding.ASCII.GetBytes("12345");
            var buffer = new byte[2];

            using (var memStream = new MemoryStream(haystackByteArray))
            using(var seekableStream = new SearchableStream(memStream))
            {
                seekableStream.Seek(0, SeekOrigin.End);
                // Act
                seekableStream.ReadBackwards(buffer, 3, 1);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadBiggerThanBuffer()
        {
            var haystackByteArray = Encoding.ASCII.GetBytes("12345");
            var buffer = new byte[2];

            using (var memStream = new MemoryStream(haystackByteArray))
            using(var seekableStream = new SearchableStream(memStream))
            {
                seekableStream.Seek(0, SeekOrigin.End);
                // Act
                seekableStream.ReadBackwards(buffer, 0, 3);
            }
        }

        [TestMethod]
        public void BufferSameSizeReadSameSizeNoOffset()
        {
            var haystackByteArray = Encoding.ASCII.GetBytes("12345");
            var buffer = new byte[5];

            using (var memStream = new MemoryStream(haystackByteArray))
            using(var seekableStream = new SearchableStream(memStream))
            {
                seekableStream.Seek(0, SeekOrigin.End);
                // Act
                seekableStream.ReadBackwards(buffer, 0, 5);

                // Assert
                var haystackReversed = haystackByteArray.Reverse().ToArray();
                Assert.IsTrue(buffer.SequenceEqual(haystackReversed));
            }
        }

        [TestMethod]
        public void ReadFromEnd()
        {
            var haystackByteArray = Encoding.ASCII.GetBytes("1234567");
            var buffer = new byte[3];

            using (var memStream = new MemoryStream(haystackByteArray))
            using(var seekableStream = new SearchableStream(memStream))
            {
                seekableStream.Seek(0, SeekOrigin.End);
                // Act
                seekableStream.ReadBackwards(buffer, 0, 3);

                // Assert
                Assert.IsTrue(buffer.SequenceEqual(Encoding.ASCII.GetBytes("765")));
            }
        }

        [TestMethod]
        public void ReadFromEndWithOffset()
        {
            var haystackByteArray = Encoding.ASCII.GetBytes("1234567");
            var buffer = new byte[10];

            using (var memStream = new MemoryStream(haystackByteArray))
            using(var seekableStream = new SearchableStream(memStream))
            {
                seekableStream.Seek(0, SeekOrigin.End);
                // Act
                seekableStream.ReadBackwards(buffer, 7, 3);

                // Assert
                var expectedBytes = (new byte[7]).ToList();
                expectedBytes.AddRange(Encoding.ASCII.GetBytes("765"));

                Assert.IsTrue(buffer.SequenceEqual(expectedBytes));
            }
        }
    }
}
