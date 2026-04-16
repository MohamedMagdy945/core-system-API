using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AppCoreSystem.Application.Common.Bases;
using AppCoreSystem.Application.Common.Exceptions;
using AppCoreSystem.Application.Interfaces;

namespace AppCoreSystem.Application.Features.Business.Students.Commands.UpdateStudent
{
    public record UpdateStudentCommand(int Id, string? Name, string? PersonalEmail, int? Level, int DepartmentId) : IRequest<Response<object>>;

    public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand, Response<object>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public UpdateStudentHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response<object>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {

            var student = await _context.Students
              .FindAsync(request.Id, cancellationToken);

            if (student is null)
                throw new NotFoundAppException("Student", request.Id);

            var departmentExists = await _context.Departments
              .AnyAsync(d => d.Id == request.DepartmentId, cancellationToken);

            if (!departmentExists)
                throw new NotFoundAppException("Department", request.DepartmentId);

            var emailExists = await _context.Students
             .AnyAsync(x => x.PersonalEmail == request.PersonalEmail, cancellationToken);

            if (emailExists && request.PersonalEmail != student.PersonalEmail)
                return ResponseHandler.Failure<object>("Email already exists");

            var studentEntity = _mapper.Map(request, student);

            _context.Students.Update(studentEntity);



            await _context.SaveChangesAsync(cancellationToken);

            return ResponseHandler.Success<object>("Student updated successfully");
        }
    }
}
