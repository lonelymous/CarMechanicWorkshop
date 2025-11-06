using AutoMapper;
using CarMechanicWorkshop.Shared.Models.DTOs;
using CarMechanicWorkshop.Shared.Models.Database;

namespace CarMechanicWorkshop.API.Mapping;

public class ClientProfile : Profile
{
    public ClientProfile()
    {
        CreateMap<ClientDatabase, ClientDTO>().ReverseMap();
        CreateMap<CreateClientDTO, ClientDatabase>();
        CreateMap<UpdateClientDTO, ClientDatabase>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}
