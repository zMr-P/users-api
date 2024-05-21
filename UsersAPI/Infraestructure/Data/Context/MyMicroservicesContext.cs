using Microsoft.EntityFrameworkCore;
using UsersAPI.Domain.Entities;

namespace UsersAPI.Infraestructure.Data.Context
{
    public class MyMicroservicesContext : DbContext
    {
        public MyMicroservicesContext(DbContextOptions<MyMicroservicesContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
    }

}
