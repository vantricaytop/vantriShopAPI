using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VanTriShop.Model.Models
{
	[Table("AppUsers")]
	public class AppUser : IdentityUser
	{
		[MaxLength(256)]
		public string FullName { get; set; }

		[MaxLength(256)]
		public string Address { get; set; }

		public string Avatar { get; set; }

		public DateTime? BirthDay { get; set; }

		public bool? Status { get; set; }
		public bool? Gender { get; set; }
		// Phương thức này tạo ClaimsPrincipal cho người dùng
		public async Task<ClaimsPrincipal> GenerateUserPrincipalAsync(UserManager<AppUser> manager)
		{
			var userClaims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, this.UserName),
				new Claim(ClaimTypes.Email, this.Email ?? string.Empty)
			};

			if (!string.IsNullOrEmpty(this.FullName))
			{
				userClaims.Add(new Claim("FullName", this.FullName));
			}

			var identity = new ClaimsIdentity(userClaims, IdentityConstants.ApplicationScheme);
			return new ClaimsPrincipal(identity);
		}

		public virtual IEnumerable<Order> Orders { get; set; }
	}
}
