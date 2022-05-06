using AutoMapper;
using Discount.Grpc.Protos;
using Discount.Grpc.Repository;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class MessageImplemations: DiscountServices.DiscountServicesBase
    {
        private readonly ILogger<MessageImplemations> _logger;
        private readonly IMapper _Mapper;
        private readonly IDiscountRepository _DiscountRepository;

        public MessageImplemations(IMapper mapper, IDiscountRepository discountRepository,ILogger<MessageImplemations> logger)
        {
            _logger = logger;
            _Mapper = mapper;
            _DiscountRepository = discountRepository;
        }
        public override async Task<DiscountResponse> GetDiscountedProduct(DiscountRequest request, ServerCallContext context)
        {
            try
            {
                var product = await _DiscountRepository.GetDiscountedProductAsync(request.Productname);
                var DiscountProduct = _Mapper.Map<DiscountResponse>(product);
                if (DiscountProduct == null)
                    throw new RpcException(new Status(StatusCode.NotFound, $"No Discount is Available For the Product {request.Productname}"));
                return DiscountProduct;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new DiscountResponse();
            }
            
        }
    }
}
