using NetRestApi.Entities;

namespace NetRestApi.Repositories;

public interface ICartProductsRepository
{
    Task<CartProducts> AddProductToCart(int productId, int categoryId);
}