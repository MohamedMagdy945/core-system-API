using FluentValidation;
using AppCoreSystem.Application.Common.Interfaces;

namespace AppCoreSystem.Application.Features.Business.Students.Commands.CreateStudent
{
    public class CreateStudentValidator : AbstractValidator<CreateStudentCommand>
    {
        private readonly IAppDbContext _context;
        public CreateStudentValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.PersonalEmail)
                .NotEmpty().WithMessage("Personal email is required.")
                .EmailAddress().WithMessage("Personal email must be a valid email address.");

            RuleFor(x => x.Level)
                .InclusiveBetween(1, 4).WithMessage("Level must be between 1 and 4.");
        }

    }

}
