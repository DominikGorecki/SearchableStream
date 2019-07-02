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
            if (start < 0) throw new ArgumentOutOfRangeException(nameof(start), "Start must not be negative");

            if (end <= start) throw new ArgumentOutOfRangeException(nameof(end), "End of read must be larger than start");

            if (start > this.Length) throw new ArgumentOutOfRangeException(nameof(start), "Start position cannot be larger than the stream");

            if (end > this.Length) throw new ArgumentOutOfRangeException(nameof(end), "End position cannot be larger than the stream");

            throw new NotImplementedException();
        }
    }
}
