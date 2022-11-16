using AutoMapper;
using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Application.Models;
using Curso.ECommerce.Domain.models;
using Curso.ECommerce.Domain.repository;
using FluentValidation;

namespace Curso.ECommerce.Application.Service
{
    public class CartAppService : ICartAppService
    {
        private readonly ICartRepository repository;
        private readonly IProductAppService productService;
        private readonly IMapper mapper;
        private readonly IValidator<CartItemCreateUpdateDto> cartItemCUDtoValidator;

        public CartAppService(ICartRepository repository, IProductAppService productService, IMapper mapper, IValidator<CartItemCreateUpdateDto> cartItemCUDtoValidator)
        {
            this.cartItemCUDtoValidator = cartItemCUDtoValidator;
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

            if (itemProductList.Count == 0)
            {
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

        public async Task UpdateAsync(Guid cartId, CartUpdateDto cart)
        {
            var cartEntity = await repository.GetByIdAsync(cartId);
            if (cartEntity == null) {
                throw new ArgumentException($"No se ha encontrado un carrito con el id {cartId}");
            }
            // Mapeo dto => entity
            mapper.Map(cart, cartEntity);
            await repository.UpdateAsync(cartEntity);
            await repository.UnitOfWork.SaveChangesAsync();
        }
        public PaginatedList<CartDto> GetAllPaginated(int limit, int offset)
        {
            var query = repository.GetAllIncluding(c => c.CartItems, c => c.Client);
            var totalConsulta = query.Count();
            if (limit > totalConsulta) {
                limit = totalConsulta;
            }
            // var orderDtoList = consulta.Skip(offset).Take(limit).Select(o => mapper.Map<OrderDto>(o));
            var cartDtoList = query.Skip(offset).Take(limit).Select(c => new CartDto()
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
            });
            
            var result = new PaginatedList<CartDto>();
            result.Total = cartDtoList.Count();
            result.List = cartDtoList.ToList();

            return result;
        }
        public List<CartDto> GetByDateItemCount(DateTime startDate, DateTime endDate, int minItemCount = 0, int maxItemCount = 0)
        {
            var cartQuery = repository.GetAllIncluding(c => c.Client, c => c.CartItems);
            if (startDate < endDate) {
                cartQuery = cartQuery.Where(c => 
                    c.Date >= startDate && c.Date <= endDate
                );
            }
            if(startDate == endDate) {
                cartQuery = cartQuery.Where(c => 
                    c.Date == startDate
                );
            }
            if (minItemCount < maxItemCount) {
                cartQuery = cartQuery.Where(c => 
                    c.CartItems.Count >= minItemCount && c.CartItems.Count <= maxItemCount
                );
            }
            if (minItemCount > 0 && maxItemCount <= 0) {
                cartQuery = cartQuery.Where(c => 
                    c.CartItems.Count >= minItemCount
                );
            }
            if (minItemCount == maxItemCount) {
                cartQuery = cartQuery.Where(c => 
                    c.CartItems.Count == minItemCount
                );
            }
            var cartDtoList = cartQuery.Select(c => new CartDto()
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
            });
            return cartDtoList.ToList();
        }
    }
}