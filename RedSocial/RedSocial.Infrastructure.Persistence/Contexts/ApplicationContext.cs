using Microsoft.EntityFrameworkCore;
using RedSocial.Core.Domain.Entities;


namespace RedSocial.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Comment> comments { get; set; }
        public DbSet<Friend> Friends { get; set; }   
        public DbSet<Post> Posts { get; set; }   
        public DbSet<Replies> Replies { get; set; }   


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //FLUENT API

            #region tables

            modelBuilder.Entity<Comment>()
                .ToTable("comments");

            modelBuilder.Entity<Friend>()
                .ToTable("Friends");
            
            modelBuilder.Entity<Post>()
                .ToTable("Posts");
            
            modelBuilder.Entity<Replies>()
                .ToTable("Replies");

            #endregion

            #region "primary keys"
            modelBuilder.Entity<Comment>()
                .HasKey(comment => comment.Id);
            
            modelBuilder.Entity<Post>()
                .HasKey(post => post.Id);
            
            modelBuilder.Entity<Replies>()
                .HasKey(replies => replies.Id);

            modelBuilder.Entity<Friend>()
                .HasKey(f => f.Id);

            #endregion

            #region "Relationships"
            modelBuilder.Entity<Post>()
                .HasMany<Comment>(post => post.Comments)
                .WithOne(comentario => comentario.publicacion)
                .HasForeignKey(comentario => comentario.PublicacionId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Comment>()
                .HasMany<Replies>(comentario => comentario.Replies)
                .WithOne(replies => replies.Comments)
                .HasForeignKey(replies => replies.ComentarioId)
                .OnDelete(DeleteBehavior.Cascade);


            #endregion

            #region "Property configurations"

            #region post

            modelBuilder.Entity<Post>().
                Property(post => post.Descripcion)
                .IsRequired();

            modelBuilder.Entity<Comment>().
                Property( comentario=> comentario.Contenido)
                .IsRequired();

            modelBuilder.Entity<Replies>().
               Property(Replies => Replies.Contenido)
               .IsRequired();

            #endregion


            #endregion

        }

    }
}
