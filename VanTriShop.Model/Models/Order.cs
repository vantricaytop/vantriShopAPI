﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanTriShop.Model.Models
{
	[Table("Orders")]
	public class Order
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[MaxLength(256)]
		public string CustomerName { get; set; }

		[Required]
		[MaxLength(256)]
		public string CustomerAddress { get; set; }

		[Required]
		[MaxLength(256)]
		public string CustomerEmail { get; set; }

		[Required]
		[MaxLength(50)]
		public string CustomerMobile {  get; set; }

		[Required]
		[MaxLength(256)]
		public string CustomerMessage { get; set; }

		[MaxLength(256)]
		public string PaymentMethod { get; set; }

		public DateTime? CreatedDate { get; set; }
		public string CreatedBy		{ get; set; }

		public string PaymentStatus { get; set; }

		public bool Status {  get; set; }

	/*	[StringLength(256)]
		[Column(TypeName = "nvarchar")]
		public string CustomerID { get; set; }

		[ForeignKey("CustomerID")]
		public virtual ApplicationUser User { get; set; }*/

		public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
	}
}
