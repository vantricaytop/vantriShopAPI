using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanTriShop.Model.Models
{
	[Table("Permissions")]
	public class Permission
	{
		[Key]
		public int ID { get; set; }

		[MaxLength(450)]
		public string RoleId { get; set; }

		[StringLength(50)]
		[Column(TypeName = "varchar")]
		public string FunctionId { get; set; }

		public bool CanCreate { set; get; }

		public bool CanRead { set; get; }

		public bool CanUpdate { set; get; }

		public bool CanDelete { set; get; }

		[ForeignKey("RoleId")]
		public AppRole AppRole { get; set; }
		public Function Function { get; set; }
	}
}
