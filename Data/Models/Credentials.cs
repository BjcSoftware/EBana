using System;
using System.Data.Linq.Mapping;
using System.ComponentModel.DataAnnotations;

namespace EBana.Models
{
	[Table(Name = "Credentials")]
	public class Credentials
	{
		[Key]
		public int IdCredentials { get; set; }
		
		public String Password { get; set; }
	}
}
