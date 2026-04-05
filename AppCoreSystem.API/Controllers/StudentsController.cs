using AppCoreSystem.Application.Features.Business.Students.Commands.CreateStudent;
using AppCoreSystem.Application.Features.Business.Students.Commands.UpdateStudent;
using AppCoreSystem.Application.Features.Business.Students.Queries.GetStudentById;
using AppCoreSystem.Application.Features.Business.Students.Queries.GetStudentListPaginated;
using AppCoreSystem.Application.Features.Students.Commands.DeleteStudent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppCoreSystem.API.Controllers
{
    [Authorize]
    public class StudentsController : AppControllerBase
    {
        [HttpGet]
        //[HasPermission(Permissions.Students_View, Permissions.Students_Delete)]
        public async Task<IActionResult> GetStudents([FromQuery] GetStudentListPaginatedQuery request, CancellationToken cancellationToken)
        {
            var students = await _mediator.Send(request, cancellationToken);
            return NewResult(students);
        }
        //[HttpGet]
        //public async Task<IActionResult> GetAllStudents(CancellationToken cancellationToken)
        //{
        //    var students = await _mediator.Send(new GetStudentListQuery(), cancellationToken);
        //    return NewResult(students);
        //}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id, CancellationToken cancellationToken)
        {
            var students = await _mediator.Send(new GetStudentByIdQuery(id), cancellationToken);
            return NewResult(students);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return NewResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStudent(UpdateStudentCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return NewResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteStudentCommand(id), cancellationToken);
            return NewResult(result);
        }

    }
}