using CafeOps.DAL;
using CafeOps.DAL.Repositories.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeOps.Logic
{
    public record DeleteCafeCommand(Guid Id) : IRequest<Unit>;
    public class DeleteCafeCommandHandler : IRequestHandler<DeleteCafeCommand, Unit>
    {
        private readonly ICafeRepository _cafeRepository;

        public DeleteCafeCommandHandler(ICafeRepository cafeRepository)
        {
            _cafeRepository = cafeRepository;
        }

        public async Task<Unit> Handle(DeleteCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = await _cafeRepository.GetByIdAsync(request.Id);
            if (cafe == null)
            {
                throw new KeyNotFoundException($"Cafe with ID '{request.Id}' not found.");
            }
            await _cafeRepository.DeleteCafe(cafe);
            return Unit.Value;
        }
    }

}
