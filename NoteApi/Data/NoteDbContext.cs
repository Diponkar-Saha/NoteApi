using Microsoft.EntityFrameworkCore;
using NoteApi.Model.Entities;

namespace NoteApi.Data
{
    public class NoteDbContext : DbContext
    {
        public NoteDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Note> Notes { get; set;}
    }
}
