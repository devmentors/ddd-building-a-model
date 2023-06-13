using DDD.BuildingBlocks.V10.SpecificationUsage.Core.Cart;
using Microsoft.EntityFrameworkCore;

namespace DDD.BuildingBlocks.V10.SpecificationUsage.Infrastructure.Data;

internal class CheckoutRepository : ICheckoutRepository
{
    private readonly SalesDbContext _dbContext;

    public CheckoutRepository(SalesDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<CheckoutCart> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Checkouts
            .Include(x => x.Products)
            .ThenInclude(x => x.Product)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}