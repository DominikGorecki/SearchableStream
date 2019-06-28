using System;
using System.Collections.Generic;
using System.Text;

namespace AiKismet.SearchableStream.Interfaces
{
    public interface ISearchableStream
    {
        void ReadBackwards(byte[] buffer, int offset, int count);
        long IndexOf(byte[] needle);
        long[] IndexOfAll(byte[] needle, int maxNumberOfPositions = 0);
        long LastIndexOf(byte[] needle);
        long[] IndexOfAllBackwards(byte[] needle, int maxNumberOfPositions = 0);
    }
}
