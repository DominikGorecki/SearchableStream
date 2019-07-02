using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void StartPositionSameAsEnd()
        {

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void StartPositionNegative()
        {

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EndPositionLargerThanStream()
        {

        }

        [TestMethod]
        public void EmptyStream()
        {
            var emptyByteArray = new byte[0];
        }

        [TestMethod]
        public void ReadWholeStream()
        {

        }

        [TestMethod]
        public void ReadAtBeginning()
        {

        }

        [TestMethod]
        public void ReadAtEnd()
        {

        }

        [TestMethod]
        public void ReadInMiddle()
        {

        }

    }
}
