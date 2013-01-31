namespace Movies.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using BCrypt.Net;

    using Movies.DataAccess;

    public class User : IPrimaryKeyable
    {
        public User()
        {
            Movies = new List<Movie>();
        }

        public virtual int Id { get; set; }

        public virtual IList<Movie> Movies { get; set; }

        [Required]
        public virtual string Name { get; set; }

		public virtual string HashedPassword { get; set; }

        public virtual bool Authenticate(string candidate)
		{
			return BCrypt.Verify(candidate, HashedPassword);
		}

		private string _Password;

        [Required]
		public virtual string Password
		{
			get
			{
				return _Password;
			}
			set
			{
                if (value != null)
                {
    				HashedPassword = BCrypt.HashPassword(value);
                }
				_Password = value;
			}
		}

        [Required]
        public virtual string PasswordConfirmation { get; set; }
    }
}
