namespace ProductShop
{
    using AutoMapper;
    using ProductShop.Dtos.Export;
    using ProductShop.Dtos.Import;
    using ProductShop.Models;

    public class ProductShopProfile : Profile
    {
        public ProductShopProfile()
        {
            this.CreateMap<ImportUserDTO, User>();

            this.CreateMap<ImportProductDTO, Product>();

            this.CreateMap<ImportCategoryDTO, Category>();

            this.CreateMap<ImportCategoryProductDTO, CategoryProduct>();

            this.CreateMap<SoldProductDTO, Product>();

            this.CreateMap<Product, ProductsInRangeDTO>()
                .ForMember(x => x.Buyer, c => c.MapFrom(x => $"{x.Buyer.FirstName} {x.Buyer.LastName}"));

            this.CreateMap<User, UserWithLeastOneSoldItemDTO>()
                .ForMember(x => x.Products, c => c.MapFrom(x => x.ProductsSold));

            

          



        }
    }
}
