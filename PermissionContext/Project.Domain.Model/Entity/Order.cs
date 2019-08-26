using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProjectCore.Common.DomainInterfaces;

namespace ProjectCore.Domain.Model.Entity
{  
   public class Order:IAggregationRoot, ISoftDelete
    {
        public Order()
        {
            this.OrderDetailList = new List<OrderDetails>();
        }
        [Key]       
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string OrderCreateDate { get; set; }
        public decimal OrderTotlePrice { get; set; }
        public Guid CustomrId { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
        public  List<OrderDetails> OrderDetailList { get;private set; }

        public Order(Guid customrId, List<Product> productList, int numBer)
        {
            if (productList.Count == 0)
            {
                throw new ArgumentException("选择的商品为空！");
            }
            this.Id = Guid.NewGuid();
            this.CustomrId = customrId;
            this.OrderCreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.OrderDetailList = new List<OrderDetails>();
            decimal price = 0;
            foreach (var product in productList)
            {
                var orderDetail = new OrderDetails().CreateOrderDetails(product, numBer, this.Id);
                this.OrderDetailList.Add(orderDetail);
                price += product.ProductPrice * numBer;
                this.Code = product.ProductName + "订单";
            }

            this.OrderTotlePrice = price;           
        }

        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="customrId">客户Id</param>
        /// <param name="productList">商品</param>
        /// <param name="numBer">商品数量</param>
        public Order CreateOrder(Guid customrId, List<Product> productList, int numBer)
        {          
            return new Order(customrId,productList,numBer);
        }

        public void RemoveOrder(OrderDetails orderDetail)
        {
            this.OrderDetailList.Remove(orderDetail);
        }

    }
}
