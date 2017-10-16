using System;
using System.Collections.Generic;
using System.Security.Claims;
using AspNetCore2.Authentication.InMemoryStores.Models;

namespace AspNetCore2.Authentication.InMemoryStores
{
    public class MemoryIdentityUser
    {
        private List<MemoryUserClaim> _claims;
        private List<MemoryUserLogin> _logins;
        private List<MemoryUserToken> _tokens;

        public MemoryIdentityUser(string userName, string email) : this(userName)
        {
            if (email != null)
            {
                EmailContainer = new MemoryUserEmail(email);
            }
        }

        public MemoryIdentityUser(string userName, MemoryUserEmail email) : this(userName)
        {
            if (email != null)
            {
                EmailContainer = email;
            }
        }

        public MemoryIdentityUser(string userName):this()
        {
            if (userName == null)
            {
                throw new ArgumentNullException(nameof(userName));
            }

            UserName = userName;
            CreatedOn = new Occurrence();

            EnsureClaimsIsSet();
            EnsureLoginsIsSet();
        }
        public MemoryIdentityUser() { Id = Guid.NewGuid().ToString(); }

        public string Id { get; private set; }
        public string UserName { get;  set; }
        public string NormalizedUserName { get; private set; }
        public MemoryUserEmail EmailContainer { get;  set; }

        public string Email
        {
            get { return EmailContainer?.NormalizedValue; }
            set { SetEmail(value);}
        }

        public MemoryUserPhoneNumber PhoneNumberContainer { get; private set; }
        public string PhoneNumber
        {
            get { return PhoneNumberContainer?.Value; }
            set { SetPhoneNumber(value); }
        }

        public bool EmailConfirmed
        {
            get { return EmailContainer == null ? false : EmailContainer.IsConfirmed(); }
        }

        public string PasswordHash { get; private set; }
        public string SecurityStamp { get; private set; }
        public bool TwoFactorEnabled { get; private set; }

        public IEnumerable<MemoryUserClaim> Claims
        {
            get
            {
                EnsureClaimsIsSet();
                return _claims;
            }

            // ReSharper disable once UnusedMember.Local, MemoryDB serialization needs private setters
            private set
            {
                EnsureClaimsIsSet();
                if (value != null)
                {
                    _claims.AddRange(value);
                }
            }
        }

        public IEnumerable<MemoryUserLogin> Logins
        {
            get
            {
                EnsureLoginsIsSet();
                return _logins;
            }

            // ReSharper disable once UnusedMember.Local, MemoryDB serialization needs private setters
            private set
            {
                EnsureLoginsIsSet();
                if (value != null)
                {
                    _logins.AddRange(value);
                }
            }
        }
        public IEnumerable<MemoryUserToken> Tokens
        {
            get
            {
                EnsureTokensIsSet();
                return _tokens;
            }

            // ReSharper disable once UnusedMember.Local, MemoryDB serialization needs private setters
            private set
            {
                EnsureTokensIsSet();
                if (value != null)
                {
                    _tokens.AddRange(value);
                }
            }
        }

      

        public int AccessFailedCount { get; private set; }
        public bool IsLockoutEnabled { get; private set; }
        public FutureOccurrence LockoutEndDate { get; private set; }

        public Occurrence CreatedOn { get; private set; }
        public Occurrence DeletedOn { get; private set; }

        public virtual void EnableTwoFactorAuthentication()
        {
            TwoFactorEnabled = true;
        }

        public virtual void DisableTwoFactorAuthentication()
        {
            TwoFactorEnabled = false;
        }

        public virtual void EnableLockout()
        {
            IsLockoutEnabled = true;
        }

        public virtual void DisableLockout()
        {
            IsLockoutEnabled = false;
        }

        public virtual void SetEmail(string email)
        {
            var MemoryUserEmail = new MemoryUserEmail(email);
            SetEmail(MemoryUserEmail);
        }

        public virtual void SetEmail(MemoryUserEmail MemoryUserEmail)
        {
            EmailContainer = MemoryUserEmail;
        }

        public virtual void SetNormalizedUserName(string normalizedUserName)
        {
            if (normalizedUserName == null)
            {
                throw new ArgumentNullException(nameof(normalizedUserName));
            }

            NormalizedUserName = normalizedUserName;
        }

        public virtual void SetPhoneNumber(string phoneNumber)
        {
            var MemoryUserPhoneNumber = new MemoryUserPhoneNumber(phoneNumber);
            SetPhoneNumber(MemoryUserPhoneNumber);
        }

        public virtual void SetPhoneNumber(MemoryUserPhoneNumber MemoryUserPhoneNumber)
        {
            PhoneNumberContainer = MemoryUserPhoneNumber;
        }

        public virtual void SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }

        public virtual void SetSecurityStamp(string securityStamp)
        {
            SecurityStamp = securityStamp;
        }

        public virtual void SetAccessFailedCount(int accessFailedCount)
        {
            AccessFailedCount = accessFailedCount;
        }

        public virtual void ResetAccessFailedCount()
        {
            AccessFailedCount = 0;
        }

        public virtual void LockUntil(DateTime lockoutEndDate)
        {
            LockoutEndDate = new FutureOccurrence(lockoutEndDate);
        }

        public virtual void AddClaim(Claim claim)
        {
            if (claim == null)
            {
                throw new ArgumentNullException(nameof(claim));
            }

            AddClaim(new MemoryUserClaim(claim));
        }

        public virtual void AddClaim(MemoryUserClaim MemoryUserClaim)
        {
            if (MemoryUserClaim == null)
            {
                throw new ArgumentNullException(nameof(MemoryUserClaim));
            }
            EnsureClaimsIsSet();
            _claims.Add(MemoryUserClaim);
        }

        public virtual void RemoveClaim(MemoryUserClaim MemoryUserClaim)
        {
            if (MemoryUserClaim == null)
            {
                throw new ArgumentNullException(nameof(MemoryUserClaim));
            }
            EnsureClaimsIsSet();
            _claims.Remove(MemoryUserClaim);
        }
        public virtual void AddToken(MemoryUserToken memoryUserToken)
        {
            if (memoryUserToken == null)
            {
                throw new ArgumentNullException(nameof(memoryUserToken));
            }

            _tokens.Add(memoryUserToken);
        }
        public virtual void RemoveToken(MemoryUserToken memoryUserToken)
        {
            if (memoryUserToken == null)
            {
                throw new ArgumentNullException(nameof(memoryUserToken));
            }

            _tokens.Remove(memoryUserToken);
        }
        public virtual void AddLogin(MemoryUserLogin memoryUserLogin)
        {
            if (memoryUserLogin == null)
            {
                throw new ArgumentNullException(nameof(memoryUserLogin));
            }

            _logins.Add(memoryUserLogin);
        }

        public virtual void RemoveLogin(MemoryUserLogin memoryUserLogin)
        {
            if (memoryUserLogin == null)
            {
                throw new ArgumentNullException(nameof(memoryUserLogin));
            }

            _logins.Remove(memoryUserLogin);
        }

        public void Delete()
        {
            if (DeletedOn != null)
            {
                throw new InvalidOperationException($"User '{Id}' has already been deleted.");
            }

            DeletedOn = new Occurrence();
        }

        private void EnsureClaimsIsSet()
        {
            if (_claims == null)
            {
                _claims = new List<MemoryUserClaim>();
            }
        }

        private void EnsureLoginsIsSet()
        {
            if (_logins == null)
            {
                _logins = new List<MemoryUserLogin>();
            }
        }
        private void EnsureTokensIsSet()
        {
            if (_tokens == null)
            {
                _tokens = new List<MemoryUserToken>();
            }
        }

        
    }
}