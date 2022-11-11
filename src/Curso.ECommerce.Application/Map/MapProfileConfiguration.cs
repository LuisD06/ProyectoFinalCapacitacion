using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Domain.models;
using Curso.ECommerce.Domain.Models;

namespace Curso.ECommerce.Application.Map
{
    public class MapProfileConfiguration : Profile
    {
        public MapProfileConfiguration()
        {
            CreateMap<Brand, BrandDto>();
            CreateMap<BrandDto, Brand>();
            CreateMap<BrandCreateUpdateDto, Brand>();
            CreateMap<ProductTypeCreateUpdateDto, ProductType>();
            CreateMap<ProductType, ProductTypeDto>();
            CreateMap<Cart, CartDto>();
            CreateMap<CartItem, CartItemDto>();
            CreateMap<ClientCreateUpdateDto, Client>();
            CreateMap<Client, ClientDto>();
            CreateMap<ProductCreateUpdateDto, Product>();
            CreateMap<Product, ProductDto>();
        }
    }
}