using System;

namespace AspNetCore2.Authentication.InMemoryStores.Models
{
    public class MemoryUserToken : IEquatable<MemoryUserToken>
    {
        public MemoryUserToken()
        {
            
        }
        public MemoryUserToken(string loginProvider, string name, string value)
        {
            LoginProvider = loginProvider;
            Name = name;
            Value = value;
        }
        public string LoginProvider { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public bool Equals(MemoryUserToken other)
        {
            return other.LoginProvider.Equals(LoginProvider)
                   && other.Name.Equals(Name)
                   && other.Value.Equals(Value);
        }
    }
}