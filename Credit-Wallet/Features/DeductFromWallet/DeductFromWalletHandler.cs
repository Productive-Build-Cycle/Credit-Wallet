using Credit_Wallet.Data;
using Credit_Wallet.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Credit_Wallet.Features.DeductFromWallet
{
    public  class DeductFromWalletHandler
    {
        private readonly ILogger<DeductFromWalletHandler> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly DeductFfromWalletValidator _validator;
        public DeductFromWalletHandler(ILogger<DeductFromWalletHandler> logger,
                                       ApplicationDbContext dbContext,
                                       DeductFfromWalletValidator validator)
        {
            _logger = logger;
            _dbContext = dbContext;
            _validator = validator;
        }
        public async Task<DeductFromWalletResponse>HandleAsync(DeductFromWalletRequest request)
        {
           using var transaction=await _dbContext.Database.BeginTransactionAsync();
            try
            {
                if (!_validator.Validate(request))
                    {
                    return new DeductFromWalletResponse
                    {
                        Success = false,
                        Message = "Validation failed",
                    };
                }
                var wallet = await _dbContext.Wallets
                    .FirstOrDefaultAsync(w => w.UserId == request.UserId);
                if (wallet == null)
                    {
                    return new DeductFromWalletResponse
                    {
                        Success = false,
                        Message = "Wallet not found",
                    };
                }
                if (wallet.Balance < request.Amount)
                {
                    return new DeductFromWalletResponse
                    {
                        Success = false,
                        Message = "Insufficient funds",
                    };
                }
                await _dbContext.Transactions.AddAsync(new Transaction
                {
                    WalletId = wallet.Id,
                    Amount = -request.Amount,
                    TransactionType = TransactionType.Withdraw,
                    CreatedDateTime = DateTime.Now
                });
              
                wallet.Balance -= request.Amount;
              
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return new DeductFromWalletResponse
                {
                    Success = true,
                    Message = "Amount deducted successfully",
                    NewBalance = wallet.Balance
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error deducting amount from wallet for UserId: {UserId}", request.UserId);
                return new DeductFromWalletResponse
                {
                    Success = false,
                    Message = "An error occurred while processing the request",
                };
            }
        }
    }
}