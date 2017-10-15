namespace AspNetCore2.Authentication.InMemoryStores.Models
{
    public class MemoryUserPhoneNumber : MemoryUserContactRecord
    {
        public MemoryUserPhoneNumber(string phoneNumber) : base(phoneNumber)
        {
        }
    }
}