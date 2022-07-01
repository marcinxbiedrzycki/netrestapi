namespace NetRestApi.Entities;

public class CartProducts
{
    public int CartId { get; set; }
    public int ProductId { get; set; }

    public Product Product { get; set; }
    public Cart Cart { get; set; }

    public CartProducts(int productId, int cartId)
    {
        ProductId = productId;
        CartId = cartId;
    }
}