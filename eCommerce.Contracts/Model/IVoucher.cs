namespace eCommerce.Contracts.Model
{
    public interface IVoucher
    {
        int VoucherId { get; set; }
        string VoucherCode { get; set; }
        int VoucherTypeId { get; set; }
        string VoucherDescription { get; set; }
        int AppliesToProductId { get; set; }
        decimal Value { get; set; }
        decimal MinimumSpend { get; set; }
        bool MultipleUse { get; set; }
        string AssignedTo { get; set; }
    }
}