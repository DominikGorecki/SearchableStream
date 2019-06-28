using AiKismet.SearchableStream;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullBuffer()
        {

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadBiggerThanReadable()
        {

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void OffsetBiggerThanReadable()
        {

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void BufferSmallerOffsetTooLarge()
        {

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ReadBiggerThanBuffer()
        {

        }

        [TestMethod]
        public void BufferSameSize()
        {

        }

    }
}
