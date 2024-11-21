namespace VanTriShop.Web.Infrastructure.Core
{
	public class PaginationSet<T>
	{
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public int TotalRows { get; set; }
		public IEnumerable<T> Items { get; set; }
	}
}
