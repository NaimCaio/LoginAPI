using LoginAPI.Model.Entity;
using Microsoft.AspNet.Identity;
using BCryptNet = BCrypt.Net.BCrypt;

namespace LoginAPI.Model
{
    public class Password
    {
        public int _minPass=6;
        public bool _needDigt = true;
        public bool _needNumber = true;
        public string HashPassword( string password)
        {
            var hash = BCryptNet.HashPassword(password);
            return hash;
        }

        public PasswordVerificationResult VerifyHashedPassword(
          string loginAccountPassword, string baseAccountPassword)
        {
            var auth = BCryptNet.Verify(loginAccountPassword, baseAccountPassword);

            return auth ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }

        public PasswordVerificationResult PasswordValidation(
          string password)
        {
            var validator = new PasswordValidator();
            validator.RequireDigit = true;
            validator.RequireLowercase = true;
            validator.RequiredLength = 6;
            var result = validator.ValidateAsync(password).Result;
            var isValid = false;
            if (result.Succeeded)
            {
                isValid = true;
            }
            return isValid ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }
    }
}
