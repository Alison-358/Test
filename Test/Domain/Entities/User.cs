using System;

namespace Domain.Entities
{
    public class User
    {
        public User()
        {
            Role = new Role() { Id = RoleId };
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
        public string LastUpdateUser { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
