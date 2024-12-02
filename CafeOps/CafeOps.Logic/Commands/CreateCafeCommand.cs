using CafeOps.DAL.Entities;
using CafeOps.DAL.Repositories.Interfaces;
using MediatR;

namespace CafeOps.Logic
{
    public record CreateCafeCommand(string Name, string Description, string Location, byte[] Logo) : IRequest<Guid>;

    public class CreateCafeCommandHandler : IRequestHandler<CreateCafeCommand, Guid>
    {
        private readonly ICafeRepository _cafeRepository;

        public CreateCafeCommandHandler(ICafeRepository cafeRepository)
        {
            _cafeRepository = cafeRepository;
        }

        public async Task<Guid> Handle(CreateCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = new Cafe
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Location = request.Location,
                Logo = request.Logo
            };

            await _cafeRepository.AddAsync(cafe);
            return cafe.Id;
        }
    }
}
