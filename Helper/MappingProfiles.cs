using AutoMapper;
using EcommerceEjemploApi.Dto;
using EcommerceEjemploApi.Models;

namespace EcommerceEjemploApi.Helper
{
    public class MappingProfiles : Profile
    {
        //Aqui podemos especificar como se hacen los mapeos (de una class a otra classDto y a la inversa)
        //Osea de que forma mapeamos (especificaciones)
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();

            CreateMap<Review, ReviewDto>()
                 .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                 .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));
            CreateMap<ReviewDto, Review>()
                .ForMember(dest => dest.User, opt => opt.Ignore()) // Ignoras la navegación y nos quedamos con solo el ID
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId));

            //mapeo el produtDTo del product con la condicion de:  
            // Product.Category.Id == ProductDto.CategoryId
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));

            //mapeo inverso especificado 
            CreateMap<ProductDto, Product>()
                //se usa forpath porque es una propiedad aninada no es el propio member que tenga ese dato
                .ForPath(dest => dest.Category.Id, opt => opt.MapFrom(src => src.CategoryId))
                .ForMember(dest => dest.Category, opt => opt.Ignore()); // Ignoras la propiedad de navegación al crear el Product desde ProductDto

            CreateMap<OrderDetail, OrderDetailDto>()
                .ForMember(dest => dest.ProductId, opt=>opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.OrderId, opt=>opt.MapFrom(src => src.OrderId));

            CreateMap<OrderDetailDto, OrderDetail>()
                .ForMember(dest => dest.ProductId, opt => opt.Ignore())
                .ForMember(dest => dest.OrderId, opt => opt.Ignore())
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId));


            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId)); // Mapea directamente el UserId

            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.User, opt => opt.Ignore()) // Ignoras la navegación de User, pero mapeas UserId
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();




            //DEJO UN EJEMPLO DE HACERLO SIN EL AUTOMAPPER

//______________________________________________________________________________________________________
            //public Review MapReviewDtoToReview(ReviewDto reviewDto)
            //{
            //    return new Review
            //    {
            //        Id = reviewDto.Id,
            //        Title = reviewDto.Title,
            //        Text = reviewDto.Text,
            //        Rating = reviewDto.Rating,
            //        UserId = reviewDto.UserId // Solo mapeamos el UserId
            //                                  // La propiedad User se queda como null porque no la estamos mapeando
            //    };
            //}

            //Y EL MAPEO INVERSO:
            //public ReviewDto MapReviewToReviewDto(Review review)
            //{
            //    return new ReviewDto
            //    {
            //        Id = review.Id,
            //        Title = review.Title,
            //        Text = review.Text,
            //        Rating = review.Rating,
            //        UserId = review.UserId // Solo pasamos el UserId
            //                               // Aquí también se ignora la propiedad User
            //    };
            //}

            // Convertimos ReviewDto a Review
            //Review review = MapReviewDtoToReview(reviewDto);

            // Ahora convertimos Review a ReviewDto
            //ReviewDto convertedBack = MapReviewToReviewDto(review);


        }
    }
}
