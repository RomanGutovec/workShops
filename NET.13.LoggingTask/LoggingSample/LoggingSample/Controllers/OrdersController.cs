using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Routing;
using LoggingSample_BLL.Exceptions;
using LoggingSample_BLL.Helpers;
using LoggingSample_BLL.Models;
using LoggingSample_BLL.Services;
using LoggingSample_DAL.Context;
using LoggingSample_DAL.Entities;
using NLog;

namespace LoggingSample.Controllers
{
    [RoutePrefix("api")]
    public class OrdersController : ApiController
    {
        private readonly OrderService _orderService = new OrderService();
        private static Logger Logger = LogManager.GetCurrentClassLogger();

        [Route("customers/{customerId}/orders", Name = "Orders")]
        public async Task<IHttpActionResult> Get(int customerId)
        {
            Logger.Info($"Start getting all orders.");

            var customers = (await _orderService.GetOrdersAsync(customerId)).Select(InitOrder);

            Logger.Info($"Retrieving orders to response.");

            return Ok(customers);
        }

        [Route("customers/{customerId}/orders/{orderId}", Name = "Order")]
        public async Task<IHttpActionResult> Get(int customerId, int orderId)
        {
            Logger.Info($"Start getting order with id {orderId}.");

            try
            {
                var order = await _orderService.GetOrderAsync(customerId, orderId);

                if (order == null)
                {
                    Logger.Info($"No order with such id {orderId}.");
                    return NotFound();
                }

                return Ok(InitOrder(order));
            }
            catch (OrderServiceException ex)
            {
                if (ex.Type == OrderServiceException.ErrorType.WrongOrderId)
                {
                    Logger.Warn($"Wrong orderId has been requested: {orderId}.");
                }

                throw;
            }
            catch (Exception ex)
            {
                Logger.Warn(ex, $"Some error occured while getting orderId {orderId}.");
                throw;
            }
        }

        private object InitOrder(OrderModel model)
        {
            return new
            {
                _self = new UrlHelper(Request).Link("Order", new { customerId = model.CustomerId, orderId = model.Id }),
                customer = new UrlHelper(Request).Link("Customer", new { customerId = model.CustomerId }),
                data = model
            };
        }
    }
}