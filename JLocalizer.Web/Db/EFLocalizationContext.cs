using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace JLocalizer.Web.Db
{
    public class EFLocalizationContext : DbContext
    {
        public EFLocalizationContext(DbContextOptions<EFLocalizationContext> options)
            : base(options)
        {
        }

        public DbSet<Localization> Langs { get; set; }
    }

    public class Localization
    {
        public int Id { get; set; }

        public string Lang { get; set; }

        public List<Data> Data { get; set; }
    }

    public class Data 
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
    }
}
