using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Curso.ECommerce.Application.Dto;
using Curso.ECommerce.Domain.enums;
using Curso.ECommerce.Domain.Models;
using Curso.ECommerce.Domain.Repository;

namespace Curso.ECommerce.Application.Service
{
    public class OrderAppSerivce : IOrderAppService
    {
        private readonly IOrderRepository repository;
        private readonly IProductAppService productService;
        public OrderAppSerivce(IOrderRepository repository, IProductAppService productService)
        {
            this.productService = productService;
            this.repository = repository;


        }

        public async Task<OrderDto> CreateAsync(OrderCreateDto order)
        {
            // Validaciones
            if (order.OrderItems.Count == 0)
            {
                throw new ArgumentException("Se ha tratado de crear una orden sin items");
            }
            // Stock del producto
            var productIdList = order.OrderItems.Select(i => i.ProductId);
            var itemProductList = await productService.GetAllByIdAsync(productIdList.ToList());
            // TODO: Colocar logs en el caso de que no exista algun producto en específico
            if (itemProductList.Count == 0)
            {
                // TODO: Especificar los productos que no existen
                throw new ArgumentException("Los productos indicados no existen");
            }

            Order orderEntity = new Order();
            string notes = String.Empty;
            foreach (var product in itemProductList)
            {
                long quantity = order.OrderItems.Where(i => i.ProductId == product.Id).Select(i => i.Quantity).SingleOrDefault();
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
                    OrderItem orderItem = new OrderItem();
                    orderItem.ProductId = product.Id;
                    orderItem.Price = product.Price;
                    orderItem.Quantity = quantity;
                    orderItem.Notes = order.OrderItems.Where(i => i.ProductId == product.Id).Select(i => i.Notes).SingleOrDefault();
                    orderEntity.AddOrderItem(orderItem);
                }
            }
            if (orderEntity.OrderItems.Count == 0)
            {
                throw new ArgumentException("No se ha podido crear la orden, productos no disponibles");
            }
            else
            {
                orderEntity.ClientId = order.ClientId;
                orderEntity.Status = OrderStatus.Registered;
                orderEntity.Date = order.Date;
                orderEntity.Total = orderEntity.OrderItems.Sum(x => x.Price * x.Quantity);
                orderEntity.Notes = notes;

                // Actualizar stock del producto


                // Persistencia del objeto
                orderEntity = await repository.AddAsync(orderEntity);
                await repository.UnitOfWork.SaveChangesAsync();

                var orderDto = await GetByIdAsync(orderEntity.Id);

                // Confirmacion de la orden
                await ConfirmOrder(orderEntity);

                return orderDto;
            }

        }

        private async Task ConfirmOrder(Order order)
        {
            // TODO: Proceso de confirmacion de la orden 
            var productIdList = order.OrderItems.Select(i => i.ProductId);
            var productList = await productService.GetAllByIdAsync(productIdList.ToList());
            // Actualizacion del stock de los productos
            foreach (var orderItem in order.OrderItems)
            {
                var productDto = productList.Where(p => p.Id == orderItem.ProductId).SingleOrDefault();
                var product = new ProductUpdateStockDto();
                product.Stock = (int?)(productDto.Stock - orderItem.Quantity);
                await productService.UpdateStockAsync(productDto.Id, product);

            }
            order.Status = OrderStatus.Processed;
            await repository.UpdateAsync(order);
            await repository.UnitOfWork.SaveChangesAsync();
            return;

        }


        // TODO: Validar si las órdenes pueden eliminarse
        public async Task<bool> DeleteAsync(Guid orderId)
        {
            //Reglas Validaciones... 
            var orderEntity = await repository.GetByIdAsync(orderId);
            if (orderEntity == null)
            {
                throw new ArgumentException($"La orden con el id: {orderId}, no existe");
            }

            repository.Delete(orderEntity);
            await repository.UnitOfWork.SaveChangesAsync();

            return true;
        }

        public ICollection<OrderDto> GetAll()
        {
            var query = repository.GetAllIncluding(o => o.Client, o => o.OrderItems);
            var orderDtoList = query.Select(o => new OrderDto()
            {
                CancellationDate = o.CancellationDate,
                ClientId = o.ClientId,
                Client = o.Client.Name,
                Date = o.Date,
                Id = o.Id,
                Notes = o.Notes,
                Status = o.Status,
                Total = o.Total,
                OrderItems = o.OrderItems.Select(i => new OrderItemDto()
                {
                    Id = i.Id,
                    Notes = i.Notes,
                    OrderId = i.OrderId,
                    Price = i.Price,
                    Product = i.Product.Name,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }).ToList()
            });
            return orderDtoList.ToList();
        }

        public Task UpdateAsync(Guid orderId, OrderUpdateDto order)
        {
            //TODO : Implementar update en order
            throw new NotImplementedException();
        }

        public async Task<OrderDto> GetByIdAsync(Guid orderId)
        {
            var query = repository.GetAllIncluding(o => o.Client, o => o.OrderItems);
            query = query.Where(o => o.Id == orderId);

            var orderDto = query.Select(o => new OrderDto()
            {
                CancellationDate = o.CancellationDate,
                ClientId = o.ClientId,
                Client = o.Client.Name,
                Date = o.Date,
                Id = o.Id,
                Notes = o.Notes,
                Status = o.Status,
                Total = o.Total,
                OrderItems = o.OrderItems.Select(i => new OrderItemDto()
                {
                    Id = i.Id,
                    Notes = i.Notes,
                    OrderId = i.OrderId,
                    Price = i.Price,
                    Product = i.Product.Name,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity

                }).ToList()
            }).SingleOrDefault();
            return orderDto;
        }
    }
}