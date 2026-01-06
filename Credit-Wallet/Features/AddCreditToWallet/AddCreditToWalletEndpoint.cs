using Credit_Wallet.Data;

namespace Credit_Wallet.Features.AddCreditToWallet
{
    public class AddCreditToWalletEndpoint
    {
        public static void MapAddCreditToWalletEndpoint(WebApplication app)
        {
            app.MapPost("/api/wallet/add-credit", async (AddCreditToWalletRequest request,
                                                         AddCreditToWalletHandler handler) =>
            {
                var response = await handler.HandleAsync(request);
                return Results.Ok(response);
            });
        }
    }
}
