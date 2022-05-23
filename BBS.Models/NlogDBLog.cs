using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Models
{
	public class NlogDBLog
	{
		[Key]
		public int Id { get; set; }
		public string Application { get; set; }
		public TimeSpan Logged { get; set; }
		public string Level { get; set; }
		public string Message { get; set; }
		public string Logger { get; set; }
		public string Callsite { get; set; }
		public string Exception { get; set; } 
	}
}
