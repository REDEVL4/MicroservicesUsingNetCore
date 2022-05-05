using AutoMapper;
using DiscountGRPC.Protos;
using DiscountGRPC.Repository;
using Grpc.Core;
namespace DiscountGRPC.Services
{
    public class DServices: DiscountService.DiscountServiceBase
    {
        private readonly IDiscountRepository _repository;
        private readonly IMapper _mapper;

        public DServices(IDiscountRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public override async Task<DiscountResponse> GetDiscountedProduct(DiscountRequest request, ServerCallContext context)
        {
            var product=await _repository.GetDiscountedProductAsync(request.ProductName);
            if(product==null)
            {
                throw new RpcException(new Status(StatusCode.NotFound,$"The Discounted Product {request.ProductName} not Found."));
            }
            var discountProduct=_mapper.Map<DiscountResponse>(product);
            return discountProduct;
        }
    }
}
