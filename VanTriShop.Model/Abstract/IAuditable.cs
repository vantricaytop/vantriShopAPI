﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanTriShop.Model.Abstract
{
	public interface IAuditable
	{
		DateTime? CreatedDate { get; set; }
		string CreatedBy { get; set; }
		DateTime? UpdatedDate { get; set; }
		string UpdatedBy { get; set; }

		string MetaKeyword { get; set; }
		string MetaDescription { get; set; }
		bool Status { get; set; }
	}
}
