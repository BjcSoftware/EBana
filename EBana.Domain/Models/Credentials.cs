namespace EBana.Domain.Models
{
	public class Credentials
	{
		public int Id { get; private set; }
		
		public string Password { get; private set; }

		private Credentials()
		{ }

		public Credentials(string password)
		{
			Password = password;
		}
	}
}
