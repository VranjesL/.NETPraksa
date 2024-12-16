using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LibraryManagement.Data
{
    public class ApplicationDBContext : IdentityDbContext<Member>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {
            
        }

        public DbSet<Member> Members{ get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookRental> BookRentals { get; set; }

        // adding admin and user roles
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BookRental>(x => x.HasKey(p => new { p.MemberId, p.BookId }));

            List<IdentityRole> roles = new List<IdentityRole>
            {   
                // creating the roles
                new IdentityRole
                {   
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },

                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "User",
                    NormalizedName = "USER"
                }
            };

            // adding the roles
            builder.Entity<IdentityRole>().HasData(roles);
            
            // ISBN has to be unique
            builder.Entity<Book>()
                .HasIndex(b => b.ISBN)
                .IsUnique();

            // one author can have many books
            builder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);

            // one member can rent many books
            builder.Entity<BookRental>()
                .HasOne(br => br.Member)
                .WithMany(m => m.BookRentals)
                .HasForeignKey(br => br.MemberId);

            // one book can only be rented once at a time
            builder.Entity<Book>()
                .HasOne(b => b.BookRental)
                .WithOne(br => br.Book)
                .HasForeignKey<BookRental>(br => br.BookId);
        }

        public static async Task SeedAdminUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<Member>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Proverite da li postoji uloga Admin
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Proverite da li postoji admin korisnik
            var adminUser = await userManager.FindByNameAsync("admin");
            if (adminUser == null)
            {
                // Kreiranje administratorskog naloga
                adminUser = new Member
                {
                    UserName = "admin",
                };

                // Dodavanje korisnika i postavljanje lozinke
                await userManager.CreateAsync(adminUser, "Admin123!"); // Postavite jaku lozinku

                // Dodavanje uloge Admin korisniku
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }

    }
}