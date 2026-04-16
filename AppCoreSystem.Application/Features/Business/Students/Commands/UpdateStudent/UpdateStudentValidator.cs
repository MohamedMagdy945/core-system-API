using FluentValidation;
using AppCoreSystem.Application.Interfaces;

namespace AppCoreSystem.Application.Features.Business.Students.Commands.UpdateStudent
{
    public class UpdateStudentValidator : AbstractValidator<UpdateStudentCommand>
    {
        private readonly IAppDbContext _context;
        public UpdateStudentValidator(IAppDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Student ID is required.");

            RuleFor(x => x.DepartmentId)
                .NotEmpty().WithMessage("Department ID is required.");
        }
    }
}
