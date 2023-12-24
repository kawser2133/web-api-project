using Project.Core.Entities.Business;
using Project.Core.Entities.General;
using Project.Core.Interfaces.IMapper;
using Project.Core.Interfaces.IRepositories;
using Project.Core.Interfaces.IServices;
using Project.Core.Services;
using Moq;

namespace Project.UnitTest
{
    public class ProductServiceTests
    {
        private Mock<IBaseMapper<Product, ProductViewModel>> _productViewModelMapperMock;
        private Mock<IBaseMapper<ProductCreateViewModel, Product>> _productCreateMapperMock;
        private Mock<IBaseMapper<ProductUpdateViewModel, Product>> _productUpdateMapperMock;
        private Mock<IProductRepository> _productRepositoryMock;
        private Mock<IUserContext> _userContextMock;

        [SetUp]
        public void Setup()
        {
            _productViewModelMapperMock = new Mock<IBaseMapper<Product, ProductViewModel>>();
            _productCreateMapperMock = new Mock<IBaseMapper<ProductCreateViewModel, Product>>();
            _productUpdateMapperMock = new Mock<IBaseMapper<ProductUpdateViewModel, Product>>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _userContextMock = new Mock<IUserContext>();
        }

        [Test]
        public async Task CreateProductAsync_ValidProduct_ReturnsCreatedProductViewModel()
        {
            // Arrange
            var productService = new ProductService(
                _productViewModelMapperMock.Object,
                _productCreateMapperMock.Object,
                _productUpdateMapperMock.Object,
                _productRepositoryMock.Object,
                _userContextMock.Object);

            var newProductCreateViewModel = new ProductCreateViewModel
            {
                Code = "P001",
                Name = "Sample Product",
                Price = 9.99f,
                Description = "Sample description",
                IsActive = true
            };

            var newProductViewModel = new ProductViewModel
            {
                Code = "P001",
                Name = "Sample Product",
                Price = 9.99f,
                Description = "Sample description",
                IsActive = true
            };

            var createdProduct = new Product
            {
                Code = "P001",
                Name = "Sample Product",
                Price = 9.99f,
                Description = "Sample description",
                IsActive = true
            };

            _productCreateMapperMock.Setup(mapper => mapper.MapModel(newProductCreateViewModel))
                              .Returns(createdProduct);

            _productRepositoryMock.Setup(repo => repo.Create(createdProduct, It.IsAny<CancellationToken>()))
                                  .ReturnsAsync(createdProduct);

            _productViewModelMapperMock.Setup(mapper => mapper.MapModel(createdProduct))
                                       .Returns(newProductViewModel);

            // Act
            var result = await productService.Create(newProductCreateViewModel, It.IsAny<CancellationToken>());

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Code, Is.EqualTo(newProductViewModel.Code));
            // Additional assertions for other properties
        }
    }

}