namespace NetRestApi.Entities;

public class Cart
{
    public int Id { get; set; }
    
    public ICollection<CartProducts> CartProducts { get; set; }
}