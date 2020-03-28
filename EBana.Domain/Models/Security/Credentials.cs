namespace EBana.Domain.Models
{
	public class Credentials
	{
		public int Id { get; private set; }
		
		public HashedPassword Password { get; private set; }

		private Credentials()
		{ }

		public Credentials(HashedPassword password)
		{
			Password = password;
		}
	}
}
