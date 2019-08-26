using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectCore.Domain.Model.Entity;
using ProjectCore.Domain.Repository.Interfaces;
using ProjectCore.EntityFrameworkCore;


namespace ProjectCore.Infrastructure.Repository
{
   public class OrderRepository:BaseRepository<Order>,IOrderRepository
   {
       private readonly MyContext _myContext;
        public OrderRepository(MyContext context):base(context)
        {
            _myContext = context;
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="order"></param>
        public  void CreateOrder(Order order)
        {
            try
            {

                 _myContext.OrderInfo.Add(order);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }         
        }
        /// <summary>
        /// 获取订单
        /// </summary>
        /// <returns></returns>
        public async Task<Order> GetOrder(Guid customerId)
        {
            return await _myContext.OrderInfo.Include(e => e.OrderDetailList).SingleOrDefaultAsync(e => e.CustomrId == customerId);

        }

        public void RemoveOrder(Order order)
        {
            _myContext.OrderInfo.Update(order);
        }
    }
}
