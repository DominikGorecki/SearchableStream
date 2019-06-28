using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AiKismet.SearchableStream;

namespace SeekableStreamTests
{
    [TestClass]
    public class Constructor
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullStream()
        {
            // Arrange
            var needleByteArray = Encoding.ASCII.GetBytes("needle");

            using(var seekableStream = new SearchableStream(null)) { }
        }
        
    }
}
