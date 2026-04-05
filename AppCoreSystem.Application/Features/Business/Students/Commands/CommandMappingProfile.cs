using AutoMapper;
using AppCoreSystem.Application.Features.Business.Students.Commands.CreateStudent;
using AppCoreSystem.Application.Features.Business.Students.Commands.UpdateStudent;
using AppCoreSystem.Domain.Entities.Business;

namespace AppCoreSystem.Application.Features.Business.Students.Commands
{
    public class CommandMappingProfile : Profile
    {
        public CommandMappingProfile()
        {
            CreateMap<CreateStudentCommand, Student>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdateStudentCommand, Student>()
               .ForMember(dest => dest.Id, opt => opt.Ignore())
               .ForAllMembers(opt =>
               {
                   opt.Condition((src, dest, srcMember) => srcMember != null);
               });
        }
    }
}
