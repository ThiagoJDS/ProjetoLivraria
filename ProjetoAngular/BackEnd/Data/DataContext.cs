using System;
using System.Collections.Generic;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<Administrador> Administrador { get; set; }
        public DbSet<Autor> Autor { get; set; }
        public DbSet<Classificacao> Classificacao { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Compra> Compra { get; set; }
        public DbSet<CompraItem> CompraItem { get; set; }
        public DbSet<Genero> Genero { get; set; }
        public DbSet<Livro> Livro { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Segmento> Segmento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrador>()
                        .HasData(
                            new List<Administrador>()
                            {
                                new Administrador (1, "Thiago Juliano da Silva", "Thiago@hotmail.com", "123456"),
                                new Administrador (2, "Jaqueline Maria da Silva", "Jaqueline@hotmail.com", "234567")
                            }
                        );

            modelBuilder.Entity<Autor>()
                        .HasData(
                            new List<Autor>()
                            {
                                new Autor (1, "Agatha Christie"),
                                new Autor (2, "Stephen Edwin King"),
                                new Autor (3, "Alexandre Dumas"),
                                new Autor (4, "Victor Hugo"),
                                new Autor (5, "Machado de Assis")
                            }
                        );

            modelBuilder.Entity<Classificacao>()
                        .HasData(
                            new List<Classificacao>()
                            {
                                new Classificacao (1, "Conteúdo para adolescentes", 14),
                                new Classificacao (2, "Conteúdo Adultos", 18)
                            }
                        );

            modelBuilder.Entity<Cliente>()
                        .HasData(
                            new List<Cliente>()
                            {
                                new Cliente (1, "Diego", "Diego@hotmail.com", "765432", "33344455533", DateTime.Parse("09-09-1986"), "M", "(047)9 5555-7777", "Santa Catarina", "Blumenau", "Rua Legal Número 1720"),
                                new Cliente (2, "Anizio", "Anizio@Gmail.com", "345678", "44455577744", DateTime.Parse("12-10-2000"), "M", "(047)9 9999-3333", "São Paulo", "São Paulo", "Rua Bacana Número 1315"),
                                new Cliente (3, "Lucian", "Lucian@hotmail.com", "987543", "12332112323", DateTime.Parse("06-07-1989"), "M", "(047)9 3333-2222", "Santa Catarina", "Blumenau", "Rua Incrível Número 1450"),
                                new Cliente (4, "Maria", "Maria@Gmail.com", "887744", "43223443254", DateTime.Parse("12-01-1994"), "F", "(047)9 1111-4444", "Santa Catarina", "Blumenau", "Rua Incrível Número 1453")
                            }
                        );

             modelBuilder.Entity<Compra>()
                        .HasData(
                            new List<Compra>()
                            {
                                new Compra (1, 1, 26.50),
                                new Compra (2, 1, 54.00),

                                new Compra (3, 2, 29.75),
                                new Compra (4, 2, 100.00),

                                new Compra (5, 3, 15.25),
                                new Compra (6, 3, 30.50),

                                new Compra (7, 4, 44.25),
                                new Compra (8, 4, 35.00)
                            }
                        );

            modelBuilder.Entity<CompraItem>()
                        .HasData(
                            new List<CompraItem>()
                            {
                                new CompraItem (1, 26.50, 1, 1,1),
                                new CompraItem (2, 54.00, 1, 2,2),

                                new CompraItem (3, 29.75, 1, 3,3),
                                new CompraItem (4, 100.00, 1, 4,4),

                                new CompraItem (5, 15.25, 1, 3,5),
                                new CompraItem (6, 30.50, 1, 4,6),

                                new CompraItem (7, 44.25, 1, 2,7),
                                new CompraItem (8, 35.00, 1, 1,8)
                            }
                        ); 

             modelBuilder.Entity<Genero>()
                        .HasData(
                            new List<Genero>()
                            {
                                new Genero (1, "Comédia"),
                                new Genero (2, "Romance"),
                                new Genero (3, "Ação"),
                                new Genero (4, "Aventura"),
                                new Genero (5, "Sobrenatural"),
                                new Genero (6, "Terror"),
                                new Genero (7, "Suspense"),
                                new Genero (8, "Ficção"),
                                new Genero (9, "Fantasia")
                            }
                        );

            modelBuilder.Entity<Livro>()
                        .HasData(
                            new List<Livro>()
                            {
                                new Livro (1, "Morte no Nilo", 1, 320, "Dura", DateTime.Parse("15-09-2020"), 1, 1, 1, 1, 7, "morteNoNilo.jpg"),
                                new Livro (2, "O Iluminado", 1, 520, "Dura", DateTime.Parse("22-08-2017"), 1, 2, 2, 2, 6, "oIluminado.jpg"),
                                new Livro (3, "O Conde de Monte Cristo", 3, 1376, "Dura", DateTime.Parse("18-03-2020"), 1, 3, 3, 2, 8, "oCondeDeMonteCristo.jpg"),
                                new Livro (4, "Os Miseráveis", 1, 1511, "Dura", DateTime.Parse("07-10-2014"), 1, 4, 4, 2, 8, "osMiseraveis.jpg")
                            }
                        );

            modelBuilder.Entity<Marca>()
                        .HasData(
                            new List<Marca>()
                            {
                                new Marca (1, "HarperCollins"),
                                new Marca (2, "Suma"),
                                new Marca (3, "Clássicos Zahar"),
                                new Marca (4, "Martin Claret")
                            }
                        );

            modelBuilder.Entity<Segmento>()
                        .HasData(
                            new List<Segmento>()
                            {
                                new Segmento (1, "Livros"),
                                new Segmento (2, "Mangás"),
                                new Segmento (3, "HQs")
                            }
                        );
        }
    }
}