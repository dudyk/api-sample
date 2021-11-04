using CarryLoad.Application.Contract.Commands;
using CarryLoad.Application.Contract.Responses;
using CarryLoad.Application.Extensions;

namespace CarryLoad.Application.Contract.Mappers
{
    public class ContractMapper : AppMapperBase
    {
        public ContractMapper()
        {
            CreateMap<AddContractCommand, Models.Entities.Contract>();

            CreateMap<Models.Entities.Contract, CustomerContractDetailsResult>()
                .Map(x => x.CarrierName, x => x.Carrier.Company.Name);

            CreateMap<Models.Entities.Vehicle, ContractVehicleResult>();

            CreateMap<Models.Entities.Contract, CompanyContractDetailsResult>()
                .Map(x => x.CustomerName, x => x.Customer.CompanyName);

            CreateMap<Models.Entities.Vehicle, VehicleNumberResult>();
        }
    }
}
