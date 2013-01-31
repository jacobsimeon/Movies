namespace Movies.Models
{
    using System.ComponentModel.DataAnnotations;

    using Movies.DataAccess;

    public class Movie : IPrimaryKeyable
    {
        public virtual int Id { get; set; }

        public virtual User User { get; set; }

        [Required]
        public virtual string Title { get; set; }

        [Required]
        public virtual string Genre { get; set; }

        public virtual int Rating { get; set; }
    }
}