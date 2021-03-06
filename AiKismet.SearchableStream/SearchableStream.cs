﻿using AiKismet.SearchableStream.Interfaces;
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
            => FindSinglePositionUsing(IndexOfAll, needle);
        
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
            _stream.Seek(0, SeekOrigin.End);
            return FindSinglePositionUsing(IndexOfAllBackwards, needle);
        }

        private long FindSinglePositionUsing(Func<byte[], int, long[]> allFunc, byte[] needle)
        {
            var foundPositions = allFunc(needle, 1);
            if (foundPositions.Length == 0) return -1;
            else return foundPositions[0];
        }

        public long[] IndexOfAllBackwards(byte[] needle, int maxNumberofPositions = 0)
        {
            needle = needle?.Reverse().ToArray() ?? throw new ArgumentNullException(nameof(needle), "The search string cannot be null");

            if (needle.Length == 0) throw new ArgumentOutOfRangeException(nameof(needle), "The search string cannot be empty");

            var findAll = maxNumberofPositions == 0;
            var foundPositions = new List<long>();

            if (_stream.Position == 0)
                return foundPositions.ToArray();

            _stream.Position -= 1;

            var matchingBytes = 0;
            var newPosition = _stream.Position;

            while(_stream.Position >= 0 && newPosition != -1)
            {
                _stream.Position = newPosition;
                int readByte = _stream.ReadByte();
                if (needle[matchingBytes] == readByte) // Next byte matchhes current read
                {
                    if (matchingBytes == needle.Length - 1)
                    {
                        matchingBytes = 0;
                        foundPositions.Add(_stream.Position - 1);
                        if (!findAll && foundPositions.Count == maxNumberofPositions) break;
                    }
                    else 
                        matchingBytes++;
                }
                else // we didn't find the whole "string"--sequence
                {
                    matchingBytes = 0;
                }
                newPosition =_stream.Position - Math.Min(_stream.Position + 1, 2); 
            }

            return foundPositions.ToArray();
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
