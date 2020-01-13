using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingWebApi.Models
{
    public class UserParams
    {
		private int maxSize = 25;
		public int CurrentPage { get; set; } = 1;
		private int pageSize = 10;

		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = value > maxSize ? maxSize : value; }
		}

		public int UserId { get; set; }
		public string Gender { get; set; }
		public int MinAge { get; set; } = 18;
		public int MaxAge { get; set; } = 99;
		public string OrderBy { get; set; }
	}
}
