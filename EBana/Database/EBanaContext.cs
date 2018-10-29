using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SQLite.CodeFirst;
using EBana.Security.Hash;

using EBana.Models;

namespace EBana
{
	public class EBanaContext : DbContext
    {
        private readonly IHash hash;

        public EBanaContext() : base("name=EBanaDB") 
        {
            //this.hash = hash;
        }

        // définition des tables utilisables
        public DbSet<Article> Article { get; set; }
        public DbSet<TypeArticle> TypeArticle { get; set; }
        public DbSet<TypeEpi> TypeEpi { get; set; }
        public DbSet<Credentials> Credentials { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new MyDbContextInitializer(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);

            // les noms de tables sont au singulier
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // relations table/classe
            modelBuilder.Entity<Article>().ToTable("Article");
            modelBuilder.Entity<Banalise>().ToTable("Banalise");
            modelBuilder.Entity<EPI>().ToTable("EPI");
            modelBuilder.Entity<SEL>().ToTable("SEL");
            modelBuilder.Entity<Credentials>().ToTable("Credentials");
            modelBuilder.Entity<TypeEpi>().ToTable("TypeEpi");
        }
    }


    /// <summary>
    /// Initialisateur de context basé sur le SqliteCreateDatabaseIfNotExists
    /// Permet de fournir des données initiales à la base
    /// </summary>
    public class MyDbContextInitializer : SqliteCreateDatabaseIfNotExists<EBanaContext>
    {
        public MyDbContextInitializer(DbModelBuilder modelBuilder)
            : base(modelBuilder) { }

        protected override void Seed(EBanaContext context)
        {
        	// définition des types d'articles disponibles
            context.TypeArticle.Add(new TypeArticle { Libelle = "Banalisé" });
            context.TypeArticle.Add(new TypeArticle { Libelle = "SEL" });
            
            // définition du mot de passe par défaut
            string defaultPassword = "admin";
            var salt = 12;
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(defaultPassword, salt);
            
            context.Credentials.Add(new Credentials { Password = hashedPassword } );
        }
    }
}
