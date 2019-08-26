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
   public class ShoppingCartRepository:BaseRepository<ShoppingCart>, IShoppingCartRepository
   {
        private readonly MyContext _myContext;
        public ShoppingCartRepository(MyContext contxt):base(contxt)
        {
            _myContext = contxt;
        }  
        /// <summary>
        /// 创建购物车
        /// </summary>
        /// <param name="shoppingCart"></param>
        public void CreateShoppingCart(ShoppingCart shoppingCart)
        {
            _myContext.ShoppingCartInfo.Add(shoppingCart);
        }
        /// <summary>
        /// 获取购物车
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<ShoppingCart> GetShoppingCart(Guid customerId)
        {
           return await _myContext.ShoppingCartInfo.Include(e=>e.ShoppingCartDetailList).SingleOrDefaultAsync(e=>e.CustomerId==customerId);
        }

        /// <summary>
        /// 修改购物车
        /// </summary>
        /// <param name="shoppingCart"></param>
        public void ModifyShoppingCart(ShoppingCart shoppingCart)
        {
            _myContext.ShoppingCartInfo.Update(shoppingCart);
        }
        /// <summary>
        /// 移除购物车中的商品
        /// </summary>
        /// <param name="shoppingCart"></param>
        public void RemoveShoppingCart(ShoppingCart shoppingCart)
        {
            _myContext.ShoppingCartInfo.Update(shoppingCart);
        }
    }
}
