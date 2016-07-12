using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using FullTextSearch.Models;

namespace FullTextSearch.Db
{
    public class WikiContext : DbContext
    {
        public WikiContext() : base("WikiConnection") {
            DbInterception.Add(new FtsInterceptor());
        }

        public DbSet<WikiReference> WikiReferences { get; set; }

        
    }
}