using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<User> Users { get; set; }
        public DbSet<ReadingList> ReadingLists { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ReadingList>()
                .HasOne(rl => rl.Creador)
                .WithMany(u => u.ListasCreadas)
                .HasForeignKey(rl => rl.CreadorId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Book>()
                .HasOne(b => b.ListaLectura)
                .WithMany(rl => rl.Libros)
                .HasForeignKey(b => b.ListId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Usuario)
                .WithMany(u => u.Votos)
                .HasForeignKey(v => v.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Vote>()
                .HasOne(v => v.Libro)
                .WithMany(b => b.Votos)
                .HasForeignKey(v => v.LibroId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<User>()
                .Property(u => u.Rol)
                .HasConversion<string>();
        }
    }
}