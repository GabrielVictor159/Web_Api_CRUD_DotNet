using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace gcsb.ecommerce.domain.Client
{
    public class Client : Entity
    {
        public Guid Id { get; private set; }
        public String? Name { get; private set; }
        public String? Password { get; private set; }
        public String? Role { get; private set; }

        public Client(String? Name, String? Password, String? Role)
        {
            this.Id = Guid.NewGuid();
            this.Name = Name;
            this.Role = Role;

            ValidateEntity();
            
            if (Password != null)
            {
                SetPassword(Password);
            }
        }

        public Client(Guid id, String? Name, String? Password, String? Role)
        {
            this.Id = id;
            this.Name = Name;
            this.Role = Role;

            ValidateEntity();

            if (Password != null)
            {
                SetPassword(Password);
            }
        }

        public void SetName(String name)
        {
            this.Name = name;
            ValidateEntity();
        }

        public void SetRole(String role)
        {
            this.Role = role;
            ValidateEntity();
        }

        public void SetId(Guid id)
        {
            this.Id = id;
            ValidateEntity();
        }

        public void SetPassword(string newPassword)
        {
            if (newPassword.Length < 8)
            {
                AddValidationResultItem(new ValidationFailure("Password too short", "The password attribute must be at least 8 digits long."));
            }
            else
            {
                this.Password = Cryptography.Cryptography.md5Hash(newPassword);
            }
        }

        public void SetPasswordNotCryptography(string newPassword)
        {
            if (newPassword.Length < 8)
            {
                AddValidationResultItem(new ValidationFailure("Password too short", "The password attribute must be at least 8 digits long."));
            }
            else
            {
                this.Password = newPassword;
            }
        }

        public void ValidateEntity()
        {
            Validate(this, new domain.Validator.Client.ClientValidator());
        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Password: {Password}, Role: {Role}";
        }
    }
}
