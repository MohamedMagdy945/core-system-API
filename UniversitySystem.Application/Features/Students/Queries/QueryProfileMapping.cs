using AutoMapper;
using UniversitySystem.Application.Features.Students.Models;
using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Application.Features.Students.Queries
{
    public class QueryProfileMapping : Profile
    {
        public QueryProfileMapping()
        {
            CreateMap<Student, StudentItemDto>();
        }
    }
}
