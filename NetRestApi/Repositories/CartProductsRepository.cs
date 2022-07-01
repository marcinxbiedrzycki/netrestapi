using Microsoft.EntityFrameworkCore;
using NetRestApi.DAL;
using NetRestApi.Entities;

namespace NetRestApi.Repositories;

public class CartProductsRepository : ICartProductsRepository
{
    private readonly MainDbContext _context;

    public CartProductsRepository(MainDbContext context)
    {
        _context = context;
    }

    public async Task<CartProducts> AddProductToCart(int productId, int cartId)
    {
        var product = await _context.Products.Where(x => x.Id == productId).FirstOrDefaultAsync();

        if (product is null)
        {
            throw new Exception("Not found");
        }

        var cart = await _context.Cart.Where(x => x.Id == cartId).FirstOrDefaultAsync();

        if (cart is null)
        {
            throw new Exception("Not found");
        }

        var cartProducts = new CartProducts(productId, cartId);

        await _context.CartProducts.AddAsync(cartProducts);
        
        if (await _context.SaveChangesAsync() > 0)
        {
            return cartProducts;
        }

        throw new Exception("Product not found");
    }
}