using AiKismet.SearchableStream;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SearchableStringStreamTests
{
    [TestClass]
    public class ReadStringInRange
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void StartPositionBiggerThanEnd()
        {
            var byteArray = Encoding.ASCII.GetBytes("0123456789");

            using(var memStream = new MemoryStream(byteArray))
            using (var sStream = new SearchableStringStream(memStream))
            {
                sStream.ReadStringInRange(3, 2);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void StartPositionSameAsEnd()
        {
            var byteArray = Encoding.ASCII.GetBytes("0123456789");

            using(var memStream = new MemoryStream(byteArray))
            using (var sStream = new SearchableStringStream(memStream))
            {
                sStream.ReadStringInRange(2, 2);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void StartPositionNegative()
        {
            var byteArray = Encoding.ASCII.GetBytes("0123456789");

            using(var memStream = new MemoryStream(byteArray))
            using (var sStream = new SearchableStringStream(memStream))
            {
                sStream.ReadStringInRange(-2, 2);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void StartPositionLargerThanStream()
        {
            var byteArray = Encoding.ASCII.GetBytes("0123456789");

            using(var memStream = new MemoryStream(byteArray))
            using (var sStream = new SearchableStringStream(memStream))
            {
                sStream.ReadStringInRange(10, 11);
            }
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EndPositionLargerThanStream()
        {
            var byteArray = Encoding.ASCII.GetBytes("0123456789");

            using(var memStream = new MemoryStream(byteArray))
            using (var sStream = new SearchableStringStream(memStream))
            {
                sStream.ReadStringInRange(1, 11);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EmptyStream()
        {
            var emptyByteArray = new byte[0];

            using(var memStream = new MemoryStream(emptyByteArray))
            using (var sStream = new SearchableStringStream(memStream))
            {
                sStream.ReadStringInRange(0, 1);
            }
 
        }

        [TestMethod]
        public void ReadWholeStream()
        {
            var byteArray = Encoding.ASCII.GetBytes("0123456789");

            using(var memStream = new MemoryStream(byteArray))
            using (var sStream = new SearchableStringStream(memStream))
            {
                var result = sStream.ReadStringInRange(0, 9);

                Assert.AreEqual("0123456789", result);
            }
        }

        [TestMethod]
        public void ReadAtBeginning()
        {
            var byteArray = Encoding.ASCII.GetBytes("0123456789");

            using(var memStream = new MemoryStream(byteArray))
            using (var sStream = new SearchableStringStream(memStream))
            {
                var result = sStream.ReadStringInRange(0, 3);

                Assert.AreEqual("0123", result);
            }
        }

        [TestMethod]
        public void ReadAtEnd()
        {
            var byteArray = Encoding.ASCII.GetBytes("0123456789");

            using(var memStream = new MemoryStream(byteArray))
            using (var sStream = new SearchableStringStream(memStream))
            {
                var result = sStream.ReadStringInRange(7, 9);

                Assert.AreEqual("789", result);
            }
        }

        [TestMethod]
        public void ReadInMiddle()
        {
            var byteArray = Encoding.ASCII.GetBytes("0123456789");

            using(var memStream = new MemoryStream(byteArray))
            using (var sStream = new SearchableStringStream(memStream))
            {
                var result = sStream.ReadStringInRange(4, 8);

                Assert.AreEqual("45678", result);
            }
        }

    }
}
