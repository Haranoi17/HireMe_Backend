using HireMe_Backend.Models.DTOS;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HireMe_Backend.Controllers.UserValidators
{
    public class RegisterUserValidator
    {
        ApplicationDbContext dbContext;

        public RegisterUserValidator(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool isUserValid(RegisterUserDto registerDto)
        {
            bool validationResult = isEmailUnique(registerDto.Email);
            validationResult &= isNameUnique(registerDto.Name);
            validationResult &= isPasswordValid(registerDto.Password);
            return validationResult;
        }

        public Dictionary<string, bool> getMessages(RegisterUserDto registerDto)
        {
            return new Dictionary<string, bool>() { 
                { "email", isEmailUnique(registerDto.Email) }, 
                { "name", isNameUnique(registerDto.Name) }, 
                { "password", isPasswordValid(registerDto.Password) } };
        }

        private bool isEmailUnique(string email)
        {
            return !dbContext.users.Any(user => user.Email == email);
        }

        private bool isNameUnique(string name)
        {
            return !dbContext.users.Any(user => user.Name == name);
        }

        private bool isPasswordValid(string password)
        {
            return password.Length > 4;
        }
    }
}
