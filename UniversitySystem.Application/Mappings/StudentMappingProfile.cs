using AutoMapper;
using UniversitySystem.Application.Features.Students.Models;
using UniversitySystem.Domain.Entities;

namespace UniversitySystem.Application.Mappings
{
    public class StudentMappingProfile : Profile
    {
        public StudentMappingProfile()
        {
            CreateMap<Student, StudentItemDto>();
        }
    }
}