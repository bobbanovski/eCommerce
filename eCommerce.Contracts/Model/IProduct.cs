namespace eCommerce.Contracts.Model
{
    public interface IProduct
    {
        int ProductId { get; set; }
        string Description { get; set; }
        string ImageUrl { get; set; }
        decimal Price { get; set; }
        decimal CostPrice { get; set; }
    }
}