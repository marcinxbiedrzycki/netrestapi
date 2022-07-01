using NetRestApi.DTO;
using NetRestApi.Entities;

namespace NetRestApi.Repositories;

public interface IProductRepository
{
    Task<Product> ChangeProductPrice(ChangePriceDTO productDto);
    Task<Product?> GetAllProducts();
}