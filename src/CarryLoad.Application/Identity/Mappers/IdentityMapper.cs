using CarryLoad.Application.Extensions;
using CarryLoad.Application.Identity.Commands;
using CarryLoad.Application.Identity.Commands.Company;
using CarryLoad.Application.Identity.Commands.Customer;
using CarryLoad.Application.Identity.Commands.Driver;

namespace CarryLoad.Application.Identity.Mappers
{
    public class IdentityMapper : AppMapperBase
    {
        public IdentityMapper()
        {
            CreateMap<DriverRegistrationCommand, Models.Entities.User>()
                .Map(r => r.UserName, r => r.Email);
            CreateMap<DriverRegistrationCommand, Models.Entities.Bank>();
            CreateMap<DriverRegistrationCommand, Models.Entities.Driver>();
            CreateMap<DriverRegistrationCommand, Models.Entities.Carrier>();
            CreateMap<DriverRegistrationCommand, Models.Entities.Company>()
                .Map(r => r.Code, m => m.PersonalCode)
                .Map(r => r.Address, m => m.RegistrationAddress)
                .Map(r => r.Name, m => m.FirstName + " " + m.LastName);
            CreateMap<DriverRegistrationCommand, Models.Entities.Address>()
                .IncludeMembers(r => r.RegistrationAddress);

            CreateMap<CompanyRegistrationCommand, Models.Entities.User>()
                .Map(r => r.UserName, r => r.Email);
            CreateMap<CompanyRegistrationCommand, Models.Entities.Bank>();
            CreateMap<CompanyRegistrationCommand, Models.Entities.Company>()
                .Map(r => r.Name, r => r.CompanyName)
                .Map(r => r.Code, r => r.CompanyCode);
            CreateMap<CompanyRegistrationCommand, Models.Entities.Carrier>();
            CreateMap<CompanyRegistrationCommand, Models.Entities.Address>()
                .IncludeMembers(r => r.RegistrationAddress);

            CreateMap<CustomerRegistrationCommand, Models.Entities.User>()
                .Map(r => r.UserName, r => r.Email);
            CreateMap<CustomerRegistrationCommand, Models.Entities.Company>()
                .Map(r => r.Name, r => r.CompanyName)
                .Map(r => r.Code, r => r.CompanyCode);
            CreateMap<CustomerRegistrationCommand, Models.Entities.Address>()
                .IncludeMembers(r => r.RegistrationAddress);
            CreateMap<CustomerRegistrationCommand, Models.Entities.Customer>();


            CreateMap<AddressCommand, Models.Entities.Address>();
        }
    }
}
