// public class CreateClientHandler : IRequestHandler<CreateClientCommand>
// {
//     private readonly IClientsRepository _repo;
//     private readonly IUnitOfWork _uow;
//     private readonly IMapper _mapper;

//     public CreateClientHandler(IClientsRepository repo, IUnitOfWork uow, IMapper mapper)
//     {
//         _repo = repo;
//         _uow = uow;
//         _mapper = mapper;
//     }

//     public async Task Handle(CreateClientCommand request, CancellationToken cancellationToken)
//     {
//         var entity = _mapper.Map<Client>(request.Dto);
//         await _repo.AddAsync(entity);
//         await _uow.SaveChangesAsync();
//     }
// }
