using MediatR;
using AppCoreSystem.Application.Common.Interfaces;

namespace AppCoreSystem.Application.Features.Business.Department.Queries.GetDepartments
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
