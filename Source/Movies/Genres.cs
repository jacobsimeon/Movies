namespace Movies
{
    using System.Collections.Generic;

    public class Genres 
    {
        public static IEnumerable<string> All = new[] {
            "Comedy", "Drama", "Action",
            "Horror", "Sci-Fi", "Fantasy"
        };
    }
}