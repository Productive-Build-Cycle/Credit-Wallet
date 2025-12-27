using Credit_Wallet.Data;
using Microsoft.EntityFrameworkCore;

namespace Credit_Wallet.Features.GetuserWallet
{
    public class GetUserWalleetHandler
    {
        private readonly ApplicationDbContext _dbContext;
        public GetUserWalleetHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<GetUserWalletResponse?> HandleAsync(string userId)
        {
            var wallet = await _dbContext.Wallets.AsNoTracking()
                                                 .FirstOrDefaultAsync
                                                  (w => w.UserId == userId);
            if (wallet == null)
            {
                return null;
            }
            return new GetUserWalletResponse
            {
                Balance = wallet.Balance,
                CheckedDateTime = DateTime.UtcNow
            };
        }
    }
}
