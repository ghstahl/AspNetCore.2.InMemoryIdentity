namespace WebApplication1.InMemory.Models
{
    public class MemoryUserPhoneNumber : MemoryUserContactRecord
    {
        public MemoryUserPhoneNumber(string phoneNumber) : base(phoneNumber)
        {
        }
    }
}