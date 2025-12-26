namespace Credit_Wallet.Features.MakeWallet;

public interface IMakeWalletService
{
    Task<int> HandleAsync();
}