using System;
using System.Collections.Generic;
using System.Text;

namespace AiKismet.SearchableStream.Interfaces
{
    public interface ISearchableStringStream
    {
        long IndexOf(string needle);
        long[] IndexOfAll(string needle, int maxNumberOfPositions = 0);
        long LastIndexOf(string needle);
        long[] IndexOfAllBackwards(string needle, int maxNumberOfPositions = 0);
        string ReadStringInRange(long start, long end);
        string ReadLines(int totalLines);
    }
}
