namespace Movies.Mappings
{
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    using Movies.Models;

    public class MovieMap : IAutoMappingOverride<Movie>
    {
        public void Override(AutoMapping<Movie> mapping)
        {
            // mapping.HasOne(m => m.Owner).ForeignKey("Owner_id");
        }
    }
}