using AutoMapper;
using AppCoreSystem.Application.Features.Business.Students.Models;
using AppCoreSystem.Domain.Entities.Business;

namespace AppCoreSystem.Application.Features.Business.Students.Queries
{
    public class QueryProfileMapping : Profile
    {
        public QueryProfileMapping()
        {
            CreateMap<Student, StudentItemDto>();
        }
    }
}
