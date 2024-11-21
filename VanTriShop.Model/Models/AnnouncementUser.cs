using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanTriShop.Model.Models
{
	[Table("AnnouncementUsers")]
	public class AnnouncementUser
	{
		
		[Column(Order = 1)]
		public int AnnouncementId { get; set; }


		[Column(Order = 2)]
		public string UserId { get; set; }

		public bool HasRead { get; set; }

		[ForeignKey("UserId")]
		public virtual AppUser AppUser { get; set; }

		[ForeignKey("AnnouncementId")]
		public virtual Announcement Announcement { get; set; }
	}
}
