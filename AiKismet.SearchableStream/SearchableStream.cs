using AiKismet.SearchableStream.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AiKismet.SearchableStream
{
    public class SearchableStream : Stream, ISearchableStream
    {
        private readonly Stream _stream;

        public override bool CanRead => _stream.CanRead;

        public override bool CanSeek => _stream.CanSeek;

        public override bool CanWrite => _stream.CanWrite;

        public override long Length => _stream.Length; 

        public override long Position
        {
            get => _stream.Position;
            set => _stream.Position = value; 
        }

        /// <summary>
        /// Wrapper for Stream object that gives some unique seeking functionality.
        /// </summary>
        /// <param name="stream"></param>
        public SearchableStream(Stream stream)
        {
            _stream = stream ?? throw new ArgumentNullException("Stream object cannot be null");

            if (!stream.CanSeek) throw new ArgumentException("Stream must be able to seek. CanSeek must be true.");
            if (!stream.CanRead) throw new ArgumentNullException("Stream must be readable. CanRead must be true");
        }

        public void ReadBackwards(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer), "Buffer passed in cannot null");

            if (_stream.Position - count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Count of read is larger than there is left to read in stream.");

            if (count > buffer.Length - offset)
                throw new ArgumentOutOfRangeException(nameof(count), "Count of read is larger than the buffer minus the offset.");

            // To make it work similary as Read (forward) we need to go back one so we are reading the previous byte and not the current byte that wea re on.
            _stream.Position -= 1;

            for (var i = 0; i < count; i++)
            {
                buffer[offset + i] = (byte)_stream.ReadByte();
                _stream.Position -= Math.Min(_stream.Position, 2);
            }
        }

        /// <summary>
        /// Looks for a byte sequence and returns the first position of its occurance.
        /// </summary>
        /// <param name="needle">byte sequence to look for</param>
        /// <returns>position at which the sequence was found</returns>
        public long IndexOf(byte[] needle)
        {
            var foundPosition = IndexOfAll(needle, 1);
            if (foundPosition.Length == 0)
                return -1;

            return foundPosition.First();
        }

        public long[] IndexOfAll(byte[] needle, int maxNumberOfPositions = 0)
        {
            if (needle == null) throw new ArgumentNullException("Needle cannot be null");

            var findAll = maxNumberOfPositions == 0;
            var foundPositions = new List<long>();
            var buffer = new byte[needle.Length];
            var bufferSize = buffer.Length;

            using(var buffStream = new BufferedStream(_stream, buffer.Length))
            {
                while(buffStream.Read(buffer, 0, bufferSize) == bufferSize)
                {
                    if (needle.SequenceEqual(buffer))
                    {
                        foundPositions.Add(buffStream.Position - bufferSize);

                        if (!findAll && maxNumberOfPositions == foundPositions.Count) break;
                    }
                    else
                        buffStream.Position -= bufferSize - 1;
                }
            }

            return foundPositions.ToArray();
        }

        public long LastIndexOf(byte[] needle)
        {
            throw new NotImplementedException();
        }

        public long[] IndexOfAllBackwards(byte[] needle, int maxNumberofPositions = 0)
        {
            throw new NotImplementedException();
        }

        public override void Flush()
            => _stream.Flush();

        public override int Read(byte[] buffer, int offset, int count)
            => _stream.Read(buffer, offset, count);


        public override long Seek(long offset, SeekOrigin origin)
            => _stream.Seek(offset, origin);

        public override void SetLength(long value)
            => _stream.SetLength(value);

        public override void Write(byte[] buffer, int offset, int count)
            => _stream.Write(buffer, offset, count);

        public override void Close()
        {
            base.Close();
        }
    }
}
