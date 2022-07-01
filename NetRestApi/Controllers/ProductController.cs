using System.Net;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetRestApi.DAL;
using NetRestApi.DTO;
using NetRestApi.Entities;
using NetRestApi.Repositories;
using Serilog;

namespace NetRestApi.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly MainDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly ICartProductsRepository _cartProductsRepository;

        public ProductController(MainDbContext context, IProductRepository productRepository,
            ICartProductsRepository cartProductsRepository)
        {
            _context = context;
            _productRepository = productRepository;
            _cartProductsRepository = cartProductsRepository;
        }


        // GET: api/Product
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateBootstrapLogger();

            Log.Information("Starting up");

            if (_context.Products == null)
            {
                Log.Information("Products not found");

                return NotFound();
            }

            return await _context.Products.ToListAsync();
        }

        // GET: api/Product/5
        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (_context.Products == null)
            {
                Log.Information("Product with id:" + id);

                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                Log.Information("Product with id:" + id);

                return NotFound();
            }

            Log.Information("Getting product with id:" + id);

            return product;
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async Task<ActionResult<Product>> PostProduct(AddProductDTO productDTO)
        {
            var product = new Product(productDTO.ProductName, productDTO.NewPrice);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Product/5
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // PUT: api/Product/5/price
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Microsoft.AspNetCore.Mvc.HttpPut("{id}/{price}")]
        public async Task<IActionResult> ChangePriceProduct(int id, float price)
        {
            var changePriceDto = new ChangePriceDTO(id, price);
            try
            {
                Log.Information("Setting new price:" + price + "to product with id:" + id);

                await _productRepository.ChangeProductPrice(changePriceDto);
            }
            catch (Exception e)
            {
                Log.Information("Something went wrong while changing price for product with id:" + id);
             
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return NoContent();
        }

        // PUT: api/Product/add/cart
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Microsoft.AspNetCore.Mvc.HttpPost("add/{productId}/{cartId}")]
        public async Task<IActionResult> ChangePriceProduct(int productId, int cartId)
        {
            await _cartProductsRepository.AddProductToCart(productId, cartId);

            return NoContent();
        }
    }
}