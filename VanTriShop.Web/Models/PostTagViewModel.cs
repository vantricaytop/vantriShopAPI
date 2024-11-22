using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VanTriShop.Model.Models;
using VanTriShop.Web.Models.Common;

namespace VanTriShop.Web.Models
{
    public class PostTagViewModel
	{
		public int PostID { get; set; }
		public string TagID { get; set; }

		public virtual PostViewModel Post { get; set; }
		public virtual TagViewModel Tag { get; set; }
	}
}
