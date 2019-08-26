using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectCore.Common.DomainInterfaces;

namespace ProjectCore.Domain.Model.Entity
{
    public class ShoppingCartDetails : IEntity
    {
        [Key]     
        public Guid Id { get; set; }
        public string Code { get; set; }
        /// <summary>
        /// 商品的名称
        /// </summary>
        public string ShopProductName { get; private set; }
        /// <summary>
        /// 商品的颜色
        /// </summary>
        public string ShopProductColor { get; private set; }
        /// <summary>
        /// 商品的价格
        /// </summary>
        public double ShopProductPrice { get; private set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int ShopProductNumBer { get; private set; }
        /// <summary>
        /// 商品的总价
        /// </summary>
        public double CartDetailPrice { get; private set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        public Guid ProductId { get;  set; }
        public Guid? ShoppingCartId { get; set; }
        [ForeignKey("ShoppingCartId")]
        public virtual ShoppingCart ShoppingCart { get; set; }

        public ShoppingCartDetails()
        {

        }
        public ShoppingCartDetails(Product product, int numBer,Guid shoppingCartId)
        {          
            this.Id = Guid.NewGuid();
            this.Code = product.ProductName + "购物车明细";
            this.ShopProductNumBer = numBer;
            this.ProductId = product.Id;
            this.ShopProductColor = product.ProductColor;
            this.ShopProductName = product.ProductName;
            this.ShopProductPrice = product.ProductPrice;
            this.ShoppingCartId = shoppingCartId;
            this.CartDetailPrice = numBer * product.ProductPrice;
        }
        /// <summary>
        /// 创建购物车明细
        /// </summary>
        /// <param name="product">商品</param>
        /// <param name="numBer">数量</param>
        /// <param name="shoppingCartId">购物车id</param>
        /// <returns></returns>
        public ShoppingCartDetails CreateCartDetails(Product product, int numBer, Guid shoppingCartId)
        {
            return new ShoppingCartDetails(product,numBer,shoppingCartId);
        }
        /// <summary>
        /// 修改明细的数量
        /// </summary>
        /// <param name="numBer"></param>
        public void ModifyShoppingCartDetailsNumBer(int numBer)
        {
            this.ShopProductNumBer = ShopProductNumBer + numBer;
            this.CartDetailPrice = this.ShopProductPrice * this.ShopProductNumBer;
        }


    }
}
