using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AppCoreSystem.Application.Common.Bases;
using AppCoreSystem.Application.Features.Business.Students.Models;
using AppCoreSystem.Application.Interfaces;

namespace AppCoreSystem.Application.Features.Business.Students.Queries.GetStudentList
{
    public record GetStudentListQuery : IRequest<Response<List<StudentItemDto>>>;

    public class GetStudentListHandler : IRequestHandler<GetStudentListQuery, Response<List<StudentItemDto>>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public GetStudentListHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<List<StudentItemDto>>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.Students
              .AsNoTracking()
              .ProjectTo<StudentItemDto>(_mapper.ConfigurationProvider)
              .ToListAsync(cancellationToken);


            if (!data.Any())
                return ResponseHandler.Failure<List<StudentItemDto>>(
                    "No students found",
                    statusCode: 404
                    );
            return ResponseHandler.Success(data);
        }
    }
}
