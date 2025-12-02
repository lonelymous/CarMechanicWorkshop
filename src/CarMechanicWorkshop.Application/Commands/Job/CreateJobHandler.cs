// public class CreateJobHandler : IRequestHandler<CreateJobCommand>
// {
//     private readonly IJobsRepository _repo;
//     private readonly IUnitOfWork _uow;
//     private readonly IMapper _mapper;

//     public CreateJobHandler(IJobsRepository repo, IUnitOfWork uow, IMapper mapper)
//     {
//         _repo = repo;
//         _uow = uow;
//         _mapper = mapper;
//     }

//     public async Task Handle(CreateJobCommand request, CancellationToken cancellationToken)
//     {
//         var entity = _mapper.Map<Job>(request.Dto);
//         await _repo.AddAsync(entity);
//         await _uow.SaveChangesAsync();
//     }
// }
