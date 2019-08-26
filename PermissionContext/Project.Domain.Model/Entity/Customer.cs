using System;
using System.ComponentModel.DataAnnotations;
using ProjectCore.Common.DomainInterfaces;
using ProjectCore.Domain.Model.ValueObject;

namespace ProjectCore.Domain.Model.Entity
{
   public class Customer: IAggregationRoot, ISoftDelete
    {
        [Key]      
        public Guid Id { get; set; }
        /// <summary>
        /// 客户唯一标识
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 客户姓名
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 客户年龄
        /// </summary>
        public string CustomerAge { get; set; }
        /// <summary>
        /// 客户电话
        /// </summary>
        public string CustomerPhone { get; set; }
        /// <summary>
        /// 客户地址
        /// </summary>
        public Address CustomerAddress { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        public Customer()
        {
        }

        /// <summary>
        /// 验证实体，以及创建客户
        /// </summary>
        /// <param name="code">标识</param>
        /// <param name="customerName">客户名称</param>
        /// <param name="customerAge">客户年龄</param>
        /// <param name="customerPhone">客户电话</param>
        /// <param name="customerAddrDto">客户地址</param>
        public Customer CreateCustomer(string code, string customerName, string customerAge, string customerPhone, Address customerAddrDto)
        {
            if (!string.IsNullOrEmpty(customerName))
            {
                throw new ArgumentException("客户ID为空！");
            }
            this.Code = code;
            this.CustomerAddress = new Address(customerAddrDto.Province, customerAddrDto.City, customerAddrDto.County, customerAddrDto.AddressDetails);
            this.CustomerAge = customerAge;
            this.CustomerName = customerName;
            this.CustomerPhone = customerPhone;
            return this;
        }
       

    }
}
