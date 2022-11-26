﻿using FikaAmazonAPI.AmazonSpApiSDK.Models.Orders;
using FikaAmazonAPI.AmazonSpApiSDK.Models.Token;
using FikaAmazonAPI.Parameter.Order;
using FikaAmazonAPI.Search;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FikaAmazonAPI.Services
{
    public class OrderService : RequestService
    {
        public OrderService(AmazonCredential amazonCredential) : base(amazonCredential)
        {

        }

        #region GetOrders

        public OrderList GetOrders(ParameterOrderList searchOrderList) =>
            Task.Run(() => GetOrdersAsync(searchOrderList)).ConfigureAwait(false).GetAwaiter().GetResult();
        public async Task<OrderList> GetOrdersAsync(ParameterOrderList searchOrderList)
        {

            var orderList = new OrderList();

            if (searchOrderList.MarketplaceIds == null || searchOrderList.MarketplaceIds.Count == 0)
            {
                searchOrderList.MarketplaceIds = new List<string>();
                searchOrderList.MarketplaceIds.Add(AmazonCredential.MarketPlace.ID);
            }

            if (searchOrderList.IsNeedRestrictedDataToken && searchOrderList.RestrictedDataTokenRequest == null)
            {
                var restrictedResource = new RestrictedResource();
                restrictedResource.method = Method.GET.ToString();
                restrictedResource.path = OrdersApiUrls.Orders;
                restrictedResource.dataElements = new List<string> { "buyerInfo", "shippingAddress" };


                var createRDT = new CreateRestrictedDataTokenRequest()
                {
                    restrictedResources = new List<RestrictedResource> { restrictedResource }
                };
                searchOrderList.RestrictedDataTokenRequest = createRDT;
            }

            var queryParameters = searchOrderList.getParameters();

            await CreateAuthorizedRequestAsync(OrdersApiUrls.Orders, RestSharp.Method.Get, queryParameters, parameter: searchOrderList);
            var response = await ExecuteRequestAsync<GetOrdersResponse>(Utils.RateLimitType.Order_GetOrders);
            var nextToken = response.Payload.NextToken;
            orderList = response.Payload.Orders;
            int PageCount = 1;
            if (searchOrderList.MaxNumberOfPages.HasValue && searchOrderList.MaxNumberOfPages.Value == 1)
            {
                orderList.NextToken = nextToken;
            }
            else
            {
                while (!string.IsNullOrEmpty(nextToken))
                {
                    var orderPayload = GetGetOrdersByNextToken(nextToken, searchOrderList);
                    orderList.AddRange(orderPayload.Orders);
                    nextToken = orderPayload.NextToken;

                    if (searchOrderList.MaxNumberOfPages.HasValue)
                    {
                        PageCount++;
                        if (PageCount >= searchOrderList.MaxNumberOfPages.Value)
                            break;
                    }
                }
            }

            return orderList;
        }

        public OrdersList GetGetOrdersByNextToken(string nextToken, ParameterOrderList searchOrderList) =>
            Task.Run(() => GetGetOrdersByNextTokenAsync(nextToken, searchOrderList)).ConfigureAwait(false).GetAwaiter().GetResult();
        public async Task<OrdersList> GetGetOrdersByNextTokenAsync(string nextToken, ParameterOrderList searchOrderList)
        {
            List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
            queryParameters.Add(new KeyValuePair<string, string>("NextToken", nextToken));
            queryParameters.Add(new KeyValuePair<string, string>("MarketplaceIds", string.Join(",", searchOrderList.MarketplaceIds)));

            await CreateAuthorizedRequestAsync(OrdersApiUrls.Orders, RestSharp.Method.Get, queryParameters, parameter: searchOrderList);
            var response = await ExecuteRequestAsync<GetOrdersResponse>(Utils.RateLimitType.Order_GetOrders);
            return response.Payload;
        }

        #endregion

        public Order GetOrder(ParameterGetOrder parameter) =>
            Task.Run(() => GetOrderAsync(parameter)).ConfigureAwait(false).GetAwaiter().GetResult();
        public async Task<Order> GetOrderAsync(ParameterGetOrder parameter)
        {
            if (parameter.IsNeedRestrictedDataToken && parameter.RestrictedDataTokenRequest == null)
            {
                var restrictedResource = new RestrictedResource();
                restrictedResource.method = Method.GET.ToString();
                restrictedResource.path = OrdersApiUrls.Order(parameter.OrderId);
                restrictedResource.dataElements = new List<string> { "buyerInfo", "shippingAddress" };


                var createRDT = new CreateRestrictedDataTokenRequest()
                {
                    restrictedResources = new List<RestrictedResource> { restrictedResource }
                };
                parameter.RestrictedDataTokenRequest = createRDT;
            }

            await CreateAuthorizedRequestAsync(OrdersApiUrls.Order(parameter.OrderId), RestSharp.Method.Get, parameter: parameter);
            var response = await ExecuteRequestAsync<GetOrderResponse>(Utils.RateLimitType.Order_GetOrder);
            if (response != null && response.Payload != null)
                return response.Payload;
            else return null;
        }
        public OrderItemList GetOrderItems(string orderId, IParameterBasedPII parameterBasedPII = null) =>
            Task.Run(() => GetOrderItemsAsync(orderId, parameterBasedPII)).ConfigureAwait(false).GetAwaiter().GetResult();
        public async Task<OrderItemList> GetOrderItemsAsync(string orderId, IParameterBasedPII ParameterBasedPII = null)
        {
            var orderItemList = new OrderItemList();

            if (ParameterBasedPII != null && ParameterBasedPII.IsNeedRestrictedDataToken && ParameterBasedPII.RestrictedDataTokenRequest == null)
            {
                var restrictedResource = new RestrictedResource();
                restrictedResource.method = Method.GET.ToString();
                restrictedResource.path = OrdersApiUrls.OrderItems(orderId);
                restrictedResource.dataElements = new List<string> { "buyerInfo", "shippingAddress" };

                var createRDT = new CreateRestrictedDataTokenRequest()
                {
                    restrictedResources = new List<RestrictedResource> { restrictedResource }
                };
                ParameterBasedPII.RestrictedDataTokenRequest = createRDT;
            }

            await CreateAuthorizedRequestAsync(OrdersApiUrls.OrderItems(orderId), RestSharp.Method.Get, parameter: ParameterBasedPII);
            var response = await ExecuteRequestAsync<GetOrderItemsResponse>(Utils.RateLimitType.Order_GetOrderItems);
            var nextToken = response.Payload.NextToken;
            orderItemList.AddRange(response.Payload.OrderItems);
            while (!string.IsNullOrEmpty(nextToken))
            {
                var orderItemPayload = GetOrderItemsNextToken(orderId, nextToken);
                orderItemList.AddRange(orderItemPayload.OrderItems);
                nextToken = orderItemPayload.NextToken;
            }
            return orderItemList;
        }

        public OrderItemsList GetOrderItemsNextToken(string orderId, string nextToken) =>
            Task.Run(() => GetOrderItemsNextTokenAsync(orderId, nextToken)).ConfigureAwait(false).GetAwaiter().GetResult();
        public async Task<OrderItemsList> GetOrderItemsNextTokenAsync(string orderId, string nextToken)
        {
            List<KeyValuePair<string, string>> queryParameters = new List<KeyValuePair<string, string>>();
            queryParameters.Add(new KeyValuePair<string, string>("NextToken", nextToken));


            await CreateAuthorizedRequestAsync(OrdersApiUrls.OrderItems(orderId), RestSharp.Method.Get, queryParameters);
            var response = await ExecuteRequestAsync<GetOrderItemsResponse>(Utils.RateLimitType.Order_GetOrderItems);
            return response.Payload;
        }

        public OrderBuyerInfo GetOrderBuyerInfo(string orderId, List<KeyValuePair<string, string>> queryParameters = null) =>
            Task.Run(() => GetOrderBuyerInfoAsync(orderId, queryParameters)).ConfigureAwait(false).GetAwaiter().GetResult();
        public async Task<OrderBuyerInfo> GetOrderBuyerInfoAsync(string orderId, List<KeyValuePair<string, string>> queryParameters = null)
        {
            await CreateAuthorizedRequestAsync(OrdersApiUrls.OrderBuyerInfo(orderId), RestSharp.Method.Get, queryParameters);
            var response = await ExecuteRequestAsync<GetOrderBuyerInfoResponse>(Utils.RateLimitType.Order_GetOrderBuyerInfo);
            return response.Payload;
        }

        public OrderItemsBuyerInfoList GetOrderItemsBuyerInfo(string orderId) =>
            Task.Run(() => GetOrderItemsBuyerInfoAsync(orderId)).ConfigureAwait(false).GetAwaiter().GetResult();
        public async Task<OrderItemsBuyerInfoList> GetOrderItemsBuyerInfoAsync(string orderId)
        {
            await CreateAuthorizedRequestAsync(OrdersApiUrls.OrderItemsBuyerInfo(orderId), RestSharp.Method.Get);
            var response = await ExecuteRequestAsync<GetOrderItemsBuyerInfoResponse>(Utils.RateLimitType.Order_GetOrderItemsBuyerInfo);
            return response.Payload;
        }

        public Address GetOrderAddress(string orderId) =>
            Task.Run(() => GetOrderAddressAsync(orderId)).ConfigureAwait(false).GetAwaiter().GetResult();
        public async Task<Address> GetOrderAddressAsync(string orderId)
        {
            await CreateAuthorizedRequestAsync(OrdersApiUrls.OrderShipmentInfo(orderId), RestSharp.Method.Get);
            var response = await ExecuteRequestAsync<GetOrderAddressResponse>(Utils.RateLimitType.Order_GetOrderAddress);
            return response.Payload.ShippingAddress;
        }
    }
}
