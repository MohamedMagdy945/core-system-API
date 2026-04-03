using MediatR;
using UniversitySystem.Application.Interfaces;

namespace UniversitySystem.Application.Features.Business.Department.Queries.GetDepartments
{
    public class GetDepartmentsHandler : IRequestHandler<GetDepartmentsQuery, List<DepartmentSummaryResponse>>
    {
        private readonly IAppDbContext _context;
        public GetDepartmentsHandler(IAppDbContext context)
        {
            _context = context;
        }
        public Task<List<DepartmentSummaryResponse>> Handle(GetDepartmentsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
