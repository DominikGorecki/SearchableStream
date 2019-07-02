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
            => FindSinglePositionUsing(IndexOfAll
                , needle);

        private long FindSinglePositionUsing(Func<string, int, long[]> allFunc, string needle)
        {
            var foundPositions = allFunc(needle, 1);
            if (foundPositions.Length == 0) return -1;
            else return foundPositions[0];
        }

        public long[] IndexOfAll(string needle, int maxNumberOfPositions = 0)
            => IndexOfAll(_encoding.GetBytes(needle), maxNumberOfPositions);

        public long[] IndexOfAllBackwards(string needle, int maxNumberOfPositions = 0)
            => IndexOfAllBackwards(_encoding.GetBytes(needle), maxNumberOfPositions);

        public long LastIndexOf(string needle)
            => LastIndexOf(_encoding.GetBytes(needle));

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
