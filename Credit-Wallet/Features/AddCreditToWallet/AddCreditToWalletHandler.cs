using Credit_Wallet.Data;
using Credit_Wallet.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Credit_Wallet.Features.AddCreditToWallet
{
    public class AddCreditToWalletHandler
    {
        private readonly ApplicationDbContext _dbcontext;
        public AddCreditToWalletHandler(ApplicationDbContext context)
        {
            _dbcontext = context;
        }
        public async Task<AddCredittoWalletResponse> HandleAsync(AddCreditToWalletRequest request)
        {
            var validator = new AddCreditToWalletValidator();
            if (!validator.Validate(request))
            {
                 return new AddCredittoWalletResponse
                {
                    Success = false,
                    Message = "Invalid request ",
                   
                };
            }
            var wallet = await _dbcontext.Wallets
                                              .FirstOrDefaultAsync(w => w.UserId == request.UserId);
            if (wallet == null)
            {
               return new AddCredittoWalletResponse
                {
                    Success = false,
                    Message = $"Wallet not found for this {request.UserId}",
                    
                };
            }
            using var transaction=await _dbcontext.Database.BeginTransactionAsync();
            try
            {
                var walletTransaction = new Transaction
                {
                    WalletId = wallet.Id,
                    Amount = request.Amount,
                    TransactionType = TransactionType.Deposit,
                    CreatedDateTime = DateTime.Now
                };
                await _dbcontext.Transactions.AddAsync(walletTransaction);
            
                wallet.Balance += request.Amount;
                await _dbcontext.SaveChangesAsync();
                await transaction.CommitAsync();
                return new AddCredittoWalletResponse
                {
                    Success = true,
                    Message = "Credit added successfully",
                    NewBalance = wallet.Balance
                };
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                return new AddCredittoWalletResponse
                {
                    Success = false,
                    Message = "An error occurred while adding credit to the wallet: " + ex.Message,
                    
                };
            }
           
        }
    }
}
