using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniversitySystem.Application.Common.Bases;
using UniversitySystem.Application.Common.Extensions;
using UniversitySystem.Application.Common.Wrappers;
using UniversitySystem.Application.Features.Business.Students.Models;
using UniversitySystem.Application.Interfaces;

namespace UniversitySystem.Application.Features.Business.Students.Queries.GetStudentListPaginated
{
    public record GetStudentListPaginatedQuery(int PageNumber, int PageSize) : IRequest<Response<PaginatedResult<StudentItemDto>>>;


    public class GetStudentListPaginatedHandler : IRequestHandler<GetStudentListPaginatedQuery,
        Response<PaginatedResult<StudentItemDto>>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public GetStudentListPaginatedHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<PaginatedResult<StudentItemDto>>> Handle(GetStudentListPaginatedQuery request, CancellationToken cancellationToken)
        {

            var query = _context.Students.AsNoTracking();

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .ApplyPagination(request.PageNumber, request.PageSize)
                .ProjectTo<StudentItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var result = new PaginatedResult<StudentItemDto>(items, totalCount, request.PageNumber, request.PageSize);

            return ResponseHandler.Success(result);

        }
    }
}
