using System.Net;
using System.Web.Http;
using Microsoft.EntityFrameworkCore;
using NetRestApi.DAL;
using NetRestApi.DTO;
using NetRestApi.Entities;
using NuGet.Protocol;

namespace NetRestApi.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly MainDbContext _context;

    public ProductRepository(MainDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetAllProducts()
    {
        return await _context.Products.FirstOrDefaultAsync();
    }

    public async Task<Product> ChangeProductPrice(ChangePriceDTO productDto)
    {
        var product = await _context.Products.Where(x => x.Id == productDto.ProductId).FirstOrDefaultAsync();

        if (product == null)
        {
            throw new Exception("Product not found");
        }
        
        product.Price = productDto.NewPrice;

        _context.Products.Update(product);


        if (await _context.SaveChangesAsync() > 0)
        {
            return product;
        }

        throw new Exception("Product not found");
    }
}