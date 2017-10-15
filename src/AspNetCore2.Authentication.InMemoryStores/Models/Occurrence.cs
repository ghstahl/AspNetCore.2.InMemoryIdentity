using System;
using System.Diagnostics.CodeAnalysis;

namespace AspNetCore2.Authentication.InMemoryStores.Models
{
    public class Occurrence
    {
        public Occurrence() : this(DateTime.UtcNow)
        {
        }

        public Occurrence(DateTime occuranceInstantUtc)
        {
            Instant = occuranceInstantUtc;
        }

        public DateTime Instant { get; private set; }

        protected bool Equals(Occurrence other)
        {
            return Instant.Equals(other.Instant);
        }

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(null, obj)) return false;
            if (Object.ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            return Equals((Occurrence)obj);
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode", Justification = "MongoDB serialization needs private setters")]
        public override int GetHashCode()
        {
            return Instant.GetHashCode();
        }
    }
}