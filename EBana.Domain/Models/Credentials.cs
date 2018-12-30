using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EBana.Domain.Models
{
	[Table("Credentials")]
	public class Credentials
	{
		[Key]
		public int IdCredentials { get; set; }
		
		public string Password { get; set; }
	}
}
