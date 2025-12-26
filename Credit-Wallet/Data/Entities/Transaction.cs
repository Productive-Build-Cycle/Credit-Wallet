using System.ComponentModel.DataAnnotations;

namespace Credit_Wallet.Data.Entities;

public class Transaction
{
    [Key]
    public int Id { get; set; }
    public int WalletId { get; set; }
    public Wallet? Wallet { get; set; }
    public decimal Amount { get; set; }
    public TransactionType TransactionType { get; set; }
    public DateTime CreatedDateTime { get; set; }
}

public enum TransactionType
{
    Deposit = 1,
    Withdraw = 2
}