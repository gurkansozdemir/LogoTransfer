﻿using LogoTransfer.Core.DTOs.IntegrationDTOs;
using LogoTransfer.Core.Entities;

namespace LogoTransfer.Core.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        public Task<List<OrderTransaction>> GetTransactionsByOrderId(Guid orderId);
        public Task<List<Order>> GettAllWithTransactions();
    }
}
