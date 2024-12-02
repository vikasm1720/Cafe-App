using CafeOps.DAL;
using CafeOps.DAL.Repositories.Interfaces;
using MediatR;

namespace CafeOps.Logic
{
    public record UpdateCafeCommand(Guid? Id, string Name, string Description, string Location, byte[]? Logo) : IRequest<Unit>;

    public class UpdateCafeCommandHandler : IRequestHandler<UpdateCafeCommand, Unit>
    {
        private readonly ICafeRepository _cafeRepository;

        public UpdateCafeCommandHandler(ICafeRepository cafeRepository)
        {
            _cafeRepository = cafeRepository;
        }

        public async Task<Unit> Handle(UpdateCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = await _cafeRepository.GetByIdAsync(request.Id);
            if (cafe == null)
            {
                throw new KeyNotFoundException($"Cafe with ID '{request.Id}' not found.");
            }

            cafe.Name = request.Name;
            cafe.Description = request.Description;
            cafe.Location = request.Location;
            cafe.Logo = request.Logo;
            await _cafeRepository.UpdateCafe(cafe);
            return Unit.Value;
        }
    }


}
