namespace CarDealer
{
    using AutoMapper;
    using CarDealer.Dtos.Import;
    using CarDealer.Models;
    

    public class CarDealerProfile : Profile
    {
        public CarDealerProfile()
        {
            this.CreateMap<ImportSupplierDTO, Supplier>();

            this.CreateMap<ImportPartDTO, Part>();

            this.CreateMap<ImportCarDTO, Car>()
                .ForMember(c => c.TravelledDistance, x => x
                 .MapFrom(s => s.TraveledDistance));

            this.CreateMap<ImportCustomerDto, Customer>();

            this.CreateMap<ImportSalesDTO, Sale>();
                
            
        }
    }
}
