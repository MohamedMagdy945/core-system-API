using MediatR;

namespace UniversitySystem.Application.Features.Business.Department.Queries.GetDepartments
{
    public class GetDepartmentsQuery : IRequest<List<DepartmentSummaryResponse>>
    {

    }
}
