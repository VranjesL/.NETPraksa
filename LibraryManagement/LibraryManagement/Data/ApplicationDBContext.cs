using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },

                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }
            };

            // adding the roles
            builder.Entity<IdentityRole>().HasData(roles);
            

            // username has to be unique
            /*builder.Entity<Member>()
                .HasIndex(m => m.Username)
                .IsUnique();*/
            
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
    }
}