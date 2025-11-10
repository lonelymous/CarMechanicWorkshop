using AutoMapper;
using CarMechanicWorkshop.Shared.Models.Database;
using CarMechanicWorkshop.Shared.Models.DTOs;

namespace CarMechanicWorkshop.API.Mapping;

public class JobProfile : Profile
{
    public JobProfile()
    {
        // Database → DTO
        CreateMap<JobDatabase, JobDTO>()
            .ForMember(dest => dest.ClientName,
                       opt => opt.MapFrom(src => src.Client != null ? src.Client.Name : null))
            .ForMember(dest => dest.EstimatedHours,
                       opt => opt.MapFrom(src => src.EstimatedHours));

        // Create DTO → Database
        CreateMap<CreateJobDTO, JobDatabase>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Client, opt => opt.Ignore());

        // Update DTO → Database (null-safe)
        CreateMap<UpdateJobDTO, JobDatabase>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Client, opt => opt.Ignore())
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}
