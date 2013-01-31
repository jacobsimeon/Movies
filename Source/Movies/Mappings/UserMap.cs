namespace Movies.Mappings
{
    using FluentNHibernate.Automapping;
    using FluentNHibernate.Automapping.Alterations;

    using Movies.Models;

    public class UserMap : IAutoMappingOverride<User>
    {
        public void Override(AutoMapping<User> mapping)
        {
            mapping.IgnoreProperty(u => u.Password);
            mapping.IgnoreProperty(u => u.PasswordConfirmation);
        }
    }
}