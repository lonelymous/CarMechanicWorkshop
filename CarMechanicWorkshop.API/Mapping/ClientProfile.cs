using AutoMapper;
using CarMechanicWorkshop.Shared.Models.DTOs;
using CarMechanicWorkshop.Shared.Models.Database;

namespace CarMechanicWorkshop.API.Mapping;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        // Entity ↔ DTO
        CreateMap<ClientDatabase, ClientDTO>().ReverseMap();

        // Create DTO → Entity
        CreateMap<CreateClientDTO, ClientDatabase>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Jobs, opt => opt.Ignore());

        // Update DTO → Entity
        CreateMap<UpdateClientDTO, ClientDatabase>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Jobs, opt => opt.Ignore())
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
    }
}
