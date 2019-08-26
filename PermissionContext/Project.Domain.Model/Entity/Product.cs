using System;
using System.ComponentModel.DataAnnotations;
using ProjectCore.Common.DomainInterfaces;

namespace ProjectCore.Domain.Model.Entity
{
   public class Product:IAggregationRoot, ISoftDelete
    {
        [Key]
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public int ProductStock { get; set; }
        public string ProductColor { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
        public Product CreateProduct(string code, string productName, int productPrice, int productStock, string productColor)
        {
            this.Code = code;
            this.ProductColor = productColor;
            this.ProductName = productName;
            this.ProductPrice = productPrice;
            this.ProductStock = productStock;
            return this;
        }

    }
}
