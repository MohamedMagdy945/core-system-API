using MediatR;

namespace AppCoreSystem.Application.Features.Business.Department.Queries.GetDepartments
{
    public class GetDepartmentsQuery : IRequest<List<DepartmentSummaryResponse>>
    {

    }
}
