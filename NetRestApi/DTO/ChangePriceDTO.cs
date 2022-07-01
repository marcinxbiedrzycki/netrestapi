namespace NetRestApi.DTO;

public class ChangePriceDTO
{
    public ChangePriceDTO(int productId, float newPrice)
    {
        ProductId = productId;
        NewPrice = newPrice;
    }

    public int ProductId { get; set; }
    public float NewPrice { get; set; }
}