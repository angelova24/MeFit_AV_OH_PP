using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace MeFitAPI.DAL.Models.Data
{
    public class MeFitDbContext : DbContext
    {

        public MeFitDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
