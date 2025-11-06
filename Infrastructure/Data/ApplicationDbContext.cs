using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using BCrypt.Net;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        private readonly bool isTestingEnvironment;

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ReadingList> ReadingLists { get; set; }
        public DbSet<Vote> Votes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, bool isTestingEnvironment = false)
            : base(options)
        {
            this.isTestingEnvironment = isTestingEnvironment;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReadingList>()
                .HasOne(rl => rl.Creador)
                .WithMany(u => u.ListasCreadas)
                .HasForeignKey(rl => rl.CreadorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReadingList>()
                .Property(rl => rl.EsCompartida)
                .HasDefaultValue(false);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.ReadingLists)
                .WithMany(rl => rl.Libros)
                .UsingEntity<Dictionary<string, object>>(
                    "BookReadingLists",
                    j => j
                        .HasOne<ReadingList>()
                        .WithMany()
                        .HasForeignKey("ReadingListId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<Book>()
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                );

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

            modelBuilder.Entity<User>().HasData(CreateUserSeed());
            modelBuilder.Entity<ReadingList>().HasData(CreateReadingListSeed());
            modelBuilder.Entity<Book>().HasData(CreateBookSeed());
            modelBuilder.Entity<Vote>().HasData(CreateVoteSeed());
            modelBuilder.SharedTypeEntity<Dictionary<string, object>>("BookReadingLists").HasData(
                   new { BookId = 1, ReadingListId = 2 },
                   new { BookId = 2, ReadingListId = 1 },
                   new { BookId = 3, ReadingListId = 1 }
             );

            base.OnModelCreating(modelBuilder);
        }

        private User[] CreateUserSeed()
{
            if (isTestingEnvironment)
            {
                return new[]
                {
                    new User
                    {
                        Id = 1,
                        Nombre = "Test User",
                        Email = "test@bookclub.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("123456"), 
                        Rol = Rol.usuario
                    }
                };
            }

                return new[]
                {
                    new User
                    {
                        Id = 1,
                        Nombre = "Valentina García",
                        Email = "valen@gmail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("test1234"), 
                        Rol = Rol.usuario
                    },
                    new User
                    {
                        Id = 2,
                        Nombre = "Antonella Garcia",
                        Email = "anto@gmail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("test1234"),
                        Rol = Rol.usuario
                    },
                    new User
                    {
                        Id = 3,
                        Nombre = "Giuliana Alonzo",
                        Email = "giuli@gmail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("test1234"), 
                        Rol = Rol.admin
                    }
                };
        }

        private ReadingList[] CreateReadingListSeed()
        {
            return new[]
            {
                new ReadingList { Id = 1, Titulo = "Favoritos de Valen", Descripcion = "Libros favoritos", CreadorId = 1 },
                new ReadingList { Id = 2, Titulo = "Lecturas de Anto", Descripcion = "Libros de programación", CreadorId = 2 }
            };
        }

        private Book[] CreateBookSeed()
        {
            return new[]
            {
                new Book { Id = 1, Titulo = "Orgullo y Prejuicio", Autor = "Jane Austen", Genero = "Romance" },
                new Book { Id = 2, Titulo = "El Hobbit", Autor = "J.R.R. Tolkien", Genero = "Fantasía" },
                new Book { Id = 3, Titulo = "Cien años de soledad", Autor = "Gabriel García Márquez", Genero = "Realismo mágico" }
            };
        }

        private Vote[] CreateVoteSeed()
        {
            return new[]
            {
                new Vote { Id = 1, LibroId = 1, UsuarioId = 1, Valor = 5 },
                new Vote { Id = 2, LibroId = 2, UsuarioId = 2, Valor = 4 },
                new Vote { Id = 3, LibroId = 3, UsuarioId = 2, Valor = 5 }
            };
        }


    }
}
