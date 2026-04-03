using AutoMapper;
using UniversitySystem.Application.Features.Business.Students.Models;
using UniversitySystem.Domain.Entities.Business;

namespace UniversitySystem.Application.Features.Business.Students.Queries
{
    public class QueryProfileMapping : Profile
    {
        public QueryProfileMapping()
        {
            CreateMap<Student, StudentItemDto>();
        }
    }
}
