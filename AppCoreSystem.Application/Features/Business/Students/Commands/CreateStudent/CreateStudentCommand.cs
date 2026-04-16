using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AppCoreSystem.Application.Common.Bases;
using AppCoreSystem.Application.Common.Exceptions;
using AppCoreSystem.Domain.Entities.Business;
using AppCoreSystem.Application.Interfaces;

namespace AppCoreSystem.Application.Features.Business.Students.Commands.CreateStudent
{
    public record CreateStudentCommand(string Name, string PersonalEmail, int Level, int DepartmentId) : IRequest<Response<object>>;
    public class CreateStudentHandler : IRequestHandler<CreateStudentCommand, Response<object>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public CreateStudentHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response<object>> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {

            var departmentExists = await _context.Departments
                .AnyAsync(d => d.Id == request.DepartmentId, cancellationToken);

            if (!departmentExists)
                throw new NotFoundAppException("Department", request.DepartmentId);

            var emailExists = await _context.Students
            .AnyAsync(x => x.PersonalEmail == request.PersonalEmail, cancellationToken);

            if (emailExists)
                return ResponseHandler.Failure<object>("Email already exists");

            var studentEntity = _mapper.Map<Student>(request);


            var result = await _context.Students.AddAsync(studentEntity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return ResponseHandler.Success<object>(new { Id = studentEntity.Id }, message: "Student Created Successful", statusCode: StatusCodes.Status201Created);
        }
    }
}
