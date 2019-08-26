using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectCore.Domain.Model.Entity
{
   public class OrderDetails
    {    
        [Key]
        public Guid Id { get; set; }
        public string Code { get;set ; }
        public int OrderNumBer { get; set; }
        public decimal OrderDetailPrice { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order OrderInfo { get; set; }


        public OrderDetails()
        {

        }


        /// <summary>
        /// 创建订单明细
        /// </summary>
        /// <param name="product">商品</param>
        /// <param name="num">订单数量</param>
        /// <param name="id">订单Id</param>
        /// <returns></returns>
        public OrderDetails CreateOrderDetails(Product product,int num,Guid id)
        {
            this.Id = Guid.NewGuid();
            this.Code = product.ProductName + "订单明细";
            //this.OrderId = id;
            this.OrderNumBer = num;
            this.ProductId = product.Id;
            this.OrderDetailPrice = product.ProductPrice;
            return this;
        }


    }
}
