namespace AccountsReceivable.Models
{
    // Status of an account receivable, calculated from its payments
    public enum ReceivableStatus
    {
        Pending = 0,
        PartiallyPaid = 1,
        Paid = 2
    }
}
