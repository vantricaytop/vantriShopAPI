using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanTriShop.Model.Models
{
	[Table("Footers")]
	public class Footer
	{
		[Key]
		[MaxLength(50)]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }
		[Required]
		public string Content { get; set; }

	}
}
