using System;

namespace AspNetCore2.Authentication.InMemoryStores.Models
{
    public class FutureOccurrence : Occurrence
    {
        public FutureOccurrence()
        {
        }

        public FutureOccurrence(DateTime willOccurOn) : base(willOccurOn)
        {
        }
    }
}