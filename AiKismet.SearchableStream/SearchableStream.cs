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
            throw new NotImplementedException();
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
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }


        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
            base.Close();
        }
    }
}
