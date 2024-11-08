﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VanTriShop.Model.Abstract;

namespace VanTriShop.Model.Models
{
	[Table("Pages")]
	public class Page : AudiTable
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ID { set; get; }

		[Required]
		[MaxLength(256)]
		public string Name { set; get; }

		[Column(TypeName = "varchar")]
		[MaxLength(256)]
		[Required]
		public string Alias { set; get; }

		public string Content { set; get; }
	}
}
