
using AutoMapper;
using Domain.Entidade;
using mvc.Models;

namespace mvc.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Categoria, CategoriaModel>().ReverseMap();
            CreateMap<Produto, ProdutoModel>().ReverseMap();
        }
    }
}
