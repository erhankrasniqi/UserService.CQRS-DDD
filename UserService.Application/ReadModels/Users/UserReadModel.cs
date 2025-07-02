
 

namespace UserService.Application.ReadModels.User
{
    public class UserReadModel
    {
        public int Id { get; set; }
        public Guid AccountId { get; set; }
        public string Username { get; set; }
        public string TenantId { get; set; }
    }
}
