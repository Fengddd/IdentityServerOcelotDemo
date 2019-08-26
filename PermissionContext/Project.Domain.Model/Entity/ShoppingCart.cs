using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProjectCore.Common.DomainInterfaces;

namespace ProjectCore.Domain.Model.Entity
{
    public class ShoppingCart : IAggregationRoot, ISoftDelete
    {
        public ShoppingCart()
        {
            this.ShoppingCartDetailList = new List<ShoppingCartDetails>();
        }

        [Key]
        public Guid Id { get; set; }
        public string Code { get;  set; }
        /// <summary>
        /// 购物车的总价格
        /// </summary>
        public double CartTotalPrice { get; private set; }
        /// <summary>
        /// 添加购物车的客户
        /// </summary>
        public Guid CustomerId { get; private set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 购物车的商品明细
        /// </summary>
        public List<ShoppingCartDetails> ShoppingCartDetailList { get;private set; }

        public ShoppingCart(Guid customerId, Product product, int numBer)
        {
            this.Id = Guid.NewGuid();
            this.Code = "购物车";
            this.CustomerId = customerId;
            var cartDetail=new ShoppingCartDetails().CreateCartDetails(product, numBer, this.Id);
            this.ShoppingCartDetailList = new List<ShoppingCartDetails>();
            this.ShoppingCartDetailList.Add(cartDetail);
            double totlePrice = 0;
            foreach (var shoppingCart in ShoppingCartDetailList)
            {
                totlePrice += shoppingCart.CartDetailPrice;
            }
            this.CartTotalPrice = totlePrice;
        }


        /// <summary>
        /// 购物车中选择商品计算中价格
        /// </summary>
        /// <param name="code">标识</param>
        /// <param name="shoppingCartDetailList">商品明细</param>
        /// <returns></returns>
        public ShoppingCart TotleShoppingCart(string code, List<ShoppingCartDetails> shoppingCartDetailList)
        {
            this.Code = code;
            this.ShoppingCartDetailList = shoppingCartDetailList;
            double totlePrice = 0;
            foreach (var cartDetailse in shoppingCartDetailList)
            {
                totlePrice += cartDetailse.ShopProductNumBer * cartDetailse.ShopProductPrice;
            }
            this.CartTotalPrice = totlePrice;
            return this;
        }

        /// <summary>
        /// 创建购物车
        /// </summary>        
        /// <param name="customerId"></param>
        /// <param name="product"></param>
        /// <param name="numBer"></param>
        /// <returns></returns>
        public ShoppingCart CreateShoppingCart(Guid customerId, Product product, int numBer)
        {                                     
            return new ShoppingCart(customerId,product ,numBer);
        }
        /// <summary>
        /// 购物车中添加商品
        /// </summary>
        /// <param name="product"></param>
        /// <param name="numBer"></param>
        /// <param name="shoppingCareId"></param>
        /// <returns></returns>
        public ShoppingCart AddProductShoppingCart(Product product, int numBer,Guid shoppingCareId)
        {
            var shoppingCartDetail = new ShoppingCartDetails().CreateCartDetails(product,numBer, shoppingCareId);
            this.ShoppingCartDetailList.Add(shoppingCartDetail);
            return this;
        }

        /// <summary>
        /// 移除购物车中的商品
        /// </summary>    
        /// <returns></returns>
        public void RemoveShoppingCart(ShoppingCartDetails shoppingCartDetail)
        {
            this.ShoppingCartDetailList.Remove(shoppingCartDetail);
        }
        
    }
}
