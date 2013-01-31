namespace Movies.Models
{
    using BCrypt.Net;

    public class User
    {
        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public bool Authenticate(string candidate)
		{
			return BCrypt.Verify(candidate, HashedPassword);
		}

		public virtual string HashedPassword { get; set; }

		private string _Password;

		public string Password
		{
			get
			{
				return _Password;
			}
			set
			{
				HashedPassword = BCrypt.HashPassword(value);
				_Password = value;
			}
		}
    }
}
