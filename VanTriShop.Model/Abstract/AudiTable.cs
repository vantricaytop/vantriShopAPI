﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanTriShop.Model.Abstract
{
	public abstract class AudiTable : IAuditable
	{
		public DateTime? CreatedDate { get; set; }
		[MaxLength(256)]
		public string CreatedBy { get; set; }
		public DateTime? UpdatedDate { get; set; }
		[MaxLength(256)]
		public string UpdatedBy { get; set; }

		[MaxLength(256)]
		public string MetaKeyword { get; set; }
		[MaxLength(256)]
		public string MetaDescription { get; set; }
		public bool Status { get; set; }
	}
}
