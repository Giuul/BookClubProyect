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
        public DbSet<Book> Books { get; set; }
        public DbSet<ReadingList> ReadingLists { get; set; }
        public DbSet<Vote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar relaciones
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

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Nombre = "Valentina García", Email = "valen@gmail.com", Password = "test1234", Rol = Rol.usuario },
                new User { Id = 2, Nombre = "Antonella Garcia", Email = "anto@gmail.com", Password = "test1234", Rol = Rol.usuario },
                new User { Id = 3, Nombre = "Giuliana Alonzo", Email = "giuli@gmail.com", Password = "test1234", Rol = Rol.admin }
            );

            modelBuilder.Entity<ReadingList>().HasData(
                new ReadingList { Id = 1, Titulo = "Favoritos de Valen", Descripcion = "Libros favoritos", CreadorId = 1 },
                new ReadingList { Id = 2, Titulo = "Lecturas de Anto", Descripcion = "Libros de programación", CreadorId = 2 }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Titulo = "Orgullo y Prejuicio", Autor = "Jane Austen", Genero = "Romance", ListId = 2 },
                new Book { Id = 2, Titulo = "El Hobbit", Autor = "J.R.R. Tolkien", Genero = "Fantasía", ListId = 1 },
                new Book { Id = 3, Titulo = "Cien años de soledad", Autor = "Gabriel García Márquez", Genero = "Realismo mágico", ListId = 1 }
            );

            modelBuilder.Entity<Vote>().HasData(
                new Vote { Id = 1, LibroId = 1, UsuarioId = 1, Valor = 5 },
                new Vote { Id = 2, LibroId = 2, UsuarioId = 2, Valor = 4 },
                new Vote { Id = 3, LibroId = 3, UsuarioId = 2, Valor = 5 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
