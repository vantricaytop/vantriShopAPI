using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VanTriShop.Model.Models
{
	[Table("AppRoles")]
	public class AppRole : IdentityRole
	{
		public AppRole() : base() { }

		public AppRole(string name, string description): base(name)
		{
			this.Description = description;
		}

		public virtual string Description { get; set; }
	}
}
