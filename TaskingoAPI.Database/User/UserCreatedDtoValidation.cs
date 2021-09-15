using FluentValidation;
using System.Linq;
using TaskingoAPI.Database;

namespace TaskingoAPI.Dto.User
{
    public class UserCreatedDtoValidation : AbstractValidator<UserCreatedDto>
    {
        private const string regex = @"[0-9]{9}";

        public UserCreatedDtoValidation(TaskingoDbContext warehouseDbContext)
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.Role).NotEmpty();
            RuleFor(x => x.PasswordHashed).MinimumLength(8);
            RuleFor(x => x.Phone.ToString()).Matches(regex);
            RuleFor(x => x.Email).Custom((value, context) =>
            {
                var email = warehouseDbContext.Users.Any(x => x.Email == value);
                if (email)
                    context.AddFailure("email was taken.");
            });
        }
    }
}
