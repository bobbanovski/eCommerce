﻿namespace eCommerce.Contracts.Model
{
    public interface IVoucherType
    {
        int VoucherTypeId { get; set; }
        string VoucherModule { get; set; }
        string Type { get; set; }
        string Description { get; set; }
    }
}