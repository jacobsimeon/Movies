namespace Movies.Validators
{
    using System.Linq;

    using FluentValidation;

    using Movies.DataAccess;
    using Movies.Models;

    public class UserValidator : AbstractValidator<User>
    {
        private IDataContext _DataContext;

        public UserValidator(IDataContext ctx)
        {
            _DataContext = ctx;
            RuleFor(u => u.Name)
                .NotEmpty()
                .Must(username => !ctx.Users.Where(u => u.Name == username).Any());
        }
    }
}