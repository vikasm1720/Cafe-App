using CafeOps.DAL.Repositories.Interfaces;
using CafeOps.Logic.Dtos;
using MediatR;

namespace CafeOps.Logic
{
    public record GetCafesQuery(string? Location) : IRequest<List<CafeDto>>;

    public class GetCafesQueryHandler : IRequestHandler<GetCafesQuery, List<CafeDto>>
    {
        private readonly ICafeRepository _cafeRepository;

        public GetCafesQueryHandler(ICafeRepository cafeRepository)
        {
            _cafeRepository = cafeRepository;
        }

        public async Task<List<CafeDto>> Handle(GetCafesQuery request, CancellationToken cancellationToken)
        {
            var cafes = await _cafeRepository.GetByLocationAsync(request.Location);

            return cafes.Select(c => new CafeDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Employees = c.Employees.Count,
                Location = c.Location,
                Logo = c.Logo
            }).ToList();
        }
    }
}
