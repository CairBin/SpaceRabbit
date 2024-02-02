using Microsoft.AspNetCore.Identity;
using DomainCommons.Models;


namespace IdentityService.Domain.Entity
{
    public class User : IdentityUser<Guid>, IHasCreationTime, IHasDeletionTime, IHasModificationTime, ISoftDelete
    {
        public DateTime Created { get; init; }
        public DateTime Updated { get; private set; }
        public DateTime? Deleted { get;private set; }

        public bool IsDeleted { get; private set; }

        public string? Email { get;private set; }

        public User(string username):base(username)
        {
            this.Created = DateTime.Now;
            this.Updated = DateTime.Now;
            this.IsDeleted = false;
        }

        private User() { }

        public User(string username,string email):base(username)
        {
            this.Created = DateTime.Now;
            this.Updated = DateTime.Now;
            this.IsDeleted = false;
            this.Email = email;
        }

        public void SoftDelete()
        {
            this.IsDeleted = true;
            this.Deleted = DateTime.Now;
        }

        public void ChangeEmail(string email)
        {
            this.Email = email;
        }

    }
}
