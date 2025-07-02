using SharedKernel; 

namespace UserService.Domain.Aggregates.UsersAggregates
{
    public class Users : AggregateRoot<int>
    {  
        public Guid AccountId { get; set; }  
        public string Username { get; set; }   
        public string TenantId { get; set; }   

        public static Users Create(Guid accountId, string username, string tenantId)
        {
            var user = new Users
            { 
                 
                AccountId = accountId,
                Username = username, 
                TenantId = tenantId
            };
              
            user.ValidateUser();

            return user;
        }

        public void ChangePersonalInfo(string username)
        {
            Username = username; 

            ValidateUser();
        }

        public void ToggleUserActive(ref bool isActive)
        {
            isActive = !isActive;

            ValidateUser();
        }

        private void ValidateUser()
        {
            if (string.IsNullOrWhiteSpace(Username))
                ThrowDomainException("Username is required.");

            if (Username.Length < 6 || Username.Length > 30)
                ThrowDomainException("Username must be between 6 and 30 characters.");

            if (!Username.All(char.IsLetterOrDigit))
                ThrowDomainException("Username must be alphanumeric only.");

            if (string.IsNullOrWhiteSpace(TenantId))
                ThrowDomainException("TenantId is required.");
        }
    }
}
