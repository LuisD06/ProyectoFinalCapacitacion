using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Domain.models;
using Curso.ECommerce.Domain.repository;

namespace Curso.ECommerce.Application.Service
{
    public class CartAppService : ICartAppService
    {
        private readonly ICartRepository repository;
        private readonly IProductAppService productService;
        private readonly IMapper mapper;

        public CartAppService(ICartRepository repository, IProductAppService productService, IMapper mapper)
        {
            this.repository = repository;
            this.productService = productService;
            this.mapper = mapper;
        }
        public async Task<CartDto> CreateAsync(CartCreateDto cart)
        {
            // Validaciones
            if (cart.CartItems.Count == 0)
            {
                throw new ArgumentException("Se ha tratado de crear un  carrito sin items");
            }
            // Stock del producto
            var productIdList = cart.CartItems.Select(i => i.ProductId);
            var itemProductList = await productService.GetAllByIdAsync(productIdList.ToList());
            // TODO: Colocar logs en el caso de que no exista algun producto en específico
            if (itemProductList.Count == 0)
            {
                // TODO: Especificar los productos que no existen
                throw new ArgumentException("Los productos indicados no existen");
            }

            Cart cartEntity = new Cart();
            string notes = String.Empty;
            foreach (var product in itemProductList)
            {
                long quantity = cart.CartItems.Where(i => i.ProductId == product.Id).Select(i => i.Quantity).SingleOrDefault();
                if (product.Stock == 0)
                {
                    notes += $"El producto {product.Name} no tiene existencias";
                    // Si el producto no tiene stock se recomienda otro del mismo tipo
                    var productDtoList = await productService.GetAllByTypeAsync(product.ProductType, product.Id);
                    if (productDtoList.Count > 0)
                    {
                        notes += $"Producto similar: {productDtoList.ElementAt(0).Name}";
                    }
                }
                else
                {
                    if (product.Stock < quantity)
                    {
                        notes += $"Existencias insuficientes del producto: {product.Name}";
                        quantity = (long)product.Stock;
                    }
                    CartItem cartItem = new CartItem();
                    cartItem.ProductId = product.Id;
                    cartItem.Price = product.Price;
                    cartItem.Quantity = quantity;
                    cartItem.Notes = cart.CartItems.Where(i => i.ProductId == product.Id).Select(i => i.Notes).SingleOrDefault();
                    cartEntity.AddCartItem(cartItem);
                }
            }
            if (cartEntity.CartItems.Count == 0)
            {
                throw new ArgumentException("No se ha podido crear la orden, productos no disponibles");
            }
            else
            {
                cartEntity.ClientId = cart.ClientId;
                cartEntity.Date = cart.Date;
                cartEntity.Total = cartEntity.CartItems.Sum(x => x.Price * x.Quantity);
                cartEntity.Notes = notes;

                // Actualizar stock del producto


                // Persistencia del objeto
                cartEntity = await repository.AddAsync(cartEntity);
                await repository.UnitOfWork.SaveChangesAsync();

                var orderDto = await GetByIdAsync(cartEntity.Id);

                return orderDto;
            }
        }

        public async Task<bool> DeleteAsync(Guid cartId)
        {
            //Reglas Validaciones... 
            var cartEntity = await repository.GetByIdAsync(cartId);
            if (cartEntity == null)
            {
                throw new ArgumentException($"El carrito con el id: {cartId}, no existe");
            }

            repository.Delete(cartEntity);
            await repository.UnitOfWork.SaveChangesAsync();

            return true;
        }

        public ICollection<CartDto> GetAll()
        {
            var query = repository.GetAllIncluding(c => c.Client, c => c.CartItems);
            // var cartDtoList = query.Select(c => new CartDto()
            // {
            //     CancellationDate = c.CancellationDate,
            //     ClientId = c.ClientId,
            //     Date = c.Date,
            //     Id = c.Id,
            //     Notes = c.Notes,
            //     Total = c.Total,
            //     CartItems = c.CartItems.Select(i => new CartItemDto()
            //     {
            //         Id = i.Id,
            //         Notes = i.Notes,
            //         CartId = i.CartId,
            //         Price = i.Price,
            //         ProductId = i.ProductId,
            //         Quantity = i.Quantity
            //     }).ToList()
            // });
            var cartDtoList = query.Select(c => mapper.Map<CartDto>(c));
            return cartDtoList.ToList();
        }

        public async Task<CartDto> GetByIdAsync(Guid cartId)
        {
            var query = repository.GetAllIncluding(c => c.Client, c => c.CartItems);
            query = query.Where(c => c.Id == cartId);

            var cartDto = query.Select(c => new CartDto()
            {
                CancellationDate = c.CancellationDate,
                ClientId = c.ClientId,
                Date = c.Date,
                Id = c.Id,
                Notes = c.Notes,
                Total = c.Total,
                CartItems = c.CartItems.Select(i => new CartItemDto()
                {
                    Id = i.Id,
                    Notes = i.Notes,
                    CartId = i.CartId,
                    Price = i.Price,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity

                }).ToList()
            }).SingleOrDefault();
            return cartDto;
        }

        public Task UpdateAsync(Guid cartId, CartUpdateDto cart)
        {
            //TODO : Implementar update en order
            throw new NotImplementedException();
        }
    }
}