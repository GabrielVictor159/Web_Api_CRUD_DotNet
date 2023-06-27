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
        public String? Password {get; private set; }
        public String? Role { get; private set; }
        public Client(String? Name, String? Password, String? Role)
        {
            this.Id = Guid.NewGuid();
            if(Name != null)
            {
            this.Name = Name;
            }
            if(Role!=null)
            {
            this.Role = Role;
            }
            Validate(this, new domain.Validator.Client.ClientValidator());
            if(Password!=null)
            {
            WithPassword(Password);
            }
        }
        public Client(Guid id,String? Name, String? Password, String? Role)
        {
            this.Id =id;
            if(Name!=null)
            {
            this.Name = Name;
            }
            if(Role!=null)
            {
            this.Role = Role;
            }
            Validate(this, new domain.Validator.Client.ClientValidator());
            if(Password!=null)
            {
            WithPassword(Password);
            }
        }
        public void WithName(String name)
        {
            this.Name = name;
            Validate(this, new domain.Validator.Client.ClientValidator());
        }
        public void WithRole(String role)
        {
            this.Role = role;
            Validate(this, new domain.Validator.Client.ClientValidator());
        }
        public void WithId(Guid id)
        {
            this.Id = id;
            Validate(this, new domain.Validator.Client.ClientValidator());
        }
        public void WithPassword(string newPassword)
        {
            if(newPassword.Length<8)
            {
                AddValidationResultItem(new ValidationFailure("Password too short","The password attribute must be at least 8 digits long."));
            }
            else{
                this.Password = Cryptography.Cryptography.md5Hash(newPassword);
            }
        }
        public void WithPasswordNotCryptography(string newPassword)
        {
            if(newPassword.Length<8)
            {
                AddValidationResultItem(new ValidationFailure("Password too short","The password attribute must be at least 8 digits long."));
            }
            else{
                this.Password = newPassword;
            }
        }
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Password: {Password}, Role: {Role}";
        }

    }
}