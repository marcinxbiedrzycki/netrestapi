using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using NetRestApi.Controllers;
using NetRestApi.DAL;
using NetRestApi.Entities;
using NetRestApi.Repositories;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace NetRestApiTests;

public class Tests
{
    private DbContextOptions<MainDbContext> _options;
    private MainDbContext _context;
    private ProductRepository _productRepository;
    private CartProductsRepository _cartProductsRepository;
    private ConfigurationBuilder _configuration;

    [SetUp]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<MainDbContext>()
        .UseInMemoryDatabase(databaseName: "net-rest-api-db")
        .Options;
        _context = new MainDbContext(_options, new ConfigurationManager());
        // _context = new Mock<MainDbContext>().Object;
        _productRepository = new Mock<ProductRepository>().Object;
        _cartProductsRepository = new Mock<CartProductsRepository>().Object;
    }

    // [Fact]
    // public void Test1()
    // {
    //     // Arrange
    //     var productId = 1;
    //     var newPrice = (float)2.50;
    //     var changePriceDTO = new ChangePriceDTO(productId, newPrice);
    //     
    //     var options = new DbContextOptionsBuilder<MainDbContext>()
    //         .UseInMemoryDatabase(databaseName: "net-rest-api-db")
    //         .Options;
    //     
    //     using (var context = new MainDbContext(options, new ConfigurationManager()))
    //     {
    //         context.Products.Add(new Product("'qwe'", newPrice));
    //         context.Products.
    //         context.SaveChanges();
    //
    //         context.Products.Find(1);
    //     }
    //     
    //     var repositoryMock = new Mock<IProductRepository>();
    //     repositoryMock
    //         .Setup(r => r.ChangeProductPrice(changePriceDTO));
    //     
    //     var controller = new ProductController(_context, _productRepository, _cartProductsRepository);
    //     
    //     
    //     // Act
    //     controller.ChangePriceProduct(productId, newPrice);
    //     
    //     if (_context == null)
    //     {
    //         throw new Exception();
    //     }
    //     // _context.Products.Find()
    //     
    //     var product = _context.Products.FindAsync(productId);
    //
    //     // Assert
    //     product.Result.Id.Equals(productId);
    //     product.Result.Price.Equals(newPrice);
    //     
    //     // repositoryMock.Verify(r => r. as ("Blog2"));
    //     // Assert.Equal("http://blog2.com", blog.Url);
    // }
    
    // [Fact]
    // public void GetReturnsProductWithSameId()
    // {
    //     // Arrange
    //     var productId = 1;
    //     var newPrice = (float)2.50;
    //     var changePriceDTO = new ChangePriceDTO(productId, newPrice);
    //     
    //     var mockRepository = new Mock<IProductRepository>();
    //     mockRepository.Setup()
    //         .Returns(new Product { Id = 42 });
    //
    //     var controller = new Products2Controller(mockRepository.Object);
    //
    //     // Act
    //     IHttpActionResult actionResult = controller.Get(42);
    //     var contentResult = actionResult as OkNegotiatedContentResult<Product>;
    //
    //     // Assert
    //     Assert.IsNotNull(contentResult);
    //     Assert.IsNotNull(contentResult.Content);
    //     Assert.AreEqual(42, contentResult.Content.Id);
    // }
    
    [Fact]
    public void getAllProductsTest()
    {
        // Arrange
        var mockRepository = new Mock<IProductRepository>();
        var controller = new ProductController(_context, mockRepository.Object, _cartProductsRepository);

        // Act
        Task<ActionResult<IEnumerable<Product>>> actionResult = controller.GetProducts();

        // Assert
        Assert.IsNotNull(actionResult);
    }
    
    [Fact]
    public void getProductById()
    {
        // Arrange
        var productId = 1;
        var mockRepository = new Mock<IProductRepository>();
        var controller = new ProductController(_context, mockRepository.Object, _cartProductsRepository);

        // Act
        Task<ActionResult<Product>> actionResult = controller.GetProduct(productId);

        // Assert
        Assert.That(actionResult.Id, Is.EqualTo(1));
    }
    
    // [Fact]
    // public void changeProductPrice()
    // {
    //     // Arrange
    //     var productId = 1;
    //     var newPrice = (float)2.50;
    //     // var changePriceDTO = new Mock<ChangePriceDTO>();
    //     // changePriceDto.NewPrice = newPrice;
    //     // changePriceDto.ProductId = productId;
    //     
    //     var mockRepository = new Mock<IProductRepository>();
    //     var controller = new ProductController(_context, mockRepository.Object, _cartProductsRepository);
    //
    //     // Act
    //     Task<IActionResult> actionResult = controller.ChangePriceProduct(productId, newPrice);
    //     var product = controller.GetProduct(productId);
    //     
    //     // Assert
    //     Assert.That(actionResult.Id, Is.EqualTo(productId));
    //     Assert.That(product.Id, Is.EqualTo(productId));
    // }
}