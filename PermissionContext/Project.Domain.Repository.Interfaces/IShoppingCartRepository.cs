using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProjectCore.Domain.Model.Entity;

namespace ProjectCore.Domain.Repository.Interfaces
{
    public interface IShoppingCartRepository:IBaseRepository<ShoppingCart>
    {
        /// <summary>
        /// 获取购物车聚合跟
        /// </summary>
        /// <returns></returns>
        Task<ShoppingCart> GetShoppingCart(Guid customerId);     
        void CreateShoppingCart(ShoppingCart shoppingCart);
        void ModifyShoppingCart(ShoppingCart shoppingCart);
        void RemoveShoppingCart(ShoppingCart shoppingCart);
    }
}
