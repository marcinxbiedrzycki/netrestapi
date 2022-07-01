namespace NetRestApi.DTO;

public class AddProductDTO
{
    public string ProductName { get; set; }

    public float NewPrice { get; set; }

    public AddProductDTO(string productName, float newPrice)
    {
        ProductName = productName;
        NewPrice = newPrice;
    }
}