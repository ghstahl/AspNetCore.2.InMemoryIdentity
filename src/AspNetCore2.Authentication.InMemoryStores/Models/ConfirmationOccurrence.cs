using System;

namespace AspNetCore2.Authentication.InMemoryStores.Models
{
    public class ConfirmationOccurrence : Occurrence
    {
        public ConfirmationOccurrence()
        {
        }

        public ConfirmationOccurrence(DateTime confirmedOn) : base(confirmedOn)
        {
        }
    }
}