using Microsoft.AspNetCore.Mvc;
using UniversitySystem.Application.Features.Students.Commands.CreateStudent;
using UniversitySystem.Application.Features.Students.Commands.DeleteStudent;
using UniversitySystem.Application.Features.Students.Commands.UpdateStudent;
using UniversitySystem.Application.Features.Students.Queries.GetStudentById;
using UniversitySystem.Application.Features.Students.Queries.GetStudentListPaginated;

namespace UniversitySystem.API.Controllers
{
    public class StudentsController : AppControllerBase
    {
        [HttpGet]
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