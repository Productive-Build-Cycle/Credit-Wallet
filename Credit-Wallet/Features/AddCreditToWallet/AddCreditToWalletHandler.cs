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
                var response=new AddCredittoWalletResponse
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
            
                wallet.Balance += request.Amount;
              //  _dbcontext.Wallets.Update(wallet);
               
            await _dbcontext.SaveChangesAsync();
            return new AddCredittoWalletResponse
            {
                Success = true,
                Message = "Credit added successfully",
                NewBalance = wallet.Balance
            };
        }
    }
}
