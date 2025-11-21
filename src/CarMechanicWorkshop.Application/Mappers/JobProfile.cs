using AutoMapper;
using CarMechanicWorkshop.Domain.Entities;
using CarMechanicWorkshop.Shared.DTOs.Jobs;

namespace CarMechanicWorkshop.Application.Mappers;

public class JobProfile : Profile
{
    public JobProfile()
    {
        // Database → DTO
        CreateMap<Job, JobDTO>()
            .ForMember(dest => dest.ClientName,
                       opt => opt.MapFrom(src => src.Client != null ? src.Client.Name : null))
            .ForMember(dest => dest.EstimatedHours,
                       opt => opt.MapFrom(src => src.EstimatedHours));

        // Create DTO → Database
        CreateMap<CreateJobDTO, Job>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Client, opt => opt.Ignore());

        // Update DTO → Database (null-safe)
        CreateMap<UpdateJobDTO, Job>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Client, opt => opt.Ignore())
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}
