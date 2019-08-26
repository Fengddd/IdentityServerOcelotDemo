using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProjectCore.Domain.Model.Entity;

namespace ProjectCore.Domain.Repository.Interfaces
{
    public interface IOrderRepository:IBaseRepository<Order>
    {
        Task<Order> GetOrder(Guid customerId);
        void CreateOrder(Order order);

        void RemoveOrder(Order order);
    } 
    
}
