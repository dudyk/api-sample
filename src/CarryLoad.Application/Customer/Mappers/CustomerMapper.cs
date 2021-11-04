using CarryLoad.Application.Customer.Commands;

namespace CarryLoad.Application.Customer.Mappers
{
    public class CustomerMapper : AppMapperBase
    {
        public CustomerMapper()
        {
            CreateMap<CreateOrderRequestCommand, Models.Entities.OrderRequest>();

            CreateMap<CreatePointCommand, Models.Entities.Point>();
        }
    }
}
