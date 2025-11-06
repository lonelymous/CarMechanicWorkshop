using AutoMapper;
using CarMechanicWorkshop.Shared.Models.DTOs;
using CarMechanicWorkshop.Shared.Models.Database;

namespace CarMechanicWorkshop.API.Mapping;

public class JobProfile : Profile
{
    public JobProfile()
    {
        // Entity ↔ DTO
        CreateMap<JobDatabase, JobDTO>().ReverseMap();

        // Create DTO → Entity
        CreateMap<CreateJobDTO, JobDatabase>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Clients, opt => opt.Ignore());

        // Update DTO → Entity
        CreateMap<UpdateJobDTO, JobDatabase>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Clients, opt => opt.Ignore())
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}
