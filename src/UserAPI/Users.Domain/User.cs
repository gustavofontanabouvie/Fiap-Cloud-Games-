using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Users.Domain.Constants;

namespace Users.Domain
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string Role { get; set; }

        public DateOnly BirthDate { get; set; }

        protected User() { }

        public User(string name, string email, string passwordHash, string role, DateOnly birthDate)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            BirthDate = birthDate;
        }

        private void ValidateDomain(string name, string email, string passwordHash, string role)
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

    internal class DomainException : Exception
    {
        public DomainException(string message) : base(message) { }
    }
}
