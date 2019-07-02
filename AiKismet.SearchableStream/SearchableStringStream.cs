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

        public string ReadStringInRange(long start, long end)
        {
            if (start < 0) throw new ArgumentOutOfRangeException(nameof(start), "Start must not be negative");

            if (end <= start) throw new ArgumentOutOfRangeException(nameof(end), "End of read must be larger than start");

            if (start > this.Length) throw new ArgumentOutOfRangeException(nameof(start), "Start position cannot be larger than the stream");

            if (end > this.Length) throw new ArgumentOutOfRangeException(nameof(end), "End position cannot be larger than the stream");

            var stringLength = end - start + 1; //Include las char
            var buffer = new byte[stringLength];

            var bufStream = new BufferedStream(this);
            bufStream.Seek(start, SeekOrigin.Begin);
            bufStream.Read(buffer);

            return _encoding.GetString(buffer);
        }
        public string ReadLines(int totalLines)
        {
            if (totalLines <= 0) throw new ArgumentOutOfRangeException(nameof(totalLines), "totalLines must be a positive number");

            throw new NotImplementedException();
        }

        private bool IsEndOfLineByte(out char currentChar)
        {
            // There are three legal possibilities for end-of-line. They are, in hex: 0x200A, 0x200D, or 0x0D0A
            var eol200A = int.Parse("200A", System.Globalization.NumberStyles.HexNumber);
            var eol200D = int.Parse("200D", System.Globalization.NumberStyles.HexNumber);
            var eol0D = int.Parse("0D", System.Globalization.NumberStyles.HexNumber); // \r
            var eol0A = int.Parse("0A", System.Globalization.NumberStyles.HexNumber); // \n
            var currentByte = new byte[1];

            this.Read(currentByte, 0, 1);
            var foundEOL = false;
            if (currentByte[0] == eol0D) // 0D must be followed by 0A
            {
                this.Read(currentByte, 0, 1);
                if (currentByte[0] == eol0A)
                    foundEOL = true;
                else
                    this.Position--;
            }
            else if (currentByte[0] == eol200A || currentByte[0] == eol200D || currentByte[0] == eol0A)
                foundEOL = true;

            currentChar = foundEOL ? '\n' : _encoding.GetChars(currentByte)[0];

            //nextChar = ascii.GetString(currentByte)[0];
            return foundEOL;
        }

    }
}
