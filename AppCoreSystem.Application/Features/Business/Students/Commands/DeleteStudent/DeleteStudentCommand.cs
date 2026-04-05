using MediatR;
using AppCoreSystem.Application.Common.Bases;
using AppCoreSystem.Application.Common.Exceptions;
using AppCoreSystem.Application.Common.Interfaces;

namespace AppCoreSystem.Application.Features.Students.Commands.DeleteStudent
{
    public record DeleteStudentCommand(int Id) : IRequest<Response<object>>;
    public class DeleteStudentHandler : IRequestHandler<DeleteStudentCommand, Response<object>>
    {
        private readonly IAppDbContext _context;

        public DeleteStudentHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Response<object>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            if (request.Id < 0) throw new ArgumentException("In valid Id ");

            var student = await _context.Students.FindAsync(request.Id, cancellationToken);

            if (student == null) throw new NotFoundAppException("Student", request.Id);

            _context.Students.Remove(student);

            await _context.SaveChangesAsync(cancellationToken);

            return ResponseHandler.Success<object>(new { Id = request.Id }, "Student deleted successfully");

        }
    }
}
