using AiKismet.SearchableStream;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SearchableStringStreamTests
{
    [TestClass]
    public class ReadLines
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NegativeLines()
        {
            var byteArray = Encoding.ASCII.GetBytes("0123456789");

            using(var memStream = new MemoryStream(byteArray))
            using (var sStream = new SearchableStringStream(memStream))
            {
                sStream.ReadLines(-1);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ZeroLines()
        {
            var byteArray = Encoding.ASCII.GetBytes("0123456789");

            using(var memStream = new MemoryStream(byteArray))
            using (var sStream = new SearchableStringStream(memStream))
            {
                sStream.ReadLines(0);
            }
        }

        [TestMethod]
        public void LessLinesThanAskedFor()
        {
            var byteArray = Encoding.ASCII.GetBytes("0123456789");

            using(var memStream = new MemoryStream(byteArray))
            using (var sStream = new SearchableStringStream(memStream))
            {
                var result = sStream.ReadLines(10);

                Assert.AreEqual("0123456789", result);
            }
        }

        [TestMethod]
        public void FirstLineOfMany()
        {
            var byteArray = Encoding.ASCII.GetBytes("0\n1\n2\n3\n4\n5\n6\n7\n8\n9");

            using(var memStream = new MemoryStream(byteArray))
            using (var sStream = new SearchableStringStream(memStream))
            {
                var result = sStream.ReadLines(1);

                Assert.AreEqual("0\n", result);
            }
        } 

        [TestMethod]
        public void StartPostionPastFirstLine_GetOne()
        {
            var byteArray = Encoding.ASCII.GetBytes("0\n1\n2\n3\n4\n5\n6\n7\n8\n9");

            using(var memStream = new MemoryStream(byteArray))
            using (var sStream = new SearchableStringStream(memStream))
            {
                sStream.Position = 2;
                var result = sStream.ReadLines(1);

                Assert.AreEqual("1\n", result);
            }
        }

        [TestMethod]
        public void GetThreeOfMany()
        {
            var byteArray = Encoding.ASCII.GetBytes("0\n1\n2\n3\n4\n5\n6\n7\n8\n9");

            using(var memStream = new MemoryStream(byteArray))
            using (var sStream = new SearchableStringStream(memStream))
            {
                var result = sStream.ReadLines(3);

                Assert.AreEqual("0\n1\n2\n", result);
            }
        }

        [TestMethod]
        public void StartPostionPastFirstLine_GetThree()
        {
            var byteArray = Encoding.ASCII.GetBytes("0\n1\n2\n3\n4\n5\n6\n7\n8\n9");

            using(var memStream = new MemoryStream(byteArray))
            using (var sStream = new SearchableStringStream(memStream))
            {
                sStream.Position = 2;
                var result = sStream.ReadLines(3);

                Assert.AreEqual("1\n2\n3\n", result);
            }
        }

        [TestMethod]
        public void StartPostionPastFirstLine_GetThree_CRLF()
        {
            var byteArray = Encoding.ASCII.GetBytes("0\n1\n2\n3\n4\n5\n6\n7\n8\n9");

            using(var memStream = new MemoryStream(byteArray))
            using (var sStream = new SearchableStringStream(memStream))
            {
                sStream.Position = 2;
                var result = sStream.ReadLines(3);

                Assert.AreEqual("1\r\n2\r\n3\r\n", result);
            }
        }

    }
}
