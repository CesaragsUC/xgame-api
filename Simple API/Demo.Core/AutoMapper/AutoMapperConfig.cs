using Application.API.Model.DTO;
using AutoMapper;
using Domain.Entidade;
using mvc.Models;

namespace Demo.Core.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Categoria, CategoriaModel>().ReverseMap();
            CreateMap<Produto, ProdutoModel>().ReverseMap();


            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
            CreateMap<Produto, ProdutoAddDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaAddDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaEditDTO>().ReverseMap();
            CreateMap<Produto, ProdutoEditDTO>().ReverseMap();
        }
    }
}