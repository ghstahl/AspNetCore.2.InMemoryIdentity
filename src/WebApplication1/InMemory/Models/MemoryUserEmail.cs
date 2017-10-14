using System;

namespace WebApplication1.InMemory.Models
{
    public class MemoryUserEmail : MemoryUserContactRecord
    {
        public MemoryUserEmail(string email) : base(email)
        {
        }

        public string NormalizedValue { get; private set; }

        public virtual void SetNormalizedEmail(string normalizedEmail)
        {
            if (normalizedEmail == null)
            {
                throw new ArgumentNullException(nameof(normalizedEmail));
            }

            NormalizedValue = normalizedEmail;
        }
    }
}