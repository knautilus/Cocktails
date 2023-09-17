using AutoMapper;
using Cocktails.Data.Entities;
using Cocktails.Models.Cms.Requests.Cocktails;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cocktails.Cqrs.Sql.Cms.CommandHandlers
{
    public class CocktailCreateCommandHandler : IRequestHandler<CocktailCreateCommand, long>
    {
        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;

        public CocktailCreateCommandHandler(DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<long> Handle(CocktailCreateCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Cocktail>(request);

            await _dbContext.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}