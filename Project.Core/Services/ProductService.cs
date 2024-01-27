using Project.Core.Entities.Business;
using Project.Core.Entities.General;
using Project.Core.Interfaces.IMapper;
using Project.Core.Interfaces.IRepositories;
using Project.Core.Interfaces.IServices;

namespace Project.Core.Services
{
    public class ProductService : BaseService<Product, ProductViewModel>, IProductService
    {
        private readonly IBaseMapper<Product, ProductViewModel> _productViewModelMapper;
        private readonly IBaseMapper<ProductCreateViewModel, Product> _productCreateMapper;
        private readonly IBaseMapper<ProductUpdateViewModel, Product> _productUpdateMapper;
        private readonly IProductRepository _productRepository;
        private readonly IUserContext _userContext;

        public ProductService(
            IBaseMapper<Product, ProductViewModel> productViewModelMapper,
            IBaseMapper<ProductCreateViewModel, Product> productCreateMapper,
            IBaseMapper<ProductUpdateViewModel, Product> productUpdateMapper,
            IProductRepository productRepository,
            IUserContext userContext)
            : base(productViewModelMapper, productRepository)
        {
            _productCreateMapper = productCreateMapper;
            _productUpdateMapper = productUpdateMapper;
            _productViewModelMapper = productViewModelMapper;
            _productRepository = productRepository;
            _userContext = userContext;
        }

        public async Task<ProductViewModel> Create(ProductCreateViewModel model, CancellationToken cancellationToken)
        {
            //Mapping through AutoMapper
            var entity = _productCreateMapper.MapModel(model);
            entity.EntryDate = DateTime.Now;
            entity.EntryBy = Convert.ToInt32(_userContext.UserId);

            return _productViewModelMapper.MapModel(await _productRepository.Create(entity, cancellationToken));
        }

        public async Task Update(ProductUpdateViewModel model, CancellationToken cancellationToken)
        {
            var existingData = await _productRepository.GetById(model.Id, cancellationToken);

            //Mapping through AutoMapper
            _productUpdateMapper.MapModel(model, existingData);

            // Set additional properties or perform other logic as needed
            existingData.UpdatedDate = DateTime.Now;
            existingData.UpdatedBy = Convert.ToInt32(_userContext.UserId);

            await _productRepository.Update(existingData, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var entity = await _productRepository.GetById(id, cancellationToken);
            await _productRepository.Delete(entity, cancellationToken);
        }

        public async Task<double> PriceCheck(int productId, CancellationToken cancellationToken)
        {
            return await _productRepository.PriceCheck(productId, cancellationToken);
        }

    }
}
