using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AppCoreSystem.Application.Common.Bases;
using AppCoreSystem.Application.Common.Exceptions;
using AppCoreSystem.Application.Common.Interfaces;
using AppCoreSystem.Application.Features.Business.Students.Models;

namespace AppCoreSystem.Application.Features.Business.Students.Queries.GetStudentById
{
    public record GetStudentByIdQuery(int Id) : IRequest<Response<StudentItemDto>>;

    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, Response<StudentItemDto>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public GetStudentByIdQueryHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response<StudentItemDto>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var studentDto = await _context.Students
                .Where(s => s.Id == request.Id)
                .ProjectTo<StudentItemDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            if (studentDto is null)
            {
                throw new NotFoundAppException("Student", request.Id);

            }
            return ResponseHandler.Success(studentDto);
        }
    }
}
