using AspServer.Models;

namespace AspServer
{
    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<User> Users([Service] ShareDb db)
        {
            return db.Users;
        }
    }
}
