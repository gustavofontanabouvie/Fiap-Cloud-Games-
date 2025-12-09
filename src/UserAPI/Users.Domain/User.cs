using System.ComponentModel.DataAnnotations;
using static Users.Domain.Constants;

namespace Users.Domain
{
    public class User
    {
        [Key]
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string Role { get; private set; }
        public DateOnly BirthDate { get; private set; }

        protected User() { }

        public User(string name, string email, string passwordHash, string role, DateOnly birthDate)
        {
            ValidateDomain(name, email, passwordHash, role);

            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            BirthDate = birthDate;
        }


        private static void ValidateDomain(string name, string email, string passwordHash, string role)
        {
            if (string.IsNullOrEmpty(name))
                throw new DomainException("Name is mandatory");

            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("Email is mandatory");

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new DomainException("PasswordHash is mandatory");

            if (string.IsNullOrWhiteSpace(role) || !Roles.IsValidRole(role))
                throw new DomainException("Role must be either 'admin' or 'user'");
        }
    }

    public class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }
}
