using AiKismet.SearchableStream.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AiKismet.SearchableStream
{
    public class SearchableStringStream : SearchableStream, ISearchableStringStream
    {
        private readonly Encoding _encoding;

        public SearchableStringStream(Stream stream) : base(stream)
        {
            _encoding = Encoding.ASCII;
        }
        public SearchableStringStream(Stream stream, Encoding stringEncoding) : base(stream)
        {
            _encoding = stringEncoding;
        }

        public long IndexOf(string needle)
        {
            throw new NotImplementedException();
        }

        public long[] IndexOfAll(string needle, int maxNumberOfPositions = 0)
        {
            throw new NotImplementedException();
        }

        public long[] IndexOfAllBackwards(string needle, int maxNumberOfPositions = 0)
        {
            throw new NotImplementedException();
        }

        public long LastIndexOf(string needle)
        {
            throw new NotImplementedException();
        }

        public string ReadLines(int totalLines)
        {
            throw new NotImplementedException();
        }

        public string ReadStringInRange(long start, long end)
        {
            throw new NotImplementedException();
        }
    }
}
