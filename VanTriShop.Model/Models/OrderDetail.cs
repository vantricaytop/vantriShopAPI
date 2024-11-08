using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanTriShop.Model.Models
{
	[Table("OrderDetails")]
	public class OrderDetail
	{
		
		[Column(Order = 1)]
		public int OrderId { get; set; }

		
		[Column(Order = 2)]
		public int ProductId { get; set; }

		public int Quantity { get; set; }

		public decimal Price { get; set; }

		[ForeignKey("OrderId")]
		public virtual Order Order { get; set; }

		[ForeignKey("ProductId")]
		public virtual Product Product { get; set; }
	}
}
