﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VanTriShop.Model.Abstract;

namespace VanTriShop.Model.Models
{
	[Table("Products")]
	public class Product : AudiTable
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		[MaxLength(256)]
		public string Name { get; set; }
		[Required]
		[MaxLength(256)]
		public string Alias { get; set; }
		public int CategoryID { get; set; }
		[MaxLength(256)]
		public string ThumbnailImage { get; set; }
		
		[Column(TypeName = "xml")]
		public string MoreImages	{ get; set; }

		public decimal Price { get; set; }
		public decimal? PromotionPrice	{ get; set; }
		public int? Warranty { get; set; }
		public string Content { get; set; }
		[MaxLength(500)]
		public string Description { get; set; }

		public bool? HomeFlag	{ get; set; }
		public bool? HotFlag { get; set; }

		public int? ViewCount	{ get; set; }

		public string Tags { get; set; }
		public int Quantity { get; set; }
		public decimal OriginalPrice { get; set; }

		[ForeignKey("CategoryID")]
		public virtual ProductCategory ProductCategory { get; set; }
		public virtual IEnumerable<ProductTag> ProductTags	{ get; set; }

	}
}
